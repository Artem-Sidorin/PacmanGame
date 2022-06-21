using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pc_Man_Game_MOO_ICT
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            Closing += ExecuteOnClosing;
            tableRecords();
            this.Name = "Pacman";
        }

        private void tableRecords()
        {
            Save save = new Save();

            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Таблица рекордов";
            column1.Width = 100;
            column1.ReadOnly = true;
            column1.Name = "score";
            column1.Frozen = true;
            column1.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Add(column1);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;

            for (int i = 0; i < save.ReadScore().Count; ++i)
            {
                dataGridView1.Rows.Add();
                dataGridView1["score", dataGridView1.Rows.Count - 1].Value = save.ReadScore()[i];
            }
        }

        private void ExecuteOnClosing(object sender, CancelEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameLogic frm = new GameLogic();
            frm.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Visible == false)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox3.Visible = true; 
            }
            else
            if (pictureBox1.Visible == true)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
            }
        }
    }
}
