using ReaLTaiizor.Forms;
using System.Data;
using System.Xml.Linq;
using TodoList.Repositories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ReaLTaiizor.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;

namespace TodoList
{
    enum Mode
    {
        Add,
        Update
    }
    internal partial class Todos : MaterialForm
    {
        private List<TodoModel> todos;
        private Mode mode;
        private XMLRepository repository;
        private DateTime date;
        private DataRow dataRow;

        public Todos(XMLRepository repository)
        {
            InitializeComponent();
            todos = new List<TodoModel>();
            //dataGridView_tasks.DataSource = todos;
            SetMode(Mode.Add);
            this.repository = repository;
            //this.date = DateTime.Now.Date;
            //hopeDatePicker1.Date = date;

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
                case Mode.Update:
                    button_action.Text = "Update";
                    break;
            }
        }

        // add or edit based on mode
        private void button_action_Click(object sender, EventArgs e)
        {
            string title = textbox_title.Text;
            bool isDone = checkbox_isDone.Checked;

            if (mode == Mode.Add)
            {

                TodoModel todoModel = new TodoModel(repository.GetSumTasks() + 1, title, date.ToString("yyyy-MM-dd"), isDone);
                repository.Add(todoModel);
                ShowGrid();
            }
            if (mode == Mode.Update)
            {
                TodoModel todoModel = new TodoModel(int.Parse(dataRow["id"] + ""), title, date.ToString("yyyy-MM-dd"), isDone);
                repository.Update(todoModel);
                ShowGrid();
            }

        }

        private void hopeDatePicker1_Click_1(object sender, EventArgs e)
        {
            SetMode(Mode.Add);
            date = hopeDatePicker1.Date;
            ShowGrid();
        }

        public void ShowGrid()
        {
            XElement datexml = repository.GetByDate(date.ToString("yyyy-MM-dd"));
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
                    datarow["id"] = item.Element("id")!.Value;
                    datarow["title"] = item.Element("title")!.Value;
                    datarow["date"] = date.ToString("yyyy-MM-dd");
                    dataTable.Rows.Add(datarow);
                }
                dataGridView_tasks.DataSource = dataTable;
            }
            if (datexml == null)
            {
                dataTable = new DataTable();
                dataGridView_tasks.DataSource = dataTable;
            }


        }

        private void dataGridView_tasks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetMode(Mode.Update);
            int row = dataGridView_tasks.CurrentRow.Index;
            DataRow dataRow;
            dataRow = ((dataGridView_tasks.DataSource) as DataTable)!.Rows[row];
            this.dataRow = dataRow;
            textbox_title.Text = dataRow["title"].ToString();
            if (dataRow["status"].ToString() == "true")
                checkbox_isDone.Checked = true;
            else
                checkbox_isDone.Checked = false;
        }
    }
}
