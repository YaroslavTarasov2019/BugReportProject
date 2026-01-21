using BugReportApp.Contexts;
using BugReportApp.ModelDB;
using Microsoft.EntityFrameworkCore;
using BugReportApp.Controllers;
using BugReportApp.Model.Common;
using System.Collections.Immutable;

namespace BugReportApp.Services.Common
{
    public class CommonService : ICommonService
    {
        private readonly DBContext _db;

        public CommonService(DBContext db)
        {
            _db = db;
        }

        public ProjectStatisticsHead GetStatisticsInformationHead(RoleAndProjectRequest model)
        {
            var bugReports = _db.bugReport.Where(b => b.ProjectId == model.ProjectId).ToList();
            var bugIds = bugReports.Select(b => b.ID).ToList();

            var bugReportsStatusHistory = _db.bugReportUpdateHistory
                .Where(b => bugIds.Contains(b.BugId))
                .ToList();

            return new ProjectStatisticsHead
            {
                FoundBugs = bugReports.Count,
                ClosedBugs = bugReportsStatusHistory.Count(b => b.NewValue == "Closed"),                   
                BugsByPriority = new BugsByPriority
                {
                    High = bugReports.Count(b => b.Priority == "High"),
                    Medium = bugReports.Count(b => b.Priority == "Medium"),
                    Low = bugReports.Count(b => b.Priority == "Low")
                },
                BugsBySeverity = new BugsBySeverity
                {
                    Blocker = bugReports.Count(b => b.Severity == "Blocker"),
                    Critical = bugReports.Count(b => b.Severity == "Critical"),
                    Major = bugReports.Count(b => b.Severity == "Major"),
                    Minor = bugReports.Count(b => b.Severity == "Minor"),
                    Trivial = bugReports.Count(b => b.Severity == "Trivial"),
                },
            };
        }

        public ProjectStatisticsDeveloper GetStatisticsInformationDeveloper(RoleAndProjectRequest model)
        {
            var bugReports = _db.bugReport.Where(b => b.ProjectId == model.ProjectId && b.DeveloperId == model.UserId).ToList();
            var bugIds = bugReports.Select(b => b.ID).ToList();

            var bugReportsStatusHistory = _db.bugReportUpdateHistory
                .Where(b => bugIds.Contains(b.BugId))
                .ToList();

            var closedBugIds = bugReportsStatusHistory
                .GroupBy(h => h.BugId)
                .Where(group =>
                {
                    var lastStatus = group.OrderByDescending(h => h.ChangedAt).FirstOrDefault()?.NewValue;
                    var wasFixed = group.Any(h => h.NewValue == "Fixed");
                    return lastStatus == "Closed" && wasFixed;
                })
                .Select(g => g.Key)
                .ToList();

            var fixDurations = new List<TimeSpan>();
            int returnedCount = 0;

            foreach (var bugId in bugIds)
            {
                var bugHistory = bugReportsStatusHistory.Where(h => h.BugId == bugId).ToList();

                var fixedStatus = bugHistory.FirstOrDefault(h => h.NewValue == "Fixed");
                var closedStatus = bugHistory.LastOrDefault(h => h.NewValue == "Closed");

                if (fixedStatus != null && closedStatus != null && fixedStatus.ChangedAt < closedStatus.ChangedAt)
                {
                    fixDurations.Add(closedStatus.ChangedAt - fixedStatus.ChangedAt);
                }

                var indexFixed = bugHistory.FindIndex(h => h.NewValue == "Fixed");
                if (indexFixed >= 0)
                {
                    var hasReopened = bugHistory
                        .Skip(indexFixed + 1)
                        .Any(h => h.NewValue == "Reopened");

                    if (hasReopened)
                        returnedCount++;
                }
            }

            int averageFixTimeHours = fixDurations.Any()
                ? (int)fixDurations.Average(d => d.TotalHours)
                : 0;

            int returnedPercent = bugIds.Count > 0
                ? (int)((returnedCount / (double)bugIds.Count) * 100)
                : 0;

            return new ProjectStatisticsDeveloper
            {
                AssignedBugs = bugReports.Count,
                SuccessfullyFixedBugs = closedBugIds.Count,
                AverageFixTime = averageFixTimeHours,
                ReturnedForReworkPercent = returnedPercent,
                BugsByPriority = new BugsByPriority
                {
                    High = bugReports.Count(b => b.Priority == "High"),
                    Medium = bugReports.Count(b => b.Priority == "Medium"),
                    Low = bugReports.Count(b => b.Priority == "Low")
                },
                BugsBySeverity = new BugsBySeverity
                {
                    Blocker = bugReports.Count(b => b.Severity == "Blocker"),
                    Critical = bugReports.Count(b => b.Severity == "Critical"),
                    Major = bugReports.Count(b => b.Severity == "Major"),
                    Minor = bugReports.Count(b => b.Severity == "Minor"),
                    Trivial = bugReports.Count(b => b.Severity == "Trivial"),
                },
            };
        }

        public ProjectStatisticsTester GetStatisticsInformationTester(RoleAndProjectRequest model)
        {
            var bugReports = _db.bugReport.Where(b => b.ProjectId == model.ProjectId && b.TesterId == model.UserId).ToList();
            

            return new ProjectStatisticsTester
            {
                CreatedBugs = bugReports.Count,
                BugsByPriority = new BugsByPriority
                {
                    High = bugReports.Count(b => b.Priority == "High"),
                    Medium = bugReports.Count(b => b.Priority == "Medium"),
                    Low = bugReports.Count(b => b.Priority == "Low")
                },
                BugsBySeverity = new BugsBySeverity
                {
                    Blocker = bugReports.Count(b => b.Severity == "Blocker"),
                    Critical = bugReports.Count(b => b.Severity == "Critical"),
                    Major = bugReports.Count(b => b.Severity == "Major"),
                    Minor = bugReports.Count(b => b.Severity == "Minor"),
                    Trivial = bugReports.Count(b => b.Severity == "Trivial"),
                },
            };
        }

        public List<TesterWithTasks> GetTestersByProject(int projectId)
        {
            var userProjects = _db.userProject
                .Where(up => up.ProjectId == projectId && up.Role == "TESTER")
                .Include(up => up.User)
                .ToList();

            return userProjects.Select(up => new TesterWithTasks
            {
                Id = up.User.ID,
                Name = up.User.Name,
                Surname = up.User.Surname,
                Patronymic = up.User.Patronymic,
                PhoneNumber = up.User.PhoneNumber,
                Email = up.User.Email,
                Tasks = _db.testersTask
                    .Where(t => t.TesterID == up.User.ID && t.ProjectID == projectId)
                    .Select(t => t.TaskText)
                    .ToList()
            }).ToList();
        }

        public (BugReport, List<BugReportUpdateHistory>) GetBugById(int bugId)
        {
            var bug = _db.bugReport.FirstOrDefault(b => b.ID == bugId);
            var history = _db.bugReportUpdateHistory.Where(h => h.BugId == bugId).ToList();
            return (bug, history);
        }

        public List<object> GetDevelopersByProjectId(int projectId)
        {
            return _db.userProject
                .Where(up => up.ProjectId == projectId && up.Role == "DEVELOPER")
                .Select(up => new { id = up.UserId, email = up.User.Email })
                .ToList<object>();
        }

        public void AssignTask(TaskAssignment model)
        {
            var newTask = new TestersTask
            {
                TesterID = model.TesterId,
                ProjectID = model.ProjectId,
                TaskText = model.TaskDescription,
                CreatedAt = DateTime.UtcNow
            };

            _db.testersTask.Add(newTask);
            _db.SaveChanges();
        }
    }
}
