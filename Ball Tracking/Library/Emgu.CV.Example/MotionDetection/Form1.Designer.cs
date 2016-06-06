namespace MotionDetection
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.afterProcesImage = new Emgu.CV.UI.ImageBox();
            this.highlightBody = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.afterProcesImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Captured Image:";
            // 
            // afterProcesImage
            // 
            this.afterProcesImage.Location = new System.Drawing.Point(12, 95);
            this.afterProcesImage.Name = "afterProcesImage";
            this.afterProcesImage.Size = new System.Drawing.Size(1638, 896);
            this.afterProcesImage.TabIndex = 2;
            this.afterProcesImage.TabStop = false;
            // 
            // highlightBody
            // 
            this.highlightBody.AutoSize = true;
            this.highlightBody.Location = new System.Drawing.Point(15, 30);
            this.highlightBody.Name = "highlightBody";
            this.highlightBody.Size = new System.Drawing.Size(132, 21);
            this.highlightBody.TabIndex = 3;
            this.highlightBody.Text = "Highlight Bodies";
            this.highlightBody.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1662, 1003);
            this.Controls.Add(this.highlightBody);
            this.Controls.Add(this.afterProcesImage);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.afterProcesImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Emgu.CV.UI.ImageBox afterProcesImage;
        private System.Windows.Forms.CheckBox highlightBody;
    }
}

