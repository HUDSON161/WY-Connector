using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WY_Connector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static bool axMsRdpClient7NotSafeForScriptingIsFree=true;
        public static AxMSTSCLib.AxMsRdpClient7NotSafeForScripting rdp;
        public static int VariantCurent = -1;
        Thread tn;
        Thread rt;
        private static bool bEnableLoL;

        public static ComboBox AL;
        public static ComboBox LN;
        public static ComboBox CS;

        private void Form1_Load(object sender, EventArgs e)
        {
        AL=comboBox1;
        LN = comboBox2;
        CS = comboBox3;
        rdp = axMsRdpClient7NotSafeForScripting1;
            if (checkBox1.Checked == true)
            {
                bEnableLoL = true;
            }
            if (checkBox1.Checked == false)
            {
                bEnableLoL = false;
            }

            rt = new Thread(delegate ()
            {
                while (true)
                {
                    while (bEnableLoL == true)
                    {
                        if (tn != null)
                        {
                            try
                            {
                                tn.Abort();
                            }
                            catch
                            {

                            }
                        }

                        if (rdp.Connected != 1)
                        {
                            try
                            {
                                rdp.Disconnect();
                                Thread.Sleep(1000);
                            }
                            catch
                            {

                            }
                            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                            sw.Start();
                            NextBack.Reconnect(ref textBox1, ref textBox4, ref textBox5, ref textBox2, ref textBox3);
                            while (Form1.rdp.Connected == 2 && sw.ElapsedMilliseconds < Convert.ToInt32(textBox4.Text))
                            {
                                Thread.Sleep(Convert.ToInt32(textBox4.Text));
                            }
                        }
                        else
                        {

                        }
                    }
                    //MessageBox.Show("sgs");
                    Thread.Sleep(1000);
                }
            });
            rt.Start();
            //MessageBox.Show(rdp.AdvancedSettings8.EnableWindowsKey.ToString());
            //MessageBox.Show(rdp.AdvancedSettings8.HotKeyCtrlAltDel.ToString());
            //rdp.AdvancedSettings8.HotKeyCtrlAltDel=1;
            //rdp.AdvancedSettings8.RedirectClipboard = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tn != null)
            {
                try
                {
                    tn.Abort();
                }
                catch
                {

                }
            }
            tn = new Thread(delegate ()
            {
                try
                {
                    rdp.Disconnect();
                    Thread.Sleep(1000);
                }
                catch
                {

                }
                NextBack.Next(ref textBox1, ref textBox4, ref textBox5, ref textBox2, ref textBox3);
                tn.Abort();
            });
            tn.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tn != null)
            {
                try
                {
                    tn.Abort();
                }
                catch
                {

                }
            }
            tn = new Thread(delegate ()
            {
                try
                {
                    rdp.Disconnect();
                    Thread.Sleep(1000);
                }
                catch
                {

                }
                NextBack.Back(ref textBox1, ref textBox4, ref textBox5, ref textBox2, ref textBox3);
                tn.Abort();
            });
            tn.Start();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    rdp.Disconnect();
                    Thread.Sleep(1000);
                }
                catch
                {

                }
            }
            catch (Exception Ex)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox6.Text = textBox1.Lines.Length.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tn != null)
            {
                try
                {
                    tn.Abort();
                }
                catch
                {

                }
            }
            tn = new Thread(delegate ()
            {
                try
                {
                    rdp.Disconnect();
                    Thread.Sleep(1000);
                }
                catch
                {

                }
                NextBack.Connect(ref textBox7, ref textBox4, ref textBox5, ref textBox2, ref textBox3);
                tn.Abort();
            });
            tn.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tn != null)
            {
                try
                {
                    tn.Abort();
                }
                catch
                {

                }
            }
            tn = new Thread(delegate ()
            {
                try
                {
                    rdp.Disconnect();
                    Thread.Sleep(1000);
                }
                catch
                {

                }
                NextBack.Jump(ref textBox1, ref textBox4, ref textBox5, ref textBox2, ref textBox3,ref textBox8);
                tn.Abort();
            });
            tn.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (tn != null)
            {
                try
                {
                    tn.Abort();
                }
                catch
                {

                }
            }
            tn = new Thread(delegate ()
            {
                try
                {
                    rdp.Disconnect();
                    Thread.Sleep(1000);
                }
                catch
                {

                }
                NextBack.Reconnect(ref textBox1, ref textBox4, ref textBox5, ref textBox2, ref textBox3);
                tn.Abort();
            });
            tn.Start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ( checkBox1.Checked == true )
            {
                bEnableLoL = true;
            }
            if (checkBox1.Checked == false)
            {
                bEnableLoL = false;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ( rt != null )
            {
                try
                {
                    rt.Abort();
                }
                catch
                {

                }
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            rdp.RemoteProgram.ServerStartProgram(@"%SYSTEMROOT%\notepad.exe", "", "%SYSTEMROOT%", true, "", true);
        }
    }
}
