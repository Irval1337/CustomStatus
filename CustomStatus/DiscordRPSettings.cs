using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomStatus
{
    public partial class DiscordRPSettings : Form
    {
        public DiscordRPSettings()
        {
            InitializeComponent();
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start($"https://discord.com/developers/applications/{linkLabel1.Text}/information");
        }

        private void DiscordRPSettings_Load(object sender, EventArgs e)
        {
            linkLabel1.Text = String.IsNullOrEmpty(Properties.Settings.Default.DSRPkey) ? "796786581708079115" : Properties.Settings.Default.DSRPkey;
            if (!String.IsNullOrEmpty(Properties.Discord.Default.State))
                textBox4.Text = Properties.Discord.Default.State;
            if (!String.IsNullOrEmpty(Properties.Discord.Default.Details))
                textBox1.Text = Properties.Discord.Default.Details;
            if (!String.IsNullOrEmpty(Properties.Discord.Default.LargeImg))
                textBox2.Text = Properties.Discord.Default.LargeImg;
            if (!String.IsNullOrEmpty(Properties.Discord.Default.LargeText))
                textBox3.Text = Properties.Discord.Default.LargeText;
            if (!String.IsNullOrEmpty(Properties.Discord.Default.SmallImg))
                textBox5.Text = Properties.Discord.Default.SmallImg;
            if (!String.IsNullOrEmpty(Properties.Discord.Default.SmallText))
                textBox6.Text = Properties.Discord.Default.SmallText;
            checkBox1.Checked = Properties.Discord.Default.UseTimer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Discord.Default.State = textBox4.Text != (string)textBox4.Tag ? textBox4.Text : null;
            Properties.Discord.Default.Details = textBox1.Text != (string)textBox1.Tag ? textBox1.Text : null;
            Properties.Discord.Default.LargeImg = textBox2.Text != (string)textBox2.Tag ? textBox2.Text : null;
            Properties.Discord.Default.LargeText = textBox3.Text != (string)textBox3.Tag ? textBox3.Text : null;
            Properties.Discord.Default.SmallImg = textBox5.Text != (string)textBox5.Tag ? textBox5.Text : null;
            Properties.Discord.Default.SmallText = textBox6.Text != (string)textBox6.Tag ? textBox6.Text : null;
            Properties.Discord.Default.UseTimer = checkBox1.Checked;
            Properties.Discord.Default.Save();
            MessageBox.Show("Наастройки Discord RP успешно сохранены", "CustomStatus");
        }
    }
}
