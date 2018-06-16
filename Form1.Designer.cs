namespace BMPVisualizer
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
            this.readGarbageButton = new System.Windows.Forms.Button();
            this.widthNum = new System.Windows.Forms.NumericUpDown();
            this.heightNum = new System.Windows.Forms.NumericUpDown();
            this.garbageFileName = new System.Windows.Forms.TextBox();
            this.saveGarbageAsImageButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.widthNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNum)).BeginInit();
            this.SuspendLayout();
            // 
            // readGarbageButton
            // 
            this.readGarbageButton.Location = new System.Drawing.Point(483, 6);
            this.readGarbageButton.Name = "readGarbageButton";
            this.readGarbageButton.Size = new System.Drawing.Size(71, 28);
            this.readGarbageButton.TabIndex = 0;
            this.readGarbageButton.Text = "read";
            this.readGarbageButton.UseVisualStyleBackColor = true;
            this.readGarbageButton.Click += new System.EventHandler(this.readGarbageButton_Click);
            // 
            // widthNum
            // 
            this.widthNum.Location = new System.Drawing.Point(12, 12);
            this.widthNum.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.widthNum.Name = "widthNum";
            this.widthNum.Size = new System.Drawing.Size(120, 20);
            this.widthNum.TabIndex = 1;
            // 
            // heightNum
            // 
            this.heightNum.Location = new System.Drawing.Point(138, 12);
            this.heightNum.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.heightNum.Name = "heightNum";
            this.heightNum.Size = new System.Drawing.Size(120, 20);
            this.heightNum.TabIndex = 1;
            // 
            // garbageFileName
            // 
            this.garbageFileName.Location = new System.Drawing.Point(264, 11);
            this.garbageFileName.Name = "garbageFileName";
            this.garbageFileName.Size = new System.Drawing.Size(213, 20);
            this.garbageFileName.TabIndex = 2;
            this.garbageFileName.Text = "garbage.bmp";
            // 
            // saveGarbageAsImageButton
            // 
            this.saveGarbageAsImageButton.Location = new System.Drawing.Point(560, 6);
            this.saveGarbageAsImageButton.Name = "saveGarbageAsImageButton";
            this.saveGarbageAsImageButton.Size = new System.Drawing.Size(71, 28);
            this.saveGarbageAsImageButton.TabIndex = 0;
            this.saveGarbageAsImageButton.Text = "save";
            this.saveGarbageAsImageButton.UseVisualStyleBackColor = true;
            this.saveGarbageAsImageButton.Click += new System.EventHandler(this.saveGarbageAsImageButton_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 403);
            this.Controls.Add(this.garbageFileName);
            this.Controls.Add(this.heightNum);
            this.Controls.Add(this.widthNum);
            this.Controls.Add(this.saveGarbageAsImageButton);
            this.Controls.Add(this.readGarbageButton);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.widthNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readGarbageButton;
        private System.Windows.Forms.NumericUpDown widthNum;
        private System.Windows.Forms.NumericUpDown heightNum;
        private System.Windows.Forms.TextBox garbageFileName;
        private System.Windows.Forms.Button saveGarbageAsImageButton;
    }
}

