using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncWinForms
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cts;

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            _cts = new CancellationTokenSource();
            btnStart.Enabled = false;
            btnCancel.Enabled = true;
            progressBar.Value = 0;

            try
            {
                int result = await Task.Run(() => LongRunningOperation(_cts.Token));
                lblStatus.Text = $"result: {result}";
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "canceled";
            }
            finally
            {
                btnStart.Enabled = true;
                btnCancel.Enabled = false;
            }
        }

        private int LongRunningOperation(CancellationToken token)
        {
            int result = 0;
            for (int i = 1; i <= 100; i++)
            {
                Thread.Sleep(100);
                result += i;

                Invoke((Action)(() =>
                {
                    progressBar.Value = i;
                    lblStatus.Text = $"progress: {i}%";
                }));

                if (token.IsCancellationRequested)
                    throw new OperationCanceledException();
            }
            return result;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }


        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.Checked)
            {
                if (radioButton == radioButton1)
                {
                    MessageBox.Show("Option 1 selected", "Info");
                }
                else if (radioButton == radioButton2)
                {
                    MessageBox.Show("Option 2 selected", "Info");
                }
                else if (radioButton == radioButton3)
                {
                    MessageBox.Show("Option 3 selected", "Info");
                }
            }
        }

    }
}