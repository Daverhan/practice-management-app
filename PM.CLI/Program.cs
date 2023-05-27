using PM.Library.Models;
using PM.Library.Services;

namespace PM.CLI
{
    public class Program
    {
        // DisplayMenu displays the available menu options to the user
        static public void DisplayMenu()
        {
            Console.WriteLine("=-=-=- Practice Management Solution Portal -=-=-=");
            Console.WriteLine("1) Create a Client\n2) Display all Clients\n3) Update a Client\n4) Delete a Client\n");
            Console.WriteLine("5) Create a Project\n6) Display all Projects\n7) Update a Project\n8) Delete a Project\n");
            Console.WriteLine("9) Display Menu");
            Console.WriteLine("10) Exit Program");
            Console.WriteLine("=-=-=- Practice Management Solution Portal -=-=-=");
        }

        static public void Main(string[] args)
        {
            int startingClientIds = 1;                     
            int startingProjectIds = 1;                   
            string option = "";

            var myClientService = ClientService.Current;
            var myProjectService = ProjectService.Current;

            DisplayMenu();

            // while the user does not want to exit the program
            while(option != "10")
            {
                string clientName = "", projectLongName = "", projectShortName = "", updateOption = "", tempProjName = "", toggleStatus = "";
                int clientId, projectId;

                Console.WriteLine("\nEnter a Menu Option: ");
                option = Console.ReadLine() ?? string.Empty;

                // handles the functionality for the menu options
                switch(option)
                {
                    // creating a client
                    case "1":
                        Console.WriteLine("\nEnter Client Name:");
                        clientName = Console.ReadLine() ?? string.Empty;

                        myClientService.AddClient(new Client
                        {
                            Name = clientName,
                            Id = startingClientIds++,
                            OpenDate = DateTime.Now,
                            IsActive = true,
                        });

                        Console.WriteLine("\nClient \"" + clientName + "\" has been successfully created!");
                        break;
                    // displaying all clients
                    case "2":
                        if (myClientService.GetSize() == 0)
                        {
                            Console.WriteLine("\nThere are currently no clients in the system!");
                            break;
                        }

                        myClientService.DisplayLongDetails();
                        break;
                    // updating a client
                    case "3":
                        if(myClientService.GetSize() == 0)
                        {
                            Console.WriteLine("\nThere are currently no clients in the system!");
                            break;
                        }

                        Console.WriteLine("\nList of Clients: ");
                        myClientService.DisplayShortDetails();

                        Console.WriteLine("\nSearch Client by ID: ");
                        clientId = int.Parse(Console.ReadLine() ?? string.Empty);

                        var clientToUpdate = myClientService.GetClient(clientId);

                        if(clientToUpdate == null)
                        {
                            Console.WriteLine("\nThe client was not found!");
                            break;
                        }

                        Console.WriteLine("\nYou are currently modifying the client: \"" + clientToUpdate.Name + "\"");
                        Console.WriteLine("\n1) Update Name\n2) Update Active Account Status\n3) Update Notes");
                        Console.WriteLine("\nEnter a Menu Option: ");
                        updateOption = Console.ReadLine() ?? string.Empty;

                        // handles the functionality for the updating a client options
                        switch(updateOption)
                        {
                            case "1":
                                Console.WriteLine("\nEnter New Client Name: ");
                                clientName = Console.ReadLine() ?? string.Empty;

                                clientToUpdate.Name = clientName;

                                Console.WriteLine("\nClient name has been updated sucessfully!");
                                break;
                            case "2":
                                Console.WriteLine("\nThe active account status is currently: " + clientToUpdate.IsActive);
                                Console.WriteLine("Entering 'Y' will toggle the active account status. Enter 'N' to not make any changes.");

                                toggleStatus = Console.ReadLine() ?? string.Empty;

                                if(toggleStatus.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    clientToUpdate.IsActive = !clientToUpdate.IsActive;
                                    Console.WriteLine("\nClient active account status has been updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("\nClient active account status has not been updated.");
                                }
                                break;
                            case "3":
                                Console.WriteLine("\nThe current notes:\n" + clientToUpdate.Notes);

                                Console.WriteLine("\nEnter the new text of notes: ");
                                clientToUpdate.Notes = Console.ReadLine() ?? string.Empty;

                                Console.WriteLine("\nThe client's notes has been updated successfully!");
                                        break;
                            default:
                                Console.WriteLine("\nInvalid Menu Option!");
                                break;
                        }
                        break;
                    // deleting a client
                    case "4":
                        if(myClientService.GetSize() == 0)
                        {
                            Console.WriteLine("\nThere are currently no clients in the system!");
                            break;
                        }

                        Console.WriteLine("\nList of Clients: ");
                        myClientService.DisplayShortDetails();

                        Console.WriteLine("\nRemove Client by ID:");
                        clientId = int.Parse(Console.ReadLine() ?? string.Empty);

                        if(myClientService.DeleteClient(clientId))
                        {
                            Console.WriteLine("\nThe client has been successfully deleted!");
                        } 
                        else
                        {
                            Console.WriteLine("\nThe client was not found!");
                        }
                        break;
                    // creating a project
                    case "5":
                        Console.WriteLine("\nEnter Project's Long Name:");
                        projectLongName = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("\nEnter Project's Short Name:");
                        projectShortName = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("\nList of Clients: ");
                        myClientService.DisplayShortDetails();

                        Console.WriteLine("\nEnter a Client ID to link them to the following Project: \"" + projectLongName + "\"");
                        clientId = int.Parse(Console.ReadLine() ?? string.Empty);

                        myProjectService.AddProject(new Project
                        {
                            LongName = projectLongName,
                            ShortName = projectShortName,
                            Id = startingProjectIds++,
                            OpenDate = DateTime.Now,
                            IsActive = true,
                            Client = myClientService.GetClient(clientId)
                        });

                        Console.WriteLine("\nProject \"" + projectLongName + "\" has been successfully created!");
                        break;
                    // displaying all projects
                    case "6":
                        if(myProjectService.GetSize() == 0)
                        {
                            Console.WriteLine("\nThere are currently no projects in the system!");
                            break;
                        }

                        myProjectService.DisplayLongDetails();
                        break;
                    // updating a project
                    case "7":
                        if(myProjectService.GetSize() == 0)
                        {
                            Console.WriteLine("\nThere are currently no projects in the system!");
                            break;
                        }

                        Console.WriteLine("\nList of Projects: ");
                        myProjectService.DisplayShortDetails();

                        Console.WriteLine("\nSearch Project by ID: ");
                        projectId = int.Parse(Console.ReadLine() ?? string.Empty);

                        var projectToUpdate = myProjectService.GetProject(projectId);

                        if(projectToUpdate == null)
                        {
                            Console.WriteLine("\nThe project was not found!");
                            break;
                        }

                        Console.WriteLine("\nYou are currently modifying the project: \"" + projectToUpdate.LongName + "\"");
                        Console.WriteLine("\n1) Update Long Name\n2) Update Short Name\n3) Update Active Project Status\n4) Update Linked Client");
                        Console.WriteLine("\nEnter a Menu Option: ");
                        updateOption = Console.ReadLine() ?? string.Empty;

                        // handles the functionality for the updating a project options
                        switch (updateOption)
                        {
                            case "1":
                                Console.WriteLine("\nEnter New Long Name: ");
                                tempProjName = Console.ReadLine() ?? string.Empty;

                                projectToUpdate.LongName = tempProjName;

                                Console.WriteLine("\nProject long name has been updated sucessfully!");
                                break;
                            case "2":
                                Console.WriteLine("\nEnter New Short Name: ");
                                tempProjName = Console.ReadLine() ?? string.Empty;

                                projectToUpdate.ShortName = tempProjName;

                                Console.WriteLine("\nProject short name has been updated sucessfully!");
                                break;
                            case "3":
                                Console.WriteLine("\nThe active project status is currently: " + projectToUpdate.IsActive);
                                Console.WriteLine("Entering 'Y' will toggle the active project status. Enter 'N' to not make any changes.");

                                toggleStatus = Console.ReadLine() ?? string.Empty;

                                if (toggleStatus.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    projectToUpdate.IsActive = !projectToUpdate.IsActive;
                                    Console.WriteLine("\nProject active status has been updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("\nProject active status has not been updated.");
                                }
                                break;
                            case "4":
                                Console.WriteLine("\nThe current linked client is: " + projectToUpdate.Client.Name);
                                Console.WriteLine("\nList of Clients: ");
                                myClientService.DisplayShortDetails();

                                Console.WriteLine("\nEnter the ID of the new client to link: ");
                                projectToUpdate.Client = myClientService.GetClient(int.Parse(Console.ReadLine() ?? string.Empty));

                                Console.WriteLine("\nProject's linked client has been updated successfully!");
                                break;
                        }
                        break;
                    // deleting a project
                    case "8":
                        if(myProjectService.GetSize() == 0)
                        {
                            Console.WriteLine("\nThere are currently no projects in the system!");
                            break;
                        }

                        Console.WriteLine("\nList of Projects: ");
                        myProjectService.DisplayShortDetails();

                        Console.WriteLine("\nRemove Project by ID:");
                        projectId = int.Parse(Console.ReadLine() ?? string.Empty);

                        if(myProjectService.DeleteProject(projectId))
                        {
                            Console.WriteLine("\nThe project has been successfully deleted!");
                        }
                        else
                        {
                            Console.WriteLine("\nThe project was not found!");
                        }
                        break;
                    // displaying menu
                    case "9":
                        Console.WriteLine();
                        DisplayMenu();
                        break;
                    // exiting program message
                    case "10":
                        Console.WriteLine("\nExiting Program...");
                        break;
                    default:
                        Console.WriteLine("\nInvalid Menu Option!");
                        break;
                }
            }
        }
    }
}