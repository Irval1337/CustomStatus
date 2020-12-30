using System;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CustomStatus
{
    public partial class Importer : Form
    {
        Form1 main;
        DataSettings settings;

        public Importer(Form1 main)
        {
            InitializeComponent();
            this.main = main;
            settings = main.settings;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "JSON Settings(*.json)|*.json|All files(*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                string strSettings = System.IO.File.ReadAllText(openFileDialog1.FileName);
                settings = JsonConvert.DeserializeObject<DataSettings>(strSettings);
                System.IO.File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CustomStatus\Settings.json", strSettings);
                main.settings = this.settings;

                main.listBox1.Items.Clear();
                foreach (var Date in settings.Dates)
                    main.listBox1.Items.Add(Date.Name);

                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
            {
                saveFileDialog1.Filter = "JSON Settings(*.json)|*.json|All files(*.*)|*.*";
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                System.IO.File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(settings));
                this.Close();
            }
        }
    }
}
