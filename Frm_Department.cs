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
    public partial class Frm_Department : Form
    {
        public Frm_Department()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=COMPUTERADMINIS;Initial Catalog=CollageManagement;Integrated Security=True");
        private void populate()
        {
            con.Open();
            string query = "Select* from  Departmenttbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DptDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_name.Text == "" && txt_desc.Text == "" && txt_duration.Text == "" )
            {
                MessageBox.Show("Missing Record");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Departmenttbl Values('" + txt_name.Text + "','" + txt_desc.Text + "','" + txt_duration.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Saved");
                    con.Close();
                    populate();


                }
                catch 
                {
                    MessageBox.Show("Something Went Wrong");
                }
            }
        }

        private void Frm_Department_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            if (txt_name.Text == "")
            {
                MessageBox.Show("Enter The Department Name");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "Delete From Departmenttbl where DepName ='" + txt_name.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Deleted Successfully");
                    con.Close();
                    populate();
                }
                catch 
                {
                    MessageBox.Show("Oops...Department Not Deleted");
                }

            }
        }

        private void DptDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_name.Text = DptDGV.SelectedRows[0].Cells[0].Value.ToString();
            txt_desc.Text = DptDGV.SelectedRows[0].Cells[1].Value.ToString();
            txt_duration.Text = DptDGV.SelectedRows[0].Cells[2].Value.ToString();
            
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (txt_name.Text == "" && txt_desc.Text == "" && txt_duration.Text == "" )
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "Update Departmenttbl set DepDesc='" + txt_desc.Text + "',DepDuration='" + txt_duration.Text + "'where DepName='" + txt_name.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Successfully Updated");
                    con.Close();
                    populate();
                }
                catch 
                {
                    MessageBox.Show("oops....Department Not Updatedted");
                }
            }
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Main fm = new Frm_Main();
            fm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
