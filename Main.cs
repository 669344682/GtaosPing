

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
            PingViewer.Plot.Title("PING实时图表");
            PingViewer.Plot.XLabel("测试时间（单位：秒）");
            PingViewer.Plot.YLabel("响应时间（单位：毫秒）");
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
                                AddNotification("Error: 已存在相同的IP，请尝试其他IP");
                                return;
                            }
                        }
                        IPList.Items.Add(IPBox.Text);
                        AddNotification("Success: 添加IP成功");
                    }
                    else
                    {
                        AddNotification("Error: 你输入了非法的IP");
                    }

                }
                else
                {
                    AddNotification("Error: 你没有输入任何IP");
                }
            else
            {
                AddNotification("Error: 请先关闭测试后再添加IP");
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (!status)
                if (IPList.SelectedIndex != -1)
                {
                    IPList.Items.Remove(IPList.SelectedItem);

                    AddNotification("Success: 删除IP成功");
                }
                else
                {
                    AddNotification("Error: 你需要首先选择一个列表中的IP");
                }
            else
            {
                AddNotification("Error: 请先关闭测试后再删除IP");
            }
        }

        private void StartStopTest_Click(object sender, EventArgs e)
        {
            if (status)
            {
                StartStopTest.Text = "开始测试";
                status = false;
                AddNotification("Success: 成功关闭PING值测试");

                PingTimer.Stop();
            }
            else
            {
                if (IPList.Items.Count > 0)
                {
                    StartStopTest.Text = "关闭测试";
                    status = true;
                    AddNotification("Success: 成功开启PING值测试");

                    ResetApp();

                    PingTimer.Start();
                }
                else
                {
                    AddNotification("Error: 你还没有添加任何IP");
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