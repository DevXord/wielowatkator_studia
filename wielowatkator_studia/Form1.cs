using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wielowatkator_studia
{
    public partial class Form1 : Form
    {
        List<int> listINT = new List<int>();
        List<string> listStrings = new List<string>();
        Random rd = new Random();
        bool isAsyncRun = false;
        private void SendProgramMessage(char type, string message)
        {
            switch (type)
            {
                case 'e':
                case 'E':
                    error_lb.ForeColor = Color.DarkRed;
                    error_lb.Text = "Error: " + message;
                    break;
                case 'c':
                case 'C':
                    error_lb.ForeColor = Color.DarkRed;
                    error_lb.Text = "";
                    break;
            }
        }
        private void ClearAll()
        {
            foreach (Control x in this.Controls)
            {
                if (x is Label)
                    x.Text = "";
                if (x is TextBox)
                    x.Text = "";
            }
            label1.Text = "Number cunt";
            label2.Text = "Send";
            a_lb.Text = "a";
            b_lb.Text = "b";
            c_lb.Text = "c";
            isAsyncRun = false;
            thread_4_bt2.Visible = false;
            thread_4_pb1.Visible = false;
            thread_4_pb2.Visible = false;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private async void thread_1_bt_Click(object sender, EventArgs e)
        {
            SendProgramMessage('c', "");
            string numberFromTB = thread_1_tb.Text;
            int number;
            bool parse = Int32.TryParse(numberFromTB, out number);
            if (parse)
            {
                thread_1_bt.Enabled = false;
                await Task.Run(() =>
                {
                    thread_1_pb.Invoke(new Action(() =>
                    {
                        thread_1_pb.Visible = true;
                        thread_1_pb.Maximum = number;
                    }));
                    for (int i = 1; i <= number; i++)
                    {
                        thread_1_pb.Invoke(new Action(() =>
                        {
                            thread_1_pb.Value = i;
                        }));
                        thread_1_lb.Invoke(new Action(() =>
                        {
                            thread_1_lb.Text = "Added elements to the list";
                        }));
                        listINT.Add(i);
                    }
                    thread_1_lb.Invoke(new Action(() =>
                    {
                        thread_1_lb.Text = "Finished adding";
                    }));
                    thread_1_pb.Invoke(new Action(() =>
                    {
                        thread_1_pb.Visible = false;
                    }));
                    Thread.Sleep(2000);

                });
                thread_1_bt.Enabled = true;
            }
            else
                SendProgramMessage('e', "Enter number in the Number cunt field!");
        }

        private async void thread_2_bt_Click(object sender, EventArgs e)
        {
            if (thread_2_tb.Text != "")
            {
                SendProgramMessage('c', "");
                thread_2_bt.Enabled = false;
                await Task.Run(() =>
                {
                    listStrings.Add(thread_2_tb.Text);
                    thread_2_lb.Invoke(new Action(() =>
                    {
                        thread_2_lb.Text = "Added to list text: " + thread_2_tb.Text;
                    }));
                    Thread.Sleep(5000);
                    thread_2_lb.Invoke(new Action(() =>
                    {
                        thread_2_lb.Text = "The contents of the list: " + Environment.NewLine;
                    }));
                    int u = 0;
                    foreach (var item in listStrings)
                    {
                        if (u % 4 == 0)
                        {
                            thread_2_lb.Invoke(new Action(() =>
                            {
                                thread_2_lb.Text += Environment.NewLine;
                            }));
                        }
                        thread_2_lb.Invoke(new Action(() =>
                        {
                            thread_2_lb.Text += item + " ";
                        }));
                        u++;
                    }
                });
                thread_2_bt.Enabled = true;
            }
            else
                SendProgramMessage('e', "Enter text in the Send field!");
        }

        private async void thread_3_bt_Click(object sender, EventArgs e)
        {
            SendProgramMessage('c', "");
            double a, b, c, delta;
            double a2 = 1, b2 = 1, c2 = 1;
            string astr = thread_3_a_tb.Text, bstr = thread_3_b_tb.Text, cstr = thread_3_c_tb.Text;

            if (a_chb.Checked == true)
                a2 = -1;
            if (b_chb.Checked == true)
                b2 = -1;
            if (c_chb.Checked == true)
                c2 = -1;

            if (astr.Contains('.'))
                astr = astr.Replace('.', ',');

            if (bstr.Contains('.'))
                bstr = bstr.Replace('.', ',');

            if (cstr.Contains('.'))
                cstr = cstr.Replace('.', ',');
            bool parseA = Double.TryParse(astr, out a), parseB = Double.TryParse(bstr, out b), parse = Double.TryParse(cstr, out c);

            if (parseA && parseB && parse && b != 0 && a != 0)
            {
                a = a * a2;
                b = b * b2;
                c = c * c2;
                thread_3_bt.Enabled = false;
                await Task.Run(() =>
                {
                    delta = (b * b) - 4 * a * c;
                    Thread.Sleep(2000);
                    thread_3_lb.Invoke(new Action(() =>
                    {
                        string formatingString = "f(x) = " + a + "x\u00B2" + ((b < 0) ? " " + b + "x" : " + " + b + "x") + ((c < 0) ? " " + c : " + " + c);
                        thread_3_lb.Text = formatingString + Environment.NewLine;
                    }));
                    Thread.Sleep(2000);
                    thread_3_lb.Invoke(new Action(() => { thread_3_lb.Text += "Delta = " + delta + Environment.NewLine; }));
                    Thread.Sleep(2000);
                    if (delta > 0)
                    {
                        double x1, x2;
                        x1 = (((-b) - Math.Sqrt(delta)) / (2 * a));
                        x2 = (((-b) + Math.Sqrt(delta)) / (2 * a));
                        Thread.Sleep(2000);
                        thread_3_lb.Invoke(new Action(() => { thread_3_lb.Text += "x1 = " + Math.Round(x1, 2) + Environment.NewLine + "x2 = " + Math.Round(x2, 2); }));
                    }
                    else if (delta == 0)
                    {
                        double x1;
                        x1 = ((-b) / (2 * a));
                        thread_3_lb.Invoke(new Action(() => { thread_3_lb.Text += "x1 = " + Math.Round(x1, 2); }));
                    }
                    else
                        thread_3_lb.Invoke(new Action(() => { thread_3_lb.Text += "Is negative";  }));
                });
                thread_3_bt.Enabled = true;
            }
            else
                SendProgramMessage('e', "Field a, b or c is invalid or empty!");

        }

        private async void thread_4_bt_Click(object sender, EventArgs e)
        {
            SendProgramMessage('c', "");
            isAsyncRun = true;
            thread_4_bt2.Visible = true;
            thread_4_pb1.Visible = true;
            thread_4_pb2.Visible = true;
            thread_4_bt.Enabled = false;

            await Task.Run(() =>
             {
                 while (isAsyncRun)
                 {
                     foreach (Control x in this.Controls)
                     {
                         if (x is PictureBox)
                         {
                             if ((string)x.Tag == "platform")
                             {

                                 int rand = rd.Next(10, 15);
                                 if (rd.Next(0, 2) == 1)
                                     rand = -rand;

                                 if (rd.Next(0, 2) == 1)
                                     if (thread_4_pb2.Top - rand >= 1 && thread_4_pb2.Top - rand <= 55)
                                         thread_4_pb2.Invoke(new Action(() => { thread_4_pb2.Top -= rand; }));
                                 else
                                     if (thread_4_pb2.Left - rand >= 1 && thread_4_pb2.Left - rand <= 185)
                                         thread_4_pb2.Invoke(new Action(() => { thread_4_pb2.Left -= rand; }));
                                 Thread.Sleep(100);
                             }
                         }
                     }
                 }
             });
            thread_4_bt.Enabled = true;
        }
        private void thread_4_bt2_Click(object sender, EventArgs e)
        {
            isAsyncRun = false;
            thread_4_bt2.Visible = false;
            thread_4_pb1.Visible = false;
            thread_4_pb2.Visible = false;
        }

        private void clear_bt_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread_4_pb2.Parent = thread_4_pb1;
            thread_4_pb2.Location =
                    new Point(
                         thread_4_pb2.Location.X
                        - thread_4_pb1.Location.X,
                         thread_4_pb2.Location.Y
                        - thread_4_pb1.Location.Y);
        }
    }
}
