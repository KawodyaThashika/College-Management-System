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
    public partial class Frm_Fees : Form
    {
        public Frm_Fees()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=COMPUTERADMINIS;Initial Catalog=CollageManagement;Integrated Security=True");
        private void fillDepartment()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select StdId from stdntbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StdId", typeof(int));
            dt.Load(rdr);
            cmb_stid.ValueMember = "StdId";
            cmb_stid.DataSource = dt;
            con.Close();
        }
        private void populate()
        {
            con.Open();
            string query = "Select* from Feestbl ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FeesDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void updatestd ()
        {
            con.Open();
            String query = "Update Stdntbl set StdFees='" + txt_amount.Text +  "'where StdId='" + cmb_stid.SelectedValue.ToString()+ "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            //MessageBox.Show("Student Successfully Updated");
            con.Close();
        }

        private void Frm_Fees_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
        }
        
        private void cmb_stid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con.Open();
            string query = "Select * from stdntbl where StdId ='" + cmb_stid.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                txt_name.Text = dr["StdName"].ToString();
            }
            con.Close();
        }

        private void btn_pay_Click(object sender, EventArgs e)
        {
            if (txt_number.Text == "" && txt_name.Text == "" && txt_amount.Text == "")
            {
                MessageBox.Show("Missing Record");
            }
            else
            {
                try
                {
                    string DTP = periodate.Value.Year.ToString();

                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Feestbl where StdId ='" + cmb_stid.SelectedValue.ToString() + "'  and Period ='" + DTP + "' ", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show("No Dues For The Selected Period");
                        con.Close();
                    }
                    else
                    {
                       // con.Open();
                        String query = "insert into Feestbl Values('" + txt_number.Text + "','" + cmb_stid.SelectedValue.ToString() + "','" + txt_name.Text + "','" + DTP + "','" + txt_amount.Text + "')";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Fees Successfully Saved");
                        con.Close();
                        populate();
                        updatestd();

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (txt_number.Text == "" && txt_name.Text == "" && txt_amount.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "Update Feestbl set StdId='" + cmb_stid.SelectedValue.ToString() + "',StdName='" + txt_name.Text + "', Period = '" + periodate.Text + "',Amount='" + txt_amount.Text +  "'where FeesNum='" + txt_number.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated");
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
            if (txt_number.Text == "")
            {
                MessageBox.Show("Enter The Number");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "Delete From Feestbl where FeesNum ='" + txt_number.Text + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Deleted");
                    con.Close();
                    populate();
                }
                catch
                {
                    MessageBox.Show("Oops...Not Deleted");
                }

            }
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Main fm = new Frm_Main();
            fm.Show();
        }

        private void FeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            
          
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("FEES RECEIPT", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Receipt Number", new Font("Century", 20, FontStyle.Bold), Brushes.Blue, new Point(40,50));
            e.Graphics.DrawString(FeesDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(300,50));
            e.Graphics.DrawString("Student Usn", new Font("Century", 20, FontStyle.Bold), Brushes.Blue, new Point(40,80));
            e.Graphics.DrawString(FeesDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(300, 80));
            e.Graphics.DrawString("Student Name", new Font("Century", 20, FontStyle.Bold), Brushes.Blue, new Point(40, 110));
            e.Graphics.DrawString(FeesDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(300, 110));
            e.Graphics.DrawString("Period", new Font("Century", 20, FontStyle.Bold), Brushes.Blue, new Point(40, 140));
            e.Graphics.DrawString(FeesDGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(300, 140));
            e.Graphics.DrawString("Amount", new Font("Century", 20, FontStyle.Bold), Brushes.Blue, new Point(40, 170));
            e.Graphics.DrawString(FeesDGV.SelectedRows[0].Cells[4].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(300, 170));
            e.Graphics.DrawString("CollegeManagementSystem", new Font("Century", 18, FontStyle.Bold), Brushes.Red, new Point(230,250));

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

            

        }

        private void StdntDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FeesDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();

            }
        }
    }
}
