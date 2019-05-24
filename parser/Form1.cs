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
                    var name = ff.FindElement(By.TagName("a")).Text;
                    var photo = friend.FindElement(By.TagName("img")).GetAttribute("src");

                    pictureBox1.ImageLocation = photo;

                    this.Controls.Add(new Label()
                    {
                        Name = "labeel" + flag.ToString(),
                        Location = new Point(x, y),
                        BackColor = Color.Red,
                        AutoSize = true,
                        Text = name
                    });
                    y += 50;
                    flag++;
                    if (flag == 13) break;
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
                driver.Quit();
            }
        }
    }
}
