
using System.Xml.Linq;
using TodoList.Context;

namespace TodoList.Repositories
{
    internal class XMLRepository : IRepository<TodoModel>
    {
        private static int _Increment = 0;

        private readonly XMLService _xmlService;
        private readonly string _path;
        private List<TodoModel> _todos;
        public XMLRepository(string path)
        {
            _path = path;
            _xmlService = new XMLService(_path);
            _todos = GetAll();
        }

        private int GetNextId() => _Increment++;

        public TodoModel ToModel(XElement node)
        {
            return new TodoModel
            {
                Id = int.Parse(node.Element("ID")!.Value),
                Title = node.Element("Title")!.Value,
                Date = DateOnly.Parse(node.Element("Date")!.Value),
                Done = node.Element("Done")!.Value.ToUpper() == "TRUE"
            };

            //return new TodoModel(
            //    int.Parse(node.Element("ID")!.Value),
            //    node.Element("Title")!.Value,
            //    DateOnly.Parse(node.Element("Date")!.Value),
            //    bool.Parse(node.Element("Done")!.Value)
            //);
        }

        public XElement ToXML(TodoModel todo)
        {
            XElement todoNode = new XElement("Todo");
            XElement idNode = new XElement("ID", todo.Id);
            XElement titleNode = new XElement("Title", todo.Title);
            XElement dateNode = new XElement("Date", todo.Date.ToString("yyyy-MM-dd"));
            XElement doneNode = new XElement("Done", todo.Done.ToString());
            todoNode.Add(idNode, titleNode, dateNode, doneNode);
            return todoNode;
        }

        public TodoModel Add(TodoModel todo)
        {
            todo.Id = GetNextId();
            var node = ToXML(todo);

            _xmlService._xDocument.Root!.Add(node);
            _xmlService._xDocument.Save(_path);
            _todos = GetAll();
            return todo;
        }

        public void DeleteById(int id)
        {
            _todos = _todos.Where(todo => todo.Id != id).ToList();
            var nodes = _todos.Select(ToXML).ToList();
            _xmlService._xDocument.Root!.ReplaceNodes(nodes);
            _xmlService._xDocument.Save(_path);
        }

        /// <summary>
        /// Gets all the todos
        /// </summary>
        /// <returns></returns>
        public List<TodoModel> GetAll() =>
        _xmlService._xDocument.Root!.Elements("Todo").Select(ToModel).ToList();

        /// <summary>
        /// Gets all the todos that satisfy the predicate
        /// </summary>
        /// <param name="predicate">
        /// A filter function that returns true if the todo should be included
        /// </param>
        /// <returns></returns>
        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate) => _xmlService._xDocument.Root!.Elements("Todo").Select(ToModel).Where(predicate).ToList();

        public TodoModel GetById(int id) =>
            _todos.Single(todo => todo.Id == id);

        /// <summary>
        /// Updates list of todos and saves it to the xml file
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        public TodoModel Update(TodoModel todo)
        {
            int index = _todos.FindIndex(a => a.Id == todo.Id);
            _todos[index] = todo;
            var nodes = _todos.Select(ToXML).ToList();
            _xmlService._xDocument.Root!.ReplaceNodes(nodes);
            _xmlService._xDocument.Save(_path);
            return todo;
        }
    }
}
