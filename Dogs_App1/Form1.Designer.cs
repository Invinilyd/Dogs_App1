namespace Dogs_App1
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chipIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.breedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new Dogs_App1.DataSet1();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.trainingIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fKChipIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.instructorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sessionTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.durationMinutesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trainingSessionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainingSessionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chipIDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.breedDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dogBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(32, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(515, 142);
            this.dataGridView1.TabIndex = 0;
            // 
            // chipIDDataGridViewTextBoxColumn
            // 
            this.chipIDDataGridViewTextBoxColumn.DataPropertyName = "Chip_ID";
            this.chipIDDataGridViewTextBoxColumn.HeaderText = "ЧипID";
            this.chipIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.chipIDDataGridViewTextBoxColumn.Name = "chipIDDataGridViewTextBoxColumn";
            this.chipIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Кличка";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // breedDataGridViewTextBoxColumn
            // 
            this.breedDataGridViewTextBoxColumn.DataPropertyName = "Breed";
            this.breedDataGridViewTextBoxColumn.HeaderText = "Порода";
            this.breedDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.breedDataGridViewTextBoxColumn.Name = "breedDataGridViewTextBoxColumn";
            this.breedDataGridViewTextBoxColumn.Width = 150;
            // 
            // dogBindingSource
            // 
            this.dogBindingSource.DataMember = "Dog";
            this.dogBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trainingIDDataGridViewTextBoxColumn,
            this.fKChipIDDataGridViewTextBoxColumn,
            this.instructorDataGridViewTextBoxColumn,
            this.sessionTypeDataGridViewTextBoxColumn,
            this.durationMinutesDataGridViewTextBoxColumn,
            this.costDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.trainingSessionBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(79, 244);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 62;
            this.dataGridView2.RowTemplate.Height = 28;
            this.dataGridView2.Size = new System.Drawing.Size(964, 163);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // trainingIDDataGridViewTextBoxColumn
            // 
            this.trainingIDDataGridViewTextBoxColumn.DataPropertyName = "Training_ID";
            this.trainingIDDataGridViewTextBoxColumn.HeaderText = "IDТренировки";
            this.trainingIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.trainingIDDataGridViewTextBoxColumn.Name = "trainingIDDataGridViewTextBoxColumn";
            this.trainingIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // fKChipIDDataGridViewTextBoxColumn
            // 
            this.fKChipIDDataGridViewTextBoxColumn.DataPropertyName = "FK_Chip_ID";
            this.fKChipIDDataGridViewTextBoxColumn.HeaderText = "ЧипID";
            this.fKChipIDDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.fKChipIDDataGridViewTextBoxColumn.Name = "fKChipIDDataGridViewTextBoxColumn";
            this.fKChipIDDataGridViewTextBoxColumn.Width = 150;
            // 
            // instructorDataGridViewTextBoxColumn
            // 
            this.instructorDataGridViewTextBoxColumn.DataPropertyName = "Instructor";
            this.instructorDataGridViewTextBoxColumn.HeaderText = "Инструктор";
            this.instructorDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.instructorDataGridViewTextBoxColumn.Name = "instructorDataGridViewTextBoxColumn";
            this.instructorDataGridViewTextBoxColumn.Width = 150;
            // 
            // sessionTypeDataGridViewTextBoxColumn
            // 
            this.sessionTypeDataGridViewTextBoxColumn.DataPropertyName = "SessionType";
            this.sessionTypeDataGridViewTextBoxColumn.HeaderText = "ВидЗанятия";
            this.sessionTypeDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.sessionTypeDataGridViewTextBoxColumn.Name = "sessionTypeDataGridViewTextBoxColumn";
            this.sessionTypeDataGridViewTextBoxColumn.Width = 150;
            // 
            // durationMinutesDataGridViewTextBoxColumn
            // 
            this.durationMinutesDataGridViewTextBoxColumn.DataPropertyName = "DurationMinutes";
            this.durationMinutesDataGridViewTextBoxColumn.HeaderText = "ДлительностьМинут";
            this.durationMinutesDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.durationMinutesDataGridViewTextBoxColumn.Name = "durationMinutesDataGridViewTextBoxColumn";
            this.durationMinutesDataGridViewTextBoxColumn.Width = 150;
            // 
            // costDataGridViewTextBoxColumn
            // 
            this.costDataGridViewTextBoxColumn.DataPropertyName = "Cost";
            this.costDataGridViewTextBoxColumn.HeaderText = "Оплата";
            this.costDataGridViewTextBoxColumn.MinimumWidth = 8;
            this.costDataGridViewTextBoxColumn.Name = "costDataGridViewTextBoxColumn";
            this.costDataGridViewTextBoxColumn.Width = 150;
            // 
            // trainingSessionBindingSource
            // 
            this.trainingSessionBindingSource.DataMember = "TrainingSession";
            this.trainingSessionBindingSource.DataSource = this.dataSet1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(568, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 85);
            this.button1.TabIndex = 2;
            this.button1.Text = "Создать карточку собаки";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(754, 81);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 85);
            this.button2.TabIndex = 3;
            this.button2.Text = "Редактировать карточку собаки";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(924, 81);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(157, 85);
            this.button3.TabIndex = 4;
            this.button3.Text = "Удалить карточку собаки";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(79, 443);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(243, 66);
            this.button4.TabIndex = 5;
            this.button4.Text = "Создать запись о тренировке";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(458, 443);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(243, 66);
            this.button5.TabIndex = 6;
            this.button5.Text = "Редактировать запись о тренировке";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(800, 443);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(243, 66);
            this.button6.TabIndex = 7;
            this.button6.Text = "Удалить запись о тренировке";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 534);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainingSessionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.BindingSource dogBindingSource;
        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource trainingSessionBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridViewTextBoxColumn chipIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn breedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trainingIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fKChipIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn instructorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sessionTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn durationMinutesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn costDataGridViewTextBoxColumn;
    }
}