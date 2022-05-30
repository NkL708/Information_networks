using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Information_networks
{
    public partial class Form1 : Form
    {
        private static string host = "localhost";
        private static readonly string user = "postgres";
        private static string dbName = "InformationNetworks";
        private static string port = "5432";
        private static readonly string password = "hugelong123";
        private static string conString;
        private static NpgsqlConnection con;
        private static string conTreeString;
        private static NpgsqlConnection treeCon;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitButtonClick(object sender, EventArgs e)
        {
            openSessionButton.Enabled = true;
            closeSessionButton.Enabled = true;
            serverAdressTextBox.Enabled = true;
            serverPortTextBox.Enabled = true;
        }

        private void OpenSessionButtonClick(object sender, EventArgs e)
        {
            host = serverAdressTextBox.Text;
            port = serverPortTextBox.Text;
            conString = $"Server={host}; Username={user}; Database={dbName}; Port={port}; Password={password}; SSLMode=Prefer";
            con = new NpgsqlConnection(conString);
            con.Open();
            FillTreeView();
            connectionLabel.Text = "Подключено";
            connectionLabel.ForeColor = Color.Green;
            showUsersButton.Enabled = true;
            loginTextBox.Enabled = true;
            passwordTextBox.Enabled = true;
            insertButton.Enabled = true;
            deleteButton.Enabled = true;
            showFunctionsButton.Enabled = true;
            executeSqlButton.Enabled = true;
            updateDBButton.Enabled = true;
        }

        private void CloseSessionButtonClick(object sender, EventArgs e)
        {
            // Close connection
            con.Close();
            showUsersButton.Enabled = false;
            loginTextBox.Enabled = false;
            passwordTextBox.Enabled = false;
            connectionLabel.Text = "Не подключено";
            connectionLabel.ForeColor = Color.Red;
        }

        private void ExecuteButtonClick(object sender, EventArgs e)
        {
            // What is here?
        }

        private void ViewButtonClick(object sender, EventArgs e)
        {
            tableView.Clear();
            tableView.Text += "id\t username\t password\n";
            using (NpgsqlCommand command = new NpgsqlCommand(
                "SELECT * FROM users;", con)) 
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    String user = reader[0].ToString() + "\t" +
                        reader[1].ToString() + "\t\t" + reader[2].ToString() + "\n";
                    tableView.Text += user;
                }
            }
        }

        private void InsertButtonClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(loginTextBox.Text) && !String.IsNullOrEmpty(passwordTextBox.Text))
            {
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT insert_user(@u, @p);", con))
                {
                    command.Parameters.AddWithValue("u", loginTextBox.Text);
                    command.Parameters.AddWithValue("p", passwordTextBox.Text);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                incorrectFields.Text = "Заполните поля!";
                incorrectFields.Visible = true;
            }
        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(loginTextBox.Text) && !String.IsNullOrEmpty(passwordTextBox.Text))
            {
                using (NpgsqlCommand command = new NpgsqlCommand("SELECT delete_user(@u, @p);", con))
                {
                    command.Parameters.AddWithValue("u", loginTextBox.Text);
                    command.Parameters.AddWithValue("p", passwordTextBox.Text);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                incorrectFields.Text = "Заполните поля!";
                incorrectFields.Visible = true;
            }
        }

        private void ShowFunctionsButtonClick(object sender, EventArgs e)
        {
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(
                "SELECT routine_name\n" +
                "FROM information_schema.routines\n" +
                "WHERE routine_type = 'FUNCTION'\n" +
                "AND specific_schema = 'public';", con))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                gridView.DataSource = dataTable;
            }
        }

        private void ExecuteSqlButtonClick(object sender, EventArgs e)
        {
            using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(sqlText.Text, con))
            {   
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                gridView.DataSource = dataTable;
            }
        }

        private void UpdateDBButtonClick(object sender, EventArgs e)
        {   
            DataTable dataTable = (DataTable) gridView.DataSource;
            DataTable modifiedElements = new DataTable();
            modifiedElements.Columns.Add("id");
            modifiedElements.Columns.Add("username");
            modifiedElements.Columns.Add("password");
            foreach(DataRow row in dataTable.Rows) 
            {
                using (NpgsqlCommand command = new NpgsqlCommand(
                    "INSERT INTO users(id, username, password)" +
                    "VALUES(@i, @u, @p);", con))
                {
                    command.Parameters.AddWithValue("i", row[0]);
                    command.Parameters.AddWithValue("u", row[1]);
                    command.Parameters.AddWithValue("p", row[2]);
                    try
                    {
                        //con = new NpgsqlConnection()
                        command.ExecuteNonQuery();
                        modifiedElements.Rows.Add(row.ItemArray);
                    }
                    // If already in DB
                    catch (NpgsqlException) { }
                }
            }
            gridView.DataSource = modifiedElements;
        }
        public void FillTreeView()
        {
            using (NpgsqlCommand command = new NpgsqlCommand(
                "SELECT datname\n" +
                "FROM pg_database\n" +
                "WHERE datistemplate = false;", con))
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    TreeNode node = new TreeNode(reader[0].ToString())
                    {
                        Tag = "database"
                    };
                    treeView.Nodes.Add(node);
                }
            }
        }

        private void TreeViewNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode current = e.Node;
            current.Nodes.Clear();
            if (current.Tag.ToString() == "database")
            {
                dbName = current.Text;
                conTreeString = $"Server={host}; Username={user}; Database={dbName}; Port={port}; Password={password}; SSLMode=Prefer";
                treeCon = new NpgsqlConnection(conTreeString);
                treeCon.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(
                    "SELECT table_name\n" +
                    "FROM information_schema.tables\n" +
                    "WHERE table_schema = 'public'\n" +
                    "ORDER BY table_name;", treeCon))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TreeNode node = new TreeNode(reader[0].ToString())
                        {
                            Tag = "table"
                        };
                        current.Nodes.Add(node);
                    }
                }
            }
            else
            {
                dbName = current.Parent.Text;
                conTreeString = $"Server={host}; Username={user}; Database={dbName}; Port={port}; Password={password}; SSLMode=Prefer";
                treeCon = new NpgsqlConnection(conTreeString);
                treeCon.Open();
                using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter($"SELECT * FROM {current.Text};", treeCon))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    gridView.DataSource = dataTable;
                }
            }
        }

        private void RestoreItemClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = string.Empty;
                OpenFileDialog objOpenFileDialog = new OpenFileDialog
                {
                    Title = "Восстановить БД",
                    Filter = "Dump files|*.dump",
                    RestoreDirectory = true
                };
                if (objOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = objOpenFileDialog.FileName;
                }
                else return;

                if (filePath == string.Empty)
                {
                    MessageBox.Show("Укажите путь файла восстановления");
                    return;
                }

                StreamWriter sw = new StreamWriter("DBRestore.bat");
                StringBuilder strSB = new StringBuilder($"\"C:\\Program Files\\PostgreSQL\\13\\bin\\pg_restore.exe\" -c -h {host} -p {port} -U {user} -d {dbName} {filePath}");
                sw.WriteLine(strSB);
                sw.Dispose();
                sw.Close();
                Process processDB = Process.Start("DBRestore.bat");
                do { }
                while (!processDB.HasExited);
                {
                    MessageBox.Show($"База данных {dbName} восстановлена из {filePath}");
                }
            }
            catch (Exception) { }
        }
        private void DumpItemClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = string.Empty;
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Title = "Сохранить БД",
                    Filter = "Dump files|*.dump",
                    FilterIndex = 0,
                    RestoreDirectory = true,
                    FileName = $"backup_{DateTime.Now:dd-MM-yyyy_HH-mm}"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    filePath = saveFileDialog.FileName;
                else
                    return;

                if (filePath == string.Empty)
                {
                    MessageBox.Show("Укажите путь сохранения файла");
                    return;
                }

                StreamWriter sw = new StreamWriter("DBBackup.bat");
                string command = $"\"C:\\Program Files\\PostgreSQL\\13\\bin\\pg_dump.exe\" -h {host} -p {port} -U {user} -d {dbName} -F c -f {filePath}";

                sw.WriteLine(command);
                sw.Dispose();
                sw.Close();
                Process processDB = Process.Start("DBBackup.bat");
                do { }
                while (!processDB.HasExited);
                {
                    MessageBox.Show($"База данных {dbName} сохранена. Имя файла - {filePath}");
                }
            }
            catch (Exception) { }
        }
    }
}