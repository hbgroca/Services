using ConsoleApp2.Factories;
using ConsoleApp2.Models;

namespace ConsoleApp2.Services;

public static class MenuDialogs
{
    // Instansiera ToDo Factory
    private static readonly ToDoFactory ToDoList = new ToDoFactory();

    // Start method
    public static void Start()
    {
        // By running the script in an while loop we make shure it always returns to the main menu when completed with current task.
        while (true)
        {
            // We render the MainMenu() and return the keyboard input to MenuOptionsSelector.
            MenuOptionsSelector(MainMenu());


            /* In simpler form
            // Render main menu then save the keyboard input to variable
            string keyboardInputFromMainMenu = MainMenu();

            // Do the switch loop and run the method depending on the input key.
            MenuOptionsSelector(keyboardInputFromMainMenu);
            */
        }
    }

    private static string MainMenu()
    {
        // Clear console
        Console.Clear();

        // Render header
        DisplayHeader();

        // Render todo list
        DisplayToDoList();

        // Change text color
        Console.ForegroundColor = ConsoleColor.Red;

        // Render selections
        Console.WriteLine($"{"║",-83}║");
        Console.WriteLine($"{"║",-3} {"Knappval:",-79}║");
        Console.WriteLine($"{"║",-5} {"1. Lägg till" , -77}║");
        Console.WriteLine($"{"║",-5} {"2. Redigera", -77}║");
        Console.WriteLine($"{"║",-83}║");
        Console.WriteLine($"{"║",-5} {"Q. Avsluta", -77}║");

        // Render footer
        DisplayFooter();

        // Get input from user
        var option = Console.ReadKey().KeyChar.ToString();

        // Change text color back to default
        Console.ResetColor();

        // Return input from user
        return option!;
    }

    private static void MenuOptionsSelector(string option)
    {
        // Compaire input in an switch case. Run the method if case exist.
        switch (option.ToUpper())
        {
            case "1": { ToDoCreate(); break; }
            case "2": { ViewToDoList(); break; }
            case "Q": { ExitAppliation(); break; }
            default: { ErrorMessage("Felaktivt val"); break; }
        }
    }

    // Renders header
    private static void DisplayHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                               DBP TODO-LISTA 9000                                ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    // Renders footer
    private static void DisplayFooter()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("║                                                                                  ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    // Renders todo list (for the main menu)
    private static void DisplayToDoList()
    {
        // Get Todo list from ToDoFactory()
        var TodoList = ToDoList.GetAll();
        bool hasFinishedToDoListInList = false;

        // Change text color
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        // If we have any todos in list then we render it to screen.
        if (TodoList.Count > 0)
        {
            Console.WriteLine($"{"║",-83}║");
            Console.WriteLine($"{"║",-5} {"Att göra:",-77}║");

            // Render to screen.
            for (int i = 0; i < TodoList.Count; i++)
            {
                // Render only NOT done tasks
                if (!TodoList[i].isDone)
                {
                    Console.WriteLine($"{"║",-8} {TodoList[i].Description,-74}║");
                }
                else hasFinishedToDoListInList = true;
            }

            if (hasFinishedToDoListInList)
            {
                // Change text color
                Console.ForegroundColor = ConsoleColor.Green;

                // Render to screen.
                Console.WriteLine($"{"║",-83}║");
                Console.WriteLine($"{"║",-5} {"Avklarade:",-77}║");
                for (int i = 0; i < TodoList.Count; i++)
                {
                    // Render only done tasks
                    if (TodoList[i].isDone)
                    {
                        Console.WriteLine($"{"║",-8} {TodoList[i].Description,-74}║");
                    }
                }
            }

            Console.WriteLine($"{"║",-83}║");
            Console.WriteLine("════════════════════════════════════════════════════════════════════════════════════");
        };

        // Change text color back to default
        Console.ResetColor();
    }

    // Create new task
    private static void ToDoCreate()
    {
        // Instaciate new ToDo from model
        var newToDo = new ToDo();

        // Clear console
        Console.Clear();

        // Render header
        DisplayHeader();

        // Get userinput and save to newToDo
        Console.WriteLine($"{"║",-83}║");
        Console.Write($"{"║",-5} {"Beskrivning: "}");

        newToDo.Description = UserInput();

        // Push newToDo in to ToDo List
        bool result = ToDoList.Add(newToDo);

        // Print out status
        if (result)
        {
            Console.WriteLine($"{"║",-83}║");
            Console.WriteLine($"{"║",-5} {newToDo.Description + " har lagts till",-77}║");
            DisplayFooter();
        }
        else {
            Console.WriteLine($"{"║",-83}║");
            Console.WriteLine($"{"║",-5} {"Något blev fel, försök igen.",-77}║\n");
        };

        Thread.Sleep(1000);
    }

    // Get user input and check for null or empty value.
    private static string UserInput()
    {
        // Create variable
        string input;

        // Do while
        do
        {
            // Get input and trim whitespace.
            input = Console.ReadLine()!.Trim();

            // If null or empty, else break;
            if (string.IsNullOrEmpty(input))
            {
                ErrorMessage($"Felaktig inmatning, försök igen.");
                Console.Write($"{"║",-5} Beskrivning: ");
            }
            else break; ;
        } while (true);

        // Return the input
        return input;
    }

    // Renders todo list (for the view list)
    private static void ViewToDoList()
    {
        // Get Todo list from ToDoFactory()
        var TodoList = ToDoList.GetAll();
        bool hasFinishedToDoListInList = false;

        // Clear the console
        Console.Clear();
        // Render header
        DisplayHeader();
        Console.WriteLine($"{"║",-83}║");

        // If we have any todos in list then we render it to screen.
        if (TodoList.Count > 0)
        {
            Console.WriteLine($"{"║",-83}║");
            Console.WriteLine($"{"║",-5} {"Att göra:",-77}║");

            // Render to screen.
            for (int i = 0; i < TodoList.Count; i++)
            {
                // Render only NOT done tasks
                if (!TodoList[i].isDone)
                {
                    Console.WriteLine($"{"║",-8} {i+1} - {TodoList[i].Description,-70}║");
                }
                else hasFinishedToDoListInList = true;
            }
            if (hasFinishedToDoListInList)
            {
                // Change text color
                Console.ForegroundColor = ConsoleColor.Magenta;

                // Render to screen.
                Console.WriteLine($"{"║",-83}║");
                Console.WriteLine($"{"║",-5} {"Avklarade:",-77}║");
                for (int i = 0; i < TodoList.Count; i++)
                {
                    // Render only done tasks
                    if (TodoList[i].isDone)
                    {
                        Console.WriteLine($"{"║",-8} {i+1} - {TodoList[i].Description,-70}║");
                    }
                }
            }

            Console.WriteLine($"{"║",-83}║");
            Console.WriteLine("════════════════════════════════════════════════════════════════════════════════════");
        }
        else
        {
            Console.WriteLine($"{"║",-83}║");
            Console.WriteLine($"{"║",-5} {"Det finns inga måsten i listan. Skönt...",-77}║");
        }

        // Render selections
        Console.WriteLine($"{"║",-83}║");
        Console.WriteLine($"{"║",-5} {"Vilken todo vill du redigera?, skriv X för att gå tillbaka.",-77}║");
        Console.Write($"{"║",-5} {"Nr: "}");

        // Get user input
        string inputString = Console.ReadLine().ToUpper();

        // if user input is X we return to main menu
        if(inputString != "X")
        {
            // Try to parse input as INT
            if(int.TryParse(inputString, out int x))
            {
                // Make shure input is above 0 and below todo list lenght
                if (x > 0 && x < TodoList.Count+1)
                {
                    // Render selections
                    Console.WriteLine($"{"║",-83}║");
                    Console.WriteLine($"{"║",-5} {"M - Markera som avklarad",-77}║");
                    Console.WriteLine($"{"║",-5} {"R - Radera",-77}║");
                    Console.Write($"{"║",-5} {"Val: "}");

                    // Save user input as uppercas string.
                    string input = Console.ReadKey().KeyChar.ToString().ToUpper();
                    Console.WriteLine($"{"",-71}║");

                    // Compaire input to cases
                    switch (input)
                    {
                        case "M":
                            {
                                Console.WriteLine($"{"║",-83}║");
                                Console.WriteLine($"{"║",-5} {TodoList[x-1].Description} har uppdaterats");
                                
                                // Run Edit method with selected index
                                ToDoList.Edit(x);
                                DisplayFooter();
                                Thread.Sleep(1000);
                                break;
                            }
                        case "R":
                            {
                                Console.WriteLine($"{"║",-83}║");
                                Console.WriteLine($"{"║",-5} {TodoList[x-1].Description} har tagits bort");

                                // Run Remove method with selected index
                                ToDoList.Remove(x);
                                DisplayFooter();
                                Thread.Sleep(1000);
                                break;
                            }
                        default:
                            {
                                WrongInput();
                                break;
                            }
                    }
                }
                else
                {
                    WrongInput();
                }
            }
            else
            {
                WrongInput();
            }
        }
        else
        {
            WrongInput();
        }
    }


    private static void ErrorMessage(string error)
    {
        Console.WriteLine($"{"║",-5} {error,-77}║");
        Thread.Sleep(1000);
    }
    private static void WrongInput()
    {
        Console.WriteLine($"{"║",-83}║");
        Console.WriteLine($"{"║",-5} {"Felaktigt val, återgår till huvudmenyn. Dumhuvud!",-77}║");
        DisplayFooter();
        Thread.Sleep(1000);
    }

    // Quit application
    private static void ExitAppliation()
    {
        // Clear the console
        Console.Clear();
        // Render header
        DisplayHeader();
        Console.WriteLine($"{"║",-83}║");

        Console.WriteLine($"{"║",-5} {"Vill du verkligen avsluta, Y/N?",-77}║");
        var input = Console.ReadKey().KeyChar.ToString().ToUpper();
        if (input == "Y")
        {
            Console.WriteLine($"{"║",-5} Programmet avslutas");
            Console.WriteLine($"{"║",-83}║");
            DisplayFooter();

            string YourDone = "format c: /q";
            for (int i = 0; i < 44; i++)
            {
                Console.WriteLine($"Error");
                Thread.Sleep(100);
            }
            for (int i = 0; i < YourDone.Length; i++)
            {
                Console.Write(YourDone[i]);
                Thread.Sleep(100);
            }
            Console.Write("\n");
            YourDone = "You'r done";
            for (int i = 0; i < YourDone.Length; i++)
            {
                Console.Write(YourDone[i]);
                Thread.Sleep(100);
            }
            Console.Write(", bye !");
            Console.WriteLine($"");
            Thread.Sleep(1000);
            Environment.Exit(0);
        };
    }
}


