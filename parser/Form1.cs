using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace parser
{
    public partial class Form1 : Form
    {
        public Label lb;
        public PictureBox pb;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                string pass = textBox2.Text,
                       log = textBox1.Text;

                driver.Url = @"https://vk.com";
                driver.FindElement(By.Id("index_email")).SendKeys(log);
                driver.FindElement(By.XPath(".//div[@id='index_login']/form/input[@id='index_pass']")).SendKeys(pass);
                driver.FindElement(By.XPath(".//div[@id='index_login']/form/button[@id='index_login_button']")).Click();

                driver.Url = @"https://vk.com/friends";

                var FrList = driver.FindElements(By.XPath(".//div[@class='friends_list_bl']/div"));
                int flag = 0;
                int x = 180, 
                    y = 10;
                foreach (IWebElement friend in FrList)
                {
                    var ff = friend.FindElement(By.CssSelector(".friends_field"));
                    var name = ff.FindElement(By.TagName("a"));
                    var photo = friend.FindElement(By.TagName("img")).GetAttribute("src");



                    this.lb = new Label()
                    {
                        Name = "lbFriend" + flag.ToString(),
                        Location = new Point(x, y + 90),
                        AutoSize = false,
                        Text = name.Text,
                        Tag = name.GetAttribute("href"),
                        Cursor = Cursors.Hand
                    };

                    this.pb = new PictureBox()
                    {
                        Name = "pbFriend" + flag.ToString(),
                        Location = new Point(x, y - 20),
                        ImageLocation = photo,
                        Width = 100,
                        Height = 100,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Tag = name.GetAttribute("href"),
                        Cursor = Cursors.Hand
                    };
                    Controls.Add(this.lb);
                    Controls.Add(this.pb);

                    lb.Click += label_Click;
                    pb.Click += pictureBox_Click;

                    x += 100;
                    flag++;
                    if (flag == 5) break;
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            finally
            {
                driver.Quit();
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                string pass = textBox2.Text,
                          log = textBox1.Text;

                driver.Url = "https://vk.com";
                driver.FindElement(By.Id("index_email")).SendKeys(log);
                driver.FindElement(By.XPath(".//div[@id='index_login']/form/input[@id='index_pass']")).SendKeys(pass);
                driver.FindElement(By.XPath(".//div[@id='index_login']/form/button[@id='index_login_button']")).Click();

                this.lb = (sender as Label);
                driver.Url = this.lb.Tag.ToString();

                var CountModule = driver.FindElement(By.XPath(".//div[@class='counts_module']"));
                var allCounts = CountModule.FindElements(By.ClassName("page_counter"));

                int k = 0;
                List<string> res = new List<string>();
                foreach (IWebElement abc in allCounts)
                {
                    char[] txt = abc.Text.ToCharArray();
                    
                    res.Add("");

                    for(int i = 0; i < txt.Length; i++)
                    {
                        if (txt[i] != '\r' && txt[i] != '\n') res[k] += txt[i].ToString();
                        else break;
                    }

                    k++;
                }
                if (res[0] != "") label1.Text = "Общих друзей: " + res[0];
                else label1.Text = "Общих друзей: 0 (либо данные скрыты)";
                
                if (res[1] != "") label2.Text = "Друзей: " + res[1];
                else label2.Text = "Друзей: 0 (либо данные скрыты)";
                
                if (res[2] != "") label3.Text = "Подписчиков: " + res[2];
                else label3.Text = "Подписчиков: 0 (либо данные скрыты)";
                
                if (res[3] != "") label4.Text = "Фотографий: " + res[3];
                else label4.Text = "Фотографий: 0 (либо данные скрыты)";
                
                if (res[4] != "") label5.Text = "Отметок: " + res[4];
                else label5.Text = "Отметок: 0 (либо данные скрыты)";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            finally
            {
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            try
            {
                string pass = textBox2.Text,
                          log = textBox1.Text;

                driver.Url = "https://vk.com";
                driver.FindElement(By.Id("index_email")).SendKeys(log);
                driver.FindElement(By.XPath(".//div[@id='index_login']/form/input[@id='index_pass']")).SendKeys(pass);
                driver.FindElement(By.XPath(".//div[@id='index_login']/form/button[@id='index_login_button']")).Click();


                this.pb = (sender as PictureBox);
                driver.Url = this.pb.Tag.ToString();

                var CountModule = driver.FindElement(By.XPath(".//div[@class='counts_module']"));
                var allCounts = CountModule.FindElements(By.ClassName("page_counter"));

                int k = 0;
                List<string> res = new List<string>();
                foreach (IWebElement abc in allCounts)
                {
                    char[] txt = abc.Text.ToCharArray();
                    res.Add("");

                    for (int i = 0; i < txt.Length; i++)
                    {
                        if (txt[i] != '\r' && txt[i] != '\n') res[k] += txt[i].ToString();
                        else break;
                    }

                    k++;
                }
                if (res[0] != "") label1.Text = "Общих друзей: " + res[0];
                else label1.Text = "Общих друзей: 0 (либо данные скрыты)";

                if (res[1] != "") label2.Text = "Друзей: " + res[1];
                else label2.Text = "Друзей: 0 (либо данные скрыты)";

                if (res[2] != "") label3.Text = "Подписчиков: " + res[2];
                else label3.Text = "Подписчиков: 0 (либо данные скрыты)";

                if (res[3] != "") label4.Text = "Фотографий: " + res[3];
                else label4.Text = "Фотографий: 0 (либо данные скрыты)";

                if (res[4] != "") label5.Text = "Отметок: " + res[4];
                else label5.Text = "Отметок: 0 (либо данные скрыты)";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            finally
            {
                driver.Quit();
            }
        }
    }
}
