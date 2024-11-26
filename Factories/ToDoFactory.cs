using ConsoleApp2.Models;
using System.Diagnostics;

namespace ConsoleApp2.Factories;

class ToDoFactory
{
    // Instansiate new todo list
    private readonly List<ToDo> _todo = new ()
    {
        // Create some default todos
        new ToDo { Description = "Gå ut med soporna" },
        new ToDo { Description = "Besiktiga bilen" },
        new ToDo { Description = "Betala räkningar" },
        new ToDo { Description = "Gör något annat än att sitta vid datorn" },
        new ToDo { Description = "Köp mjölk", isDone = true},
    };

    // Add an new item to the todo list
    public bool Add(ToDo input)
    {
        try
        {
            _todo.Add(input);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }


    // Sends all todos as an list komponent
    public List<ToDo> GetAll() 
    { 
        return _todo.Select(_todo => _todo).ToList();
    }

    // Edit todo by index
    public void Edit(int input)
    {
        try
        {
            _todo[input - 1].isDone = !_todo[input - 1].isDone;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }


    // Remove todo by index
    public void Remove(int input) {
        try
        {
            _todo.RemoveAt(input - 1);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
