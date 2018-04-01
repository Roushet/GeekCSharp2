using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameApp
{
    /// <summary>Базовый игровой объект, отвечающий за отрисовку графики</summary>
    class BaseObject
    {
        /// <summary>Положение объекта в пространстве</summary>
        protected Point _Position;
        /// <summary>Скорость перемещения объекта за единицу времени (за кадр)</summary>
        protected Point _Speed;
        /// <summary>Размер объекта</summary>
        protected Size _Size;

        private Image _DefaultImage = Image.FromFile("asteroid.png");

        /// <summary>Инициализация нового игрового боъекта</summary>
        /// <param name="position">Положение объекта в пространстве</param>
        /// <param name="speed">Скорость перемещения объекта за единицу времени (за кадр)</param>
        /// <param name="size">Размер объекта</param>
        public BaseObject(Point position, Point speed, Size size)
        {
            _Position = position;
            _Speed = speed;
            _Size = size;
        }

        //Вывел данные наружу для расчёта физики столкновений
        public Point Position
        {
            get
            {
                return new Point(_Position.X + _Size.Width / 2, _Position.Y - _Size.Height / 2);
            }
        }
        public Point Speed { get => _Speed; set => _Speed = value; }

        public float Radius { get => Math.Max(_Size.Height, _Size.Width) / 2; }

        public int ObjId { get => this.GetHashCode(); }


        /// <summary>Нарисовать объект</summary>
        public virtual void Draw()
        {
            //GameLogic.Buffer.Graphics.DrawEllipse(Pens.White,
            //    _Position.X, _Position.Y, _Size.Width, _Size.Height); 

            GameLogic.Buffer.Graphics.DrawImage(_DefaultImage, new Rectangle(_Position.X, _Position.Y, _Size.Width, _Size.Height));
        }

        /// <summary>Обновить состояние объекта</summary>
        public virtual void Update()
        {
            _Position.X += _Speed.X;
            _Position.Y += _Speed.Y;
            //сделал правильный отскок от краёв экрана
            if (_Position.X <= 0 || _Position.X + this._Size.Width >= GameLogic.Width) _Speed.X *= -1;
            if (_Position.Y <= 0 || _Position.Y + this._Size.Height >= GameLogic.Height) _Speed.Y *= -1;

        }
    }
}
