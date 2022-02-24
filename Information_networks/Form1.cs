using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Information_networks
{
    public partial class Form1 : Form
    {
        private static string host;
        private static string user = "postgres";
        private static string dbName = "InformationNetworks";
        private static string port;
        private static string password = "hugelong123";
        private static string connString;
        private static NpgsqlConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void initButtonClick(object sender, EventArgs e)
        {
            // Set visible TBoxes
            loginTextBox.Visible = true;
            passwordTextBox.Visible = true;
            serverAdressTextBox.Visible = true;
            serverPortTextBox.Visible = true;
        }

        private void openSessionButtonClick(object sender, EventArgs e)
        {
            // Get host and port from TBoxes
            host = serverAdressTextBox.Text;
            port = serverPortTextBox.Text;
            connString = String.Format(
                "Server={0}; Username={1}; Database={2}; Port={3}; Password={4}; SSLMode=Prefer",
                host,
                user,
                dbName,
                port,
                password);
            // Connect to DB
            conn = new NpgsqlConnection(connString);
            conn.Open();
            connectionLabel.Text = "Подключено";
            connectionLabel.ForeColor = Color.Green;
            executeButton.Visible = true;
            // Execute INSERT in PGSQL
            using (var command = new NpgsqlCommand("INSERT INTO users(username, password) VALUES(@u, @p)", conn))
            {
                command.Parameters.AddWithValue("u", loginTextBox.Text);
                command.Parameters.AddWithValue("p", passwordTextBox.Text);
                command.ExecuteNonQuery();
            }
        }

        private void closeSessionButtonClick(object sender, EventArgs e)
        {
            // Close connection
            conn.Close();
            executeButton.Visible = false;
            connectionLabel.Text = "Не подключено";
            connectionLabel.ForeColor = Color.Red;
        }

        private void executeButtonClick(object sender, EventArgs e)
        {
            // What is here?
        }
    }
}
