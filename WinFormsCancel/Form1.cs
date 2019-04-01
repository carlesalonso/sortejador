using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;

namespace WinFormsCancel
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cancel;
        int NombreAlumnes;
        public Form1()
        {
            InitializeComponent();
            textBox1.Select();
            label1.Text = "";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            circularProgressBar1.Value = 100;
            button2.Enabled = false;
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            UInt32 nombre;
            if (String.IsNullOrWhiteSpace(textBox1.Text) || 
                !UInt32.TryParse(textBox1.Text,out nombre) || nombre == 0)
            {
                MessageBox.Show("Cal entrar el nombre d'alumnes", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.ResetText();
                textBox1.Select();
            }
            else
                NombreAlumnes = (int)nombre;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            cancel = new CancellationTokenSource();
            var token = cancel.Token;
            try
            {
                button1.Enabled = false;
                textBox1.Enabled = false;
                button2.Enabled = true;
                var winner = CalculateRandomWinner();
                await StartCircleBar(token);
                label1.Text = $"{winner}!!!";
                await Task.Delay(TimeSpan.FromSeconds(2));

            }
            catch(Exception)
            {
                
            }
            finally 
            {
                circularProgressBar1.Value = 100;
                button1.Enabled = true;
                button2.Enabled = false;
                textBox1.Enabled = true;
                textBox1.ResetText();
                label1.ResetText();
                textBox1.Select();
                cancel.Dispose();
            }
            
        }

        private int CalculateRandomWinner()
        {
            var provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[4];
            provider.GetBytes(byteArray);

            //convert 4 bytes to an integer
            var randomInteger = BitConverter.ToUInt32(byteArray, 0);
            return (int)(randomInteger % NombreAlumnes + 1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancel.Cancel();
        }


        private  async Task StartCircleBar(CancellationToken token)
        {
            for (int i = 100; i >= 0; i--)
            {
                token.ThrowIfCancellationRequested();
                await Task.Delay(TimeSpan.FromSeconds(0.05));
                circularProgressBar1.Value = i;
            }
            await Task.Delay(700);
        }

        
    }
}
