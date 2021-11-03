using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertLoader
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            DB.server = textBox1.Text;
            DB.port = textBox2.Text;
            DB.username = textBox3.Text;
            DB.password = textBox4.Text;
            DB.database = textBox5.Text;
            DB.pathRemoteFile = textBox6.Text;
            DB.sshuser = textBox7.Text;
            DB.sshpass = textBox8.Text;
            DB.ftpserver = textBox9.Text;
            this.Close();
        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            DB.ports = textBox10.Text.Split(' ');
            DB.address = textBox11.Text;
            db.portKnocking();
        }
    }
}
