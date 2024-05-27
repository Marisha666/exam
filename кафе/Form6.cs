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
    //повар(заказы)
    public partial class Form6 : Form
    {
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder comBui;

        //Подключение базы данных
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=|DataDirectory|\Kafe.mdf;
            Integrated Security=True;Current Language=Russian";
        string sql = "SELECT * FROM TabZak";

        public Form6()
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
                dataGridView1.Columns["IdZak"].ReadOnly = true;
                dataGridView1.Columns["StatZak"].ReadOnly = true;

                //названия столбцов
                dataGridView1.Columns["IdZak"].HeaderText = "Код заказа";
                dataGridView1.Columns["NSmen"].HeaderText = "№ смены";
                dataGridView1.Columns["NStol"].HeaderText = "№ стола";
                dataGridView1.Columns["Zakaz"].HeaderText = "Заказ";
                dataGridView1.Columns["Sto"].HeaderText = "Стоимость";
                dataGridView1.Columns["StatZak"].HeaderText = "Статус";
            }
        }

        //изменить на готов
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                DataTable dt = ds.Tables[0];
                dt.Rows[0]["StatZak"] = "Готов";
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
                adapter.InsertCommand = new SqlCommand("Procedure3", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@NSmen", SqlDbType.Int, 0, "NSmen"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@NStol", SqlDbType.Int, 0, "NStol"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Zakaz", SqlDbType.NVarChar, 100, "Zakaz"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Sto", SqlDbType.NVarChar, 50, "Sto"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@StatZak", SqlDbType.NVarChar, 50, "StatZak"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@IdZak", SqlDbType.Int, 0, "IdZak");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
        }

        //назад
        private void button4_Click(object sender, EventArgs e)
        {
            Form newForm = new Form1();
            newForm.Show();
            this.Close();
        }
    }
}
