using System.Drawing;
using System;
namespace GameApp
{
    class Player : BaseObject
    {
        //public static int LinearSpeed;
        //private static int _angle;
        //public static double Angle;

        public Player(Point position, Point speed, Size size) : base(position, speed, size) { }

        private Image _DefaultImage = Image.FromFile("ship.png");


        public override void Draw()
        {
            var g = GameLogic.Buffer.Graphics;

            //ОГОНЬ! 
            //смешаем точку поворота картники в её центр из левого верхнего угла (пивот)
            g.TranslateTransform(_Position.X + _Size.Width / 2, _Position.Y + _Size.Height / 2);
            //крутим картинку на угол из класса ввода 
            //TODO: после переноса угла в класс игрока брать угол оттуда
            g.RotateTransform((float)(Input.Angle * 180 / Math.PI) - 90);
            //обновляем изображение
            g.DrawImage(_DefaultImage, new Rectangle(-_Size.Width / 2, -_Size.Height / 2, _Size.Width, _Size.Height));
            //возвращаем пивот в изначальную точку
            g.ResetTransform();

            //g.DrawImage(_DefaultImage, new Rectangle(_Position.X, _Position.Y, _Size.Width, _Size.Height));
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
