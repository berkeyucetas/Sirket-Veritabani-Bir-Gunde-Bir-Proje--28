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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Şirket_Veritabanı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-LQE0JO3;Initial Catalog=sirket;Integrated Security=True");
        private void verilerimiGöstter()
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand cmd = new SqlCommand("select * from bilgiler", baglan);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem add = new ListViewItem();
                add.Text = reader["ad"].ToString();
                add.SubItems.Add(reader["soyad"].ToString());
                add.SubItems.Add(reader["sehir"].ToString());
                add.SubItems.Add(reader["bolum"].ToString());
                add.SubItems.Add(reader["universite"].ToString());
                add.SubItems.Add(reader["departman"].ToString());
                listView1.Items.Add(add);
            }
            baglan.Close();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            verilerimiGöstter();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            
                string ad = txtAd.Text;
                string soyad = txtSoyad.Text;
                string sehir = comboSehir.Text;
                string universite = comboUni.Text;
                string bolum = txtBolum.Text;
                string departman = txtDprtmn.Text;

                
                string query = "INSERT INTO bilgiler (ad, soyad, sehir, bolum, universite, departman) VALUES (@ad, @soyad, @sehir, @bolum, @universite, @departman)";

                baglan.Open();
                SqlCommand cmd = new SqlCommand(query, baglan);

                cmd.Parameters.AddWithValue("@ad", ad);
                cmd.Parameters.AddWithValue("@soyad", soyad);
                cmd.Parameters.AddWithValue("@sehir", sehir);
                cmd.Parameters.AddWithValue("@bolum", bolum);
                cmd.Parameters.AddWithValue("@universite", universite);
                cmd.Parameters.AddWithValue("@departman", departman);

                cmd.ExecuteNonQuery();

                baglan.Close();
            verilerimiGöstter();
            txtAd.Text = "";
                txtSoyad.Text = "";
                comboSehir.Text = "";
                comboUni.Text = "";
                txtBolum.Text = "";
                txtDprtmn.Text = "";
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string ad = txtAd.Text;
                string soyad = txtSoyad.Text;
                string sehir = comboSehir.Text;
                string universite = comboUni.Text;
                string bolum = txtBolum.Text;
                string departman = txtDprtmn.Text;

                baglan.Open();
                SqlCommand cmd = new SqlCommand("UPDATE bilgiler SET ad=@ad, soyad=@soyad, sehir=@sehir, bolum=@bolum, universite=@universite, departman=@departman WHERE ad=@selectedAd AND soyad=@selectedSoyad", baglan);

                cmd.Parameters.AddWithValue("@ad", ad);
                cmd.Parameters.AddWithValue("@soyad", soyad);
                cmd.Parameters.AddWithValue("@sehir", sehir);
                cmd.Parameters.AddWithValue("@bolum", bolum);
                cmd.Parameters.AddWithValue("@universite", universite);
                cmd.Parameters.AddWithValue("@departman", departman);

                ListViewItem selectedItem = listView1.SelectedItems[0];
                string selectedAd = selectedItem.Text;
                string selectedSoyad = selectedItem.SubItems[1].Text;

                cmd.Parameters.AddWithValue("@selectedAd", selectedAd);
                cmd.Parameters.AddWithValue("@selectedSoyad", selectedSoyad);
                cmd.ExecuteNonQuery();
                baglan.Close();                
                verilerimiGöstter();                
            }
            else
            {
                MessageBox.Show("Lütfen Güncelleme Yapmak İstediğiniz Yere Tıklayın..!");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                txtAd.Text = selectedItem.Text;
                txtSoyad.Text = selectedItem.SubItems[1].Text;
                comboSehir.Text = selectedItem.SubItems[2].Text;
                comboUni.Text = selectedItem.SubItems[4].Text;
                txtBolum.Text = selectedItem.SubItems[3].Text;
                txtDprtmn.Text = selectedItem.SubItems[5].Text;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                string ad = selectedItem.Text;
                string soyad = selectedItem.SubItems[1].Text;

                baglan.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM bilgiler WHERE ad=@ad AND soyad=@soyad", baglan);
                cmd.Parameters.AddWithValue("@ad", ad);
                cmd.Parameters.AddWithValue("@soyad", soyad);
                cmd.ExecuteNonQuery();
                baglan.Close();                
                listView1.Items.Remove(selectedItem);
                txtAd.Text = "";
                txtSoyad.Text = "";
                comboSehir.Text = "";
                comboUni.Text = "";
                txtBolum.Text = "";
                txtDprtmn.Text = "";
            }
            else
            {
                MessageBox.Show("Lütfen Silmek İstediğiniz Yere Tıklayın..!");
            }
        }
    }
}
