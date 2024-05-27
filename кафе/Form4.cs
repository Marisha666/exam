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
    //админ.смены
    public partial class Form4 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder comBui;

        //Подключение базы данных
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\Kafe.mdf;
            Integrated Security=True;Current Language=Russian";
        string sql = "SELECT * FROM TabSmen";

        public Form4()
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
                dataGridView1.Columns["IdSmen"].ReadOnly = true;

                //названия столбцов
                dataGridView1.Columns["IdSmen"].HeaderText = "Код смены";
                dataGridView1.Columns["NaS"].HeaderText = "Время начала";
                dataGridView1.Columns["KoS"].HeaderText = "Время конца";
                dataGridView1.Columns["DnN"].HeaderText = "День недели";
                dataGridView1.Columns["Shef"].HeaderText = "Шеф-повар";
                dataGridView1.Columns["Povar"].HeaderText = "Повар";
                dataGridView1.Columns["AdminS"].HeaderText = "Администратор";
                dataGridView1.Columns["Of1"].HeaderText = "Официант 1";
                dataGridView1.Columns["Of2"].HeaderText = "Официант 2";
                dataGridView1.Columns["Ubor"].HeaderText = "Уборщик";
            }
        }

        //добавить
        private void button1_Click(object sender, EventArgs e)
        {
            //Добавление новой строки в DataTable
            DataRow row = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(row);
        }

        //удалить
        private void button2_Click(object sender, EventArgs e)
        {
            //Удаляние выделенной строки из dataGridView1
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
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
                adapter.InsertCommand = new SqlCommand("Procedure2", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@NaS", SqlDbType.NVarChar, 50, "NaS"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@KoS", SqlDbType.NVarChar, 50, "KoS"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@DnN", SqlDbType.NVarChar, 50, "DnN"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Shef", SqlDbType.NVarChar, 50, "Shef"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Povar", SqlDbType.NVarChar, 50, "Povar"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@AdminS", SqlDbType.NVarChar, 50, "AdminS"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Of1", SqlDbType.NVarChar, 50, "Of1"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Of2", SqlDbType.NVarChar, 50, "Of2"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Ubor", SqlDbType.NVarChar, 50, "Ubor"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@IdSmen", SqlDbType.Int, 0, "IdSmen");
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
