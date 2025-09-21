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
    public partial class Frm_Student : Form
    {
        public Frm_Student()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=tcp:ASHEN\\SQLEXPRESS,1433;Initial Catalog=CollageManagement;Integrated Security=True");
        private void fillDepartment()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select DepName from Departmenttbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DepName", typeof(string));
            dt.Load(rdr);
            cmb_department.ValueMember = "DepName";
            cmb_department.DataSource = dt;
            con.Close();
        }
            private void populate()
        {
            con.Open();
            string query = "Select* from Stdntbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            StdntDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void noduelist()
        {
            con.Open();
            string query = "Select* from Stdntbl where StdFees > '"+0+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            StdntDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "" && txt_name.Text == "" && txt_phone.Text == "" && txt_fees.Text == "")
            {
                MessageBox.Show("Missing Record");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "insert into Stdntbl Values('" + txt_id.Text + "','" + txt_name.Text + "','" + cmb_gender.SelectedItem.ToString() + "','" + DTP1.Text + "','" + txt_phone.Text + "','" + cmb_department.SelectedValue.ToString() + "','" + txt_fees.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Record Saved");
                    con.Close();
                    populate();


                }
                catch
                {
                    MessageBox.Show("Something Went Wrong");
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "" && txt_name.Text == "" && txt_phone.Text == "" && txt_fees.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "Update Stdntbl set StdName='" + txt_name.Text + "',StdGender='" + cmb_gender.SelectedItem.ToString() + "', StdDOB = '" + DTP1.Text + "',StdPhone='" + txt_phone.Text + "',StdDep='" + cmb_department.SelectedItem.ToString() + "',StdFees='" + txt_fees.Text + "'where StdId='" + txt_id.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Successfully Updated");
                    con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "")
            {
                MessageBox.Show("Enter The Student Id");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "Delete From Stdntbl where StdId ='" + txt_id.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Deleted Successfully");
                    con.Close();
                    populate();
                }
                catch
                {
                    MessageBox.Show("Oops...Student Not Deleted");
                }

            }
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Main fm = new Frm_Main();
            fm.Show();
        }

        private void Frm_Student_Load(object sender, EventArgs e)
        {
            populate();
            fillDepartment();
        }

        private void StdntDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_id.Text = StdntDGV.SelectedRows[0].Cells[0].Value.ToString();
            txt_name.Text = StdntDGV.SelectedRows[0].Cells[1].Value.ToString();
            cmb_gender.SelectedItem = StdntDGV.SelectedRows[0].Cells[2].Value.ToString();
            txt_phone.Text = StdntDGV.SelectedRows[0].Cells[4].Value.ToString();
            txt_fees.Text = StdntDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void btn_duelist_Click(object sender, EventArgs e)
        {
            noduelist();
        }
    }
}
