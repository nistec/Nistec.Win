using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SyntaxEditorTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] words=new string[]{"aaa","bbb","ccc","abc","aabc","aaabc"};
            this.ctlSyntaxEditor1.AddWords(words, MControl.SyntaxEditor.MemberType.Member, false);
           
            //this.ctlSyntaxEditor1.ReservedWords


            //ctlSyntaxEditor1.WordList.AddRange(MControl.SyntaxEditor.CSWords.GetCSWords());// = MControl.SyntaxEditor.CSWords.GetCSCatalog();

            //Application.StartupPath + @"\Snippets.xml");

            //ctlSyntaxEditor1.LoadReservedWords(Application.StartupPath + @"\Snippets.xml", "snippets");
        }
    }
}