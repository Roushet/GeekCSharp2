using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{
    class Asteroid : BaseObject
    {

        //public Asteroid(Point position, Point speed, Size size) : base(position, speed, size) { }

        public Asteroid(Point position, Point speed, Size size, int mass) : base(position, speed, size)
        {
            _Mass = mass;
            _Size = new Size(20 * mass, 20 * mass);
        }

        private Image _DefaultImage = Image.FromFile("asteroid.png");

        public override void Draw()
        {
            //GameLogic.Buffer.Graphics.DrawEllipse(Pens.White,
            //    _Position.X, _Position.Y, _Size.Width, _Size.Height);

            GameLogic.Buffer.Graphics.DrawImage(_DefaultImage, new Rectangle(_Position.X, _Position.Y, _Size.Width, _Size.Height));

        }

        public override void Update()
        {
            _Position.X += _Speed.X;
            _Position.Y += _Speed.Y;
            //сделал правильный отскок от краёв экрана
            if (_Position.X <= 0 || _Position.X + this._Size.Width >= GameLogic.Width) _Speed.X *= -1;
            if (_Position.Y <= 0 || _Position.Y + this._Size.Height >= GameLogic.Height) _Speed.Y *= -1;
        }
    }
}
