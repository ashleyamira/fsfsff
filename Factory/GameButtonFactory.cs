using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Final_Project.Factory
{
    public abstract class ButtonFactory
    {
        public abstract Button CreateButton(string name);
    }

    public class GameButtonFactory : ButtonFactory
    {
        public override Button CreateButton(string name)
        {
            return new Button
            {
                Name = name,
                Text = name,
                Size = new Size(40, 40),
                BackColor = Color.White
            };
        }
    }
}
