using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyProject
{
    public partial class StartScreen : Form
    {

        private SoundPlayer player = new SoundPlayer();

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        private const int MAX_VOLUME = 65535;
        private const int MIN_VOLUME = 0;

        public StartScreen()
        {
            InitializeComponent();
            player.SoundLocation = "music.wav";
            player.PlayLooping();
        }

        private void musicBar_Scroll(object sender, EventArgs e)
        {
            int volume = (int)((double)musicBar.Value / musicBar.Maximum * MAX_VOLUME);
            waveOutSetVolume(IntPtr.Zero, (uint)((ulong)volume << 16 | (ulong)volume));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            this.Hide();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ControlsScreen controlsscreen = new ControlsScreen();
            controlsscreen.FormClosed += new FormClosedEventHandler(ControlsScreen_FormClosed);
            this.Hide();
            controlsscreen.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void ControlsScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}
