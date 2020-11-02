using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.InteropServices;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
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
   

        private void txtUsername_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Clear();
            }
        }

        private void txtPassword_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Clear();
                txtPassword.PasswordChar = '*';
            }
        }

        private void pictureBoxTwitter_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/krbkuni");
        }

        private void pictureBoxInstagram_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/karabukuniv");
        }

        private void pictureBoxWebsite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.karabuk.edu.tr/");
        }

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-5TIHETO;Initial Catalog=libraryManagment;Integrated Security=True"); 
       
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open(); 
                string sql = "SELECT * FROM loginTable WHERE username=@userName AND pass = @password "; 
               
                SqlParameter prm1 = new SqlParameter("userName", txtUsername.Text.Trim());
                SqlParameter prm2 = new SqlParameter("password", txtPassword.Text.Trim());

                SqlCommand command1 = new SqlCommand(sql, connection); 
              
                command1.Parameters.Add(prm1);
                command1.Parameters.Add(prm2);
               
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command1);
            
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    this.Hide();
                    Dashboard ds = new Dashboard();
                    ds.Show();
                    //MessageBox.Show("Giriş başarılı");

                }
            }
            catch (Exception)
            { 

                MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        
    }
}
