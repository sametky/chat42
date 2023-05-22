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

namespace chat42
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string numara;

        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-R3D59GR9;Initial Catalog=chat42;Integrated Security=True");

        void gelenkutusu()
        {
            SqlDataAdapter da1=new SqlDataAdapter("select * from tbl_mesajlar where alici="+ numara,baglanti);
            DataTable dt1=new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource= dt1;
        }
        void gidenkutusu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("select * from tbl_mesajlar where gonderen=" + numara, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            lblnumara.Text = numara.ToString();

            gelenkutusu();
            gidenkutusu();
            //ad soyadı çekiyoruz
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select ad, soyad from tbl_kisiler where numara="+ numara, baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text=dr[0] + " " + dr[1];
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into tbl_mesajlar(gonderen,alici,mesajkonu,mesajicerik) values(@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            komut.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p3", textBox1.Text);
            komut.Parameters.AddWithValue("@p4", richTextBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("mesajınız iletildi");
            gidenkutusu();
        }
    }
}
