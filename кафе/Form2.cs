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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //кнопка назад
        private void button4_Click(object sender, EventArgs e)
        {
            Form newForm = new Form1();
            newForm.Show();
            this.Close();
        }

        //переход на форму сотрудников
        private void button1_Click(object sender, EventArgs e)
        {
            Form newForm = new Form3();
            newForm.Show();
            this.Close();
        }

        //переход на форму смен
        private void button2_Click(object sender, EventArgs e)
        {
            Form newForm = new Form4();
            newForm.Show();
            this.Close();
        }
        
        //переход на форму заказов
        private void button3_Click(object sender, EventArgs e)
        {
            Form newForm = new Form5();
            newForm.Show();
            this.Close();
        }
    }
}
