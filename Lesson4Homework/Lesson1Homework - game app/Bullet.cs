using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{
    class Bullet : BaseObject
    {
        private Size _defaultSize = new Size(10, 1);

        public Bullet(Point position, Point speed, Size size) : base (position, speed, size) { }

        public override void Draw()
        {
            var g = GameLogic.Buffer.Graphics;
            g.DrawRectangle(Pens.DarkOrange, new Rectangle(_Position, _defaultSize));
        }

        public override void Update()
        {
            _Position.X += _Speed.X;
            _Position.Y += _Speed.Y;
        }
    }
}
