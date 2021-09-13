using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WY_Connector
{
    class NextBack
    {
        public static void Connect(ref TextBox TBTh, ref TextBox TBT, ref TextBox TBV, ref TextBox TBCM, ref TextBox TBSt)
        {
            string Tt = Filter.CleanMask(TBT.Text, "0123456789");
            if (Tt != "")
            {
                if (Form1.axMsRdpClient7NotSafeForScriptingIsFree == true)
                {
                    TBSt.Text = "none";
                    TBV.Text = "User Value";
                    TBCM.Text = TBTh.Text;
                    WYConnect(TBTh.Text, Convert.ToInt32(Tt), ref TBSt);
                }
            }
        }

        public static void Back(ref TextBox TB1, ref TextBox TBT, ref TextBox TBV, ref TextBox TBCM, ref TextBox TBSt)
        {
            if (Form1.VariantCurent > 0)
            {
                string Tt = Filter.CleanMask(TBT.Text, "0123456789");
                if (Tt != "")
                {
                    if (Form1.axMsRdpClient7NotSafeForScriptingIsFree == true)
                    {
                        TBSt.Text = "none";
                        if (Form1.VariantCurent > 0)
                        {
                            Form1.VariantCurent = Form1.VariantCurent - 1;
                        }
                        else
                        {
                            Form1.VariantCurent = 0;
                        }
                        TBV.Text = Form1.VariantCurent.ToString();
                        TBCM.Text = TB1.Lines[Form1.VariantCurent];
                        WYConnect(TB1.Lines[Form1.VariantCurent], Convert.ToInt32(Tt), ref TBSt);
                    }
                }
            }

        }

        public static void Next(ref TextBox TB1, ref TextBox TBT,ref TextBox TBV, ref TextBox TBCM, ref TextBox TBSt)
        {
            string Tt = Filter.CleanMask(TBT.Text, "0123456789");
            if (Tt != "")
            {
                if (Form1.VariantCurent < TB1.Lines.Length)
                {
                    if (Form1.axMsRdpClient7NotSafeForScriptingIsFree == true)
                    {
                        TBSt.Text = "none";
                        Form1.VariantCurent = Form1.VariantCurent + 1;
                        TBV.Text = Form1.VariantCurent.ToString();
                        TBCM.Text = TB1.Lines[Form1.VariantCurent];
                        WYConnect(TB1.Lines[Form1.VariantCurent], Convert.ToInt32(Tt), ref TBSt);
                    }
                }
            }
        }

        public static void Jump(ref TextBox TB1, ref TextBox TBT, ref TextBox TBV, ref TextBox TBCM, ref TextBox TBSt,ref TextBox TBJ)
        {
            if (Form1.VariantCurent < TB1.Lines.Length )
            {
                string Tt = Filter.CleanMask(TBT.Text, "0123456789");
                string Tbj = Filter.CleanMask(TBJ.Text, "0123456789");
                if (Tt != "" && Tbj != "")
                {
                    if (Form1.axMsRdpClient7NotSafeForScriptingIsFree == true)
                    {
                        TBSt.Text = "none";
                        Form1.VariantCurent = Convert.ToInt32(Tbj);
                        TBV.Text = Form1.VariantCurent.ToString();
                        TBCM.Text = TB1.Lines[Form1.VariantCurent];
                        WYConnect(TB1.Lines[Form1.VariantCurent], Convert.ToInt32(Tt), ref TBSt);
                    }
                }
            }

        }

        public static void Reconnect(ref TextBox TB1, ref TextBox TBT, ref TextBox TBV, ref TextBox TBCM, ref TextBox TBSt)
        {
            string Tt = Filter.CleanMask(TBT.Text, "0123456789");
            if (Tt != "")
            {
                if (Form1.axMsRdpClient7NotSafeForScriptingIsFree == true)
                {
                    TBSt.Text = "none";
                    TBV.Text = "Prev Variant";
                    if (TBCM.Text == "")
                    {
                        TBCM.Text = TB1.Lines[Form1.VariantCurent];
                        WYConnect(TB1.Lines[Form1.VariantCurent], Convert.ToInt32(Tt), ref TBSt);
                    }
                    else
                    {
                        WYConnect(TBCM.Text, Convert.ToInt32(Tt), ref TBSt);
                    }
                }
            }
        }

        public static string WYConnect(string basicway, int Timeout0,ref TextBox TBSt)//проверка текущего RDP соединения используя логин , домен и пароль
        {
            string rez = "";
            //MessageBox.Show("sss");
            Form1.axMsRdpClient7NotSafeForScriptingIsFree = false;
            //Form1.axMsRdpClient7NotSafeForScripting[Client].OnChannelReceivedData += delegate (object sender, AxMSTSCLib.IMsTscAxEvents_OnUserNameAcquiredEvent e) { Loginned = "true"; MessageBox.Show("asassaas"); throw new NotImplementedException(); };
            string basicwayStart = basicway;
            basicway = Filter.RemoveAfterSpace(basicway);
            int dividers = 0;
            int dividert = 0;
            int points = 0;
            int[] divider = new int[3];
            int doublepoint = 0;
            for (int i = 0; i < basicway.Length; i++)
            {
                dividert = dividers;
                if (basicway[i] == ':' && dividers == 0)
                {
                    doublepoint = i;//нашли двоеточие
                }
                if (doublepoint != 0 && dividert == 0 && (basicway[i] == '@' || basicway[i] == ';' || basicway[i] == '\\') && dividert < 3)
                {
                    divider[dividers] = i;//нашли очередной разделитель
                    dividers++;
                }
                if (doublepoint != 0 && dividert != 0 && (basicway[i] == ';' || basicway[i] == '\\') && dividert < 3)
                {
                    divider[dividers] = i;//нашли очередной разделитель
                    dividers++;
                }
            }
            for (int i = 0; i < doublepoint; i++)
            {
                if (basicway[i] == '.')
                {
                    points++;//нашли точку
                }
            }
            //MessageBox.Show(dividers.ToString());
            //MessageBox.Show("Connected = "+rdp.Connected.ToString());
            if (points == 3 && dividers == 3 && doublepoint != 0 && divider[0] != doublepoint && divider[0] != divider[1] && divider[1] != divider[2] )//если найдены 3 разделителя и двоеточие не в начале то это корректная строка
            {
                try
                {
                    Form1.rdp.AdvancedSettings8.AuthenticationLevel= Convert.ToUInt32(Form1.AL.Text);
                    Form1.rdp.AdvancedSettings8.NegotiateSecurityLayer= Convert.ToBoolean(Convert.ToUInt32(Form1.LN.Text));
                    Form1.rdp.AdvancedSettings8.EnableCredSspSupport = Convert.ToBoolean(Convert.ToUInt32(Form1.CS.Text));
                    Form1.rdp.Server = basicway.Substring(0, doublepoint); //адрес удаленной машины
                    Form1.rdp.AdvancedSettings8.RDPPort = Convert.ToInt32(basicway.Substring(doublepoint + 1, divider[0] - doublepoint - 1)); //порт соединения
                    Form1.rdp.Domain = basicway.Substring(divider[0] + 1, divider[1] - divider[0] - 1); //домен
                    Form1.rdp.UserName = basicway.Substring(divider[1] + 1, divider[2] - divider[1] - 1); //логин
                    if (divider[2] != basicway.Length - 1)
                    {
                        Form1.rdp.AdvancedSettings8.ClearTextPassword = basicway.Substring(divider[2] + 1, basicway.Length - divider[2] - 1); //пароль
                    }
                    else
                    {
                        Form1.rdp.AdvancedSettings8.ClearTextPassword = "";
                    }
                    Form1.rdp.AdvancedSettings8.DisplayConnectionBar = true;
                    Form1.rdp.AdvancedSettings8.EncryptionEnabled = -1;
                    Form1.rdp.Connect();
                    //MessageBox.Show("Connected = " + Form1.axMsRdpClient7NotSafeForScripting[Client].Connected.ToString());
                    //MessageBox.Show(Form1.axMsRdpClient7NotSafeForScripting[Client].Server + ":" + Form1.axMsRdpClient7NotSafeForScripting[Client].AdvancedSettings8.RDPPort + ";" + Form1.axMsRdpClient7NotSafeForScripting[Client].Domain + "\\" + Form1.axMsRdpClient7NotSafeForScripting[Client].UserName + ";" + basicway.Substring(divider[2] + 1, basicway.Length - divider[2] - 1));
                }
                catch (Exception Ex)
                {

                }

                try
                {

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();

                    while (Form1.rdp.Connected == 2 && sw.ElapsedMilliseconds < Timeout0)
                    {
                        Thread.Sleep(100);
                    }

                    if (Form1.rdp.Connected == 1)
                    {
                        TBSt.Text = "Connected";
                        rez = basicwayStart;
                    }
                    if (Form1.rdp.Connected != 1)
                    {
                        TBSt.Text = "No Connect";
                        rez = basicwayStart;
                    }

                    Form1.axMsRdpClient7NotSafeForScriptingIsFree = true;
                }
                catch (Exception e)
                {
                    TBSt.Text = "Error";
                    Form1.axMsRdpClient7NotSafeForScriptingIsFree = true;
                }
            }

            if (points == 3 && dividers == 2 && doublepoint != 0 && divider[0] != doublepoint && divider[0] != divider[1] )//если найдены 2 разделителя и двоеточие не в начале то это строка без домена
            {
                if (divider[1] != '\\')
                {
                    try
                    {
                        Form1.rdp.AdvancedSettings8.AuthenticationLevel = Convert.ToUInt32(Form1.AL.Text);
                        Form1.rdp.AdvancedSettings8.NegotiateSecurityLayer = Convert.ToBoolean(Convert.ToUInt32(Form1.LN.Text));
                        Form1.rdp.AdvancedSettings8.EnableCredSspSupport = Convert.ToBoolean(Convert.ToUInt32(Form1.CS.Text));
                        Form1.rdp.Server = basicway.Substring(0, doublepoint); //адрес удаленной машины
                        Form1.rdp.AdvancedSettings8.RDPPort = Convert.ToInt32(basicway.Substring(doublepoint + 1, divider[0] - doublepoint - 1)); //порт соединения
                        Form1.rdp.UserName = basicway.Substring(divider[0] + 1, divider[1] - divider[0] - 1); //логин
                        if (divider[1] != basicway.Length - 1)
                        {
                            Form1.rdp.AdvancedSettings8.ClearTextPassword = basicway.Substring(divider[1] + 1, basicway.Length - divider[1] - 1); //пароль
                        }
                        else
                        {
                            Form1.rdp.AdvancedSettings8.ClearTextPassword = "";
                        }
                        Form1.rdp.AdvancedSettings8.DisplayConnectionBar = true;
                        Form1.rdp.AdvancedSettings8.EncryptionEnabled = -1;
                        Form1.rdp.Connect();
                        //MessageBox.Show("Connected = " + Form1.axMsRdpClient7NotSafeForScripting[Client].Connected.ToString());
                        //MessageBox.Show(Form1.axMsRdpClient7NotSafeForScripting[Client].Server + ":" + Form1.axMsRdpClient7NotSafeForScripting[Client].AdvancedSettings8.RDPPort + ";" + Form1.axMsRdpClient7NotSafeForScripting[Client].UserName + ";" + basicway.Substring(divider[1] + 1, basicway.Length - divider[1] - 1));
                    }
                    catch (Exception Ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        Form1.rdp.AdvancedSettings8.AuthenticationLevel = Convert.ToUInt32(Form1.AL.Text);
                        Form1.rdp.AdvancedSettings8.NegotiateSecurityLayer = Convert.ToBoolean(Convert.ToUInt32(Form1.LN.Text));
                        Form1.rdp.AdvancedSettings8.EnableCredSspSupport = Convert.ToBoolean(Convert.ToUInt32(Form1.CS.Text));
                        Form1.rdp.Server = basicway.Substring(0, doublepoint); //адрес удаленной машины
                        Form1.rdp.AdvancedSettings8.RDPPort = Convert.ToInt32(basicway.Substring(doublepoint + 1, divider[0] - doublepoint - 1)); //порт соединения
                        Form1.rdp.Domain = basicway.Substring(divider[0] + 1, divider[1] - divider[0] - 1); //домен
                        if (divider[1] != basicway.Length - 1)
                        {
                            Form1.rdp.UserName = basicway.Substring(divider[1] + 1, basicway.Length - divider[1] - 1); //логин
                        }
                        else
                        {
                            Form1.rdp.UserName = "";
                        }
                        Form1.rdp.UserName = basicway.Substring(divider[1] + 1, basicway.Length - divider[1] - 1); //логин
                        Form1.rdp.AdvancedSettings8.DisplayConnectionBar = true;
                        Form1.rdp.AdvancedSettings8.EncryptionEnabled = -1;
                        Form1.rdp.Connect();
                        //MessageBox.Show("Connected = " + Form1.axMsRdpClient7NotSafeForScripting[Client].Connected.ToString());
                        //MessageBox.Show(Form1.axMsRdpClient7NotSafeForScripting[Client].Server + ":" + Form1.axMsRdpClient7NotSafeForScripting[Client].AdvancedSettings8.RDPPort + ";" + Form1.axMsRdpClient7NotSafeForScripting[Client].UserName + ";" + basicway.Substring(divider[1] + 1, basicway.Length - divider[1] - 1));
                    }
                    catch (Exception Ex)
                    {

                    }
                }

                try
                {

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();

                    while (Form1.rdp.Connected == 2 && sw.ElapsedMilliseconds < Timeout0)
                    {
                        Thread.Sleep(100);
                    }

                    if (Form1.rdp.Connected == 1)
                    {
                        TBSt.Text = "Connected";
                        rez = basicwayStart;
                    }
                    if (Form1.rdp.Connected != 1)
                    {
                        TBSt.Text = "No Connect";
                        rez = basicwayStart;
                    }

                    Form1.axMsRdpClient7NotSafeForScriptingIsFree = true;
                }
                catch (Exception e)
                {
                    TBSt.Text = "Error";
                    Form1.axMsRdpClient7NotSafeForScriptingIsFree = true;
                }
            }

            if (points == 3 && dividers == 1 && doublepoint != 0 && divider[0] != doublepoint )
            {
                try
                {
                    Form1.rdp.AdvancedSettings8.AuthenticationLevel = Convert.ToUInt32(Form1.AL.Text);
                    Form1.rdp.AdvancedSettings8.NegotiateSecurityLayer = Convert.ToBoolean(Convert.ToUInt32(Form1.LN.Text));
                    Form1.rdp.AdvancedSettings8.EnableCredSspSupport = Convert.ToBoolean(Convert.ToUInt32(Form1.CS.Text));
                    Form1.rdp.Server = basicway.Substring(0, doublepoint); //адрес удаленной машины
                    Form1.rdp.AdvancedSettings8.RDPPort = Convert.ToInt32(basicway.Substring(doublepoint + 1, divider[0] - doublepoint - 1)); //порт соединения
                    if (divider[0] != basicway.Length - 1)
                    {
                        Form1.rdp.UserName = basicway.Substring(divider[0] + 1, basicway.Length - divider[0] - 1); //логин
                    }
                    else
                    {
                        Form1.rdp.UserName = "";
                    }
                    Form1.rdp.AdvancedSettings8.DisplayConnectionBar = true;
                    Form1.rdp.AdvancedSettings8.EncryptionEnabled = -1;
                    Form1.rdp.Connect();
                    //MessageBox.Show("Connected = " + Form1.axMsRdpClient7NotSafeForScripting[Client].Connected.ToString());
                    //MessageBox.Show(Form1.axMsRdpClient7NotSafeForScripting[Client].Server + ":" + Form1.axMsRdpClient7NotSafeForScripting[Client].AdvancedSettings8.RDPPort + ";" + Form1.axMsRdpClient7NotSafeForScripting[Client].Domain + "\\" + Form1.axMsRdpClient7NotSafeForScripting[Client].UserName + ";" + basicway.Substring(divider[2] + 1, basicway.Length - divider[2] - 1));
                }
                catch (Exception Ex)
                {

                }

                try
                {

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();

                    while (Form1.rdp.Connected == 2 && sw.ElapsedMilliseconds < Timeout0)
                    {
                        Thread.Sleep(100);
                    }

                    if (Form1.rdp.Connected == 1)
                    {
                        TBSt.Text = "Connected";
                        rez = basicwayStart;
                    }
                    if (Form1.rdp.Connected != 1)
                    {
                        TBSt.Text = "No Connect";
                        rez = basicwayStart;
                    }

                    Form1.axMsRdpClient7NotSafeForScriptingIsFree = true;
                }
                catch (Exception e)
                {
                    TBSt.Text = "Error";
                    Form1.axMsRdpClient7NotSafeForScriptingIsFree = true;
                }
            }
            if (points == 3 && dividers == 0 && doublepoint != 0 && doublepoint != basicway.Length - 1)
            {
                try
                {
                    Form1.rdp.AdvancedSettings8.AuthenticationLevel = Convert.ToUInt32(Form1.AL.Text);
                    Form1.rdp.AdvancedSettings8.NegotiateSecurityLayer = Convert.ToBoolean(Convert.ToUInt32(Form1.LN.Text));
                    Form1.rdp.AdvancedSettings8.EnableCredSspSupport = Convert.ToBoolean(Convert.ToUInt32(Form1.CS.Text));
                    Form1.rdp.Server = basicway.Substring(0, doublepoint); //адрес удаленной машины
                    Form1.rdp.AdvancedSettings8.RDPPort = Convert.ToInt32(basicway.Substring(doublepoint + 1, divider[0] - doublepoint - 1)); //порт соединения
                    Form1.rdp.AdvancedSettings8.DisplayConnectionBar = true;
                    Form1.rdp.AdvancedSettings8.EncryptionEnabled = -1;
                    Form1.rdp.Connect();
                    //MessageBox.Show("Connected = " + Form1.axMsRdpClient7NotSafeForScripting[Client].Connected.ToString());
                    //MessageBox.Show(Form1.axMsRdpClient7NotSafeForScripting[Client].Server + ":" + Form1.axMsRdpClient7NotSafeForScripting[Client].AdvancedSettings8.RDPPort + ";" + Form1.axMsRdpClient7NotSafeForScripting[Client].Domain + "\\" + Form1.axMsRdpClient7NotSafeForScripting[Client].UserName + ";" + basicway.Substring(divider[2] + 1, basicway.Length - divider[2] - 1));
                }
                catch (Exception Ex)
                {

                }

                try
                {

                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();

                    while (Form1.rdp.Connected == 2 && sw.ElapsedMilliseconds < Timeout0)
                    {
                        Thread.Sleep(100);
                    }

                    if (Form1.rdp.Connected == 1)
                    {
                        TBSt.Text = "Connected";
                        rez = basicwayStart;
                    }
                    if (Form1.rdp.Connected != 1)
                    {
                        TBSt.Text = "No Connect";
                        rez = basicwayStart;
                    }

                    Form1.axMsRdpClient7NotSafeForScriptingIsFree = true;
                }
                catch (Exception e)
                {
                    TBSt.Text = "Error";
                    Form1.axMsRdpClient7NotSafeForScriptingIsFree = true;
                }
            }

            return rez;
        }

    }
}
