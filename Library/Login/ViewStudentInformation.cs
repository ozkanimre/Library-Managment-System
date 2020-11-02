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

namespace Login
{
    public partial class ViewStudentInformation : Form
    {
        public ViewStudentInformation()
        {
            InitializeComponent();
        }

       

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-5TIHETO;Initial Catalog=libraryManagment;Integrated Security=True");
        private void ViewStudentInformation_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM NewStudent", connection);
            SqlDataAdapter da = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            connection.Close();

        }
        //

        int bid;
        Int64 rowid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                bid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                // MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            panel2.Visible = true;

            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM NewStudent WHERE stuid = '" + bid + "'", connection);
            SqlDataAdapter da = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            da.Fill(ds);

            panel2.Visible = true;
            rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());

            txtName.Text = ds.Tables[0].Rows[0][1].ToString();
            txtEnroll.Text = ds.Tables[0].Rows[0][2].ToString();
            txtDepartment.Text = ds.Tables[0].Rows[0][3].ToString();
            txtSemester.Text = ds.Tables[0].Rows[0][4].ToString();
            txtContact.Text = ds.Tables[0].Rows[0][5].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0][6].ToString();
            connection.Close();
        }
        
        private void txtStudentName_TextChanged(object sender, EventArgs e)
        {
            if (txtStudentName.Text != "")
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM NewStudent WHERE sname LIKE '" + txtStudentName.Text + "%'", connection);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
                connection.Close();
            }
            else
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM NewStudent", connection);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
                connection.Close();
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtStudentName.Clear();
            panel2.Visible = false;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {


                String name = txtName.Text;
                String enroll = txtEnroll.Text;
                String department = txtDepartment.Text;
                String semester = txtSemester.Text;
                String contact = txtContact.Text;
                String email = txtEmail.Text;
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE NewStudent SET sname = '" + name + "',enroll='" + enroll + "',dep='" + department + "',sem='" + semester + "',contact='" + contact + "',email='" + email + "' WHERE stuid='" + rowid + "'", connection);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataSet ds = new DataSet();
                da.Fill(ds);
                connection.Close();
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be deleted. Confirm?", "Confirmation Dialog", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {


                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM NewStudent WHERE stuid='" + rowid + "'", connection);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataSet ds = new DataSet();
                da.Fill(ds);
                connection.Close();
            }
        }
    }
}
