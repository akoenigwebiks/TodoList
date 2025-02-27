using TodoList.Repositories;

namespace TodoList
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //string path = "Data/todos.xml";
            string path = "Data/todos.json";

            Application.Run(new Todos(new JsonRepository(path)));
            //Application.Run(new Todos(new XMLRepository(path)));
        }
    }
}