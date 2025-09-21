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
    public partial class Frm_Teachers : Form
    {
        public Frm_Teachers()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=COMPUTERADMINIS;Initial Catalog=CollageManagement;Integrated Security=True");
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
            string query = "Select* from  Techrstbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TchrDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Frm_Teachers_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            if (txt_id.Text == "" && txt_name.Text == "" && txt_phone.Text == "" && txt_address.Text=="")
            {
                MessageBox.Show("Missing Record");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "insert into Techrstbl Values('" + txt_id.Text + "','" + txt_name.Text + "','" + cmb_gender.SelectedItem.ToString() + "','"+DTP1.Text+"','"+txt_phone.Text+"','"+cmb_department.SelectedValue.ToString()+"','"+txt_address.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teachers Record Saved");
                    con.Close();
                    populate();


                }
                catch
                {
                    MessageBox.Show("Something Went Wrong");
                }
            }
        }

        private void TchrDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_id.Text = TchrDGV.SelectedRows[0].Cells[0].Value.ToString();
            txt_name.Text = TchrDGV.SelectedRows[0].Cells[1].Value.ToString();
            cmb_gender.SelectedItem = TchrDGV.SelectedRows[0].Cells[2].Value.ToString();
            txt_phone.Text = TchrDGV.SelectedRows[0].Cells[4].Value.ToString();
            txt_address.Text = TchrDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "")
            {
                MessageBox.Show("Enter The Teacher's Id");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "Delete From Techrstbl where TchrId ='" + txt_id.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher  Deleted Successfully");
                    con.Close();
                    populate();
                }
                catch
                {
                    MessageBox.Show("Oops...Teacher Not Deleted");
                }

            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "" && txt_name.Text == "" && txt_address.Text =="")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "Update Techrstbl set TchrName='" + txt_name.Text + "',TchrGender='" + cmb_gender.SelectedItem.ToString() +"', TchrDOB = '" + DTP1.Text + "',TchrPhone='" + txt_phone.Text + "',TchrDep='" + cmb_department.SelectedItem.ToString() + "',TchrAdd='" + txt_address.Text + "'where TchrId='" + txt_id.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Successfully Updated");
                    con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
