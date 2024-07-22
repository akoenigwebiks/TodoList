using ReaLTaiizor.Forms;
using System.Data;
using System.Xml.Linq;
using TodoList.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ReaLTaiizor.Controls;

namespace TodoList
{
    enum Mode
    {
        Add,
        Edit
    }
    internal partial class Todos : MaterialForm
    {
        private List<TodoModel> todos;
        private Mode mode;
        private XMLRepository repository;
        private DateTime date;

        public Todos(XMLRepository repository)
        {
            InitializeComponent();
            todos = new List<TodoModel>();
            dataGridView_tasks.DataSource = todos;
            this.repository = repository;
            this.date = DateTime.Now.Date;
            hopeDatePicker1.Date = date;

        }

        private void populateViewWithTodo() { }

        // populate form from selected row
        private void dataGridView_tasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SetMode(Mode mode)
        {
            this.mode = mode;
            switch (mode)
            {
                case Mode.Add:
                    button_action.Text = "Add";
                    break;
                case Mode.Edit:
                    button_action.Text = "Edit";
                    break;
            }
        }

        // add or edit based on mode
        private void button_action_Click(object sender, EventArgs e)
        {

        }

        private void onDateChanged(DateTime newDateTime)
        {
            var foo = date;
            if (date == newDateTime)
            {
                return;
            }
            throw new NotImplementedException();
        }

        private void hopeDatePicker1_Click(object sender, EventArgs e)
        {
            // Get the selected date from the HopeDatePicker
            DateTime selectedDate = hopeDatePicker1.Date;
            var mse = (MouseEventArgs)e;
            // Check if the clicked area corresponds to a date
            if (IsDateClicked(mse.Location))
            {
                // Handle the date click event
                MessageBox.Show($"Date clicked: {selectedDate.ToShortDateString()}", "Date Clicked");
            }
        }

        private bool IsDateClicked(System.Drawing.Point location)
        {
            return location.X >= 10 && location.X <= 240 && location.Y >= 70 && location.Y <= 260;
            // Implement your logic to determine if a date was clicked based on the location
            // This might involve checking the bounds of date elements in the HopeDatePicker
            // Return true if a date was clicked, otherwise false
        }
        public void ShowGrid()
        {
            XElement datexml = repository.GetByDate(date.ToString());
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("status");
            dataTable.Columns.Add("id");
            dataTable.Columns.Add("title");
            dataTable.Columns.Add("date");
            if (datexml != null)
            {
                foreach (XElement item in datexml.Elements())
                {
                    DataRow datarow = dataTable.NewRow();
                    datarow["status"] = item.Element("done").Value;
                    datarow["id"] = item.Element("id").Value;
                    datarow["title"] = item.Element("title").Value;
                    datarow["date"] = date;
                    dataTable.Rows.Add(datarow);
                }
                dataGridView_tasks.DataSource = dataTable;
            }
            if (datexml == null)
            {
                dataTable = new DataTable();
                dataGridView_tasks.ClearSelection();
            }


        }
    }
}
