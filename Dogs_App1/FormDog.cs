using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Dogs_App1
{
    public partial class FormDog : Form
    {
        private DataSet1 dataSet1;
        private bool isNew;
        private string editChipId;

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChipID.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Заполните обязательные поля (Чип ID и Кличка)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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