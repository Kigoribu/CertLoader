using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace CertLoader
{
    class DB
    {
        public static string server = "193.169.88.2";
        public static string ftpserver = "193.169.88.2";
        public static string port = "3306";
        public static string username = "root";
        public static string password = "root";
        public static string database = "orgdb";
        public static string sshuser = "admin";
        public static string sshpass = "admin";
        public static string pathRemoteFile = "/ClientCert/cert_export_Client@193.169.88.2.p12";
        public static string[] ports;
        public static string address = "193.169.88.2";

        MySqlConnection connection = new MySqlConnection($"server={server};port={port};username={username};password={password};database={database}");

        public object Messagebox { get; private set; }

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public void downloadFile()
        {

            string pathLocalFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cert_export_Client@193.169.88.2.p12");

            using (SftpClient sftp = new SftpClient(ftpserver, sshuser, sshpass))
            {
                try
                {
                    sftp.Connect();

                    using (Stream fileStream = File.OpenWrite(pathLocalFile))
                    {
                        sftp.DownloadFile(pathRemoteFile, fileStream);
                    }

                    sftp.Disconnect();
                }
                catch (Exception er)
                {
                    Console.WriteLine("An exception has been caught " + er.ToString());
                }
            }
        }

        public void portKnocking()
        {
                   int n = 0;
                   int[] portfork = new int[ports.Length];
                   foreach (var singleport in ports)
                   {
                        portfork[n] = Int32.Parse(singleport);
                        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        IPAddress serverAddr = IPAddress.Parse($"{address}");
                        IPEndPoint endPoint = new IPEndPoint(serverAddr, portfork[n]);
                        string text = " ";
                        byte[] send_buffer = Encoding.ASCII.GetBytes(text);
                        sock.SendTo(send_buffer, endPoint);
                   }
        }
    }
}
