using System.Drawing;
using System;

namespace GameApp
{
    class Player : BaseObject
    {
        //Модуль скорости корабля
        private int _absSpeed;

        //Угол атаки, то куда смотрит корабль и куда он будет стрелять - управляется из Инпута. 
        //Корабль не обязательно смотрит куда летит, можно летать боком
        private double _attackAngle;

        //жизни корабля
        public int Lives { get; set; }

        /// <summary>
        /// Событие для логгирования выстрела игроком, вызывается в методе MakeShot()
        /// </summary>
        Action<LogEntry> ev = new Action<LogEntry>(Logger.LogAdd);


        public int LinearSpeed
        {
            get { return _absSpeed; }
            set { _absSpeed = value; }
        }

        /// <summary>
        /// Публичное поле для управления углом атаки корабля
        /// Позволяет крутить корабль вокруг своей оси, не меняя направление движения
        /// В радианах
        /// </summary>
        public double ShipAngle
        {
            get { return _attackAngle; }
            set { _attackAngle += (value * System.Math.PI) / 180; }
        }


        private static Player _instance;
        /// <summary>
        /// Пытаюсь делать игрока синглтоном. 
        /// Метод создаёт игрока и возвращает ссылку на него, если игрок есть.
        /// </summary>
        /// <returns>Player</returns>
        public static Player GetPlayer()
        {
            if (_instance == null)
                _instance = new Player(
                                    new Point(200, 200),
                                    new Point(0, 0),
                                    new Size(40, 40)
                                    );
            return _instance;
        }

        private Player(Point position, Point speed, Size size) : base(position, speed, size) { }


        private Image _DefaultImage = Image.FromFile("ship.png");


        public override void Draw()
        {
            var g = GameLogic.Buffer.Graphics;

            //ОГОНЬ! 
            //смешаем точку поворота картники в её центр из левого верхнего угла (пивот)
            g.TranslateTransform(_Position.X + _Size.Width / 2, _Position.Y + _Size.Height / 2);
            //крутим картинку на угол из класса ввода 
            g.RotateTransform((float)(_attackAngle * 180 / Math.PI) - 90);
            //обновляем изображение
            g.DrawImage(_DefaultImage, new Rectangle(-_Size.Width / 2, -_Size.Height / 2, _Size.Width, _Size.Height));
            //возвращаем пивот в изначальную точку
            g.ResetTransform();

            //g.DrawImage(_DefaultImage, new Rectangle(_Position.X, _Position.Y, _Size.Width, _Size.Height));
        }

        public void MakeShot()
        {
            int bulletSpeed = 50;
            Point vectorSpeed = new Point();

            vectorSpeed.X = (int)(bulletSpeed * System.Math.Cos(_attackAngle));
            vectorSpeed.Y = (int)(bulletSpeed * System.Math.Sin(_attackAngle));

            GameLogic.GlobalObjList.Add(new Bullet(Position, vectorSpeed, new Size(10, 10)));

            ev(new LogEntry(DateTime.Now, "Player has shot"));
        }


        public override void Update()
        {

            Point addSpeed = new Point();

            addSpeed.X = (int)(_absSpeed * System.Math.Cos(_attackAngle));
            addSpeed.Y = (int)(_absSpeed * System.Math.Sin(_attackAngle));

            //если не сбрасывать модуль скорости, то корабль начинает носиться как сумасшедший, управлять им невозможно
            _absSpeed = 0;

            _Speed.X += addSpeed.X;
            _Speed.Y += addSpeed.Y;

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
