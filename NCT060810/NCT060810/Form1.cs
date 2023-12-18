using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json.Linq;

namespace NCT060810
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        MqttClient client;
        string clientId;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string BrokerAddress = "broker.mqtt-dashboard.com";
            client = new MqttClient(BrokerAddress);

            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            string[] mytopic =
           {
                 "bssm_iot/livingroom/rfid"
            };

            byte[] myqos =
            {
                0

            };
            client.Subscribe(mytopic, myqos);

        }
        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            metroLabel6.Text = ReceivedMessage;

            string message = System.Text.Encoding.UTF8.GetString(e.Message);

            int value = (message == "On") ? 1 : 0;

            this.Invoke(new Action(() =>
            {
                chart1.Series[0].Points.AddY(value);
            }));

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.Disconnect();
        }
       


        //ROOM1
        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle1.Checked)
            {
                //off -> on
                client.Publish("bssm_iot/livingroom/light1", Encoding.UTF8.GetBytes("1"), 0, false);

            }
            else
            {
                //on -> off
                client.Publish("bssm_iot/livingroom/light1", Encoding.UTF8.GetBytes("0"), 0, false);

            }
        }

        //ROOM2
        private void metroToggle2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (metroToggle2.Checked)
            {
                //off -> on
                client.Publish("bssm_iot/livingroom/light2", Encoding.UTF8.GetBytes("1"), 0, false);

            }
            else
            {
                //on -> off
                client.Publish("bssm_iot/livingroom/light2", Encoding.UTF8.GetBytes("0"), 0, false);

            }
        }






        private void metroPanel3_Paint(object sender, PaintEventArgs e)
        {
            if (metroToggle1.Checked)
            {
                //off -> on
                client.Publish("bssm_iot/livingroom/light4", Encoding.UTF8.GetBytes("1"), 0, false);

            }
            else
            {
                //on -> off
                client.Publish("bssm_iot/livingroom/light4", Encoding.UTF8.GetBytes("0"), 0, false);

            }
        }

        private void metroPanel5_Paint(object sender, PaintEventArgs e)
        {
            if (metroToggle1.Checked)
            {
                //off -> on
                client.Publish("bssm_iot/livingroom/humi", Encoding.UTF8.GetBytes("1"), 0, false);

            }
            else
            {
                //on -> off
                client.Publish("bssm_iot/livingroom/humi", Encoding.UTF8.GetBytes("0"), 0, false);

            }
        }

        private void metroPanel6_Paint(object sender, PaintEventArgs e)
        {
            if (metroToggle1.Checked)
            {
                //off -> on
                client.Publish("bssm_iot/livingroom/light3", Encoding.UTF8.GetBytes("1"), 0, false);

            }
            else
            {
                //on -> off
                client.Publish("bssm_iot/livingroom/light3", Encoding.UTF8.GetBytes("0"), 0, false);

            }
        }

        private void metroPanel7_Paint(object sender, PaintEventArgs e)
        {
            if (metroToggle1.Checked)
            {
                //off -> on
                client.Publish("bssm_iot/livingroom/fan", Encoding.UTF8.GetBytes("1"), 0, false);

            }
            else
            {
                //on -> off
                client.Publish("bssm_iot/livingroom/fan", Encoding.UTF8.GetBytes("0"), 0, false);

            }
        }

    }
}
