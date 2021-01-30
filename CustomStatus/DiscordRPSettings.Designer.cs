namespace CustomStatus
{
    partial class DiscordRPSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ClientID приложения:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabel1.Location = new System.Drawing.Point(130, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(133, 15);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "796786581708079115";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(10, 54);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(253, 20);
            this.textBox4.TabIndex = 6;
            this.textBox4.TabStop = false;
            this.textBox4.Tag = "Null";
            this.textBox4.Text = "Null";
            this.textBox4.Enter += new System.EventHandler(this.RemoveText);
            this.textBox4.Leave += new System.EventHandler(this.AddText);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(75, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 25);
            this.button1.TabIndex = 7;
            this.button1.TabStop = false;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Значение State по умолчанию";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Значение Details по умолчанию";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 93);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(253, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.TabStop = false;
            this.textBox1.Tag = "Idling";
            this.textBox1.Text = "Idling";
            this.textBox1.Enter += new System.EventHandler(this.RemoveText);
            this.textBox1.Leave += new System.EventHandler(this.AddText);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(10, 144);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(253, 20);
            this.textBox2.TabIndex = 6;
            this.textBox2.TabStop = false;
            this.textBox2.Tag = "Null";
            this.textBox2.Text = "Null";
            this.textBox2.Enter += new System.EventHandler(this.RemoveText);
            this.textBox2.Leave += new System.EventHandler(this.AddText);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Большое изображение";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(10, 183);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(253, 20);
            this.textBox3.TabIndex = 9;
            this.textBox3.TabStop = false;
            this.textBox3.Tag = "Null";
            this.textBox3.Text = "Null";
            this.textBox3.Enter += new System.EventHandler(this.RemoveText);
            this.textBox3.Leave += new System.EventHandler(this.AddText);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Текст большого изображения";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(10, 234);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(253, 20);
            this.textBox5.TabIndex = 6;
            this.textBox5.TabStop = false;
            this.textBox5.Tag = "Null";
            this.textBox5.Text = "Null";
            this.textBox5.Enter += new System.EventHandler(this.RemoveText);
            this.textBox5.Leave += new System.EventHandler(this.AddText);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Маленькое изображение";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(10, 273);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(253, 20);
            this.textBox6.TabIndex = 9;
            this.textBox6.TabStop = false;
            this.textBox6.Tag = "Null";
            this.textBox6.Text = "Null";
            this.textBox6.Enter += new System.EventHandler(this.RemoveText);
            this.textBox6.Leave += new System.EventHandler(this.AddText);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Текст маленького изображения";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(10, 309);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(234, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "Запустить отсчет от запуска программы";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // DiscordRPSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 377);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DiscordRPSettings";
            this.Text = "Дополнительные настройки DiscordRP";
            this.Load += new System.EventHandler(this.DiscordRPSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}