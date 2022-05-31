using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Npgsql;

namespace RGZ
{
    public partial class Form1 : Form
    {
        private static string host = "localhost";
        private static readonly string user = "postgres";
        private static readonly string dbName = "IN_RGZ";
        private static readonly string port = "5432";
        private static readonly string password = "hugelong123";
        private static string conString;
        private static NpgsqlConnection connection;
        private static string currentTable;

        public Form1()
        {
            InitializeComponent();
            Connect();
        }

        private static void Connect()
        {
            conString = $"Server={host}; Username={user}; Database={dbName}; Port={port}; Password={password}; SSLMode=Prefer";
            connection = new NpgsqlConnection(conString);
            connection.Open();
        }
        private void TeachersButtonClick(object sender, EventArgs e)
        {
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(
                "SELECT id, last_name, first_name, patronymic,\n" +
                "department_name, employment_date\n" +
                "FROM teachers;", connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
            currentTable = "teachers";
        }

        private void DisciplinesButtonClick(object sender, EventArgs e)
        {
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(
                "SELECT id, name, speciality, semester\n" +
                "FROM disciplines", connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
            currentTable = "disciplines";
        }

        private void DepartmentDisciplinesButtonClick(object sender, EventArgs e)
        {
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(
                "SELECT id, teacher_id, discipline_id, hours_count\n" +
                "FROM department_disciplines", connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
            currentTable = "department_disciplines";
        }

        private void UpdateTableButtonClick(object sender, EventArgs e)
        {
            DataTable table = (DataTable) dataGridView.DataSource;
            if (currentTable == null)
                return;
            if (currentTable == "teachers")
            {
                foreach (DataRow row in table.Rows)
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(
                        "INSERT INTO teachers(id, last_name, first_name,\n" +
                        "patronymic, department_name, employment_date)\n" +
                        "VALUES (@id, @ln, @fn, @p, @dn, @ed);", connection))
                    {
                        command.Parameters.AddWithValue("id", row[0]);
                        command.Parameters.AddWithValue("ln", row[1]);
                        command.Parameters.AddWithValue("fn", row[2]);
                        command.Parameters.AddWithValue("p", row[3]);
                        command.Parameters.AddWithValue("dn", row[4]);
                        command.Parameters.AddWithValue("ed", row[5]);
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        // Already in db
                        catch(NpgsqlException) 
                        {

                        }
                    }
                }
            }
            if (currentTable == "disciplines")
            {

            }
            if (currentTable == "department_disciplines")
            {

            }
        }
    }
}