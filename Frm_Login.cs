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
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_user.Text == "ADMIN" && txt_pswrd.Text == "ADMIN11")
            {
                MessageBox.Show("Login Successfull!");
                Frm_Main frm = new Frm_Main();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Try again");
            }
        }

        private void lbl_clear_Click(object sender, EventArgs e)
        {
            txt_user.Clear();
            txt_pswrd.Clear();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
