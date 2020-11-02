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
    public partial class completeBookDetails : Form
    {
        public completeBookDetails()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-5TIHETO;Initial Catalog=libraryManagment;Integrated Security=True");
        private void completeBookDetails_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM IRBook WHERE book_return_date is null",connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            SqlCommand command2 = new SqlCommand("SELECT * FROM IRBook WHERE book_return_date is not null", connection);
            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);

            dataGridView2.DataSource = ds2.Tables[0];
        }
    }
}
