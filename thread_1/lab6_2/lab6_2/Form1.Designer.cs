using System;
using System.Windows.Forms;
using System.Drawing;

namespace lab6_2
{
    partial class ExplorerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textBoxPath = new TextBox();
            this.listBoxFiles = new ListBox();
            this.btnReload = new Button();
            this.btnGoBack = new Button();
            this.btnRemove = new Button();
            this.btnNewFolder = new Button();
            this.btnNewFile = new Button();

            this.SuspendLayout();

            // === Палітра кольорів ===
            Color primaryColor = Color.FromArgb(240, 248, 255);
            Color buttonBgColor = Color.FromArgb(220, 230, 241);

            Font buttonFont = new Font("Segoe UI", 10, FontStyle.Bold);

            // === textBoxPath ===
            this.textBoxPath.Location = new Point(125, 18);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new Size(480, 25);
            this.textBoxPath.TabIndex = 0;
            this.textBoxPath.BackColor = Color.WhiteSmoke;
            this.textBoxPath.BorderStyle = BorderStyle.FixedSingle;

            // === listBoxFiles ===
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.ItemHeight = 18;
            this.listBoxFiles.Location = new Point(60, 55);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new Size(680, 238);
            this.listBoxFiles.TabIndex = 1;
            this.listBoxFiles.Font = new Font("Consolas", 10);
            this.listBoxFiles.DoubleClick += new EventHandler(this.listBoxFiles_DoubleClick);
            this.listBoxFiles.BackColor = Color.White;


            // === Buttons ===

            // btnGoBack
            this.btnGoBack.Text = "⟵";
            this.btnGoBack.Font = buttonFont;
            this.btnGoBack.FlatStyle = FlatStyle.Flat;
            this.btnGoBack.BackColor = buttonBgColor;
            this.btnGoBack.ForeColor = Color.Black;
            this.btnGoBack.Location = new Point(20, 10);
            this.btnGoBack.Size = new Size(45, 40);
            this.btnGoBack.FlatAppearance.BorderSize = 0;
            this.btnGoBack.Click += new EventHandler(this.btnGoBack_Click);

            // btnReload
            this.btnReload.Text = "⟳";
            this.btnReload.Font = buttonFont;
            this.btnReload.FlatStyle = FlatStyle.Flat;
            this.btnReload.BackColor = buttonBgColor;
            this.btnReload.ForeColor = Color.Black;
            this.btnReload.Location = new Point(70, 10);
            this.btnReload.Size = new Size(45, 40);
            this.btnReload.FlatAppearance.BorderSize = 0;
            this.btnReload.Click += new EventHandler(this.btnReload_Click);

            // btnNewFolder
            this.btnNewFolder.Text = "📁";
            this.btnNewFolder.Font = buttonFont;
            this.btnNewFolder.FlatStyle = FlatStyle.Flat;
            this.btnNewFolder.BackColor = buttonBgColor;
            this.btnNewFolder.ForeColor = Color.Black;
            this.btnNewFolder.Location = new Point(620, 12);
            this.btnNewFolder.Size = new Size(40, 43);
            this.btnNewFolder.FlatAppearance.BorderSize = 0;
            this.btnNewFolder.Click += new EventHandler(this.btnNewFolder_Click);

            // btnNewFile
            this.btnNewFile.Text = "📃";
            this.btnNewFile.Font = buttonFont;
            this.btnNewFile.FlatStyle = FlatStyle.Flat;
            this.btnNewFile.BackColor = buttonBgColor;
            this.btnNewFile.ForeColor = Color.Black;
            this.btnNewFile.Location = new Point(665, 12);
            this.btnNewFile.Size = new Size(45, 43);
            this.btnNewFile.FlatAppearance.BorderSize = 0;
            this.btnNewFile.Click += new EventHandler(this.btnNewFile_Click);

            // btnRemove
            this.btnRemove.Text = "🗑";
            this.btnRemove.Font = buttonFont;
            this.btnRemove.FlatStyle = FlatStyle.Flat;
            this.btnRemove.BackColor = buttonBgColor;
            this.btnRemove.ForeColor = Color.Black;
            this.btnRemove.Location = new Point(715, 12);
            this.btnRemove.Size = new Size(35, 43);
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.Click += new EventHandler(this.btnRemove_Click);

            // === Form Config ===
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = primaryColor;
            this.ClientSize = new Size(800, 320);
            this.Controls.Add(this.btnNewFile);
            this.Controls.Add(this.btnNewFolder);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnGoBack);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.listBoxFiles);
            this.Controls.Add(this.textBoxPath);
            this.Name = "ExplorerForm";
            this.Text = "Файловий Менеджер";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Оголошення змінних для контролів
        private TextBox textBoxPath;
        private ListBox listBoxFiles;
        private Button btnReload;
        private Button btnGoBack;
        private Button btnRemove;
        private Button btnNewFolder;
        private Button btnNewFile;
    }
}