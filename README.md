# A Desktop Practice Management Application

A Practice Management application I created while taking Florida State University's COP4870 Full-Stack Application Development with C# and .NET course

## Features

This application features a way to manage clients, projects, employees, and time entries in one place. For each entity, you can perform the basic operations such as creating, displaying, updating, and deleting the entity. The application stores all of the entities in a Filebase in JSON representation to the local file system of the WebAPI server. Data is communicated to and from the client-side application and the WebAPI server.

### Clients
The application provides a UI to display the list of every client on the screen. Clients can be searched by their names. A client is created with three fields; the name, the notes section, and an active or inactive status. Upon selecting a client in the list, you are provided basic information about the client, such as their name, status, date opened, date closed, and notes. You can also view a client's associated projects, and upon selecting a project associated with a client, you can view the bills for the project. A client cannot be inactivated if they are under an active project.

### Projects
The application provides a UI to display the list of every project on the screen. Projects can be searched by their names. A project is created with four fields; the long name, the short name, an active or inactive status, and the client. Upon selecting a project in the list, you are provided the bills associated with the project. You can also generate a bill when selecting a project that only requires a due date to be inputted. 

### Employees
The application provides a UI to display the list of every employee on the screen. Employees can be searched by their names. An employee is created with two fields; a name and their hourly rate.

### Time Entries
The application provides a UI to display the list of every time entry on the screen. Time entries can be searched by their employee's name. A time entry is created with four fields; the number of hours work was done, the narrative of the work performed, the project, and the employee. 
