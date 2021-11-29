using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegExp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {            
            if (Regex.valid(textRegExp.Text))
                MessageBox.Show("Regular expration is valid");
            else
                MessageBox.Show("Regular expration is invalid");            
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            List<string> mtch = Regex.Match(textToCheck.Text);
            StringBuilder sb = new StringBuilder();
            foreach(string str in mtch) {
                sb.Append(str + "\n");
            }
            richTextBox1.Text = sb.ToString();
            Recolor(mtch);
            sb.Clear();
        }

        public void Recolor(List<string> matches)
        {
            string txt = textToCheck.Text;
            foreach(string str in matches)
            {
                int k = txt.IndexOf(str, 0);
                while (k > -1)
                {
                    textToCheck.SelectionStart = k;
                    textToCheck.SelectionLength = str.Length;
                    textToCheck.SelectionBackColor = Color.FromArgb(227, 37, 107);

                    k = txt.IndexOf(str, k + str.Length); 
                }
            }
        }
    }
}
