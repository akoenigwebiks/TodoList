using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoList
{
    
    public class TodoModel
    {
        public bool IsDone { get; set; }
        public int Id { get; set; }

        public string Title { get; set; }

        public string DateStr
        {
            get { return Date.ToString("yyyy-MM-dd"); } 
            set { Date = DateOnly.Parse(value); } 
        }

        public DateOnly Date { get; set; }

        public TodoModel(string title, string dateStr )
        {
            //Id = id;
            Title = title;
            DateStr = dateStr;
            IsDone = false;
        }
    }
}
