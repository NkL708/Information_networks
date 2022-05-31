using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
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
            //DataTable table = (DataTable)dataGridView.DataSource;
            //if (table == null)
            //    return;
            //DataTable newTable = new DataTable();
            //string colsStr = "";
            //// Getting parameters
            //foreach (DataColumn column in table.Columns)
            //{
            //    if (column.ColumnName == "id")
            //        continue;
            //    newTable.Columns.Add(column.ColumnName);
            //    colsStr += $"{column}, ";
            //}
            //// Removing last ","
            //colsStr = colsStr.Remove(colsStr.Length - 2);
            //foreach (DataRow row in table.Rows)
            //{
            //    string valuesStr = "";
            //    // Parsing datetime
            //    int i = 0;
            //    foreach (object cell in row.ItemArray)
            //    {
            //        if (table.Columns[i].ColumnName == "id")
            //        {
            //            i++;
            //            continue;
            //        }
            //        bool isDate = DateTime.TryParse(cell.ToString(), out DateTime date);
            //        if (isDate)
            //            valuesStr += $"@{date:yyyy-MM-dd}, ";
            //        else
            //            valuesStr += $"@{cell}, ";
            //        i++;
            //    }
            //    valuesStr = valuesStr.Remove(valuesStr.Length - 2);
            //    using (NpgsqlCommand command = new NpgsqlCommand(
            //        $"INSERT INTO {currentTable}({colsStr})\n" +
            //        $"VALUES({valuesStr});", connection))
            //    {
            //        // Set parameters for SQL query
            //        string[] cols = colsStr.Split();
            //        string[] values = valuesStr.Split();
            //        var colsAndValues = cols.Zip(values, (c, v) => new { Col = c, Value = v });
            //        foreach (var cv in colsAndValues)
            //        {
            //            if (cv.Col.Contains(",") || cv.Value.Contains(","))
            //                command.Parameters.AddWithValue(cv.Col.Trim(','), cv.Value.Trim(','));
            //            else
            //                command.Parameters.AddWithValue(cv.Col, cv.Value);
            //        }
            //        try
            //        {
            //            Debug.WriteLine($"{command.CommandText}\n");
            //            command.ExecuteNonQuery();
            //            newTable.Rows.Add(row.ItemArray);
            //        }
            //        // If already in DB
            //        catch (NpgsqlException) { }
            //    }
            //}
            //dataGridView.DataSource = newTable;

            DataTable table = (DataTable) dataGridView.DataSource;
            DataTable newTable = new DataTable();
            if (currentTable == null)
                return;
            if (currentTable == "teachers")
            {
                foreach (DataColumn column in table.Columns)
                {

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