using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CustomStatus
{
    public partial class ProxySettings : Form
    {
        public DataSettings settings;

        public ProxySettings()
        {
            InitializeComponent();
        }

        private void ProxySettings_Load(object sender, EventArgs e)
        {
            settings = JsonConvert.DeserializeObject<DataSettings>(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) 
                + @"\CustomStatus\Settings.json"));
            switch (settings.UseProxy)
            {
                case 0:
                    radioButton3.Checked = true;
                    break;
                case 1:
                    radioButton2.Checked = true;
                    break;
                case 2:
                    radioButton1.Checked = true;
                    break;
            }
            richTextBox1.Clear();
            foreach (var proxy in settings.Proxies)
                richTextBox1.Text += proxy + "\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                settings.UseProxy = 2;
            else if (radioButton2.Checked)
                settings.UseProxy = 1;
            else
                settings.UseProxy = 0;
            settings.Proxies = richTextBox1.Lines.Length != 0 && richTextBox1.Lines[richTextBox1.Lines.Length - 1] == "\n" ? richTextBox1.Lines.Take(richTextBox1.Lines.Length - 1).ToList() 
                : richTextBox1.Lines.ToList();
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) 
                + @"\CustomStatus\Settings.json", JsonConvert.SerializeObject(settings));
            MessageBox.Show("Наастройки прокси сервера успешно сохранены", "CustomStatus");
        }
    }
}
