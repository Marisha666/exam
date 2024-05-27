using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace кафе
{
    //админ.сотрудники
    public partial class Form3 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder comBui;

        //Подключение базы данных
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\Kafe.mdf;
            Integrated Security=True;Current Language=Russian";
        string sql = "SELECT * FROM TabSot";

        public Form3()
        {
            InitializeComponent();

            //Добавление столбцов в таблицу, отключение автодобавления строк
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

                //Делаем недоступным столбцы для изменения
                dataGridView1.Columns["IdSot"].ReadOnly = true;
                dataGridView1.Columns["Stat"].ReadOnly = true;

                //названия столбцов
                dataGridView1.Columns["IdSot"].HeaderText = "Код";
                dataGridView1.Columns["Name1"].HeaderText = "Имя";
                dataGridView1.Columns["Name2"].HeaderText = "Отчество";
                dataGridView1.Columns["Name3"].HeaderText = "Фамилия";
                dataGridView1.Columns["DateR"].HeaderText = "Дата Рождения";
                dataGridView1.Columns["Adres"].HeaderText = "Адрес";
                dataGridView1.Columns["Dol"].HeaderText = "Должность";
                dataGridView1.Columns["Stat"].HeaderText = "Статус";
            }
        }

        //добавить
        private void button1_Click(object sender, EventArgs e)
        {
            //Добавление новой строки в DataTable
            DataRow row = ds.Tables[0].NewRow();
            row["Stat"] = "Работает";
            ds.Tables[0].Rows.Add(row);
        }

        //уволить
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                DataTable dt = ds.Tables[0];
                dt.Rows[0]["Stat"] = "Уволен";
            }
        }

        //сохранить
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                comBui = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("Procedure1", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Name1", SqlDbType.NVarChar, 50, "Name1"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Name2", SqlDbType.NVarChar, 50, "Name2"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Name3", SqlDbType.NVarChar, 50, "Name3"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@DateR", SqlDbType.Date, 0, "DateR"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Adres", SqlDbType.NVarChar, 50, "Adres"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Dol", SqlDbType.NVarChar, 50, "Dol"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Stat", SqlDbType.NVarChar, 50, "Stat"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@IdSot", SqlDbType.Int, 0, "IdSot");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
        }

        //кнопка назад
        private void button4_Click(object sender, EventArgs e)
        {
            Form newForm = new Form2();
            newForm.Show();
            this.Close();
        }
    }
}
