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
    public partial class ReturnBook : Form
    {
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void ReturnBook_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }


        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-5TIHETO;Initial Catalog=libraryManagment;Integrated Security=True");
        private void btnSearch_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM IRBook WHERE std_enroll ='" + txtEnrollment.Text + "' and book_return_date IS NULL", connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                 MessageBox.Show("Enter valid enroolment number or no book issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }


        String bname,bdate;
        Int64 rowid;

       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != "")
            {
                rowid = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bname = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                bdate = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }

            txtBookName.Text = bname;
            txtBookissueDate.Text = bdate;
        }

        

        private void btnReturn_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE IRBook SET book_return_date ='" + dateTimePicker1.Text + "' WHERE std_enroll = '" + txtEnrollment.Text + "' and id = '" + rowid + "'", connection);

            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Return succesfull", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReturnBook_Load(this, null);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollment.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void txtEnrollment_TextChanged(object sender, EventArgs e)
        {
            if (txtEnrollment.Text == "") 
            {
                panel2.Visible = false;
                dataGridView1.DataSource = null;
            }
        }
    }
}
