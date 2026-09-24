using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dogs_App1
{
    public partial class FormTraining : Form
    {
        private DataSet1 dataSet;
        private bool isEdit;
        private string currentTrainingId;

        // Данные сохранённой (добавленной/отредактированной) тренировки — для выделения строк в главной форме
        public string SavedTrainingId { get; private set; }
        public string SavedChipId { get; private set; }

        // ФИО инструктора: буквы, дефис и пробел (несколько слов — Фамилия Имя Отчество)
        private static readonly Regex InstructorInputRegex = new Regex(@"^[a-zA-Zа-яА-ЯёЁ\-\s]$");
        // Полная проверка: каждое слово с заглавной буквы, буквы/дефис между
        private static readonly Regex InstructorFullRegex = new Regex(@"^[A-ZА-ЯЁ][a-zA-Zа-яёА-ЯЁ\-]*(\s[A-ZА-ЯЁ][a-zA-Zа-яёА-ЯЁ\-]*)*$");

        // ID тренировки: SS-ДД-ММ-ГГ (порядковый номер тренировки за день — дата создания)

        public FormTraining(DataSet1 ds, bool editMode, string trainingId = null, string preselectedChipId = null)
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

            if (isEdit && !string.IsNullOrEmpty(currentTrainingId))
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

                // Если создаём тренировку с уже выбранной в главной форме собакой — подставляем её сразу
                if (!string.IsNullOrEmpty(preselectedChipId))
                {
                    comboBoxDog.SelectedValue = preselectedChipId;
                }
            }
        }

        // Формирует ID тренировки: SS-ДД-ММ-ГГ, где SS — порядковый номер тренировки за указанный день
        private static string GenerateTrainingId(DataSet1 dataSet, DateTime date)
        {
            string datePart = date.ToString("dd-MM-yy");
            DataRow[] todaysRows = dataSet.TrainingSession.Select($"Training_ID LIKE '*-{datePart}'");
            int sequence = todaysRows.Length + 1;
            return sequence.ToString("00") + "-" + datePart;
        }

        // Собирает все ошибки заполнения формы сразу в один список и показывает одним окном
        private bool ValidateTrainingInput()
        {
            string errors = "";

            if (comboBoxDog.SelectedValue == null || string.IsNullOrEmpty(comboBoxDog.SelectedValue.ToString()))
            {
                errors += "Не выбрана собака.\n";
            }

            if (string.IsNullOrWhiteSpace(textBoxInstructor.Text))
            {
                errors += "Не заполнено ФИО инструктора.\n";
            }
            else if (!InstructorFullRegex.IsMatch(textBoxInstructor.Text.Trim()))
            {
                errors += "ФИО инструктора должно начинаться с заглавной буквы и содержать только буквы, пробел и дефис.\n";
            }

            if (comboBoxSessionType.SelectedItem == null)
            {
                errors += "Не выбран вид занятия.\n";
            }

            if (errors != "")
            {
                MessageBox.Show(errors, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateTrainingInput())
                return;

            string chipId = comboBoxDog.SelectedValue.ToString();

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
                SavedTrainingId = currentTrainingId;
            }
            else
            {
                // Находим выбранную собаку в таблице Dog
                DataRowView selectedDog = (DataRowView)comboBoxDog.SelectedItem;
                DataSet1.DogRow dogRow = (DataSet1.DogRow)selectedDog.Row;

                // Формируем ID тренировки по дате создания и добавляем новую строку
                string newTrainingId = GenerateTrainingId(dataSet, DateTime.Now);

                DataSet1.TrainingSessionRow newRow = dataSet.TrainingSession.NewTrainingSessionRow();
                newRow.Training_ID = newTrainingId;
                newRow.FK_Chip_ID = dogRow.Chip_ID;
                newRow.Instructor = textBoxInstructor.Text;
                newRow.SessionType = comboBoxSessionType.SelectedItem.ToString();
                newRow.DurationMinutes = (int)numericUpDownDuration.Value;
                newRow.Cost = numericUpDownCost.Value;
                dataSet.TrainingSession.Rows.Add(newRow);

                SavedTrainingId = newTrainingId;
            }

            SavedChipId = chipId;

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

        // Разрешаем только буквы, дефис, пробел и Backspace
        private void textBoxInstructor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) // Backspace, Delete и т.п.
                return;

            if (!InstructorInputRegex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
                return;
            }

            // Нельзя, чтобы первым символом были пробел или дефис
            if (textBoxInstructor.SelectionStart == 0 && (e.KeyChar == '-' || e.KeyChar == ' '))
            {
                e.Handled = true;
                return;
            }

            // Запрещаем два пробела подряд
            if (e.KeyChar == ' ' && textBoxInstructor.SelectionStart > 0 &&
                textBoxInstructor.Text[textBoxInstructor.SelectionStart - 1] == ' ')
            {
                e.Handled = true;
            }
        }

        // Автоматическая капитализация первой буквы каждого слова
        private void textBoxInstructor_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxInstructor.Text))
                return;

            int pos = textBoxInstructor.SelectionStart;
            string text = textBoxInstructor.Text;
            bool capitalizeNext = true;
            char[] chars = text.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == ' ')
                {
                    capitalizeNext = true;
                    continue;
                }

                if (capitalizeNext && char.IsLower(chars[i]))
                {
                    chars[i] = char.ToUpper(chars[i]);
                }
                capitalizeNext = false;
            }

            string newText = new string(chars);
            if (newText != text)
            {
                textBoxInstructor.Text = newText;
                textBoxInstructor.SelectionStart = pos;
            }
        }

    }
}