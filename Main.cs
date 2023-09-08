

using ScottPlot.Plottable;
using System.Drawing.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GtaosPing
{
    public partial class Main : Form
    {

        private bool status = false;
        private double timerCount = 0;
        private Dictionary<string, ScottPlot.Plottable.DataLogger> dic = new();

        public Main()
        {
            InitializeComponent();
            PingViewer.Plot.Title("PINGʵʱͼ��");
            PingViewer.Plot.XLabel("����ʱ�䣨��λ���룩");
            PingViewer.Plot.YLabel("��Ӧʱ�䣨��λ�����룩");
        }

        private void AddNotification(string text)
        {
            DateTime now = DateTime.Now;
            Output.Text = "[" + now.ToString() + "] " + text + "\n" + Output.Text;
        }
        private static bool ValidateIPv4(string ip)
        {
            IPAddress address;
            return ip != null && ip.Count(c => c == '.') == 3 && IPAddress.TryParse(ip, out address);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (!status)
                if (IPBox.TextLength > 0)
                {
                    if (ValidateIPv4(IPBox.Text))
                    {
                        foreach (object item in IPList.Items)
                        {
                            if (item.ToString() == IPBox.Text)
                            {
                                AddNotification("Error: �Ѵ�����ͬ��IP���볢������IP");
                                return;
                            }
                        }
                        IPList.Items.Add(IPBox.Text);
                        AddNotification("Success: ���IP�ɹ�");
                    }
                    else
                    {
                        AddNotification("Error: �������˷Ƿ���IP");
                    }

                }
                else
                {
                    AddNotification("Error: ��û�������κ�IP");
                }
            else
            {
                AddNotification("Error: ���ȹرղ��Ժ������IP");
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (!status)
                if (IPList.SelectedIndex != -1)
                {
                    IPList.Items.Remove(IPList.SelectedItem);

                    AddNotification("Success: ɾ��IP�ɹ�");
                }
                else
                {
                    AddNotification("Error: ����Ҫ����ѡ��һ���б��е�IP");
                }
            else
            {
                AddNotification("Error: ���ȹرղ��Ժ���ɾ��IP");
            }
        }

        private void StartStopTest_Click(object sender, EventArgs e)
        {
            if (status)
            {
                StartStopTest.Text = "��ʼ����";
                status = false;
                AddNotification("Success: �ɹ��ر�PINGֵ����");

                PingTimer.Stop();
            }
            else
            {
                if (IPList.Items.Count > 0)
                {
                    StartStopTest.Text = "�رղ���";
                    status = true;
                    AddNotification("Success: �ɹ�����PINGֵ����");

                    ResetApp();

                    PingTimer.Start();
                }
                else
                {
                    AddNotification("Error: �㻹û������κ�IP");
                }
            }
        }

        private (string, long, int, bool, int) PingIP(string ip)
        {
            Ping ping = new();
            PingReply reply = ping.Send(ip, 1000);
            //MessageBox.Show(reply.Status.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return (reply.Address.ToString(), reply.RoundtripTime, reply.Options.Ttl, reply.Options.DontFragment, reply.Buffer.Length);
        }

        private void PingTimer_Tick(object sender, EventArgs e)
        {
            if (status)
            {
                foreach (object item in IPList.Items)
                {
                    if (item != null)
                    {
                        (string Address, long RoundtripTime, int Ttl, bool DontFragment, int BufferLength) = PingIP(item.ToString());

                        //Dictionary<string, long> innerDic = new();
                        //innerDic.Add(item.ToString(), RoundtripTime);


                        if (dic.ContainsKey(item.ToString()))
                        {
                            dic[item.ToString()].Add(timerCount, RoundtripTime / 2);
                            PingViewer.Refresh();
                        }
                        else
                        {
                            dic.Add(item.ToString(), PingViewer.Plot.AddDataLogger(label: item.ToString()));
                            dic[item.ToString()].Add(timerCount, RoundtripTime / 2);
                            dic[item.ToString()].ViewFull();
                            PingViewer.Refresh();
                        }
                    }
                }
                timerCount += 1;
            }
        }
        private void ResetApp()
        {
            timerCount = 0;
            foreach (var kvp in dic)
            {
                kvp.Value.Clear();
                dic.Remove(kvp.Key);
                PingViewer.Refresh();
            }
        }
    }
}