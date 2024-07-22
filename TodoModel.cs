using System.Xml.Serialization;

namespace TodoList
{
    public class TodoModel
    {
        public TodoModel(int id, string title, DateOnly date, bool done)
        {
            Id = id;
            Title = title;
            Date = date;
            Done = done;
        }

        public TodoModel()
        {
            
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateOnly Date { get; set; }

        public bool Done { get; set; }


    }
}
