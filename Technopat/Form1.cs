using HtmlAgilityPack;
using MetroFramework.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;

namespace Technopat
{
    public partial class TechnoIzle : MetroForm
    {
        public TechnoIzle()
        {
            InitializeComponent();
        }

        string url = "https://www.technopat.net/video/";
        string link = "https://www.technopat.net/video/page/";

        private void ekle(int iter)
        {
            HtmlAttribute linkk;
            string linkiter;
            ArrayList linklistesi = new ArrayList();
            for (int i = 1; i <= iter; i++)
            {
                if (i == 1)
                {
                    linkiter = url;
                }
                else
                {
                    linkiter = link + i.ToString();
                }
                //listBox1.Items.Add(linkiter);
                var html = @linkiter;
                HtmlWeb web = new HtmlWeb();
                var htmlDoc = web.Load(html);
                //HtmlNodeCollection node = htmlDoc.DocumentNode.SelectNodes("//div[@class='meta-info']//h3//a");

                HtmlNodeCollection node = htmlDoc.DocumentNode.SelectNodes("//div[@class='jeg_postblock_content']//h3//a");
                HtmlNodeCollection nodeUp = htmlDoc.DocumentNode.SelectNodes("//div[@class='entry-wrap']//header[@class='entry-header']//h2[@class='entry-title']//a");
                /*<div class="entry-wrap">
<header class="entry-header">
<h2 class="entry-title">*/
                //int ncnt = node.Count;
                int nucnt = nodeUp.Count;
                //MessageBox.Show(ncnt.ToString());
                for (int k = 0; k < nucnt; k++)
                {
                    linkk = nodeUp[k].Attributes["href"];
                    if (!linklistesi.Contains(linkk.Value) && !linklistesi.Contains(nodeUp[k].InnerText))
                    {
                        linklistesi.Add(nodeUp[k].InnerText);
                        linklistesi.Add(linkk.Value);
                    }
                }
                /*for (int j = 0; j < ncnt; j++)
                {
                    linkk = node[j].Attributes["href"];
                    if (!linklistesi.Contains(linkk.Value) && !linklistesi.Contains(node[j].InnerText))
                    {
                        linklistesi.Add(node[j].InnerText);
                        linklistesi.Add(linkk.Value);
                    }
                }*/
            }
            for (int i = linklistesi.Count - 1; i >= 0; i--)
            {
                listBox1.Items.Add(linklistesi[i]);
            }
        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void MbListBoxuTemizle_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void kaydet()
        {
            int sayac = 1;
            int ksayac = 2;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "technopat.html";
            sfd.Filter = "Html Dosyası (*.html)|*.html";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string item = "";
                StreamWriter Kayit = new StreamWriter(sfd.FileName);
                string onsatir = "<DT><H3 ADD_DATE=\"1515536908\" LAST_MODIFIED=\"1531485215\">TP</H3>";
                Kayit.WriteLine(onsatir);
                string satir1 = "<DL><p>";
                Kayit.WriteLine(satir1);
                string satir2 = "<DT><H3 ADD_DATE=\"1531485215\" LAST_MODIFIED=\"1531485215\">TP1</H3>";
                Kayit.WriteLine(satir2);
                string satir3 = "</DL><p>";
                Kayit.WriteLine(satir1);
                for (int i = 0; i < listBox1.Items.Count; i += 2)
                {
                    if (sayac < 10)
                    {
                        item = "<DT><A HREF=\"" + listBox1.Items[i] + "\" ADD_DATE=\"1531485215\">" + listBox1.Items[i + 1] + "</A>";
                        Kayit.WriteLine(item);
                        sayac++;
                    }
                    else
                    {
                        Kayit.WriteLine(satir3);
                        satir2 = "<DT><H3 ADD_DATE=\"1531485215\" LAST_MODIFIED=\"1531485215\">TP" + ksayac + "</H3>";
                        Kayit.WriteLine(satir2);
                        Kayit.WriteLine(satir1);
                        ksayac++;
                        sayac = 0;
                    }
                }
                Kayit.Close();
            }
        }
        private void MbDosyayaKaydet_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                kaydet();
            }
            else
            {
                MessageBox.Show("Listede eklenecek link yok.");
            }
        }

        private void MbListBoxaEkle_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text != "")
            {
                int sayfa = Convert.ToInt32(metroTextBox1.Text);
                ekle(sayfa);
            }
            else
            {
                MessageBox.Show("Lütfen sayfa sayısını giriniz.");
                metroTextBox1.Focus();
            }
        }

        private void TechnoIzle_Load(object sender, EventArgs e)
        {
        }
    }
}
