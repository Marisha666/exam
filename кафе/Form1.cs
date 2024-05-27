using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace кафе
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //вход
        private void button1_Click(object sender, EventArgs e)
        {
            Form newForm2 = new Form2(); //админ
            Form newForm6 = new Form6(); //повар
            Form newForm7 = new Form7(); //официант

            if ((textBox1.Text == "admin") & (textBox2.Text == "1234"))
            {
                newForm2.Show();
                this.Hide();
            }
            else
                if ((textBox1.Text == "povar") & (textBox2.Text == "4321"))
            {
                newForm6.Show();
                this.Hide();
            }
            else
                if ((textBox1.Text == "off") & (textBox2.Text == "5678"))
            {
                newForm7.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверно введены данные","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //выход
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
