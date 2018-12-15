using HtmlAgilityPack;
using MetroFramework.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;

namespace Technopat
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        string url = "https://www.technopat.net/video/";
        IWebDriver drivertp = new EdgeDriver();

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            drivertp.Navigate().GoToUrl(url);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            ReadOnlyCollection<IWebElement> lists1 = drivertp.FindElements(By.CssSelector("#td-outer-wrap > div > div.td-container.td-category-container > div > div > div.td-pb-span8.td-main-content > div > div > div.meta-info-container > div.meta-info > h3 > a"));

            for (int i = 0; i < lists1.Count; i++)
            {
                listBox1.Items.Add(lists1[i].Text);
                listBox1.Items.Add(lists1[i].GetAttribute("href"));
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            IWebElement tplink = drivertp.FindElement(By.ClassName("td-icon-menu-right"));
            tplink.Click();
            //MessageBox.Show(tplink.GetAttribute("href"));
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            int sayac = 1;
            int ksayac = 2;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.CreatePrompt = true;
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
                for (int i = 0; i < listBox2.Items.Count; i += 2)
                {
                    if (sayac < 10)
                    {
                        item = "<DT><A HREF=\"" + listBox2.Items[i] + "\" ADD_DATE=\"1531485215\">" + listBox2.Items[i + 1] + "</A>";
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

        private void ekle(string link, int iter)
        {
            HtmlAttribute linkk;
            string linkiter;
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

                HtmlNodeCollection node = htmlDoc.DocumentNode.SelectNodes("//div[@class='meta-info']//h3//a");
                int ncnt = node.Count;
                //MessageBox.Show(ncnt.ToString());
                for (int j = 0; j < ncnt; j++)
                {
                    linkk = node[j].Attributes["href"];
                    if (!listBox1.Items.Contains(linkk.Value) && !listBox1.Items.Contains(node[j].InnerText))
                    {
                        listBox1.Items.Add(node[j].InnerText);
                        listBox1.Items.Add(linkk.Value);
                    }
                }              
            }
            for (int i= listBox1.Items.Count-1;i>=0;i--)
            {
                listBox2.Items.Add(listBox1.Items[i]);
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            string link = "https://www.technopat.net/video/page/";
            //string son = "https://www.technopat.net/2018/07/12/cooler-master-mm530-rgb-oyuncu-faresi-incelemesi/";
            ekle(link, 8);
        }
    }
}
