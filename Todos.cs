using ReaLTaiizor.Forms;

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
        private IRepository<TodoModel> repository;

        public Todos(IRepository<TodoModel> repository)
        {
            InitializeComponent();
            todos = repository.GetAll();
            dataGridView_tasks.DataSource = todos;
            this.repository = repository;
            populateViewWithTodo(todos[0]);
            this.mode = Mode.Add;
            button_action.Text = "Add";
        }

        private void populateViewWithTodo(TodoModel todo)
        {
            textbox_title.Text = todo.Title;
            textbox_title.Tag = todo.Id;
            datePicker.Value = new DateTime(todo.Date.Year, todo.Date.Month, todo.Date.Day);

            checkbox_isDone.Checked = todo.Done;
        }

        // populate form from selected row
        private void dataGridView_tasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            if (idx < 0) return;
            SetMode(Mode.Edit);
            int row = dataGridView_tasks.CurrentRow.Index;
            //DataRow dataRow;
            var dataRow = (List<TodoModel>)dataGridView_tasks.DataSource;
            TodoModel todo = dataRow[row];
            populateViewWithTodo(todo);
      
            //var foo!.Rows[row];
            //int id = (int)dataRow["id"];
            //TodoModel todo = todos.Find(t => t.Id == id);
            //populateViewWithTodo(todo);
            //todos = repository.GetAll();
            //dataGridView_tasks.DataSource = todos;
            ////    int idx = e.RowIndex;
            ////if (idx < 0) return;
            //foreach (DataGridViewRow todoRow in dataGridView_tasks.SelectedRows)
            //{
            //    int id = (int)todoRow.Cells[0].Value;
            //    TodoModel todo = todos.Find(t => t.Id == id);
            //    populateViewWithTodo(todo);
            //}
            //todos = repository.GetAll();
            //dataGridView_tasks.DataSource = todos;

            //int idx = e.RowIndex;
            //if (idx < 0) return;
            //DataGridViewRow row = dataGridView_tasks.Rows[e.RowIndex];
            //if (e.RowIndex >= 0)
            //{
            //    SetMode(Mode.Edit);

            //    int id = (int)row.Cells[0].Value;
            //    TodoModel todo = todos.Find(t => t.Id == id);

            //    populateViewWithTodo(todo);
            //}
        }

        private void SetMode(Mode mode)
        {
            this.mode = mode;
            button_action.Text = Mode.Add == this.mode ? "Add" : "Edit";
        }

        // add or edit based on mode
        private void button_action_Click(object sender, EventArgs e)
        {
            int id = (int)textbox_title.Tag;
            string title = textbox_title.Text;
            DateOnly date = DateOnly.FromDateTime(datePicker.Value);
            bool done = checkbox_isDone.Checked;
            switch (mode)
            {
                case Mode.Add:
                    repository.Add(new TodoModel(id, title, date, done));
                    break;
                case Mode.Edit:
                    repository.Update(new TodoModel(id, title, date, done));
                    break;
                default:
                    MessageBox.Show("HOW ARE YOU EVEN SEEING THIS WHAT DID YOU DO>!>!>!>!>!?!?");
                    break;
            }
            todos = repository.GetAll();
            dataGridView_tasks.DataSource = todos;
        }
    }
}
