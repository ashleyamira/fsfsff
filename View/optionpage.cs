using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;


namespace Final_Project
{
    public partial class optionpage: Form
    {
        public optionpage()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Form2.wplayer.controls.play();
            btn_sound.Image = Properties.Resources.sound_on;
            Form2.wplayer.settings.volume = trackBar1.Value;

            if (trackBar1.Value == 0)
            {
                btn_sound.Image = Properties.Resources.sound_off;
            }
        }
    }
}
