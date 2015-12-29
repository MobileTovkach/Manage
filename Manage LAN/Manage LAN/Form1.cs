using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

        private void metroButton1_Click(object sender, EventArgs e)
        {
            String roomNumber;
            if (metroRadioButton1.Checked)
            {
                roomNumber = "007";
            }
            else if (metroRadioButton2.Checked)
            {
                roomNumber = "009";
            }
            else if (metroRadioButton3.Checked)
            {
                roomNumber = "203";
            }
            else
            {
                return;
            }
            FillListbox(roomNumber);
        }

        private void FillListbox(string roomNumber)
        {
            listBox1.Items.Clear();
            string path = $"{Directory.GetCurrentDirectory()}\\data\\{roomNumber}.txt";
            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                listBox1.Items.Add(line);
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
    }
}

