using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace Information_networks
{
    public partial class Form1 : Form
    {
        private static string host;
        private static readonly string user = "postgres";
        private static readonly string dbName = "Information_networks";
        private static string port;
        private static readonly string password = "hugelong123";
        private static string connString;
        private static NpgsqlConnection con;

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
            connString = String.Format(
                "Server={0}; Username={1}; Database={2}; Port={3}; Password={4}; SSLMode=Prefer",
                host,
                user,
                dbName,
                port,
                password);
            con = new NpgsqlConnection(connString);
            con.Open();
            connectionLabel.Text = "Подключено";
            connectionLabel.ForeColor = Color.Green;
            executeButton.Enabled = true;
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
            executeButton.Enabled = false;
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

        private void ViewButton_Click(object sender, EventArgs e)
        {
            tableView.Clear();
            tableView.Text += "id\t username\t password\n";
            using (NpgsqlCommand command = new NpgsqlCommand(
                "SELECT * FROM users;", con)) 
            {
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
                        command.ExecuteNonQuery();
                        //DataRow modifiedRow = new DataRow(row[0], row[1], row[2]);
                        //modifiedElements.Rows.Add(modifiedRow);
                    }
                    // If already in DB
                    catch (NpgsqlException) { }
                    finally
                    {
                        gridView.DataSource = modifiedElements;
                    }
                }
            }
        }
    }
}