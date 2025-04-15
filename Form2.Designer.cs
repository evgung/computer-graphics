
namespace ComputerGraphics
{
    partial class Form2
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
            pictureBox = new System.Windows.Forms.PictureBox();
            clearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox.Location = new System.Drawing.Point(0, 0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new System.Drawing.Size(994, 722);
            pictureBox.TabIndex = 1;
            pictureBox.TabStop = false;
            pictureBox.Click += pictureBox_Click;
            pictureBox.Resize += pictureBox_Resize;
            // 
            // clearButton
            // 
            clearButton.Location = new System.Drawing.Point(0, 0);
            clearButton.Name = "clearButton";
            clearButton.Size = new System.Drawing.Size(43, 23);
            clearButton.TabIndex = 2;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(994, 722);
            Controls.Add(clearButton);
            Controls.Add(pictureBox);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button clearButton;
    }
}