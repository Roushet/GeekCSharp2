using System.Drawing;
using System.Windows.Forms;
namespace GameApp
{
    static class Input
    {

        public static int Speed;
        private static int _angle;
        public static double Angle;


        public static void PlayerKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) Speed += 1;
            if (e.KeyCode == Keys.Down) Speed -= 1;

            if (e.KeyCode == Keys.Left) _angle += 10;
            if (e.KeyCode == Keys.Right) _angle -= 10;

            Angle = (_angle * System.Math.PI) / 180;
        }

    }
}

