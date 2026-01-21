using BugReportApp.Contexts;
using BugReportApp.ModelDB;
using Microsoft.EntityFrameworkCore;

namespace BugReportApp.Services.BugReportFlow
{
    public class BugReportFlowService : IBugReportFlowService
    {
        private readonly DBContext _dbContext;

        public BugReportFlowService(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> PassBugReportOnAsync(int bugId, int userId)
        {
            var bugreport = await _dbContext.bugReport.FindAsync(bugId);
            if (bugreport == null) return false;

            var history = await _dbContext.bugReportUpdateHistory
                .Where(h => h.BugId == bugId)
                .OrderBy(h => h.ID)
                .LastOrDefaultAsync();

            if (history == null) return false;

            var now = DateTime.Now;

            if (history.NewValue == "New")
            {
                _dbContext.bugReportUpdateHistory.Add(new BugReportUpdateHistory
                {
                    BugId = bugId,
                    ChangedBy = userId,
                    NewValue = "Open",
                    ChangedAt = now
                });
            }
            else if (history.NewValue == "In Progress")
            {
                _dbContext.bugReportUpdateHistory.AddRange(
                    new BugReportUpdateHistory
                    {
                        BugId = bugId,
                        ChangedBy = userId,
                        NewValue = "Fixed",
                        ChangedAt = now
                    },
                    new BugReportUpdateHistory
                    {
                        BugId = bugId,
                        ChangedBy = userId,
                        NewValue = "Testing",
                        ChangedAt = now.AddSeconds(1) // чтобы сохранить порядок
                    });
            }
            else
            {
                return false;
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> RejectBugReportAsync(int bugId, int userId)
        {
            var bugreport = await _dbContext.bugReport.FindAsync(bugId);
            if (bugreport == null) return false;

            _dbContext.bugReportUpdateHistory.AddRange(
                new BugReportUpdateHistory
                {
                    BugId = bugId,
                    ChangedBy = userId,
                    NewValue = "Reject",
                    ChangedAt = DateTime.Now
                },
                new BugReportUpdateHistory
                {
                    BugId = bugId,
                    ChangedBy = userId,
                    NewValue = "Closed",
                    ChangedAt = DateTime.Now
                });

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConfirmBugFixAsync(int bugId, int testerId, int headTesterId)
        {
            var bugreport = await _dbContext.bugReport.FindAsync(bugId);
            if (bugreport == null) return false;

            var history = await _dbContext.bugReportUpdateHistory
                .Where(h => h.BugId == bugId)
                .OrderBy(h => h.ID)
                .LastOrDefaultAsync();

            if (history?.NewValue == "Verified" && testerId == -1)
            {
                _dbContext.bugReportUpdateHistory.Add(new BugReportUpdateHistory
                {
                    BugId = bugId,
                    ChangedBy = headTesterId,
                    NewValue = "Closed",
                    ChangedAt = DateTime.Now
                });
            }
            else if (history?.NewValue == "Testing" && headTesterId == -1)
            {
                _dbContext.bugReportUpdateHistory.Add(new BugReportUpdateHistory
                {
                    BugId = bugId,
                    ChangedBy = testerId,
                    NewValue = "Verified",
                    ChangedAt = DateTime.Now
                });
            }
            else
            {
                return false;
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NotConfirmBugFixAsync(int bugId, int userId)
        {
            var bugreport = await _dbContext.bugReport.FindAsync(bugId);
            if (bugreport == null) return false;

            var history = await _dbContext.bugReportUpdateHistory
                .Where(h => h.BugId == bugId)
                .OrderByDescending(h => h.ID)
                .FirstOrDefaultAsync();

            if (history?.NewValue == "Verified" || history?.NewValue == "Testing")
            {
                var now = DateTime.Now;

                _dbContext.bugReportUpdateHistory.Add(new BugReportUpdateHistory
                {
                    BugId = bugId,
                    ChangedBy = userId,
                    NewValue = "Reopened",
                    ChangedAt = now
                });

                _dbContext.bugReportUpdateHistory.Add(new BugReportUpdateHistory
                {
                    BugId = bugId,
                    ChangedBy = userId,
                    NewValue = "In Progress",
                    ChangedAt = now.AddSeconds(1)
                });

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> AssignDeveloperAsync(int bugId, int developerId, int headDeveloperId)
        {
            var bug = await _dbContext.bugReport.FindAsync(bugId);
            if (bug == null) return false;

            var role = await _dbContext.userProject
                .Where(up => up.ProjectId == bug.ProjectId && up.UserId == developerId)
                .Select(up => up.Role)
                .FirstOrDefaultAsync();

            var history = await _dbContext.bugReportUpdateHistory
                .Where(h => h.BugId == bugId)
                .OrderBy(h => h.ID)
                .LastOrDefaultAsync();

            if ((history?.NewValue == "Open" || history?.NewValue == "Reopened") && role == "DEVELOPER")
            {
                _dbContext.bugReportUpdateHistory.Add(new BugReportUpdateHistory
                {
                    BugId = bugId,
                    ChangedBy = headDeveloperId,
                    NewValue = "In Progress",
                    ChangedAt = DateTime.Now
                });

                bug.DeveloperId = developerId;

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
