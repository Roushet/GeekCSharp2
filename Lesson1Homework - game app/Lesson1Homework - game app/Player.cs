using System.Drawing;
using System.Windows.Forms;

namespace GameApp
{
    class Player : BaseObject
    {
        public Player(Point position, Point speed, Size size) : base(position, speed, size) { }

        private Image _DefaultImage = Image.FromFile("ship.png");


        public override void Draw()
        {
            var g = GameLogic.Buffer.Graphics;

            /*
            Graphics img = Graphics.FromImage(_DefaultImage);
            img.TranslateTransform((float)_Size.Width / 2, (float)_Size.Height / 2);
            img.RotateTransform((float)playerDirection);
            img.TranslateTransform(-(float)_Size.Width / 2, -(float)_Size.Height / 2);
            img.DrawImage(_DefaultImage, new Point(0, 0));
            */

            g.DrawImage(_DefaultImage, new Rectangle(_Position.X, _Position.Y, _Size.Width, _Size.Height));
        }


        public override void Update()
        {

            _Speed.X = (int)(Input.Speed * System.Math.Cos(Input.Angle));
            _Speed.Y = (int)(Input.Speed * System.Math.Sin(Input.Angle));

            _Position.X += _Speed.X;
            _Position.Y += _Speed.Y;

            //Input.SpeedX = 0;
            //Input.SpeedY = 0;

            //сделал правильный отскок от краёв экрана
            if (_Position.X <= 0 || _Position.X + this._Size.Width >= GameLogic.Width) _Speed.X *= -1;
            if (_Position.Y <= 0 || _Position.Y + this._Size.Height >= GameLogic.Height) _Speed.Y *= -1;


        }


    }


}
