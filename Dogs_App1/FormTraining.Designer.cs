namespace Dogs_App1
{
    partial class FormTraining
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
            this.labelDog = new System.Windows.Forms.Label();
            this.comboBoxDog = new System.Windows.Forms.ComboBox();
            this.labelInstructor = new System.Windows.Forms.Label();
            this.textBoxInstructor = new System.Windows.Forms.TextBox();
            this.labelSessionType = new System.Windows.Forms.Label();
            this.comboBoxSessionType = new System.Windows.Forms.ComboBox();
            this.labelDuration = new System.Windows.Forms.Label();
            this.numericUpDownDuration = new System.Windows.Forms.NumericUpDown();
            this.labelCost = new System.Windows.Forms.Label();
            this.numericUpDownCost = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCost)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDog
            // 
            this.labelDog.AutoSize = true;
            this.labelDog.Location = new System.Drawing.Point(54, 55);
            this.labelDog.Name = "labelDog";
            this.labelDog.Size = new System.Drawing.Size(58, 16);
            this.labelDog.TabIndex = 0;
            this.labelDog.Text = "Собака:";
            this.labelDog.Click += new System.EventHandler(this.labelDog_Click);
            // 
            // comboBoxDog
            // 
            this.comboBoxDog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDog.FormattingEnabled = true;
            this.comboBoxDog.Location = new System.Drawing.Point(287, 55);
            this.comboBoxDog.Name = "comboBoxDog";
            this.comboBoxDog.Size = new System.Drawing.Size(235, 24);
            this.comboBoxDog.TabIndex = 1;
            // 
            // labelInstructor
            // 
            this.labelInstructor.AutoSize = true;
            this.labelInstructor.Location = new System.Drawing.Point(54, 119);
            this.labelInstructor.Name = "labelInstructor";
            this.labelInstructor.Size = new System.Drawing.Size(88, 16);
            this.labelInstructor.TabIndex = 2;
            this.labelInstructor.Text = "Инструктор:";
            this.labelInstructor.Click += new System.EventHandler(this.labelInstructor_Click);
            // 
            // textBoxInstructor
            // 
            this.textBoxInstructor.Location = new System.Drawing.Point(288, 113);
            this.textBoxInstructor.Name = "textBoxInstructor";
            this.textBoxInstructor.Size = new System.Drawing.Size(234, 22);
            this.textBoxInstructor.TabIndex = 3;
            // 
            // labelSessionType
            // 
            this.labelSessionType.AutoSize = true;
            this.labelSessionType.Location = new System.Drawing.Point(51, 174);
            this.labelSessionType.Name = "labelSessionType";
            this.labelSessionType.Size = new System.Drawing.Size(91, 16);
            this.labelSessionType.TabIndex = 4;
            this.labelSessionType.Text = "Вид занятия:";
            // 
            // comboBoxSessionType
            // 
            this.comboBoxSessionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSessionType.FormattingEnabled = true;
            this.comboBoxSessionType.Location = new System.Drawing.Point(287, 166);
            this.comboBoxSessionType.Name = "comboBoxSessionType";
            this.comboBoxSessionType.Size = new System.Drawing.Size(235, 24);
            this.comboBoxSessionType.TabIndex = 5;
            // 
            // labelDuration
            // 
            this.labelDuration.AutoSize = true;
            this.labelDuration.Location = new System.Drawing.Point(51, 233);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(138, 16);
            this.labelDuration.TabIndex = 6;
            this.labelDuration.Text = "Длительность (мин):";
            // 
            // numericUpDownDuration
            // 
            this.numericUpDownDuration.Location = new System.Drawing.Point(288, 227);
            this.numericUpDownDuration.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDuration.Name = "numericUpDownDuration";
            this.numericUpDownDuration.Size = new System.Drawing.Size(155, 22);
            this.numericUpDownDuration.TabIndex = 7;
            this.numericUpDownDuration.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownDuration.ValueChanged += new System.EventHandler(this.numericUpDownDuration_ValueChanged);
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Location = new System.Drawing.Point(54, 296);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(59, 16);
            this.labelCost.TabIndex = 8;
            this.labelCost.Text = "Оплата:";
            // 
            // numericUpDownCost
            // 
            this.numericUpDownCost.DecimalPlaces = 2;
            this.numericUpDownCost.Location = new System.Drawing.Point(288, 290);
            this.numericUpDownCost.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownCost.Name = "numericUpDownCost";
            this.numericUpDownCost.Size = new System.Drawing.Size(155, 22);
            this.numericUpDownCost.TabIndex = 9;
            this.numericUpDownCost.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(166, 365);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(385, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormTraining
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numericUpDownCost);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.numericUpDownDuration);
            this.Controls.Add(this.labelDuration);
            this.Controls.Add(this.comboBoxSessionType);
            this.Controls.Add(this.labelSessionType);
            this.Controls.Add(this.textBoxInstructor);
            this.Controls.Add(this.labelInstructor);
            this.Controls.Add(this.comboBoxDog);
            this.Controls.Add(this.labelDog);
            this.Name = "FormTraining";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "FormTraining";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelDog;
        private System.Windows.Forms.ComboBox comboBoxDog;
        private System.Windows.Forms.Label labelInstructor;
        private System.Windows.Forms.TextBox textBoxInstructor;
        private System.Windows.Forms.Label labelSessionType;
        private System.Windows.Forms.ComboBox comboBoxSessionType;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.NumericUpDown numericUpDownDuration;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.NumericUpDown numericUpDownCost;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}