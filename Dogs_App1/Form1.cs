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
        }

        private void SaveToXML()
        {
            // Сохраняем и данные, и схему (важно для AutoIncrement)
            dataSet1.WriteXml(filePath, XmlWriteMode.WriteSchema);
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
        }

        // Сохраняем данные при закрытии программы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveToXML();
        }

        // 1. ИСПРАВЛЕННАЯ фильтрация тренировок при выборе собаки
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Используем точное имя столбца Chip_ID из твоего Designer.cs
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells["Chip_ID"].Value != DBNull.Value)
            {
                string selectedChipId = dataGridView1.CurrentRow.Cells["Chip_ID"].Value.ToString();

                // Используем точное имя столбца FK_Chip_ID
                (dataGridView2.DataSource as DataView).RowFilter = $"FK_Chip_ID = '{selectedChipId}'";
            }
            else
            {
                // Если собака не выбрана, показываем все тренировки
                (dataGridView2.DataSource as DataView).RowFilter = "";
            }
        }

        // 2. ЭТОТ МЕТОД УБИРАЕТ ОШИБКУ КОМПИЛЯЦИИ
        // Он нужен, потому что указан в Designer.cs, но раньше его не было в коде
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Пока оставляем пустым
        }

        // === КНОПКИ ДЛЯ СОБАК ===

        // Кнопка 1: Создать карточку собаки
        private void button1_Click(object sender, EventArgs e)
        {
            FormDog form = new FormDog(dataSet1, true); // true = режим добавления
            if (form.ShowDialog() == DialogResult.OK)
            {
                SaveToXML();
            }
        }

        // Кнопка 2: Редактировать карточку собаки
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                string chipId = dataGridView1.CurrentRow.Cells["Chip_ID"].Value.ToString();
                // Если твоя FormDog умеет принимать ID для редактирования, раскомментируй строку ниже:
                // FormDog form = new FormDog(dataSet1, false, chipId);
                // if (form.ShowDialog() == DialogResult.OK) SaveToXML();

                MessageBox.Show("Редактирование собаки (требуется доработка FormDog для передачи chipId).");
            }
            else
            {
                MessageBox.Show("Выберите собаку для редактирования.");
            }
        }

        // Кнопка 3: Удалить карточку собаки
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
                        rowView.Row.Delete();
                    }
                    SaveToXML();
                }
            }
            else
            {
                MessageBox.Show("Выберите собаку для удаления.");
            }
        }

        // === КНОПКИ ДЛЯ ТРЕНИРОВОК ===

        // Кнопка 4: Создать запись о тренировке
        private void button4_Click(object sender, EventArgs e)
        {
            // Здесь мы позже вызовем новую форму FormTraining
            MessageBox.Show("Здесь будет открываться форма добавления тренировки (FormTraining).");
        }

        // Кнопка 5: Редактировать запись о тренировке
        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Здесь будет открываться форма редактирования тренировки.");
        }

        // Кнопка 6: Удалить запись о тренировке
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
                MessageBox.Show("Выберите тренировку для удаления.");
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}