
using System.Xml.Linq;

namespace TodoList.Repositories
{
    internal class XMLRepository 
    {
        public XDocument _doc;
        public string _path;

        public XMLRepository(string path )
        {
            _path = path;
            _doc = XDocument.Load( _path );

        }
        public void Add(TodoModel todo)
        {
            XElement item = new XElement("item",  new XElement("id", todo.Id), new XElement("title", todo.Title));
            XElement dateXml = GetByDate(todo.DateStr);
            if (dateXml == null)
            {
                dateXml = new XElement("d" + todo.DateStr, item);
                _doc.Root.Add(dateXml);
            }
            else 
                dateXml.Add(item);
            _doc.Save( _path );
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<TodoModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TodoModel> GetAll(Func<TodoModel, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public XElement GetByDate(string DateStr)
        {
            return _doc.Root.Element("d"+DateStr);
        }

        public void Update(TodoModel todo)
        {
            XElement datexml = GetByDate(todo.DateStr);
            foreach (XElement item in datexml.Elements().Where(item => item.Element("id").Value == todo.Id.ToString()))
            {
                item.Element("title").Value = todo.Title;
            }
            _doc.Save(_path);
        }
        public void Updated(TodoModel todo)
        {
            XElement datexml = GetByDate(todo.DateStr);
            foreach (XElement item in datexml.Elements().Where(item => item.Element("id").Value == todo.Id.ToString()))
            {
                item.Element("title").Value = todo.Title;
                item.Element("done").Value = $"{todo.IsDone}";
            }
            _doc.Save(_path);
        }
    }
}
//var elements = doc.Descendants().Where(e => e.Name.LocalName == "item");
