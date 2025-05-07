using System;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace Final_Project
{
    public partial class Form2 : Form
    {
        // Make backgroundPlayer static so it persists across forms
        public static WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        public static SoundPlayer backgroundPlayer;
        private SoundPlayer hoverSound;

        public Form2()
        {
            InitializeComponent();
            this.ControlBox = false;

            // Initialize and play the background music once
            backgroundPlayer = new SoundPlayer(new MemoryStream(Properties.Resources.backgroundSound));
            backgroundPlayer.PlayLooping(); // Start background music

            // Initialize the hover sound
            hoverSound = new SoundPlayer(new MemoryStream(Properties.Resources.anotherBgMusic));
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            // Don't stop the background music, let it keep playing
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();  // Hide the current form (Form2)
        }

        private void btn_start_MouseHover(object sender, EventArgs e)
        {
            btn_start.Image = Properties.Resources.start_hover;
            PlayHoverSound();
        }

        private void btn_start_MouseLeave(object sender, EventArgs e)
        {
            btn_start.Image = Properties.Resources.start_normal;
        }

        private void btn_option_Click(object sender, EventArgs e)
        {
            optionpage option = new optionpage();
            option.ShowDialog();
        }

        private void btn_option_MouseHover(object sender, EventArgs e)
        {
            btn_option.Image = Properties.Resources.option_hover;
            PlayHoverSound();
        }

        private void btn_option_MouseLeave(object sender, EventArgs e)
        {
            btn_option.Image = Properties.Resources.option_normal;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_exit_MouseHover(object sender, EventArgs e)
        {
            btn_exit.Image = Properties.Resources.exit_hover;
            PlayHoverSound();
        }

        private void btn_exit_MouseLeave(object sender, EventArgs e)
        {
            btn_exit.Image = Properties.Resources.exit_normal;
        }

        private void PlayHoverSound()
        {
            hoverSound.Play(); // Play the hover sound
        }
    }
}
