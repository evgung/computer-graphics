namespace ComputerGraphics
{
    partial class StartForm
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
            button1 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            button2 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(107, 142);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(110, 41);
            button1.TabIndex = 0;
            button1.Text = "1. Растеризация";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(197, 93);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(227, 15);
            label1.TabIndex = 1;
            label1.Text = "Выберите номер лабораторной работы";
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(258, 142);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(106, 41);
            button2.TabIndex = 2;
            button2.Text = "2. Отсечение";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(414, 142);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(102, 41);
            button3.TabIndex = 3;
            button3.Text = "3. Заполнение";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(642, 315);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "StartForm";
            Text = "StartForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}