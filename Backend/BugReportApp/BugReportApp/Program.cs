using BugReportApp.Services.Authentication;
using BugReportApp.Services.BugReportFlow;
using Microsoft.Extensions.FileProviders;
using BugReportApp.Contexts;
using Microsoft.EntityFrameworkCore;
using BugReportApp.Services.Common;
using BugReportApp.Services.Form;
using BugReportApp.Services.File;
using BugReportApp.Services.Main;
using BugReportApp.Services.Project;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer("Data Source=YAROSLAV\\MSSQLSERVER01;Initial Catalog=BugReport;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True"));

builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IBugReportFlowService, BugReportFlowService>();
builder.Services.AddScoped<ICommonService, CommonService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IMainService, MainService>();
builder.Services.AddScoped<IProjectService, ProjectService>();


builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // 👈 Разрешаем Angular
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
