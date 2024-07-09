namespace SpoolerCleaner
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnClean = new Button();
            btnExit = new Button();
            ssMain = new StatusStrip();
            sslblMessage = new ToolStripStatusLabel();
            sspbTask = new ToolStripProgressBar();
            lblAuthor = new Label();
            ssMain.SuspendLayout();
            SuspendLayout();
            // 
            // btnClean
            // 
            btnClean.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnClean.BackgroundImage = Resources.broom;
            btnClean.BackgroundImageLayout = ImageLayout.Zoom;
            btnClean.FlatStyle = FlatStyle.Popup;
            btnClean.Font = new Font("Consolas", 12F);
            btnClean.Location = new Point(12, 12);
            btnClean.Name = "btnClean";
            btnClean.Size = new Size(407, 170);
            btnClean.TabIndex = 0;
            btnClean.Text = "&Limpiar";
            btnClean.TextAlign = ContentAlignment.BottomRight;
            btnClean.TextImageRelation = TextImageRelation.ImageAboveText;
            btnClean.UseVisualStyleBackColor = true;
            btnClean.Click += btnClean_Click;
            // 
            // btnExit
            // 
            btnExit.FlatStyle = FlatStyle.Popup;
            btnExit.Font = new Font("Consolas", 12F);
            btnExit.Location = new Point(12, 188);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(407, 42);
            btnExit.TabIndex = 1;
            btnExit.Text = "&Salir";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // ssMain
            // 
            ssMain.Items.AddRange(new ToolStripItem[] { sslblMessage, sspbTask });
            ssMain.Location = new Point(0, 260);
            ssMain.Name = "ssMain";
            ssMain.Size = new Size(431, 28);
            ssMain.TabIndex = 2;
            ssMain.Text = "statusStrip1";
            // 
            // sslblMessage
            // 
            sslblMessage.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Top | ToolStripStatusLabelBorderSides.Right | ToolStripStatusLabelBorderSides.Bottom;
            sslblMessage.BorderStyle = Border3DStyle.SunkenOuter;
            sslblMessage.Font = new Font("Consolas", 12F);
            sslblMessage.Name = "sslblMessage";
            sslblMessage.Overflow = ToolStripItemOverflow.Never;
            sslblMessage.Size = new Size(183, 23);
            sslblMessage.Spring = true;
            // 
            // sspbTask
            // 
            sspbTask.Alignment = ToolStripItemAlignment.Right;
            sspbTask.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            sspbTask.Name = "sspbTask";
            sspbTask.Overflow = ToolStripItemOverflow.Never;
            sspbTask.RightToLeft = RightToLeft.No;
            sspbTask.Size = new Size(200, 22);
            // 
            // lblAuthor
            // 
            lblAuthor.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblAuthor.AutoSize = true;
            lblAuthor.Location = new Point(195, 238);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(224, 15);
            lblAuthor.TabIndex = 3;
            lblAuthor.Text = "Developed by Carlos León Bolaños (2024)";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(431, 288);
            Controls.Add(lblAuthor);
            Controls.Add(ssMain);
            Controls.Add(btnExit);
            Controls.Add(btnClean);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = Resources.printerreset;
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SpoolCleaner";
            FormClosing += frmMain_FormClosing;
            ssMain.ResumeLayout(false);
            ssMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnClean;
        private Button btnExit;
        private StatusStrip ssMain;
        private ToolStripStatusLabel sslblMessage;
        private ToolStripProgressBar sspbTask;
        private Label lblAuthor;
    }
}
