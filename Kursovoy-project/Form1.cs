using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Kursovoy_project
{
public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPStatus status = IPStatus.Unknown;
            try
            {
                status = new Ping().Send("213.180.204.37").Status;
            }
            catch { }

            if (status == IPStatus.Success)
            {
                textBox1.Text = "Данный компьютер подключен к сети";

            }
            else
            {
                textBox1.Text = "Данный компьютер не подключен к сети";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(File.Exists("C::/Program Files/avz4/avz.exe")))
            {
                textBox2.Text = "Фаервол AVZ установлен!";
            }
            else
            {
                textBox2.Text = "Фаервол AVZ не установлен!";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebClient Client = new WebClient();
            String Response;
            try
            {
                Response = Client.DownloadString("https://2ip.ru");
            }
            catch
            {
                textBox3.Text = "Межсетевой экран функционирует правильно!";
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = "Межсетевой экран функционирует неверно, или не функционирует вовсе!";
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetInstalledSoftware();
        }

        private void GetInstalledSoftware()
        {
            List<string> items = new List<string>();
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var rk = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            if (sk.GetValue("DisplayName") != null)
                            {
                                items.Add(sk.GetValue("DisplayName").ToString());
                                listBox1.Items.Add(new ListViewItem(items.ToArray()));
                                listBox1.SetSelected(listBox1.Items.Count - 1, true);
                                if (listBox1.SelectedItem.ToString() == "ListViewItem: {Microsoft Update Health Tools}")
                                {
                                    textBox4.Text = "Антивирус Microsoft Defender установлен в системе по умолчанию";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    items.Clear();
                }
            }
        }
        private string CheckValue(object input)
        {
            if (input != null)
                return input.ToString();
            else
                return string.Empty;

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var runningProcs = from proc in Process.GetProcesses(".") orderby proc.Id select proc;
            if (runningProcs.Count(p => p.ProcessName.Contains("dllhost")) > 0)
            {
                textBox5.Text = "Антивирус Microsoft Defender работает!";
            }
            else
            {
                textBox5.Text = "Антивирус Microsoft Defender не работает!";
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
            textBox6.Text = "Результаты проведенного тестирования антивируса и фаервола" + System.Environment.NewLine + System.Environment.NewLine;
            if (textBox1.Text == "Данный компьютер подключен к сети") textBox6.Text = textBox6.Text + "1. Данный компьютер подключен к сети";
            else
            {
                if (textBox1.Text == "Данный компьютер не подключен к сети") textBox6.Text = textBox6.Text + "1. Данный компьютер не подключен к сети";
                else
                {
                    textBox6.Text = textBox6.Text + "1. Тестирование подключения к сети не выполнялось";
                }
            }
            textBox6.Text = textBox6.Text + System.Environment.NewLine;

            if (textBox2.Text == "Фаервол AVZ не установлен!") textBox6.Text = textBox6.Text + "2. Фаервол AVZ не установлен на этом ПК!";
            else
            {
                if (textBox2.Text == "Фаервол AVZ установлен!") textBox6.Text = textBox6.Text + "2. Фаервол AVZ установлен на этом ПК!";
                else
                {
                    textBox6.Text = textBox6.Text + "2. Проверка наличия фаервола на данном ПК не выполнялась";
                }
            }
            textBox6.Text = textBox6.Text + System.Environment.NewLine;

            if (textBox3.Text == "Межсетевой экран функционирует правильно!") textBox6.Text = textBox6.Text + "3. Межсетевой экран функционирует правильно на этом ПК!";
            else
            {
                if (textBox3.Text == "Межсетевой экран функционирует неверно, или не функционирует вовсе!") textBox6.Text = textBox6.Text + "3. Межсетевой экран функционирует неверно, или не функционирует вовсе на этом ПК!";
                else
                {
                    textBox6.Text = textBox6.Text + "3. Проверка межсетевого экрана на данном ПК не выполнялась";
                }
            }
            textBox6.Text = textBox6.Text + System.Environment.NewLine;

            if (textBox4.Text == "Антивирус Microsoft Defender не установлен в системе по умолчанию") textBox6.Text = textBox6.Text + "4. Антивирус Microsoft Defender не работает";
            else
            {
                if (textBox4.Text == "Антивирус Microsoft Defender установлен в системе по умолчанию") textBox6.Text = textBox6.Text + "4. Антивирус Microsoft Defender работает по умолчанию на этом ПК!";
                else
                {
                    textBox6.Text = textBox6.Text + "4. Проверка антивируса на данном ПК не выполнялась";
                }
            }
            textBox6.Text = textBox6.Text + System.Environment.NewLine;

            if (textBox5.Text == "Антивирус Microsoft Defender не работает!") textBox6.Text = textBox6.Text + "5. Антивирус Microsoft Defender не работает на этом ПК!";
            else
            {
                if (textBox5.Text == "Антивирус Microsoft Defender работает!") textBox6.Text = textBox6.Text + "5. Антивирус Microsoft Defender работает на этом ПК!";
                else
                {
                    textBox6.Text = textBox6.Text + "5. Проверка работоспособности антивируса на данном ПК не выполнялась";
                }
            }
            textBox6.Text = textBox6.Text + System.Environment.NewLine;

            textBox1.Clear(); textBox2.Clear();
            textBox3.Clear(); textBox4.Clear();
            textBox5.Clear(); listBox1.Items.Clear();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.DefaultExt = ".txt";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                string filename = savefile.FileName;
                File.WriteAllText(filename, textBox6.Text);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
