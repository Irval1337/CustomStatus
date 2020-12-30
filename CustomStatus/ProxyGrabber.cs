using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace CustomStatus
{
    public partial class ProxyGrabber : Form
    {
        private static StringBuilder builder = new StringBuilder();
        bool breakParsing = false;
        bool breakChecking = false;

        public ProxyGrabber()
        {
            InitializeComponent();
            richTextBox1.BackColor = textBox1.BackColor;
        }

        public static bool TestProxy(string prx)
        {
            using (HttpRequest reqes = new HttpRequest())
            {
                reqes.UserAgent = Http.FirefoxUserAgent();
                reqes.Proxy = HttpProxyClient.Parse(prx);
                reqes.ConnectTimeout = 1000;
                reqes.Proxy.ConnectTimeout = 1000;
                reqes.ReadWriteTimeout = 1000;
                reqes.Proxy.ReadWriteTimeout = 1000;
                try
                {
                    reqes.Post("https://google.ru");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == (string)textBox1.Tag)
                return;

            if (button1.Text == "Начать парсинг")
            {
                try
                {
                    var url = new Uri(textBox1.Text);
                    button1.Text = "Остановить парсинг";
                    var v = Task.Factory.StartNew(() =>
                    {
                        string pattern = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d{1,6}";
                        using (var wc = new WebClient())
                        {
                            wc.Encoding = Encoding.UTF8;
                            var regex = new Regex(pattern);
                            Match match = regex.Match(wc.DownloadString(url));
                            while (match.Success && !breakParsing)
                            {
                                this.Invoke(new MethodInvoker(() => { richTextBox1.Text += match.Value + "\n"; }));
                                match = match.NextMatch();
                            }
                            if (button1.Text != "Начать парсинг")
                                this.Invoke(new MethodInvoker(() => { button1.Text = "Начать парсинг"; }));
                            breakParsing = false;
                        }
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Во время парсинга возникла ошибка: " + ex.Message);
                }
            }
            else
            {
                breakParsing = true;
                button1.Text = "Начать парсинг";
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Control tb = (Control)sender;
            if (tb.Text == (string)tb.Tag)
                tb.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Control tb = (Control)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
                tb.Text = (string)tb.Tag;
        }

        static void HighlightPhrase(RichTextBox box, string phrase, Color color)
        {
            int pos = box.SelectionStart;
            string s = box.Text;
            for (int ix = 0; ;)
            {
                int jx = s.IndexOf(phrase, ix, StringComparison.CurrentCultureIgnoreCase);
                if (jx < 0) break;
                box.SelectionStart = jx;
                box.SelectionLength = phrase.Length;
                box.SelectionColor = color;
                ix = jx + 1;
            }
            box.SelectionStart = pos;
            box.SelectionLength = 0;
        }

        List<string> Valid = new List<string>();

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Начать проверку")
            {
                if (richTextBox1.Lines.Length == 0 || button1.Text != "Начать парсинг")
                    return;

                button2.Text = "Остановить проверку";
                var v = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        int len = 0;
                        this.Invoke(new MethodInvoker(() => { len = richTextBox1.Lines.Length; }));
                        for (int i = 0; i < len && !breakChecking; i++)
                        {
                            string line = "";
                            this.Invoke(new MethodInvoker(() => { line = richTextBox1.Lines[i]; }));
                            if (!TestProxy(line))
                            {
                                this.Invoke(new MethodInvoker(() => { HighlightPhrase(richTextBox1, line, Color.Red); }));
                            }
                            else
                            {
                                this.Invoke(new MethodInvoker(() => { HighlightPhrase(richTextBox1, line, Color.Green); }));
                                Valid.Add(line.Substring(0, line.Length - 2));
                            }
                        }
                        if (button2.Text != "Начать проверку")
                            this.Invoke(new MethodInvoker(() => { button2.Text = "Начать проверку"; }));
                        breakChecking = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Во время проверки возникла ошибка: " + ex.Message);
                    }
                });
            }
            else
            {
                breakChecking = true;
                button2.Text = "Начать проверку";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Valid.Count != 0)
                Clipboard.SetText(string.Join("\n", Valid));
        }
    }
}
