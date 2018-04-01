using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameApp
{
    /// <summary>Класс игровой логики</summary>
    static class GameLogic
    {
        /// <summary>Контекст рисования с двойной буферизацией</summary>
        private static BufferedGraphicsContext _context;

        /// <summary>Буфер рисования</summary>
        public static BufferedGraphics Buffer { get; set; }

        /// <summary>Ширина игрового поля</summary>
        public static int Width { get; set; }

        /// <summary>Высота игрового поля</summary>
        public static int Height { get; set; }

        /// <summary>Инициализация игровйо формы</summary>
        /// <param name="game_form">Игровая форма</param>
        public static void Initalize(Form game_form)
        {
            if (game_form.Width < 0 || game_form.Width > 1000) throw new ArgumentOutOfRangeException("Ошибка. Ширина формы более 1000 единиц или отрицательна");
            if (game_form.Height < 0 || game_form.Height > 1000) throw new ArgumentOutOfRangeException("Ошибка. Высота формы более 1000 единиц или отрицательна");

            Width = game_form.Width;
            Height = game_form.Height;

            _context = BufferedGraphicsManager.Current;

            Graphics g = game_form.CreateGraphics();
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            var timer = new Timer();
            timer.Interval = 100;
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        /// <summary>Обработчик события таймера</summary>
        /// <param name="sender">Таймер - источник события</param>
        /// <param name="e">Аргумент события</param>
        private static void OnTimerTick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>Игровые (визуальные) объекты</summary>
        private static BaseObject[] _objects;

        //public static BaseObject[] GetBaseObjectList()
        //{
        //    return _objects;
        //}

        /// <summary>Загрузить игровые объекты</summary>
        public static void Load()
        {
            _objects = new BaseObject[30];
            for (var i = 0; i < _objects.Length; i++)
                _objects[i] = new BaseObject(
                    new Point(600, i * 20),
                    new Point(15 - i, 15 - i),
                    new Size(40, 40));

            for (var i = _objects.Length / 2 ; i < _objects.Length-1; i++)
                _objects[i] = new Star(
                    new Point(600, i * 20),
                    new Point(15 - i, 15 - i),
                    new Size(5, 5));

            _objects[29] = new Player(
                    new Point(200, 200),
                    new Point(0, 0),
                    new Size(40, 40));
        }

        /// <summary>Нарисовать сцену</summary>
        public static void Draw()
        {
            Graphics g = Buffer.Graphics;
            g.Clear(Color.Black);
            //g.FillEllipse(Brushes.Red, new Rectangle(100, 100, 200, 200));

            foreach (var obj in _objects) obj.Draw();

            Buffer.Render();
        }

        /// <summary>Обновить состояние игровых объектов</summary>
        private static void Update()
        {
            foreach (var obj in _objects) obj.Update();
            //CollisionUpdate();
        }

        //Обработка коллизии объектов на кругах
        private static void CollisionUpdate()
        {
            for (int i = 0; i < _objects.Count<BaseObject>(); i++)
            {
                if (_objects[i].GetType() == typeof(Star)) continue;

                for (int j = 0; j < _objects.Count<BaseObject>(); j++)
                {

                    if (_objects[i].ObjId == _objects[j].ObjId) continue;
                    if (_objects[j].GetType() == typeof(Star)) continue;

                    double distance = Math.Sqrt(
                                                Math.Pow(_objects[j].Position.X - _objects[i].Position.X, 2) -
                                                Math.Pow(_objects[j].Position.Y - _objects[i].Position.Y, 2)
                                                );

                    if (distance <= _objects[i].Radius + _objects[j].Radius)
                        Collide(_objects[i], _objects[j]);


                }
            }
        }

        private static void Collide(BaseObject v1, BaseObject v2)
        {
            int dotProduct = v2.Speed.X * v2.Speed.X + v2.Speed.Y * v2.Speed.Y;

            double absSpeedI = Math.Sqrt(v1.Speed.X * v1.Speed.X + v1.Speed.Y * v1.Speed.Y);
            double absSpeedJ = Math.Sqrt(v2.Speed.X * v2.Speed.X + v2.Speed.Y * v2.Speed.Y);

            double angle = dotProduct / (absSpeedI + absSpeedJ);

            BaseObject v1_temp = v1;
            BaseObject v2_temp = v2;




        }

    }
}
