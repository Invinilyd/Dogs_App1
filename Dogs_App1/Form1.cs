using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Dogs_App1
{
    public partial class Form1 : Form
    {
        private readonly string filePath = "Data.xml";

        public Form1()
        {
            InitializeComponent();
            // dataSet1 уже создан в Form1.Designer.cs, здесь его объявлять не нужно!
        }

        private void SaveToXML()
        {
            dataSet1.WriteXml(filePath, XmlWriteMode.WriteSchema);
            UpdateButtonStates();
        }

        private void LoadFromXML()
        {
            dataSet1.Clear();
            if (File.Exists(filePath))
            {
                dataSet1.ReadXml(filePath, XmlReadMode.ReadSchema);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadFromXML();
            dataGridView1.DataSource = dataSet1.Dog;
            dataGridView2.DataSource = dataSet1.TrainingSession;

            // ВАЖНО: Блок с ручным изменением HeaderText УДАЛЕН. 
            // Русские заголовки ("ЧипID", "Кличка" и т.д.) уже настроены в Form1.Designer.cs!

            UpdateButtonStates();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToXML();
        }

        private void UpdateButtonStates()
        {
            bool hasDogs = dataSet1.Dog.Rows.Count > 0;
            bool hasTrainings = dataSet1.TrainingSession.Rows.Count > 0;

            button2.Enabled = hasDogs;
            button3.Enabled = hasDogs;
            button4.Enabled = hasDogs;
            button5.Enabled = hasDogs && hasTrainings;
            button6.Enabled = hasDogs && hasTrainings;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells["Chip_ID"].Value != DBNull.Value)
            {
                string selectedChipId = dataGridView1.CurrentRow.Cells["Chip_ID"].Value.ToString();
                (dataGridView2.DataSource as DataView).RowFilter = $"FK_Chip_ID = '{selectedChipId}'";
            }
            else
            {
                (dataGridView2.DataSource as DataView).RowFilter = "";
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Оставляем пустым, как требует Designer
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormDog form = new FormDog(dataSet1, true);
            if (form.ShowDialog() == DialogResult.OK)
            {
                SaveToXML();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataRowView rowView = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
                if (rowView != null)
                {
                    string chipId = rowView.Row["Chip_ID"].ToString();
                    FormDog form = new FormDog(dataSet1, false, chipId);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        SaveToXML();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите собаку для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (MessageBox.Show("Вы уверены? Это также удалит все связанные тренировки этой собаки.",
                    "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRowView rowView = dataGridView1.CurrentRow.DataBoundItem as DataRowView;
                    if (rowView != null)
                    {
                        string chipId = rowView.Row["Chip_ID"].ToString();
                        DataRow[] relatedTrainings = dataSet1.TrainingSession.Select($"FK_Chip_ID = '{chipId}'");
                        foreach (DataRow training in relatedTrainings)
                        {
                            training.Delete();
                        }
                        rowView.Row.Delete();
                    }
                    SaveToXML();
                }
            }
            else
            {
                MessageBox.Show("Выберите собаку для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataSet1.Dog.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавьте хотя бы одну собаку!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FormTraining form = new FormTraining(dataSet1, false);
            if (form.ShowDialog() == DialogResult.OK)
            {
                SaveToXML();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                try
                {
                    DataRowView rowView = dataGridView2.CurrentRow.DataBoundItem as DataRowView;
                    if (rowView != null)
                    {
                        int trainingId = -1;

                        if (rowView.Row.Table.Columns.Contains("Training_ID"))
                            trainingId = Convert.ToInt32(rowView.Row["Training_ID"]);
                        else if (rowView.Row.Table.Columns.Contains("TrainingID"))
                            trainingId = Convert.ToInt32(rowView.Row["TrainingID"]);
                        else if (rowView.Row.Table.Columns.Contains("ID"))
                            trainingId = Convert.ToInt32(rowView.Row["ID"]);
                        else
                            trainingId = Convert.ToInt32(rowView.Row[0]);

                        FormTraining form = new FormTraining(dataSet1, true, trainingId);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            SaveToXML();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при редактировании: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите тренировку для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                if (MessageBox.Show("Удалить эту запись о тренировке?",
                    "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataRowView rowView = dataGridView2.CurrentRow.DataBoundItem as DataRowView;
                    if (rowView != null)
                    {
                        rowView.Row.Delete();
                    }
                    SaveToXML();
                }
            }
            else
            {
                MessageBox.Show("Выберите тренировку для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}