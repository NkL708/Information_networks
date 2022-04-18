namespace Information_networks
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.initButton = new System.Windows.Forms.Button();
            this.openSessionButton = new System.Windows.Forms.Button();
            this.closeSessionButton = new System.Windows.Forms.Button();
            this.executeButton = new System.Windows.Forms.Button();
            this.connectionLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new CueTextBox();
            this.serverPortTextBox = new CueTextBox();
            this.serverAdressTextBox = new CueTextBox();
            this.loginTextBox = new CueTextBox();
            this.incorrectFields = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // initButton
            // 
            this.initButton.BackColor = System.Drawing.Color.Lime;
            this.initButton.Location = new System.Drawing.Point(54, 60);
            this.initButton.Name = "initButton";
            this.initButton.Size = new System.Drawing.Size(130, 25);
            this.initButton.TabIndex = 0;
            this.initButton.Text = "Инициализация";
            this.initButton.UseVisualStyleBackColor = false;
            this.initButton.Click += new System.EventHandler(this.initButtonClick);
            // 
            // openSessionButton
            // 
            this.openSessionButton.BackColor = System.Drawing.Color.Lime;
            this.openSessionButton.Location = new System.Drawing.Point(54, 106);
            this.openSessionButton.Name = "openSessionButton";
            this.openSessionButton.Size = new System.Drawing.Size(130, 25);
            this.openSessionButton.TabIndex = 1;
            this.openSessionButton.Text = "Открытие сеанса";
            this.openSessionButton.UseVisualStyleBackColor = false;
            this.openSessionButton.Click += new System.EventHandler(this.openSessionButtonClick);
            // 
            // closeSessionButton
            // 
            this.closeSessionButton.BackColor = System.Drawing.Color.Lime;
            this.closeSessionButton.Location = new System.Drawing.Point(54, 156);
            this.closeSessionButton.Name = "closeSessionButton";
            this.closeSessionButton.Size = new System.Drawing.Size(130, 25);
            this.closeSessionButton.TabIndex = 2;
            this.closeSessionButton.Text = "Закрытие сеанса";
            this.closeSessionButton.UseVisualStyleBackColor = false;
            this.closeSessionButton.Click += new System.EventHandler(this.closeSessionButtonClick);
            // 
            // executeButton
            // 
            this.executeButton.BackColor = System.Drawing.Color.Lime;
            this.executeButton.Location = new System.Drawing.Point(54, 207);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(130, 25);
            this.executeButton.TabIndex = 3;
            this.executeButton.Text = "Выполнение команды";
            this.executeButton.UseVisualStyleBackColor = false;
            this.executeButton.Visible = false;
            this.executeButton.Click += new System.EventHandler(this.executeButtonClick);
            // 
            // connectionLabel
            // 
            this.connectionLabel.AutoSize = true;
            this.connectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.connectionLabel.ForeColor = System.Drawing.Color.Red;
            this.connectionLabel.Location = new System.Drawing.Point(361, 134);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(111, 17);
            this.connectionLabel.TabIndex = 12;
            this.connectionLabel.Text = "Не подключено";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Cue = "Пароль";
            this.passwordTextBox.Location = new System.Drawing.Point(211, 109);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(130, 20);
            this.passwordTextBox.TabIndex = 5;
            this.passwordTextBox.Visible = false;
            // 
            // serverPortTextBox
            // 
            this.serverPortTextBox.Cue = "Порт";
            this.serverPortTextBox.Location = new System.Drawing.Point(211, 210);
            this.serverPortTextBox.Name = "serverPortTextBox";
            this.serverPortTextBox.Size = new System.Drawing.Size(130, 20);
            this.serverPortTextBox.TabIndex = 7;
            this.serverPortTextBox.Text = "5432";
            this.serverPortTextBox.Visible = false;
            // 
            // serverAdressTextBox
            // 
            this.serverAdressTextBox.Cue = "Адрес сервера";
            this.serverAdressTextBox.Location = new System.Drawing.Point(211, 159);
            this.serverAdressTextBox.Name = "serverAdressTextBox";
            this.serverAdressTextBox.Size = new System.Drawing.Size(130, 20);
            this.serverAdressTextBox.TabIndex = 6;
            this.serverAdressTextBox.Text = "localhost";
            this.serverAdressTextBox.Visible = false;
            // 
            // loginTextBox
            // 
            this.loginTextBox.Cue = "Логин";
            this.loginTextBox.Location = new System.Drawing.Point(211, 63);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(130, 20);
            this.loginTextBox.TabIndex = 4;
            this.loginTextBox.Visible = false;
            // 
            // incorrectFields
            // 
            this.incorrectFields.AutoSize = true;
            this.incorrectFields.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.incorrectFields.ForeColor = System.Drawing.Color.Red;
            this.incorrectFields.Location = new System.Drawing.Point(219, 28);
            this.incorrectFields.Name = "incorrectFields";
            this.incorrectFields.Size = new System.Drawing.Size(201, 17);
            this.incorrectFields.TabIndex = 13;
            this.incorrectFields.Text = "Поля не могут быть пустыми!";
            this.incorrectFields.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(347, 171);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 103);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(484, 286);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.incorrectFields);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.serverPortTextBox);
            this.Controls.Add(this.serverAdressTextBox);
            this.Controls.Add(this.loginTextBox);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.closeSessionButton);
            this.Controls.Add(this.openSessionButton);
            this.Controls.Add(this.initButton);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Джавы-джуны";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button initButton;
        private System.Windows.Forms.Button openSessionButton;
        private System.Windows.Forms.Button closeSessionButton;
        private System.Windows.Forms.Button executeButton;
        private CueTextBox loginTextBox;
        private CueTextBox serverAdressTextBox;
        private CueTextBox serverPortTextBox;
        private System.Windows.Forms.Label connectionLabel;
        private CueTextBox passwordTextBox;
        private System.Windows.Forms.Label incorrectFields;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

