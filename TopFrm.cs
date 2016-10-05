using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MailAssistant;

namespace Toefl
{
    public partial class TopFrm : Form
    {
        public TopFrm()
        {
            InitializeComponent();
        }

        private void btnWords_Click(object sender, EventArgs e)
        {
            frmWordExercise frm = new frmWordExercise();

            frm.Show();
//test
        }

        private void btnReading_Click(object sender, EventArgs e)
        {
            frmMail frm = new frmMail();

            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process frm = new  Process();

            frm.Show();
        }


    }
}
