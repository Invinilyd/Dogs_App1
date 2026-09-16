namespace Dogs_App1
{
    partial class FormDog
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtChipID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtBreed = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(445, 323);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 59);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(231, 323);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 59);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Номер чипа собаки:";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Кличка собаки:";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Порода собаки:";
            this.label3.UseCompatibleTextRendering = true;
            // 
            // txtChipID
            // 
            this.txtChipID.Location = new System.Drawing.Point(297, 67);
            this.txtChipID.Multiline = true;
            this.txtChipID.Name = "txtChipID";
            this.txtChipID.Size = new System.Drawing.Size(331, 35);
            this.txtChipID.TabIndex = 6;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(297, 136);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(331, 35);
            this.txtName.TabIndex = 7;
            // 
            // txtBreed
            // 
            this.txtBreed.Location = new System.Drawing.Point(297, 217);
            this.txtBreed.Multiline = true;
            this.txtBreed.Name = "txtBreed";
            this.txtBreed.Size = new System.Drawing.Size(331, 35);
            this.txtBreed.TabIndex = 8;
            // 
            // FormDog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtBreed);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtChipID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Name = "FormDog";
            this.Text = "FormDog";
            this.Load += new System.EventHandler(this.FormDog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtChipID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtBreed;
    }
}