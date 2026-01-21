# Web application to manage bug reports

## A web application for managing bug reports, developed as a diploma project. Backend implemented with ASP.NET Web API (C#), database design using MSSQL, frontend with Angular (HTML, CSS, and TypeScript).  
## The system provides role-based access for users, including testers, developers, and project leads (lead developer and lead tester), with features for creating projects, submitting and managing bug reports, tracking tasks, and viewing statistics.

## Features
- User account management: register and login
- Role-based access: tester, developer, lead tester, lead developer
- Project management: create projects (lead developer), view projects
- Bug reports:
  - Create, view, and manage bug reports
  - Attach files to bug reports
  - Track status, priority, and severity
- Tasks:
  - View assigned tasks
  - Create and assign tasks (lead tester)
- Statistics:
  - Project-wide and personalized statistics for bug reports and tasks
- Search and sorting:
  - Filter and sort bug reports and tasks by relevant fields

## Technologies
- Backend: ASP.NET Web API (C#)
- Database: MSSQL
- Frontend: Angular (HTML, CSS, TypeScript)
- Tools: Visual Studio 2022

## Screenshots
- Authorization
<img width="543" height="485" alt="image" src="https://github.com/user-attachments/assets/1a254e47-5287-4bef-9096-bee7570d3354" />
  
- Registration
<img width="545" height="858" alt="image" src="https://github.com/user-attachments/assets/215cd24c-3496-439f-b0e8-580d9f6462b6" />

- Choosing project when no project attached
<img width="1915" height="942" alt="image" src="https://github.com/user-attachments/assets/3b8c1ce2-f8b5-4c03-ab25-4291c0feff42" />

- "Add project to system" form
<img width="584" height="519" alt="image" src="https://github.com/user-attachments/assets/b5dc895b-1067-4830-ae19-ea630da02a96" />

- Page "About this project"
<img width="1916" height="943" alt="image" src="https://github.com/user-attachments/assets/a636d103-d2d6-4c39-9e89-3aec1f55a8f5" />

- Page "Statistics"
<img width="1916" height="941" alt="image" src="https://github.com/user-attachments/assets/b7dab337-a2bd-4032-9012-c46560026f11" />

- Page "Give an assignment" for lead tester
<img width="1903" height="946" alt="image" src="https://github.com/user-attachments/assets/8e1fd6e3-7ab5-4387-9e59-190798bafa39" />

- Page "My tasks" for tester
<img width="1914" height="941" alt="image" src="https://github.com/user-attachments/assets/58b885c4-b2c0-44d1-aa4a-7e4b13a61c45" />

- Page "Bug-reports"
<img width="1917" height="941" alt="image" src="https://github.com/user-attachments/assets/c87fc096-93eb-4a0e-9d95-e2f53fd79cd3" />

- Add bug report form
<img width="795" height="876" alt="image" src="https://github.com/user-attachments/assets/5436a023-1937-4be1-993c-c4fce12afc41" />

- Choosing project when 1 project attached
<img width="1911" height="943" alt="image" src="https://github.com/user-attachments/assets/ea575419-ede3-4e0c-b139-a513995b175b" />

- Page "About bug report"
<img width="1799" height="651" alt="image" src="https://github.com/user-attachments/assets/92ab2774-b64c-4afc-adcf-17ffbf5c5813" />

- The conceptual model of the database
<img width="1231" height="1061" alt="image" src="https://github.com/user-attachments/assets/0fc250f1-30e4-4650-a734-4e52e485fa87" />

> Note: Screenshots from a working system developed as a diploma project.

## Installation (optional)
> Note: The project requires a database. See the conceptual model above.
To run the project:
1. Clone the repository
2. Create a MSSQL database according to the conceptual model
3. Update the connection string in 'appsettings.json' (backend configuration)
4. Build and run the project in Visual Studio
> Note: You need to have .NET 9.0 installed.

## Contact
For questions or project inquiries, you can reach me via my GitHub-provided email:  
[53192124+YaroslavTarasov2019@users.noreply.github.com](mailto:53192124+YaroslavTarasov2019@users.noreply.github.com)



