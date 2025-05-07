using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using WMPLib;


namespace Final_Project
{
    public partial class Form2: Form
    {
        public static WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        public Form2()
        {
            InitializeComponent();
            wplayer.URL = (@"D:\BRICK BREAKER\BRICK BREAKER\Resources\hhhhhhhhhhhhhhhh.wav");
            wplayer.controls.play();
            axWindowsMediaPlayer1.Hide();
        }
        
        private void btn_start_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void btn_start_MouseHover(object sender, EventArgs e)
        {
            btn_start.Image = Properties.Resources.start_hover;
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer(@"D:\BRICK BREAKER\BRICK BREAKER\Resources\background sound.wav");
            sound.Play();
        }

        private void btn_option_Click(object sender, EventArgs e)
        {
            optionpage option = new optionpage();
            option.ShowDialog();
        }

        private void btn_start_MouseLeave(object sender, EventArgs e)
        {
            btn_start.Image = Properties.Resources.start_normal;
        }

        private void btn_option_MouseHover(object sender, EventArgs e)
        {
            btn_option.Image = Properties.Resources.option_hover;
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer(@"D:\BRICK BREAKER\BRICK BREAKER\Resources\background sound.wav");
            sound.Play();
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
            System.Media.SoundPlayer sound = new System.Media.SoundPlayer(@"D:\BRICK BREAKER\BRICK BREAKER\Resources\background sound.wav");
            sound.Play();
        }

        private void btn_exit_MouseLeave(object sender, EventArgs e)
        {
            btn_exit.Image = Properties.Resources.exit_normal;
        }
    }
}
