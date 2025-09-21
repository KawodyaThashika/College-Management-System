using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollegeManagementSystemNew
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        private void btn_students_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Student fs = new Frm_Student();
            fs.Show();
        }

        private void btn_teachers_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Teachers ft = new Frm_Teachers();
            ft.Show();
        }

        private void btn_department_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Department fd = new Frm_Department();
            fd.Show();
        }

        private void btn_fees_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Fees ff = new Frm_Fees();
            ff.Show();
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Dashboard f2 = new Frm_Dashboard();
            f2.Show();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Login fl = new Frm_Login();
            fl.Show();
        }

    }
}
