using System;
using System.IO;
using System.Windows.Forms;
using System.Security;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace lab6_2
{
    public partial class ExplorerForm : Form
    {
        private string currentPath = "";
        private Stack<string> navigationHistory = new Stack<string>();

        public ExplorerForm()
        {
            InitializeComponent();
            //C:\
            currentPath = Path.GetPathRoot(Environment.SystemDirectory);
            textBoxPath.Text = currentPath;
            LoadFilesAndFolders();
        }

        private void LoadFilesAndFolders()
        {
            try
            {
                listBoxFiles.Items.Clear();

                List<string> directories = new List<string>();
                List<string> files = new List<string>();

                try
                {
                    directories = Directory.GetDirectories(currentPath).ToList();
                    directories.Sort();

                    files = Directory.GetFiles(currentPath).ToList();
                    files.Sort(); 
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show($"Немає доступу до папки: {ex.Message}", "Помилка доступу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GoBack();
                    return;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Помилка безпеки: {ex.Message}", "Помилка доступу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GoBack();
                    return;
                }

                foreach (string directory in directories)
                {
                    listBoxFiles.Items.Add("[Папка] " + Path.GetFileName(directory));
                }

                foreach (string file in files)
                {
                    listBoxFiles.Items.Add("[Файл] " + Path.GetFileName(file));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні вмісту папки: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxFiles_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem != null)
            {
                string selectedItem = listBoxFiles.SelectedItem.ToString();

                if (selectedItem.StartsWith("[Папка]"))
                {
                    string folderName = selectedItem.Substring(8); // Видаляємо префікс "[Папка] "
                    string newPath = Path.Combine(currentPath, folderName);

                    try
                    {
                        Directory.GetFiles(newPath);

                        navigationHistory.Push(currentPath);

                        currentPath = newPath;
                        textBoxPath.Text = currentPath;
                        LoadFilesAndFolders();
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("У вас немає доступу до цієї папки.", "Помилка доступу", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не вдалося відкрити папку: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (selectedItem.StartsWith("[Файл]"))
                {
                    string fileName = selectedItem.Substring(7); // Видаляємо префікс "[Файл] "
                    string filePath = Path.Combine(currentPath, fileName);

                    try
                    {
                        if (!File.Exists(filePath))
                        {
                            MessageBox.Show("Файл не існує або був переміщений.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LoadFilesAndFolders();
                            return;
                        }

                        string extension = Path.GetExtension(filePath).ToLower();
                        string[] potentiallyDangerousExtensions = { ".exe", ".bat", ".cmd", ".vbs", ".js", ".ps1", ".msi" };

                        if (potentiallyDangerousExtensions.Contains(extension))
                        {
                            DialogResult result = MessageBox.Show(
                                $"Ви збираєтесь відкрити виконуваний файл ({extension}). Це може бути небезпечно. Продовжити?",
                                "Попередження безпеки",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                            if (result != DialogResult.Yes)
                                return;
                        }

                        System.Diagnostics.Process.Start(filePath);
                    }
                    catch (SecurityException ex)
                    {
                        MessageBox.Show($"Помилка безпеки: {ex.Message}", "Помилка безпеки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не вдалося відкрити файл: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void GoBack()
        {
            try
            {
                if (navigationHistory.Count > 0)
                {
                    currentPath = navigationHistory.Pop();
                }
                else
                {
                    DirectoryInfo parentDir = Directory.GetParent(currentPath);
                    if (parentDir != null)
                    {
                        currentPath = parentDir.FullName;
                    }
                    else
                    {
                        return;
                    }
                }

                textBoxPath.Text = currentPath;
                LoadFilesAndFolders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при поверненні: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadFilesAndFolders();
        }

        private string ShowInputDialog(string prompt, string title, string defaultValue = "")
        {
            Form inputForm = new Form
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label label = new Label
            {
                Left = 20,
                Top = 20,
                Width = 350,
                Text = prompt
            };

            TextBox textBox = new TextBox
            {
                Left = 20,
                Top = 50,
                Width = 350,
                Text = defaultValue
            };

            Button confirmButton = new Button
            {
                Text = "OK",
                Left = 210,
                Width = 80,
                Top = 80,
                DialogResult = DialogResult.OK
            };

            Button cancelButton = new Button
            {
                Text = "Скасувати",
                Left = 300,
                Width = 80,
                Top = 80,
                DialogResult = DialogResult.Cancel
            };

            inputForm.Controls.Add(label);
            inputForm.Controls.Add(textBox);
            inputForm.Controls.Add(confirmButton);
            inputForm.Controls.Add(cancelButton);

            inputForm.AcceptButton = confirmButton;
            inputForm.CancelButton = cancelButton;

            return inputForm.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            Regex invalidCharsRegex = new Regex(@"[\\/:*?""<>|]");
            if (invalidCharsRegex.IsMatch(fileName))
                return false;

            string[] reservedNames = { "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4",
                                      "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3",
                                      "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };

            return !reservedNames.Contains(fileName.ToUpper());
        }

        private void btnNewFolder_Click(object sender, EventArgs e)
        {
            string folderName = ShowInputDialog("Введіть ім'я нової папки:", "Нова папка");

            if (!string.IsNullOrWhiteSpace(folderName))
            {
                if (!IsValidFileName(folderName))
                {
                    MessageBox.Show("Ім'я папки містить недопустимі символи або є зарезервованим системним ім'ям.",
                                   "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    string newFolderPath = Path.Combine(currentPath, folderName);
                    if (!Directory.Exists(newFolderPath))
                    {
                        Directory.CreateDirectory(newFolderPath);
                        LoadFilesAndFolders();
                    }
                    else
                    {
                        MessageBox.Show("Папка з таким ім'ям вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при створенні папки: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNewFile_Click(object sender, EventArgs e)
        {
            string fileName = ShowInputDialog("Введіть ім'я нового файлу:", "Новий файл");

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                if (!IsValidFileName(fileName))
                {
                    MessageBox.Show("Ім'я файлу містить недопустимі символи або є зарезервованим системним ім'ям.",
                                   "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    string newFilePath = Path.Combine(currentPath, fileName);
                    if (!File.Exists(newFilePath))
                    {
                        using (FileStream fs = File.Create(newFilePath))
                        {
                        }
                        LoadFilesAndFolders();
                    }
                    else
                    {
                        MessageBox.Show("Файл з таким ім'ям вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при створенні файлу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem != null)
            {
                string selectedItem = listBoxFiles.SelectedItem.ToString();
                string itemName;
                bool isFolder = false;

                if (selectedItem.StartsWith("[Папка]"))
                {
                    itemName = selectedItem.Substring(8);
                    isFolder = true;
                }
                else if (selectedItem.StartsWith("[Файл]"))
                {
                    itemName = selectedItem.Substring(7);
                }
                else
                {
                    return;
                }

                string fullPath = Path.Combine(currentPath, itemName);

                if (isFolder)
                {
                    try
                    {
                        bool hasFiles = Directory.GetFiles(fullPath).Length > 0;
                        bool hasDirectories = Directory.GetDirectories(fullPath).Length > 0;

                        if (hasFiles || hasDirectories)
                        {
                            DialogResult result = MessageBox.Show(
                                $"Папка '{itemName}' містить вкладені файли або папки. Всі вони будуть видалені. Продовжити?",
                                "Підтвердження видалення",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                            if (result != DialogResult.Yes)
                                return;
                        }
                    }
                    catch (Exception)
                    {
                        // Якщо помилка доступу, показуємо далі
                    }
                }


                DialogResult confirmResult = MessageBox.Show(
                    $"Ви дійсно хочете видалити {(isFolder ? "папку" : "файл")} '{itemName}'?",
                    "Підтвердження видалення",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        if (isFolder)
                        {
                            Directory.Delete(fullPath, true);
                        }
                        else
                        {
                            File.Delete(fullPath);
                        }

                        LoadFilesAndFolders();
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("У вас немає прав на видалення цього елементу.", "Помилка доступу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Файл використовується іншим процесом: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при видаленні: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Виберіть файл або папку для видалення.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}