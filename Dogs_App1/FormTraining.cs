using System;
using System.Data;
using System.Windows.Forms;

namespace Dogs_App1
{
    public partial class FormTraining : Form
    {
        private DataSet1 dataSet;
        private bool isEdit;
        private int currentTrainingId;

        public FormTraining(DataSet1 ds, bool editMode, int trainingId = -1)
        {
            InitializeComponent();
            dataSet = ds;
            isEdit = editMode;
            currentTrainingId = trainingId;

            // Заполняем ComboBox собаками
            comboBoxDog.DataSource = dataSet.Dog;
            comboBoxDog.DisplayMember = "Name";
            comboBoxDog.ValueMember = "Chip_ID";

            // Виды занятий по Варианту 7
            comboBoxSessionType.Items.AddRange(new string[] { "Аджилити", "ОКД (Общий курс дрессировки)", "Охрана" });
            comboBoxSessionType.SelectedIndex = 0;

            if (isEdit && currentTrainingId != -1)
            {
                this.Text = "Редактировать тренировку";
                // ИСПОЛЬЗУЕМ ПРАВИЛЬНОЕ ИМЯ СТОЛБЦА: FK_Chip_ID
                var row = dataSet.TrainingSession.FindByTraining_ID(currentTrainingId);
                if (row != null)
                {
                    comboBoxDog.SelectedValue = row.FK_Chip_ID;  // ← ИСПРАВЛЕНО!
                    textBoxInstructor.Text = row.Instructor;
                    comboBoxSessionType.SelectedItem = row.SessionType;
                    numericUpDownDuration.Value = row.DurationMinutes;
                    numericUpDownCost.Value = row.Cost;
                }
            }
            else
            {
                this.Text = "Новая тренировка";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxInstructor.Text))
            {
                MessageBox.Show("Введите имя инструктора!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string chipId = comboBoxDog.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(chipId))
            {
                MessageBox.Show("Выберите собаку!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isEdit)
            {
                var row = dataSet.TrainingSession.FindByTraining_ID(currentTrainingId);
                if (row != null)
                {
                    row.FK_Chip_ID = chipId;  // ← ИСПРАВЛЕНО!
                    row.Instructor = textBoxInstructor.Text;
                    row.SessionType = comboBoxSessionType.SelectedItem.ToString();
                    row.DurationMinutes = (int)numericUpDownDuration.Value;
                    row.Cost = numericUpDownCost.Value;
                }
            }
            else
            {
                // Находим выбранную собаку в таблице Dog
                DataRowView selectedDog = (DataRowView)comboBoxDog.SelectedItem;
                DataSet1.DogRow dogRow = (DataSet1.DogRow)selectedDog.Row;

                // Добавляем тренировку, передавая строку собаки
                dataSet.TrainingSession.AddTrainingSessionRow(
                    dogRow,  // ← Передаём DogRow вместо string
                    textBoxInstructor.Text,
                    comboBoxSessionType.SelectedItem.ToString(),
                    (int)numericUpDownDuration.Value,
                    numericUpDownCost.Value
                );
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void labelDog_Click(object sender, EventArgs e)
        {

        }

        private void labelInstructor_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownDuration_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}