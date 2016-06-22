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
    public partial class FormTerminal : Form
    {
        FormLogin formLogin = new FormLogin();
        private SQLDeal sqlDeal;
        public FormTerminal()
        {
            InitializeComponent();
            sqlDeal = new SQLDeal();
        }

        private void textInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)    //回车按下
            {
                string comstr = textInput.Text;
                string[] com_ele = comstr.Split(' ');
                if (com_ele[0].Equals("select"))    //单独处理查询
                {
                    List<string[]> allracord = sqlDeal.dealSelect(comstr);
                    DataTable dt = new DataTable();
                    List<string> col_name = new BaseCommand().getColName(com_ele[3]);
                    for (int i=0;i<col_name.Count;i++)//添加列
                        dt.Columns.Add(col_name[i], typeof(string));

                    for (int i=0;i<allracord.Count;i++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int j=0;j<allracord[i].Length;j++)
                        {
                            dr[j] = allracord[i][j];
                        }
                        dt.Rows.Add(dr);
                    }
                    dataGridView.DataSource = dt;
                }
                else
                {
                    string result;
                    result = sqlDeal.dealTerminal(textInput.Text);
                    textOutput.AppendText(">" + textInput.Text + ";\r\n");
                    textInput.Clear();
                    textOutput.AppendText(result + "\r\n");
                }
            }
        }

        private void textInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormTerminal_Load(object sender, EventArgs e)
        {
            formLogin.ShowDialog(this);
        }
    }
}
