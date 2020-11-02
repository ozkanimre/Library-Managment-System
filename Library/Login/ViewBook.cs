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
    public partial class ViewBook : Form
    {
        public ViewBook()
        {
            InitializeComponent();
        }


        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-5TIHETO;Initial Catalog=libraryManagment;Integrated Security=True");
        private void ViewBook_Load(object sender, EventArgs e)
        {
            panel2.Visible = false; 
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM NewBook",connection);
            SqlDataAdapter da = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            connection.Close();
          
        }
     

        int bid;
        Int64 rowid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                bid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            panel2.Visible = true;

            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM NewBook WHERE bid = '"+bid+"'", connection);
            SqlDataAdapter da = new SqlDataAdapter(command);

            DataSet ds = new DataSet();
            da.Fill(ds);

            rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());

            txtbName.Text = ds.Tables[0].Rows[0][1].ToString();
            txtAuthor.Text = ds.Tables[0].Rows[0][2].ToString();
            txtPublication.Text = ds.Tables[0].Rows[0][3].ToString();
            txtPDate.Text = ds.Tables[0].Rows[0][4].ToString();
            txtPrice.Text = ds.Tables[0].Rows[0][5].ToString();
            txtQuantity.Text = ds.Tables[0].Rows[0][6].ToString();
            connection.Close();
        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            if (txtBookName.Text !="")
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM NewBook WHERE bName LIKE '"+txtBookName.Text+"%'", connection);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
                connection.Close();
            }
            else
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM NewBook", connection);
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
            txtBookName.Clear();
            panel2.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data will be updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                

                String name = txtbName.Text;
                String author = txtAuthor.Text;
                String publication = txtPublication.Text;
                String pdate = txtPDate.Text;
                Int64 price = Int64.Parse(txtPrice.Text);
                Int64 quantity = Int64.Parse(txtQuantity.Text);
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE NewBook SET bName = '" + name + "',bAuthor='" + author + "',bPubl='" + publication + "',bPDate='" + pdate + "',bPrice='" + price + "',bQuan='" + quantity + "' WHERE bid='" + rowid + "'", connection);
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
                SqlCommand command = new SqlCommand("DELETE FROM NewBook WHERE bid='" + rowid + "'", connection);
                SqlDataAdapter da = new SqlDataAdapter(command);

                DataSet ds = new DataSet();
                da.Fill(ds);
                connection.Close();
            }
        }
    }
}
