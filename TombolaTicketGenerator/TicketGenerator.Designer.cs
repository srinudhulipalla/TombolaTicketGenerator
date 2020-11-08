namespace TombolaTicketGenerator
{
    partial class TicketGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicketGenerator));
            this.button1 = new System.Windows.Forms.Button();
            this.saveTicketsDialog = new System.Windows.Forms.SaveFileDialog();
            this.lblTombolaTicketGenerator = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTicketCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.dotnet4TechiesLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtTicketCount)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button1.Location = new System.Drawing.Point(308, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblTombolaTicketGenerator
            // 
            this.lblTombolaTicketGenerator.AutoSize = true;
            this.lblTombolaTicketGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTombolaTicketGenerator.ForeColor = System.Drawing.Color.Blue;
            this.lblTombolaTicketGenerator.Location = new System.Drawing.Point(153, 36);
            this.lblTombolaTicketGenerator.Name = "lblTombolaTicketGenerator";
            this.lblTombolaTicketGenerator.Size = new System.Drawing.Size(414, 37);
            this.lblTombolaTicketGenerator.TabIndex = 1;
            this.lblTombolaTicketGenerator.Text = "Tombola Ticket Generator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(118, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Enter Number of tickets:";
            // 
            // txtTicketCount
            // 
            this.txtTicketCount.Location = new System.Drawing.Point(308, 142);
            this.txtTicketCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtTicketCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtTicketCount.Name = "txtTicketCount";
            this.txtTicketCount.Size = new System.Drawing.Size(114, 20);
            this.txtTicketCount.TabIndex = 4;
            this.txtTicketCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(428, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "(Min 1 and Max 1000)";
            // 
            // dotnet4TechiesLink
            // 
            this.dotnet4TechiesLink.AutoSize = true;
            this.dotnet4TechiesLink.Location = new System.Drawing.Point(12, 289);
            this.dotnet4TechiesLink.Name = "dotnet4TechiesLink";
            this.dotnet4TechiesLink.Size = new System.Drawing.Size(163, 13);
            this.dotnet4TechiesLink.TabIndex = 6;
            this.dotnet4TechiesLink.TabStop = true;
            this.dotnet4TechiesLink.Text = "https://www.dotnet4techies.com";
            this.dotnet4TechiesLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.dotnet4TechiesLink_LinkClicked);
            // 
            // TicketGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(713, 311);
            this.Controls.Add(this.dotnet4TechiesLink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTicketCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTombolaTicketGenerator);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TicketGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tombola Ticket Generator";
            ((System.ComponentModel.ISupportInitialize)(this.txtTicketCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveTicketsDialog;
        private System.Windows.Forms.Label lblTombolaTicketGenerator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtTicketCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel dotnet4TechiesLink;
    }
}