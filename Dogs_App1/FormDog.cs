using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dogs_App1
{
    public partial class FormDog : Form
    {
        private DataSet1 dataSet1;
        private bool isNew;
        private string editChipId;

        // Кличка: разрешаем русские и латинские буквы + дефис
        private static readonly Regex NameInputRegex = new Regex(@"^[a-zA-Zа-яА-ЯёЁ\-]$");
        // Кличка: полная проверка — с большой буквы, дальше буквы/дефис
        private static readonly Regex NameFullRegex = new Regex(@"^[A-ZА-ЯЁ][a-zA-Zа-яёА-ЯЁ\-]*$");

        // Порода: буквы, дефис и пробел (порода может состоять из нескольких слов)
        private static readonly Regex BreedInputRegex = new Regex(@"^[a-zA-Zа-яА-ЯёЁ\-\s]$");
        // Порода: полная проверка — каждое слово с заглавной буквы, буквы/дефис между
        private static readonly Regex BreedFullRegex = new Regex(@"^[A-ZА-ЯЁ][a-zA-Zа-яёА-ЯЁ\-]*(\s[A-ZА-ЯЁ][a-zA-Zа-яёА-ЯЁ\-]*)*$");

        public FormDog(DataSet1 ds, bool isNew, string editChipId = "")
        {
            InitializeComponent();
            this.dataSet1 = ds;
            this.isNew = isNew;
            this.editChipId = editChipId;
        }

        private void FormDog_Load(object sender, EventArgs e)
        {
            // Если это режим редактирования, загружаем данные в поля
            if (!isNew && !string.IsNullOrEmpty(editChipId))
            {
                // Находим собаку по Chip_ID
                DataRow[] foundRows = dataSet1.Dog.Select($"Chip_ID = '{editChipId}'");
                if (foundRows.Length > 0)
                {
                    DataRow dogRow = foundRows[0];
                    txtChipID.Text = dogRow["Chip_ID"].ToString();
                    txtName.Text = dogRow["Name"].ToString();
                    txtBreed.Text = dogRow["Breed"].ToString();

                    // Делаем Chip_ID недоступным для редактирования (это первичный ключ)
                    txtChipID.ReadOnly = true;
                    txtChipID.BackColor = SystemColors.Control;
                }
                this.Text = "Редактировать карточку собаки";
            }
            else
            {
                // Режим добавления: заголовок формы
                this.Text = "Новая карточка собаки";
            }
        }

        // ===== Валидация поля "Кличка" =====

        // Разрешаем только буквы, дефис и Backspace; цифры и всё остальное — блокируем
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) // Backspace, Delete и т.п.
                return;

            if (!NameInputRegex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
                return;
            }

            // Первый символ поля не может быть дефисом
            if (txtName.SelectionStart == 0 && e.KeyChar == '-')
            {
                e.Handled = true;
            }
        }

        // Автоматически делаем первую букву заглавной по мере ввода
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
                return;

            if (char.IsLower(txtName.Text[0]))
            {
                int pos = txtName.SelectionStart;
                txtName.Text = char.ToUpper(txtName.Text[0]) + txtName.Text.Substring(1);
                txtName.SelectionStart = pos;
            }
        }

        // ===== Валидация поля "Порода" =====

        // Разрешаем только буквы, дефис, пробел и Backspace
        private void txtBreed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) // Backspace, Delete и т.п.
                return;

            if (!BreedInputRegex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
                return;
            }

            // Нельзя, чтобы первым символом были пробел или дефис
            if (txtBreed.SelectionStart == 0 && (e.KeyChar == '-' || e.KeyChar == ' '))
            {
                e.Handled = true;
                return;
            }

            // Запрещаем два пробела подряд
            if (e.KeyChar == ' ' && txtBreed.SelectionStart > 0 &&
                txtBreed.Text[txtBreed.SelectionStart - 1] == ' ')
            {
                e.Handled = true;
            }
        }

        // Автоматическая капитализация первой буквы каждого слова
        private void txtBreed_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBreed.Text))
                return;

            int pos = txtBreed.SelectionStart;
            string text = txtBreed.Text;
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
                txtBreed.Text = newText;
                txtBreed.SelectionStart = pos;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChipID.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Заполните обязательные поля (Чип ID и Кличка)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Финальная проверка клички (на случай вставки текста из буфера обмена)
            if (!NameFullRegex.IsMatch(txtName.Text.Trim()))
            {
                MessageBox.Show(
                    "Кличка должна начинаться с заглавной буквы и содержать только буквы и дефис (без цифр и других символов)!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return;
            }

            // Финальная проверка породы (необязательное поле, но если заполнено — должно быть валидным)
            if (!string.IsNullOrWhiteSpace(txtBreed.Text) && !BreedFullRegex.IsMatch(txtBreed.Text.Trim()))
            {
                MessageBox.Show(
                    "Порода должна содержать только буквы, пробелы и дефис, каждое слово — с заглавной буквы!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBreed.Focus();
                return;
            }

            if (isNew)
            {
                // Проверка: не существует ли уже собака с таким Chip_ID
                if (dataSet1.Dog.Select($"Chip_ID = '{txtChipID.Text}'").Length > 0)
                {
                    MessageBox.Show("Собака с таким Chip ID уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ДОБАВЛЕНИЕ новой собаки
                DataRow newRow = dataSet1.Dog.NewRow();
                newRow["Chip_ID"] = txtChipID.Text;
                newRow["Name"] = txtName.Text;
                newRow["Breed"] = txtBreed.Text;
                dataSet1.Dog.Rows.Add(newRow);
            }
            else
            {
                // РЕДАКТИРОВАНИЕ существующей собаки
                DataRow[] foundRows = dataSet1.Dog.Select($"Chip_ID = '{editChipId}'");
                if (foundRows.Length > 0)
                {
                    DataRow dogRow = foundRows[0];
                    // Обновляем только имя и породу (Chip_ID не меняем, чтобы не ломать связи)
                    dogRow["Name"] = txtName.Text;
                    dogRow["Breed"] = txtBreed.Text;
                }
                // Подтверждаем изменения в таблице собак
                dataSet1.Dog.AcceptChanges();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}