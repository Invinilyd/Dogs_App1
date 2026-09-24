using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dogs_App1
{
    public partial class FormDog : Form
    {
        private DataSet1 dataSet1;
        private bool isNew;
        private string editChipId;

        // Chip_ID сохранённой (добавленной/отредактированной) собаки — для выделения строки в главной форме
        public string SavedChipId { get; private set; }

        // Кличка: разрешаем русские и латинские буквы + дефис
        private static readonly Regex NameInputRegex = new Regex(@"^[a-zA-Zа-яА-ЯёЁ\-]$");
        // Кличка: полная проверка — с большой буквы, дальше буквы/дефис
        private static readonly Regex NameFullRegex = new Regex(@"^[A-ZА-ЯЁ][a-zA-Zа-яёА-ЯЁ\-]*$");

        // Порода: буквы, дефис и пробел (порода может состоять из нескольких слов)
        private static readonly Regex BreedInputRegex = new Regex(@"^[a-zA-Zа-яА-ЯёЁ\-\s]$");
        // Порода: полная проверка — каждое слово с заглавной буквы, буквы/дефис между
        private static readonly Regex BreedFullRegex = new Regex(@"^[A-ZА-ЯЁ][a-zA-Zа-яёА-ЯЁ\-]*(\s[A-ZА-ЯЁ][a-zA-Zа-яёА-ЯЁ\-]*)*$");

        // Чип ID: 15 цифр в формате 3-4-8, разделённые тире (например, 123-4567-89012345)
        private static readonly Regex ChipIdFullRegex = new Regex(@"^\d{3}-\d{4}-\d{8}$");

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

        // ===== Валидация поля "Номер чипа" =====

        // Разрешаем только цифры (тире добавляется автоматически) и Backspace, не больше 15 цифр
        private void txtChipID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) // Backspace, Delete и т.п.
                return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (CountDigits(txtChipID.Text) >= 15)
            {
                e.Handled = true;
            }
        }

        // Автоматически расставляем тире по маске 3-4-8
        private void txtChipID_TextChanged(object sender, EventArgs e)
        {
            string digits = new string(txtChipID.Text.Where(char.IsDigit).ToArray());
            if (digits.Length > 15)
                digits = digits.Substring(0, 15);

            string formatted = FormatChipId(digits);

            if (formatted != txtChipID.Text)
            {
                int digitsBeforeCursor = CountDigits(txtChipID.Text.Substring(0, txtChipID.SelectionStart));
                txtChipID.Text = formatted;
                txtChipID.SelectionStart = PositionAfterDigitCount(formatted, digitsBeforeCursor);
            }
        }

        private static int CountDigits(string text)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (char.IsDigit(c)) count++;
            }
            return count;
        }

        private static string FormatChipId(string digits)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < digits.Length; i++)
            {
                if (i == 3 || i == 7)
                    sb.Append('-');
                sb.Append(digits[i]);
            }
            return sb.ToString();
        }

        private static int PositionAfterDigitCount(string formatted, int digitCount)
        {
            if (digitCount <= 0)
                return 0;

            int seen = 0;
            for (int i = 0; i < formatted.Length; i++)
            {
                if (char.IsDigit(formatted[i]))
                {
                    seen++;
                    if (seen == digitCount)
                        return i + 1;
                }
            }
            return formatted.Length;
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

        // Собирает все ошибки заполнения формы сразу в один список и показывает одним окном
        private bool ValidateDogInput()
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(txtChipID.Text))
            {
                errors += "Не заполнен номер чипа.\n";
            }
            else if (!ChipIdFullRegex.IsMatch(txtChipID.Text.Trim()))
            {
                errors += "Номер чипа должен состоять из 15 цифр в формате XXX-XXXX-XXXXXXXX.\n";
            }
            else if (isNew && dataSet1.Dog.Select($"Chip_ID = '{txtChipID.Text.Trim()}'").Length > 0)
            {
                errors += "Собака с таким Chip ID уже существует.\n";
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errors += "Не заполнена кличка.\n";
            }
            else if (!NameFullRegex.IsMatch(txtName.Text.Trim()))
            {
                errors += "Кличка должна начинаться с заглавной буквы и содержать только буквы и дефис.\n";
            }

            if (!string.IsNullOrWhiteSpace(txtBreed.Text) && !BreedFullRegex.IsMatch(txtBreed.Text.Trim()))
            {
                errors += "Порода должна содержать только буквы, пробелы и дефис, каждое слово — с заглавной буквы.\n";
            }

            if (errors != "")
            {
                MessageBox.Show(errors, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateDogInput())
                return;

            if (isNew)
            {
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
            }

            SavedChipId = txtChipID.Text.Trim();

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