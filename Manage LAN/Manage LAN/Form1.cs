using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Manage_LAN.Library_Class;
using MetroFramework.Forms;

namespace Manage_LAN
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string RoomNumber;
        public string TopPath;

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (metroRadioButton1.Checked)
            {
                RoomNumber = "mac 007";
            }
            else if (metroRadioButton2.Checked)
            {
                RoomNumber = "mac 009";
            }
            else if (metroRadioButton3.Checked)
            {
                RoomNumber = "mac 203";
            }
            else
            {
                return;
            }
            FillListboxMac(RoomNumber);
        }

        private void FillListboxMac(string roomNumber)
        {
            listBox1.Items.Clear();
            WakeOnLan.ReadFromFile();
            foreach (var line in WakeOnLan.Lines)
            {
                listBox1.Items.Add(line);
            }
        }

        private void FillListBoxIp(string roomNumber)
        {
            listBox2.Items.Clear();
            WakeOnLan.ReadFromFile();
            foreach (var line in WakeOnLan.Lines)
            {
                listBox2.Items.Add(line);
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            // ReSharper disable once UnusedVariable
            foreach (var s in listBox1.Items.Cast<string>().AsParallel().ForEach(macAddress =>
            {
                using (var udpClient = new UdpClient())
                {
                    byte[] mac = WakeOnLan.StrToMac(macAddress: macAddress);
                    if (mac != null)
                    {
                        udpClient.Send(dgram: mac, bytes: mac.Length,
                            endPoint: new IPEndPoint(IPAddress.Broadcast, 9));
                    }
                }
            }))
            {
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {   
            if (metroRadioButton4.Checked)
            {
                RoomNumber = "IP 007";
            }
            else if (metroRadioButton5.Checked)
            {
                RoomNumber = "IP 009";
            }
            else if (metroRadioButton6.Checked)
            {
                RoomNumber = "IP 203";
            }
            else
            {
                return;
            }
            FillListBoxIp(RoomNumber);
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            TopPath = metroTextBox1.Text;
            DeletePath.DelPath(TopPath);
            metroLabel5.ForeColor = Color.Red;
            metroLabel5.Text = DeletePath.ErrDic.Item2;
            if (DeletePath.ErrDic.Item1 == 0)
            {
                metroLabel5.ForeColor = Color.Green;
                metroLabel5.Text = DeletePath.ErrDic.Item2;
            }
            Thread.Sleep(2000);
        }
    }
}

