using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Login
{
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }





        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtENo.Clear();
            txtDepartment.Clear();
            txtSemester.Clear();
            txtContact.Clear();
            txtEmail.Clear();

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Are you sure?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-5TIHETO;Initial Catalog=libraryManagment;Integrated Security=True"); //bağlantı adresi 
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtENo.Text != "" && txtSemester.Text != "" && txtDepartment.Text != "" && txtContact.Text != "" && txtEmail.Text != "") 
            {


                String name = txtName.Text;
                String enroll = txtENo.Text;
                String semester = txtSemester.Text;
                String department = txtDepartment.Text;
                Int64 contact = Int64.Parse(txtContact.Text);
                String email = txtEmail.Text;


                connection.Open(); 
                SqlCommand command = new SqlCommand("INSERT INTO NewStudent (sname,enroll,dep,sem,contact,email) values ('" + name + "','" + enroll + "','" + department + "','" + semester + "','" + contact + "','" + email + "')", connection);
                command.ExecuteNonQuery();
                connection.Close(); 

                MessageBox.Show("Kayıt Başarılı", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                txtName.Clear();
                txtENo.Clear();
                txtDepartment.Clear();
                txtSemester.Clear();
                txtContact.Clear();
                txtEmail.Clear();

            }
            else
            {
                MessageBox.Show("Boş alan bırakılamaz.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
