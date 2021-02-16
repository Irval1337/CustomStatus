using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;    
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using VkNet;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using DiscordRPC;
using Ionic.Zip;
using System.ComponentModel;

namespace CustomStatus
{
    public partial class Form1 : Form
    {
        #region WinApi
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr window, int index);

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;

        public static void HideFromAltTab(IntPtr Handle)
        {
            SetWindowLong(Handle, GWL_EXSTYLE, GetWindowLong(Handle,
                GWL_EXSTYLE) | WS_EX_TOOLWINDOW);
        }
        #endregion

        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public DataSettings settings;

        static bool needClose = false;

        VkApi vkapi = new VkApi();

        static bool stopStatusVK = false;
        static bool stopStatusDS = false;

        bool isFirstRPC = true;
        Process winServer = null;

        public Form1()
        {
            InitializeComponent();
            notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            notifyIcon1.Visible = false;
        }

        private void авторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Irval1337");
        }

        private void данныеДляАвторизацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            данныеДляАвторизацииToolStripMenuItem1_Click(sender, e);
        }


        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            needClose = true;
            if (!notifyIcon1.Visible)
            {
                if (!String.IsNullOrEmpty(toolStripMenuItem2.Text) && toolStripMenuItem2.Text != (string)toolStripMenuItem2.Tag)
                {
                    Properties.Settings.Default.RuCaptcha = toolStripMenuItem2.Text;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(toolStripTextBox1.Text) && toolStripTextBox1.Text != (string)toolStripTextBox1.Tag)
                {
                    Properties.Settings.Default.RuCaptcha = toolStripTextBox1.Text;
                    Properties.Settings.Default.Save();
                }
            }
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!needClose)
            {
                toolStripTextBox1.Text = toolStripMenuItem2.Text;
                notifyIcon1.Visible = true;
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
                HideFromAltTab(this.Handle);
                e.Cancel = true;
            }
            else
                if (Properties.YGRPC.Default.ClientStartMode == 1 && winServer != null && !winServer.HasExited) winServer.Kill();

        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            выходToolStripMenuItem_Click(sender, e);
        }

        private void данныеДляАвторизацииToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetVisible();
            tabControl1.SelectedIndex = 1;
        }

        private void используемыеДатыToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetVisible();

            settings = JsonConvert.DeserializeObject<DataSettings>(File.ReadAllText(appData + @"\CustomStatus\Settings.json"));
            listBox1.Items.Clear();
            foreach (var Date in settings.Dates)
                listBox1.Items.Add(Date.Name);

            tabControl1.SelectedIndex = 2;
        }

        private void используемыеДатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            используемыеДатыToolStripMenuItem1_Click(sender, e);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetVisible();
        }

        void GetVisible()
        {
            toolStripMenuItem2.Text = toolStripTextBox1.Text;
            notifyIcon1.Visible = false;
            HideFromAltTab(this.Handle);
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                button10.Text = "Запустить бота";
                stopStatusVK = stopStatusDS = true;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.VkToken) && Properties.Settings.Default.VkUserId != 0)
                {
                    button5.Text = "Изменить";
                    textBox3.Enabled = textBox9.Enabled = false;
                    textBox3.Text = Properties.Settings.Default.VkToken;
                    textBox9.Text = Properties.Settings.Default.VkUserId.ToString();
                }
                if (Properties.Settings.Default.UseDSRP)
                {
                    button6.Text = "Изменить";
                    checkBox2.Enabled = false;
                    checkBox2.Checked = Properties.Settings.Default.UseDSRP;
                }
                button7.Text = "Изменить";
                radioButton1.Checked = Properties.Settings.Default.DSRPmode == 0;
                radioButton2.Checked = Properties.Settings.Default.DSRPmode == 1;
                radioButton1.Enabled = radioButton2.Enabled = false;
                textBox4.Enabled = false;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.DSRPkey))
                    textBox4.Text = Properties.Settings.Default.DSRPkey;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                isFirstRPC = true;
                checkBox3.Checked = Properties.YGRPC.Default.UseRPC;
                radioButton3.Checked = Properties.YGRPC.Default.ClientStartMode == 0;
                radioButton4.Checked = Properties.YGRPC.Default.ClientStartMode == 1;
                radioButton5.Checked = Properties.YGRPC.Default.ClientStartMode == 2;
                isFirstRPC = false;
            }
        }
        
        public void RemoveText(object sender, EventArgs e)
        {
            Control tb = (Control)sender;
            if (tb.Text == (string)tb.Tag)
                tb.Text = "";
        }

        public void AddText(object sender, EventArgs e)
        {
            Control tb = (Control)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
                tb.Text = (string)tb.Tag;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                textBox1.Text = settings.Dates[listBox1.SelectedIndex].Name;
                textBox2.Text = settings.Dates[listBox1.SelectedIndex].FormatText;
                dateTimePicker1.Value = settings.Dates[listBox1.SelectedIndex].Date;
                checkBox1.Checked = settings.Dates[listBox1.SelectedIndex].Repeat;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                settings.Dates[listBox1.SelectedIndex].Name = textBox1.Text;
                settings.Dates[listBox1.SelectedIndex].FormatText = textBox2.Text;
                settings.Dates[listBox1.SelectedIndex].Date = dateTimePicker1.Value;
                settings.Dates[listBox1.SelectedIndex].Repeat = checkBox1.Checked;

                File.WriteAllText(appData + @"\CustomStatus\Settings.json", JsonConvert.SerializeObject(settings));
                listBox1.Items[listBox1.SelectedIndex] = textBox1.Text;

                listBox1.SelectedIndex = -1;
                textBox1.Text = textBox2.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                checkBox1.Checked = false;
            }
            else
            {
                if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && DateTime.Now < dateTimePicker1.Value)
                {
                    DateItem dateItem = new DateItem();
                    dateItem.Name = textBox1.Text;
                    dateItem.FormatText = textBox2.Text;
                    dateItem.Date = dateTimePicker1.Value;
                    dateItem.Repeat = checkBox1.Checked;

                    foreach (var date in settings.Dates)
                    {
                        if (date.Date > dateItem.Date)
                        {
                            settings.Dates.Insert(settings.Dates.IndexOf(date) + 1, dateItem);
                            break;
                        }
                    }
                    if (settings.Dates.Count == 0)
                        settings.Dates.Insert(0, dateItem);

                    File.WriteAllText(appData + @"\CustomStatus\Settings.json", JsonConvert.SerializeObject(settings));
                    listBox1.Items.Insert(settings.Dates.IndexOf(dateItem), textBox1.Text);

                    textBox1.Text = textBox2.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    checkBox1.Checked = false;
                }
                else
                    MessageBox.Show("Ошибка заполнения полей!", "CustomStatus");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.BackColor = this.BackColor;
            if (!Directory.Exists(appData + @"\CustomStatus"))
                Directory.CreateDirectory(appData + @"\CustomStatus");
            else if (!File.Exists(appData + @"\CustomStatus\Settings.json"))
                    File.WriteAllText(appData + @"\CustomStatus\Settings.json", "{\"Dates\":[], \"UseProxy\": 0, \"Proxies\":[]}");

            if (!String.IsNullOrEmpty(Properties.Settings.Default.RuCaptcha))
                toolStripMenuItem2.Text = Properties.Settings.Default.RuCaptcha;

            settings = JsonConvert.DeserializeObject<DataSettings>(File.ReadAllText(appData + @"\CustomStatus\Settings.json"));

            if (!String.IsNullOrEmpty(Properties.Settings.Default.VkToken) && Properties.Settings.Default.VkUserId != 0)
            {
                try
                {
                    vkapi.Authorize(new VkNet.Model.ApiAuthParams() { AccessToken = Properties.Settings.Default.VkToken , UserId = Properties.Settings.Default.VkUserId, ApplicationId = 2685278 });
                    vkapi.Utils.ResolveScreenName("durov");
                    label10.Text = "В работе";
                    label10.ForeColor = Color.Green;
                    AddLog("[ВКонтакте] Бот успешно запущен!");

                    Task.Factory.StartNew(() => { VkStatus(); });
                }
                catch (Exception ex)
                {
                    label10.Text = "Отключено";
                    label10.ForeColor = Color.Red;
                    AddLog("[ВКонтакте] Не удалось запустить бота: " + ex.Message);
                }
            }
            else
            {
                label10.Text = "Отключено";
                label10.ForeColor = Color.Red;
            }
            if (Properties.Settings.Default.UseDSRP)
            {
                try
                {
                    StartDSActivity();
                    label11.Text = "В работе";
                    label11.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    label11.Text = "Отключено";
                    label11.ForeColor = Color.Red;
                    AddLog("[Discord] Не удалось запустить бота: " + ex.Message);
                }
            }
            else
            {
                label11.Text = "Отключено";
                label11.ForeColor = Color.Red;
            }
            if (label10.ForeColor == Color.Green || label11.ForeColor == Color.Green)
                button10.Text = "Остановить бота";
            else
                button10.Text = "Запустить бота";
            if (Properties.YGRPC.Default.UseRPC && Properties.YGRPC.Default.ClientStartMode == 1) {
                if (!Directory.Exists("Discord-RPC-Extension") || !File.Exists(@"Discord-RPC-Extension\server_win.exe"))
                {
                    MessageBox.Show("Файл server_win.exe не найден", "CustomStatus");
                    Properties.YGRPC.Default.ClientStartMode = 2;
                    Properties.YGRPC.Default.Save();
                }
                else
                {
                    ProcessStartInfo procInfo = new ProcessStartInfo();
                    procInfo.FileName = @"Discord-RPC-Extension\server_win.exe";
                    winServer = new Process { StartInfo = procInfo };
                    winServer.Start();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                settings.Dates.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                File.WriteAllText(appData + @"\CustomStatus\Settings.json", JsonConvert.SerializeObject(settings));

                textBox1.Text = textBox2.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                checkBox1.Checked = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Importer(this).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Info().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "Сохранить")
            {
                if (!String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(textBox9.Text))
                {
                    try
                    {
                        using (VkApi vkauth = new VkApi())
                        {
                            vkauth.Authorize(new VkNet.Model.ApiAuthParams()
                            {
                                AccessToken = textBox3.Text,
                                UserId = textBox9.Text.StartsWith("id") ? Convert.ToInt64(textBox9.Text.Substring(2)) : Convert.ToInt64(textBox9.Text),
                                ApplicationId = 2685278
                            });
                            vkauth.Utils.ResolveScreenName("durov");
                            vkapi = vkauth;
                            Properties.Settings.Default.VkToken = vkauth.Token;
                            Properties.Settings.Default.VkUserId = (long)vkauth.UserId;
                            Properties.Settings.Default.Save();

                            button5.Text = "Изменить";
                            textBox3.Enabled = textBox9.Enabled = false;
                        }
                    }
                    catch (VkNet.Exception.VkAuthorizationException)
                    {
                        MessageBox.Show("Введеные данные неверны!", "CustomStatus");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Во время авторизации произошла ошибка: " + ex.Message);
                    }
                }
                else
                    MessageBox.Show("Ошибка заполнения полей!", "CustomStatus");
            }
            else
            {
                button5.Text = "Сохранить";
                textBox3.Enabled = textBox9.Enabled = true;
            }
        }

        string getStatus(DateItem date)
        {
            string status = date.FormatText;
            DateTime now = DateTime.Now;
            var delta = date.Date - now;

            status = status.Replace("%yD", date.Date.Year.ToString());
            status = status.Replace("%dD", date.Date.Day.ToString());
            status = status.Replace("%hD", date.Date.Hour.ToString());
            status = status.Replace("%mD", date.Date.Minute.ToString());
            status = status.Replace("%sD", date.Date.Second.ToString());

            status = status.Replace("%yN", now.Year.ToString());
            status = status.Replace("%dN", now.Day.ToString());
            status = status.Replace("%hN", now.Hour.ToString());
            status = status.Replace("%mN", now.Minute.ToString());
            status = status.Replace("%sN", now.Second.ToString());

            status = status.Replace("%yT", (int)delta.TotalDays / 365 > 0 ? Dict.Years((int)(delta.TotalDays / 365)) : "");
            status = status.Replace("%dT", (int)delta.TotalDays % 365 > 0 ? Dict.Days((int)delta.TotalDays % 365) : "");
            status = status.Replace("%hT", (int)delta.TotalHours % 24 > 0 ? Dict.Hours((int)delta.TotalHours % 24) : "");
            status = status.Replace("%mT", (int)delta.TotalMinutes % 60 > 0 ? Dict.Minutes((int)delta.TotalMinutes % 60) : "");
            status = status.Replace("%sT", (int)delta.TotalSeconds % 60 > 0 ? Dict.Seconds((int)delta.TotalSeconds % 60) : "");

            status = status.Replace("%yT", (int)delta.TotalDays / 365 > 0 ? ((int)(delta.TotalDays / 365)).ToString() : "");
            status = status.Replace("%dT", (int)delta.TotalDays % 365 > 0 ? ((int)delta.TotalDays % 365).ToString() : "");
            status = status.Replace("%hT", (int)delta.TotalHours % 24 > 0 ? ((int)delta.TotalHours % 24).ToString() : "");
            status = status.Replace("%mT", (int)delta.TotalMinutes % 60 > 0 ? ((int)delta.TotalMinutes % 60).ToString() : "");
            status = status.Replace("%sT", (int)delta.TotalSeconds % 60 > 0 ? ((int)delta.TotalSeconds % 60).ToString() : "");

            status.Replace("  ", " ");
            return status;
        }

        private void grabberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetVisible();
            new ProxyGrabber().ShowDialog();
        }

        private void спискиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetVisible();
            new ProxySettings().ShowDialog();
        }

        public void VkStatus()
        {
            StartPosition:
            var serviceCollection = new ServiceCollection();
            if ((settings.UseProxy == 1 || settings.UseProxy == 2) && settings.Proxies.Count > 0)
                serviceCollection.AddSingleton<IWebProxy>(new WebProxy(settings.Proxies[0]));
            var api = new VkApi(serviceCollection);
            try
            {
                string token = Properties.Settings.Default.VkToken;
                long userId = Properties.Settings.Default.VkUserId;
                api.Authorize(new VkNet.Model.ApiAuthParams() { AccessToken = token, UserId = userId, ApplicationId = 2685278 });
                api.Utils.ResolveScreenName("durov");
                while (!needClose && !stopStatusVK)
                {
                    try
                    {
                        if (settings.Dates.Count == 0)
                            break;
                        var date = settings.Dates[0];
                        if (date.Date < DateTime.Now)
                        {
                            if ((DateTime.Now - date.Date).TotalDays > 0)
                            {
                                settings.Dates.RemoveAt(0);
                                if (date.Repeat)
                                    settings.Dates.Add(date);
                                date = settings.Dates[0];
                            }
                            else if (api.Status.Get(userId).Text != "Сегодня " + date.Name + "!")
                            {
                                api.Status.Set("Сегодня " + date.Name + "!");
                                AddLog($"[ВКонтакте] Статус изменен на поздравление с праздником \"{date.Name}\"");
                            }
                        }

                        string status = getStatus(date);

                        if (api.Status.Get(userId).Text != status)
                        {
                            api.Status.Set(status);
                            AddLog("[ВКонтакте] Установлен новый статус пользователя: \"" + status + "\"");
                        }
                    }
                    catch (Exception ex)
                    {
                        AddLog("[ВКонтакте] Неизвестная ошибка: " + ex.Message);
                    }
                    Thread.Sleep(100);  
                }
                
            }
            catch (HttpListenerException ex)
            {
                AddLog("[ВКонтакте] HttpListenerException: " + ex.Message);
                settings.Proxies.RemoveAt(0);
                if (settings.Proxies.Count > 0)
                    goto StartPosition;
            }
            catch (Exception ex)
            {
                AddLog("[ВКонтакте] Неизвестная ошибка: " + ex.Message);
            }
            this.Invoke(new MethodInvoker(() => {
                label10.Text = "Отключено";
                label10.ForeColor = Color.Red;
            }));
            AddLog("[ВКонтакте] Работа бота завершена!");
            stopStatusVK = false;
        }

        void DSStatus (DiscordRpcClient client)
        {
            while (!needClose && !stopStatusDS)
            {
                try
                {
                    if (settings.Dates.Count == 0) {
                        if (client.CurrentPresence.Details != Properties.Discord.Default.Details)
                            client.UpdateDetails(Properties.Discord.Default.Details);
                        Thread.Sleep(100);
                        continue;
                    }
                    var date = settings.Dates[0];
                    if (date.Date < DateTime.Now)
                    {
                        if ((DateTime.Now - date.Date).TotalDays > 0)
                        {
                            settings.Dates.RemoveAt(0);
                            if (date.Repeat)
                            {
                                date.Date = date.Date.AddYears(1);
                                settings.Dates.Add(date);
                            }
                            date = settings.Dates[0];
                        }
                        else if (client.CurrentPresence.Details != "Сегодня " + date.Name + "!")
                        {
                            client.UpdateDetails("Сегодня " + date.Name + "!");
                            AddLog($"[Discord] Статус изменен на поздравление с праздником \"{date.Name}\"");
                        }
                    }

                    string status = getStatus(date);

                    if (client.CurrentPresence.Details != status)
                        client.UpdateDetails(status);
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    AddLog("[Discord] Неизвестная ошибка: " + ex.Message);
                }
            }
            this.Invoke(new MethodInvoker(() => {
                label11.Text = "Отключено";
                label11.ForeColor = Color.Red;
            }));
            AddLog("[Discord] Работа бота завершена!");
            stopStatusDS = false;
        }

        void AddLog (string text)
        {
            this.Invoke(new MethodInvoker(() => { richTextBox1.Text += $"[{DateTime.Now.ToShortTimeString()}] {text}\n"; }));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            stopStatusDS = stopStatusVK = false;
            if (button10.Text == "Остановить бота")
            {
                stopStatusVK = true;
                stopStatusDS = true;
                button10.Text = "Запустить бота";
                label10.Text = label11.Text = "Отключено";
                label10.ForeColor = label11.ForeColor = Color.Red;
            }
            else
            {
                if (!String.IsNullOrEmpty(Properties.Settings.Default.VkToken) && Properties.Settings.Default.VkUserId != 0) //vk.com
                {
                    try
                    {
                        vkapi = new VkApi();
                        vkapi.Authorize(new VkNet.Model.ApiAuthParams() { AccessToken = Properties.Settings.Default.VkToken, UserId = Properties.Settings.Default.VkUserId, ApplicationId = 2685278 });
                        vkapi.Utils.ResolveScreenName("durov");
                        label10.Text = "В работе";
                        label10.ForeColor = Color.Green;
                        AddLog("[ВКонтакте] Бот успешно запущен!");

                        Task.Factory.StartNew(() => { VkStatus(); });
                    }
                    catch (Exception ex)
                    {
                        label10.Text = "Отключено";
                        label10.ForeColor = Color.Red;
                        button10.Text = "Запустить бота";
                        AddLog("Не удалось запустить бота: " + ex.Message);
                    }
                }
                if (Properties.Settings.Default.UseDSRP)
                {
                    try
                    {
                        StartDSActivity();
                        label11.Text = "В работе";
                        label11.ForeColor = Color.Green;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Во время инициализации Discord RP произошла ошибка: " + ex.Message, "CustomStatus");
                    }
                }
                if (label10.ForeColor != Color.Red || label11.ForeColor != Color.Red)
                    button10.Text = "Остановить бота";
            }
        }

        private void наГлавнуюToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        void StartDSActivity()
        {
            DiscordRpcClient client = new DiscordRpcClient(!String.IsNullOrEmpty(Properties.Settings.Default.DSRPkey) ? Properties.Settings.Default.DSRPkey
                        : "796786581708079115");
            client.OnReady += (se, evu) =>
            {
                AddLog("[Discord] Получено событие Ready от пользователя " + evu.User.Username);
            };

            client.OnPresenceUpdate += (se, evu) =>
            {
                AddLog("[Discord] Детали игровой активности изменены: \"" + evu.Presence.Details + "\"");
            };

            if (!client.Initialize())
                throw new Exception("Неверный ClientID приложения");

            if (Properties.Settings.Default.DSRPmode == 0)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = "Idling",
                    Assets = new Assets()
                    {
                        LargeImageKey = "logo",
                        LargeImageText = "CustomStatus",
                    }
                });
            }
            else
            {
                client.SetPresence(new RichPresence()
                {
                    Details = !String.IsNullOrEmpty(Properties.Discord.Default.Details) ? Properties.Discord.Default.Details : null,
                    State = !String.IsNullOrEmpty(Properties.Discord.Default.State) ? Properties.Discord.Default.State : null,
                    Assets = new Assets()
                    {
                        LargeImageKey = !String.IsNullOrEmpty(Properties.Discord.Default.LargeImg) ? Properties.Discord.Default.LargeImg : null,
                        LargeImageText = !String.IsNullOrEmpty(Properties.Discord.Default.LargeText) ? Properties.Discord.Default.LargeText : null,
                        SmallImageKey = !String.IsNullOrEmpty(Properties.Discord.Default.SmallImg) ? Properties.Discord.Default.SmallImg : null,
                        SmallImageText = !String.IsNullOrEmpty(Properties.Discord.Default.SmallText) ? Properties.Discord.Default.SmallText : null,
                    },
                    Timestamps = new Timestamps()
                    {
                        StartUnixMilliseconds = Properties.Discord.Default.UseTimer ? (ulong?)(DateTime.Now.ToUniversalTime()
                        - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds : null
                    }
                });
            }
            Task.Factory.StartNew(() => {
                DSStatus(client);
                client.Dispose();
            });
            AddLog("[Discord] Бот успешно запущен!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "Сохранить")
            {
                Properties.Settings.Default.UseDSRP = checkBox2.Checked;
                Properties.Settings.Default.Save();
                checkBox2.Enabled = false;
                button6.Text = "Изменить";
            }
            else
            {
                button6.Text = "Сохранить";
                checkBox2.Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "Сохранить")
            {
                if (!String.IsNullOrEmpty(textBox4.Text) && radioButton2.Checked)
                {
                    try
                    {
                        using (DiscordRpcClient client = new DiscordRpcClient(textBox4.Text))
                        {
                            if (!client.Initialize())
                                throw new Exception("Неверный ClientID приложения");

                            Properties.Settings.Default.DSRPkey = textBox4.Text;
                            Properties.Settings.Default.DSRPmode = 1;
                            Properties.Settings.Default.Save();

                            button7.Text = "Изменить";
                            textBox4.Enabled = false;
                            radioButton1.Enabled = radioButton2.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Во время авторизации произошла ошибка: " + ex.Message, "CustomStatus");
                    }
                }
                else if (radioButton1.Checked)
                {
                    Properties.Settings.Default.DSRPmode = 0;
                    Properties.Settings.Default.Save();
                    button7.Text = "Изменить";
                    textBox4.Enabled = false;
                    radioButton1.Enabled = radioButton2.Enabled = false;
                }
                else
                    MessageBox.Show("Ошибка заполнения полей!", "CustomStatus");
            }
            else
            {
                button7.Text = "Сохранить";
                radioButton1.Enabled = radioButton2.Enabled = true;
                textBox4.Enabled = radioButton2.Checked && !radioButton1.Checked;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = radioButton2.Checked && !radioButton1.Checked;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new DiscordRPSettings().ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            grabberToolStripMenuItem_Click(sender, e);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            спискиToolStripMenuItem_Click(sender, e);
        }

        private void youGameRPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetVisible();
            tabControl1.SelectedIndex = 3;
        }

        private void youGameDiscordRPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            youGameRPCToolStripMenuItem_Click(sender, e);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Process.Start("https://chrome.google.com/webstore/detail/discord-rich-presence/agnaejlkbiiggajjmnpmeheigkflbnoo");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Process.Start("https://chrome.google.com/webstore/detail/yougame-discord-rpc/kknolfoeplmjpoekmaphekhogjajgkcl");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/lolamtisch/Discord-RPC-Extension");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                string link = @"https://www.dropbox.com/s/b7rh3z9ake3zefp/windows_64bit.zip?dl=1";
                string path = Environment.CurrentDirectory;
                Directory.CreateDirectory(path + @"\Discord-RPC-Extension");
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += ((object se, AsyncCompletedEventArgs ev) => {
                    using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(path + @"\Discord-RPC-Extension\Extension.zip"))
                    {
                        foreach (ZipEntry evu in zip)
                        {
                            evu.Extract(path + @"\Discord-RPC-Extension", ExtractExistingFileAction.OverwriteSilently);
                        }
                        zip.Dispose();
                    }
                    File.Delete(path + @"\Discord-RPC-Extension\Extension.zip");
                    MessageBox.Show("Установка успешно завершена!", "CustomStatus");
                    if (Properties.YGRPC.Default.ClientStartMode == 0)
                        Process.Start(path + @"\Discord-RPC-Extension\add_startup.bat");
                    if (Properties.YGRPC.Default.ClientStartMode != 2)
                    {
                        ProcessStartInfo procInfo = new ProcessStartInfo();
                        procInfo.FileName = path + @"\Discord-RPC-Extension\server_win.exe";
                        winServer = new Process { StartInfo = procInfo };
                        winServer.Start();
                    }
                });
                webClient.DownloadFileAsync(new Uri(link), path + @"\Discord-RPC-Extension\Extension.zip");
            }
            catch (Exception ex) { MessageBox.Show("Ошибка во время установки: " + ex.Message, "CustomStatus"); }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked && !isFirstRPC)
            {
                if (!Directory.Exists("Discord-RPC-Extension") || !File.Exists(@"Discord-RPC-Extension\server_win.exe")
                        || !File.Exists(@"Discord-RPC-Extension\add_startup.bat"))
                    return;
                Process.Start(@"Discord-RPC-Extension\add_startup.bat");
                if ((winServer == null || winServer.HasExited) && Properties.YGRPC.Default.ClientStartMode != 1)
                    Process.Start(@"Discord-RPC-Extension\server_win.exe");
                Properties.YGRPC.Default.ClientStartMode = 0;
                Properties.YGRPC.Default.Save();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked && !isFirstRPC)
            {
                if (Properties.YGRPC.Default.ClientStartMode == 0)
                {
                    if (!Directory.Exists("Discord-RPC-Extension") || !File.Exists(@"Discord-RPC-Extension\server_win.exe")
                        || !File.Exists(@"Discord-RPC-Extension\remove_startup.bat"))
                        return;
                    Process.Start(@"Discord-RPC-Extension\remove_startup.bat");
                }
                if ((winServer == null || winServer.HasExited) && Properties.YGRPC.Default.ClientStartMode != 0)
                    Process.Start(@"Discord-RPC-Extension\server_win.exe");
                Properties.YGRPC.Default.ClientStartMode = 1;
                Properties.YGRPC.Default.Save();
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked && !isFirstRPC)
            {
                if (Properties.YGRPC.Default.ClientStartMode == 0)
                {
                    if (!Directory.Exists("Discord-RPC-Extension") || !File.Exists(@"Discord-RPC-Extension\server_win.exe")
                        || !File.Exists(@"Discord-RPC-Extension\remove_startup.bat"))
                        return;
                    Process.Start(@"Discord-RPC-Extension\remove_startup.bat");
                }
                Properties.YGRPC.Default.ClientStartMode = 2;
                Properties.YGRPC.Default.Save();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!isFirstRPC)
            {
                Properties.YGRPC.Default.UseRPC = checkBox3.Checked;
                Properties.YGRPC.Default.Save();

                if (checkBox3.Checked)
                {
                    ProcessStartInfo procInfo = new ProcessStartInfo();
                    procInfo.FileName = Environment.CurrentDirectory + @"\Discord-RPC-Extension\server_win.exe";
                    winServer = new Process { StartInfo = procInfo };
                    winServer.Start();
                }
                else
                    if (winServer != null && !winServer.HasExited) winServer.Kill();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Process.Start("https://addons.mozilla.org/ru/firefox/addon/yougame-discord-rpc/");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Process.Start("https://addons.mozilla.org/ru/firefox/addon/discord-rich-presence/");
        }
    }
}
