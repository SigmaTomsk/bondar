using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace CSVDataBase
{
    public partial class Form1 : Form
    {
        static string conn = "Data Source=DataBase.db;Persist Security Info=False;";
        SQLiteConnection connection = new SQLiteConnection(conn);
        SQLiteCommand command = new SQLiteCommand();
        string direct, tableName;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = ".csv|*.csv";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                direct = openFileDialog1.FileName;
                textBox1.Text = direct;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Close();
            StreamReader csv = new StreamReader(direct);
            string fl = csv.ReadLine(), // fl - FirstLine
                   columns = "";

            fl = fl.Replace("\"", "").Replace("(", "").Replace(")", "");

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    columns = "Year, Score, Title";
                    tableName = "deniro";
                break; // deniro

                case 1:
                    columns = "Index, Living Space sq ft, Beds, Baths, Zip, Year, List Price $";
                    tableName = "zillow";
                break; // zillow

                case 2:
                    columns = "Sell, List, Living, Rooms, Beds, Baths, Age, Acres, Taxes";
                    tableName = "homes";
                break; // homes

                case 3:
                    columns = "Name,     Sex, Age, Height(in), Weight(lbs)";
                    tableName = "biostats";
                break; // biostats

                case 4:
                    columns = "Last name, First name, SSN, Test1, Test2, Test3, Test4, Final, Grade";
                    tableName = "grades";
                break; // grades
            }

            if (fl == columns)
            {
                connection.Open();
                    command.Connection = connection;
                    string[] elements = columns.Split(',');
                    string values = "@";

                    for (int p = 1; p < elements.Length; p++) elements[p] = elements[p].Substring(1, elements[p].Length - 1);
                    for (int g = 0; g < elements.Length; g++) elements[g] = elements[g].Replace(" ", "_");

                    for (int i = 0; i < elements.Length; i++)
                    {
                        if (i != elements.Length - 1) values += elements[i] + ", @";
                        else values += elements[i];
                    }

                    for (int i = 0; i < elements.Length; i++)
                    {
                        string a = csv.ReadLine();
                        string[] data = a.Split(',');
                       
                        clear_garbage(data);

                        command.CommandText = "INSERT INTO " + tableName + "  (" + values.Replace("@", "") + ") VALUES (" + values + ")";
                        SQLiteParameter[] param = new SQLiteParameter[data.Length];
                        for (int j = 0; j < data.Length; j++) command.Parameters.AddWithValue(elements[j].ToString(), data[j]);

                        SQLiteDataReader reader = command.ExecuteReader();

                        command.Reset();
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Операция прошла успешно", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    connection.Close();
                
            }
            else
            {
                MessageBox.Show("Невозможно записать .csv в выбранную таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public string[] clear_garbage(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++) arr[i] = arr[i].Replace(" ", "").Replace("\"", "").Replace("\\", "");
            return arr;
        }
    }
}
