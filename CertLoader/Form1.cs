using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CertLoader
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width, 35);

            loginField.Text = "Введите логин";
            loginField.ForeColor = Color.Gray;

            passField.Text = "Введите пароль";
            passField.ForeColor = Color.Gray;
            passField.UseSystemPasswordChar = false;            
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {

            if (DB.server == "") { DB.server = "localhost"; }
            if (DB.ftpserver == "") { DB.ftpserver = "193.169.88.2"; }
            if (DB.port == "") { DB.port = "3306"; }
            if (DB.username == "") { DB.username = "root"; }
            if (DB.password == "") { DB.password = "root"; }
            if (DB.database == "") { DB.database = "orgdb"; }
            if (DB.sshuser == "") { DB.sshuser = "admin"; }
            if (DB.sshpass == "") { DB.sshpass = "admin"; }
            if (DB.address == "") { DB.address = "193.169.88.2"; }
            if (Array.Exists(DB.ports, element => element == "")) { DB.ports[0] = "2233"; DB.ports[1] = "1011"; DB.ports[2] = "459"; }
            if (DB.pathRemoteFile == "") { DB.pathRemoteFile = "/ClientCert/cert_export_Client@193.169.88.2.p12"; }

            DB db = new DB();
            
            String loginUser = loginField.Text;
            String passUser = passField.Text;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

            db.openConnection();

            try
            {
                adapter.SelectCommand = command;
                adapter.Fill(table);
                db.closeConnection();
            }
            catch (Exception er)
            {
                MessageBox.Show("An exception has been caught " + er.ToString());
            }

            if (table.Rows.Count > 0)
            { 
                db.downloadFile();
                string checkFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cert_export_Client@193.169.88.2.p12");
                MessageBox.Show(File.Exists(checkFile) ? "Сертификат успешно скачан на рабочий стол" : "Не удалось скачать сертификат");
            
            }
            else
                MessageBox.Show("Не удалось авторизоваться");            
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Введите логин")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
        }

        private void LoginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.Text = "Введите логин";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void PassField_Enter(object sender, EventArgs e)
        {
            if (passField.Text == "Введите пароль")
            {
                passField.Text = "";
                passField.ForeColor = Color.Black;
                passField.UseSystemPasswordChar = true;
            }
        }

        private void PassField_Leave(object sender, EventArgs e)
        {
            if (passField.Text == "")
            {
                passField.Text = "Введите пароль";
                passField.ForeColor = Color.Gray;
                passField.UseSystemPasswordChar = false;
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }
    }
}
