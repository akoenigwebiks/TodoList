using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TodoList.Context
{
    internal class XMLService
    {
        private string _path;
        public XDocument _xDocument;
        public XMLService(string path)
        {
            _path = path;
            _xDocument = XDocument.Load(_path);
            //xDocument.Save(_path);
        }

        //get direct children of an XElement
        public List<XElement> GetDirectChildren(XElement element) =>
            element.Elements().ToList();


    }
}
