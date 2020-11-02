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
    public partial class IssueBooks : Form
    {
        public IssueBooks()//he şumum dsadsad
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-5TIHETO;Initial Catalog=libraryManagment;Integrated Security=True");
        private void IssueBooks_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT bName FROM NewBook ",connection);
            SqlDataReader Sdr = command.ExecuteReader();
            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    txtBookName.Items.Add(Sdr.GetString(i));
                }
            }
            Sdr.Close();
            connection.Close();
        }

        

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }


        int count;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtEnroll.Text != "")
            {
                String eid = txtEnroll.Text;
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM  NewStudent WHERE enroll='"+eid+"'", connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);




                //---------------------------------------------------
                SqlCommand command2 = new SqlCommand("SELECT count(std_enroll) FROM  IRBook WHERE std_enroll='" + eid + "' and book_return_date is null", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2    );
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);

                count = int.Parse(ds2.Tables[0].Rows[0][0].ToString());
                //---------------------------------------------------


                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtSName.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtDepartment.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtSemester.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtContact.Text = ds.Tables[0].Rows[0][5].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0][6].ToString();
                }
                else
                {
                    txtSName.Clear();
                    txtDepartment.Clear();
                    txtSemester.Clear();
                    txtContact.Clear();
                    txtEmail.Clear();
                    MessageBox.Show("Invalid number ","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

                connection.Close();
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (txtSName.Text != "")
            {
                if (txtBookName.SelectedIndex != -1 && count <= 2)
                {
                    String enroll = txtEnroll.Text;
                    String sName = txtSName.Text;
                    String sDep = txtDepartment.Text;
                    String sem = txtSemester.Text;
                    Int64 contact = Int64.Parse(txtContact.Text);
                    String email = txtEmail.Text;
                    String bookname = txtBookName.Text;
                    String issueDate = dateTimePicker1.Text;
                    //
                    String eid = txtEnroll.Text;
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO IRBook (std_enroll,std_name,std_dep,std_sem,std_contact,std_email,book_name,book_issue_date) values ('" + enroll + "','" + sName + "','" + sDep + "','" + sem + "','" + contact + "','" + email + "','" + bookname + "','" + issueDate + "')", connection);
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Book isssued","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Select a book or maximum number of book has been issued", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Enter valid enroolment number ","Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtEnroll_TextChanged(object sender, EventArgs e)
        {
            if (txtEnroll.Text == "" )
            {
                txtSName.Clear();
                txtDepartment.Clear();
                txtSemester.Clear();
                txtContact.Clear();
                txtEmail.Clear();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnroll.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Are you sure?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
