using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FightClubDemo
{
    public partial class Form2 : Form
    {
        private bool willPlay = false;

        public Form2()
        {
            InitializeComponent();
        }



        public bool WillPlay()
        {
            return willPlay;
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            willPlay = true;
        }

        private void RefuseButton_Click(object sender, EventArgs e)
        {

        }
    }
}
