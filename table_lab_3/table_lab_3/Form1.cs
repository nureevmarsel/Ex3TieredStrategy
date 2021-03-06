﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace table_lab_3
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            bttn_NumOfPoints.Enabled = false;
            bttn_NumOfWorkers.Enabled = false;
        }

        TextBox[,] linesMatrix = new TextBox[0, 0]; // матрица ребер
        int numOfWorkers, numOfPoints;

        private void txtbx_NumOfWorkers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) // Вводятся только числа
                e.Handled = true;

            if (e.KeyChar == (char)Keys.Back && txtbx_NumOfWorkers.SelectionStart != 0 &&
                txtbx_NumOfWorkers.Text != "") // Удаление по нажатию backspace
            {
                txtbx_NumOfWorkers.Text = txtbx_NumOfWorkers.Text.Substring(0, txtbx_NumOfWorkers.Text.Length - 1);
                txtbx_NumOfWorkers.SelectionStart = txtbx_NumOfWorkers.Text.Length;
            }
        }

        private void txtbx_NumOfWorkers_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtbx_NumOfWorkers.Text != "")
                bttn_NumOfWorkers.Enabled = true; // Разблокировали кнопку
            else
                bttn_NumOfWorkers.Enabled = false; // Заблокировали кнопку
        }
        private void bttn_NumOfWorkers_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtbx_NumOfWorkers.Text) > 15 || Convert.ToInt32(txtbx_NumOfWorkers.Text) < 2) // ограничение на кол-во работников
            {
                MessageBox.Show("Неверный ввод!\nКол-во исполнителей принимает значение от 2 до 15", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtbx_NumOfWorkers.Text = numOfWorkers.ToString();
            }
            else
            {
                numOfWorkers = Convert.ToInt32(txtbx_NumOfWorkers.Text); // получили кол-во работников
                bttn_NumOfWorkers.Enabled = false;
            }
        }

        private void txtbx_NumOfPoints_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar)) // Вводятся только числа
                e.Handled = true;

            if (e.KeyChar == (char)Keys.Back && txtbx_NumOfPoints.SelectionStart != 0 &&
                txtbx_NumOfPoints.Text != "") // Удаление по нажатию backspace
            {
                txtbx_NumOfPoints.Text = txtbx_NumOfPoints.Text.Substring(0, txtbx_NumOfPoints.Text.Length - 1);
                txtbx_NumOfPoints.SelectionStart = txtbx_NumOfPoints.Text.Length;
            }

        }

        private void txtbx_NumOfPoints_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtbx_NumOfPoints.Text != "")
                bttn_NumOfPoints.Enabled = true; // Разблокировали кнопку
            else
                bttn_NumOfPoints.Enabled = false; // Заблокировали кнопку
        }

        private void bttn_NumOfPoints_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtbx_NumOfPoints.Text) > 15 || Convert.ToInt32(txtbx_NumOfPoints.Text) < 2) // ограничение на кол-во вершин
            {
                MessageBox.Show("Неверный ввод!\nКол-во вершин принимает значение от 2 до 15", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtbx_NumOfPoints.Text = numOfPoints.ToString();
            }
            else
            {
                bttn_NumOfPoints.Enabled = false;

                for (int i = 0; i < linesMatrix.GetLength(0); i++) // удаление предыдущих текстбоксов
                {
                    for (int j = 0; j < linesMatrix.GetLength(1); j++)
                    {
                        Controls.Remove(linesMatrix[i, j]);
                    }
                }

                numOfPoints = Convert.ToInt32(txtbx_NumOfPoints.Text); // кол-во вершин

                linesMatrix = new TextBox[numOfPoints - 1, 2]; // матрица ребер

                int startX = 12; // левая граница по х
                int endX = 400; // правая граница по х

                int startY = 170; // верхняя граница по у
                int endY = 700; // нижняя граница по у

                int stepX = (endX - startX) / 2; // шаг по х
                int stepY = (endY - startY) / 15; // шаг по у

                int currX = startX;
                int currY = startY;

                // вывод текстбоксов
                for (int i = 0; i < linesMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < linesMatrix.GetLength(1); j++)
                    {
                        linesMatrix[i, j] = new TextBox();
                        linesMatrix[i, j].Location = new Point(currX, currY);
                        linesMatrix[i, j].Size = new Size(100, 30);
                        //matrix[i, j].Show();
                        Controls.Add(linesMatrix[i, j]);

                        if (j == 0)
                        {
                            currX += stepX;
                        }
                        else
                        {
                            currY += stepY;
                            currX = startX;
                        }
                    }
                }
            }

        }

        private void bttn_Start_Click(object sender, EventArgs e)
        {
            bool ok = true;

            if (numOfWorkers == 0)
            {
                ok = false;
                MessageBox.Show("Вы не ввели кол-во работников!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (ok)
            {
                if (linesMatrix.GetLength(0) == 0 || linesMatrix.GetLength(1) == 0)
                {
                    ok = false;
                    MessageBox.Show("Вы не ввели кол-во вершин!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (ok)
            {
                for (int i = 0; i < linesMatrix.GetLength(0); i++)
                {
                    if (ok)
                    {
                        for (int j = 0; j < linesMatrix.GetLength(1); j++)
                        {
                            if (linesMatrix[i, j].Text == "")
                            {
                                ok = false;
                                break;

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не все поля были заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
            }

            if (ok)
            {
                char[] firstColumn = new char[linesMatrix.GetLength(0)]; // первый столбец в ребрах
                char[] secondColumn = new char[linesMatrix.GetLength(0)]; // второй столбец в ребрах

                for(int i = 0; i < linesMatrix.GetLength(0); i++)
                {
                    firstColumn[i] = Convert.ToChar(linesMatrix[i, 0].Text);
                    secondColumn[i] = Convert.ToChar(linesMatrix[i, 1].Text);
                }

                //вызов функции
                char[,] result = new char[numOfPoints, numOfWorkers]; // временно

                dgv_table.RowCount = result.GetLength(0); // кол-во строк
                dgv_table.ColumnCount = result.GetLength(1); // кол-во столбцов
                dgv_table.RowHeadersWidth = 50; // Задали ширину столбца с названиями


                for (int i = 0; i < dgv_table.RowCount; i++)
                {
                    dgv_table.Rows[i].HeaderCell.Value = Convert.ToString(i + 1); // Название строк
                    dgv_table.Rows[i].Height = 450 / dgv_table.RowCount; // Высота строки

                    for (int j = 0; j < dgv_table.ColumnCount; j++)
                    {
                        dgv_table.Columns[j].HeaderText = Convert.ToString(j + 1); // Названия столбцов
                        dgv_table.Columns[j].Width = 625 / dgv_table.ColumnCount; // Ширина столбцов
                        dgv_table.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable; // Нельзя сортировать

                        dgv_table.Rows[i].Cells[j].Value = result[i, j];
                    }
                }
            }
        }

    }
}
