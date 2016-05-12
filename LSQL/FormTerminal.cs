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
                string result;
                result = sqlDeal.dealTerminal(textInput.Text);
                textInput.Clear();
                textOutput.AppendText(result+"\n");
            }
        }

        private void textInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
