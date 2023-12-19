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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.IO;

namespace NCT060810
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        string Conn = "Server=localhost;Database=iot;Uid=root;Pwd=1234;";

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

                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();

                    MySqlCommand msc = new MySqlCommand($"insert into light(ledname,onoff,date) values('light1',1,'{DateTime.Now.ToString()}');", conn);
                    msc.ExecuteNonQuery();

                }

            }
            else
            {
                //on -> off
                client.Publish("bssm_iot/livingroom/light1", Encoding.UTF8.GetBytes("0"), 0, false);

                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();

                    MySqlCommand msc = new MySqlCommand($"insert into light(ledname,onoff,date) values('light1',0,'{DateTime.Now.ToString()}');", conn);
                    msc.ExecuteNonQuery();

                }
            }
        }

        //ROOM2
        private void metroToggle2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (metroToggle2.Checked)
            {
                //off -> on
                client.Publish("bssm_iot/livingroom/light2", Encoding.UTF8.GetBytes("1"), 0, false);

                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();

                    MySqlCommand msc = new MySqlCommand($"insert into light(ledname,onoff,date) values('light2',1,'{DateTime.Now.ToString()}');", conn);
                    msc.ExecuteNonQuery();

                }
            }
            else
            {
                //on -> off
                client.Publish("bssm_iot/livingroom/light2", Encoding.UTF8.GetBytes("0"), 0, false);
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();

                    MySqlCommand msc = new MySqlCommand($"insert into light(ledname,onoff,date) values('light2',0,'{DateTime.Now.ToString()}');", conn);
                    msc.ExecuteNonQuery();

                }

            }
        }






        private void metroPanel3_Paint(object sender, PaintEventArgs e)
        {
            
        }


        private void metroPanel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void metroPanel7_Paint(object sender, PaintEventArgs e)
        {
            
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * FROM light where ledname='light1';";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, "student");

                int cnt = ds.Tables[0].Rows.Count;

                chart1.Series[0].Points.Clear();
                //chart1.Series[1].Points.Clear();
                //chart1.Series[2].Points.Clear();
                //chart1.Series[3].Points.Clear();

                for (int i = 0; i < cnt; i++)
                {
                    chart1.Series[0].Points.AddY(int.Parse(ds.Tables[0].Rows[i]["onoff"].ToString()));
                }
            }
        }

       

        private void metroToggle7_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle7.Checked)
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

        private void metroToggle4_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle4.Checked)
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

        private void metroToggle3_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle3.Checked)
            {
                //off -> on
                client.Publish("bssm_iot/livingroom/light4", Encoding.UTF8.GetBytes("1"), 0, false);

                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();

                    MySqlCommand msc = new MySqlCommand($"insert into light(ledname,onoff,date) values('light3',1,'{DateTime.Now.ToString()}');", conn);
                    msc.ExecuteNonQuery();

                }

            }
            else
            {
                //on -> off
                client.Publish("bssm_iot/livingroom/light4", Encoding.UTF8.GetBytes("0"), 0, false);

                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();

                    MySqlCommand msc = new MySqlCommand($"insert into light(ledname,onoff,date) values('light3',0,'{DateTime.Now.ToString()}');", conn);
                    msc.ExecuteNonQuery();

                }

            }
        }

        private void metroToggle6_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle6.Checked)
            {
                //off -> on
                client.Publish("bssm_iot/livingroom/light3", Encoding.UTF8.GetBytes("1"), 0, false);

                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();

                    MySqlCommand msc = new MySqlCommand($"insert into light(ledname,onoff,date) values('light4',1,'{DateTime.Now.ToString()}');", conn);
                    msc.ExecuteNonQuery();

                }

            }
            else
            {
                //on -> off
                client.Publish("bssm_iot/livingroom/light3", Encoding.UTF8.GetBytes("0"), 0, false);

                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();

                    MySqlCommand msc = new MySqlCommand($"insert into light(ledname,onoff,date) values('light4',0,'{DateTime.Now.ToString()}');", conn);
                    msc.ExecuteNonQuery();

                }

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * FROM light where ledname='light2';";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, "student");

                int cnt = ds.Tables[0].Rows.Count;

                chart2.Series[0].Points.Clear();
                //chart1.Series[1].Points.Clear();
                //chart1.Series[2].Points.Clear();
                //chart1.Series[3].Points.Clear();

                for (int i = 0; i < cnt; i++)
                {
                    chart2.Series[0].Points.AddY(int.Parse(ds.Tables[0].Rows[i]["onoff"].ToString()));
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * FROM light where ledname='light3';";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, "student");

                int cnt = ds.Tables[0].Rows.Count;

                chart3.Series[0].Points.Clear();
                //chart1.Series[1].Points.Clear();
                //chart1.Series[2].Points.Clear();
                //chart1.Series[3].Points.Clear();

                for (int i = 0; i < cnt; i++)
                {
                    chart3.Series[0].Points.AddY(int.Parse(ds.Tables[0].Rows[i]["onoff"].ToString()));
                }
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                DataSet ds = new DataSet();
                string sql = "SELECT * FROM light where ledname='light4';";
                MySqlDataAdapter adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, "student");

                int cnt = ds.Tables[0].Rows.Count;

                chart4.Series[0].Points.Clear();
                //chart1.Series[1].Points.Clear();
                //chart1.Series[2].Points.Clear();
                //chart1.Series[3].Points.Clear();

                for (int i = 0; i < cnt; i++)
                {
                    chart4.Series[0].Points.AddY(int.Parse(ds.Tables[0].Rows[i]["onoff"].ToString()));
                }
            }
        }
    }
}