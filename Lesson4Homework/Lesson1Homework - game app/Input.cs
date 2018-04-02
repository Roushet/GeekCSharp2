using System.Drawing;
using System.Windows.Forms;

namespace GameApp
{
    static class Input
    {
        public static void PlayerKey(object sender, KeyEventArgs e)
        {
            Player player = Player.GetPlayer();

            if (e.KeyCode == Keys.Up) player.LinearSpeed += 1;
            if (e.KeyCode == Keys.Down) player.LinearSpeed -= 1;

            if (e.KeyCode == Keys.Left)  player.ShipAngle += 10;
            if (e.KeyCode == Keys.Right) player.ShipAngle -= 10;

            if (e.KeyCode == Keys.Space) player.MakeShot();

        }

    }
}

