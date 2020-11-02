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

namespace Login
{
    public partial class AddBooks : Form
    {
        public AddBooks()
        {
            InitializeComponent();
        }

       


        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Are you sure?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }



        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-5TIHETO;Initial Catalog=libraryManagment;Integrated Security=True"); //bağlantı adresi 
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBookName.Text != "" && txtAuthor.Text != "" && txtPublication.Text != "" && txtPrice.Text != "" && txtQuantity.Text != "")
            {


                String name = txtBookName.Text;
                String author = txtAuthor.Text;
                String publication = txtPublication.Text;
                String pDate = dateTimePicker1.Text;
                Int64 price = Int64.Parse(txtPrice.Text);
                Int64 quantity = Int64.Parse(txtQuantity.Text);

                connection.Open(); //veri tabanı bağlantısını açıyoruz
                SqlCommand command = new SqlCommand("INSERT INTO NewBook (bName,bAuthor,bPubl,bPDate,bPrice,bQuan) values ('" + name + "','" + author + "','" + publication + "','" + pDate + "','" + price + "','" + quantity + "')", connection);//burda sql komutaşrından ınsert i kullanıyoruz ekleme için
                command.ExecuteNonQuery();//
                connection.Close();//burad bağlantıy kapatıyoruz daha sağlıklı bir sistem için bukadar 

                MessageBox.Show("Kayıt Başarılı", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtBookName.Clear();//hhe bunlarmı bunlar textboxları temizliyor ekleme yaptıktan sonra that is it dsad diğrlerini dahay pmadım iyi tmmsdsad ne yapcazbundan sonra ne var he tmm view var ama bakalım nasıl yapıo adam bende videondan izliyom zaten dsadasd
                txtAuthor.Clear();
                txtPublication.Clear();
                txtPrice.Clear();
                txtQuantity.Clear();
            }
            else
            {
                MessageBox.Show("Boş alan bırakılamaz.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddBooks_Load(object sender, EventArgs e)
        {

        }
    }
}
