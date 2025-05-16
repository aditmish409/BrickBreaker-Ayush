
namespace BrickBreaker.Screens
{
    partial class HowToPlay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToPlay));
            this.Guide1 = new System.Windows.Forms.Label();
            this.Guide2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Guide1
            // 
            this.Guide1.AutoSize = true;
            this.Guide1.BackColor = System.Drawing.Color.Transparent;
            this.Guide1.ForeColor = System.Drawing.SystemColors.Control;
            this.Guide1.Location = new System.Drawing.Point(-3, 0);
            this.Guide1.Name = "Guide1";
            this.Guide1.Size = new System.Drawing.Size(325, 325);
            this.Guide1.TabIndex = 0;
            this.Guide1.Text = resources.GetString("Guide1.Text");
            // 
            // Guide2
            // 
            this.Guide2.AutoSize = true;
            this.Guide2.BackColor = System.Drawing.Color.Transparent;
            this.Guide2.ForeColor = System.Drawing.SystemColors.Control;
            this.Guide2.Location = new System.Drawing.Point(328, 0);
            this.Guide2.Name = "Guide2";
            this.Guide2.Size = new System.Drawing.Size(283, 299);
            this.Guide2.TabIndex = 1;
            this.Guide2.Text = resources.GetString("Guide2.Text");
            // 
            // HowToPlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BrickBreaker.Properties.Resources.Background;
            this.Controls.Add(this.Guide1);
            this.Controls.Add(this.Guide2);
            this.Name = "HowToPlay";
            this.Size = new System.Drawing.Size(634, 358);
            this.Load += new System.EventHandler(this.HowToPlay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Guide1;
        private System.Windows.Forms.Label Guide2;
    }
}
