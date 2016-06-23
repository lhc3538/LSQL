using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSQL
{
    public partial class FormLogin : Form
    {
        private bool hadLogin = false;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!hadLogin)
                System.Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BaseCommand basecom = new BaseCommand();
            ComSelect comselect = new ComSelect();
            string user_str = textBox1.Text;
            string pwd_str = textBox2.Text;

            basecom.useUser("admin");
            basecom.useDatabase("config");
            DataTable dt = comselect.dealCom("select password from user where username = "+ user_str);
            if (dt.Rows.Count == 0)
                MessageBox.Show("用户不存在");
            else
            {
                if (dt.Rows[0]["password"].Equals(pwd_str))
                {
                    hadLogin = true;
                    this.Visible = false;
                    basecom.useUser(user_str);
                }
                else
                    MessageBox.Show("密码错误");
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                this.button1_Click(sender, e);//触发button事件  
            }
        }
    }
}
