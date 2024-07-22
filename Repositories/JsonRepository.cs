using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TodoList.Repositories
{
    internal class JsonRepository : IRepository<TodoModel>
    {
        static int _id = 0;
        private List<TodoModel> _todos;
        private readonly string _path;

        public JsonRepository(string path)
        {
            _path = path;
            _todos = GetAll();
        }

        private int GetNextId() => _id++;

        private List<TodoModel> ReadJson(string path)
        {
            using (StreamReader reader = File.OpenText(path))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                JArray jsonArray = JArray.Load(jsonReader);
                return jsonArray.ToObject<List<TodoModel>>()!;
            }
        }

        private void WriteJson(string path, List<TodoModel> todoList)
        {
            // Check if the file exists
            if (File.Exists(path) == false)
                throw new FileNotFoundException("File does not exist: " + path);

            using (StreamWriter writer = new StreamWriter(path))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(jsonWriter, todoList);
            }

        }

        public TodoModel Add(TodoModel todo)
        {
            todo.Id = GetNextId();
            _todos.Add(todo);
            WriteJson(_path, _todos);
            return todo;
        }

        public void DeleteById(int id)
        {
            _todos = _todos.Where(todo => todo.Id != id).ToList();
            WriteJson(_path, _todos);
        }

        public List<TodoModel> GetAll() => ReadJson(_path);

        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate) => _todos.Where(predicate).ToList();

        public TodoModel GetById(int id) =>
            _todos.Single(todo => todo.Id == id);

        public TodoModel Update(TodoModel todo)
        {
            int index = _todos.FindIndex(t => t.Id == todo.Id);
            _todos[index] = todo;
            WriteJson(_path, _todos);
            return todo;
        }
    }
}
