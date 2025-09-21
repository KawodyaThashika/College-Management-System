using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CollegeManagementSystemNew
{
    public partial class Frm_Dashboard : Form
    {
        public Frm_Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=COMPUTERADMINIS;Initial Catalog=CollageManagement;Integrated Security=True");
        private void Frm_Dashboard_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("Select Count (*) from Stdntbl", con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            Stdlbl.Text = dt1.Rows[0][0].ToString();
            con.Close();

            con.Open();
            SqlDataAdapter sda2 = new SqlDataAdapter("Select Count (*) from Techrstbl", con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            Tchrlbl.Text = dt2.Rows[0][0].ToString();
            con.Close();

            con.Open();
            SqlDataAdapter sda3 = new SqlDataAdapter("Select Count (*) from Feestbl", con);
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            Feeslbl.Text = "Rs"+Convert.ToInt32(dt3.Rows[0][0].ToString())*25000;
            con.Close();

            con.Open();
            SqlDataAdapter sda4 = new SqlDataAdapter("Select Count (*) from Departmenttbl", con);
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            Deprtlbl.Text = dt4.Rows[0][0].ToString();
            con.Close();


        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Main fm = new Frm_Main();
            fm.Show();
        }
    }
}
