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
        public static List<BaseObject> GlobalObjList = new List<BaseObject>();

        /// <summary>Загрузить игровые объекты</summary>
        public static void Load()
        {
            Random random = new Random();

            for (var i = 0; i < 10; i++)
            {

                GlobalObjList.Add(new Asteroid(
                    new Point(random.Next(10, 600 - 10), random.Next(10, 600 - 10)),
                    new Point(random.Next(5, 20), random.Next(5, 20)),
                    new Size(40, 40),
                    random.Next(1, 5)
                    ));
            }

            for (var i = 10; i < 10; i++)
                GlobalObjList.Add(new Star(
                    new Point(600, i * 20),
                    new Point(15 - i, 15 - i),
                    new Size(5, 5)));

            GlobalObjList.Add(Player.GetPlayer());

        }


        /// <summary>Нарисовать сцену</summary>
        public static void Draw()
        {
            Graphics g = Buffer.Graphics;
            g.Clear(Color.Black);
            //g.FillEllipse(Brushes.Red, new Rectangle(100, 100, 200, 200));

            foreach (var obj in GlobalObjList) obj.Draw();

            Buffer.Render();
        }

        /// <summary>Обновить состояние игровых объектов</summary>
        private static void Update()
        {
            foreach (var obj in GlobalObjList) obj.Update();
            CollisionUpdate();
        }

        //Обработка коллизии объектов на кругах
        private static void CollisionUpdate()
        {
            for (int i = 0; i < GlobalObjList.Count<BaseObject>(); i++)
            {
                if (GlobalObjList[i].GetType() == typeof(Star)) continue;

                for (int j = 0; j < GlobalObjList.Count<BaseObject>(); j++)
                {

                    if (GlobalObjList[i].ObjId == GlobalObjList[j].ObjId) continue;
                    if (GlobalObjList[j].GetType() == typeof(Star)) continue;

                    double distance = Math.Sqrt(
                                                Math.Pow(GlobalObjList[j].Position.X - GlobalObjList[i].Position.X, 2) +
                                                Math.Pow(GlobalObjList[j].Position.Y - GlobalObjList[i].Position.Y, 2)
                                                );

                    if (distance <= GlobalObjList[i].Radius + GlobalObjList[j].Radius)
                        Collide(GlobalObjList[i], GlobalObjList[j]);


                }
            }
        }

        private static void Collide(BaseObject obj1, BaseObject obj2)
        {
            Logger.LogAdd(new LogEntry(DateTime.Now, String.Format("{0} and {1} has just collided", obj1.GetType() +" " + obj1.ObjId.ToString(), obj2.GetType())));

            Point obj1NewSpeed = new Point();
            Point obj2NewSpeed = new Point();

            //новые скорости объекта 1
            obj1NewSpeed.X = (obj1.Speed.X * (obj1.Mass - obj2.Mass) + (2 * obj2.Mass * obj2.Speed.X)) / (obj1.Mass + obj2.Mass);
            obj1NewSpeed.Y = (obj1.Speed.Y * (obj1.Mass - obj2.Mass) + (2 * obj2.Mass * obj2.Speed.Y)) / (obj1.Mass + obj2.Mass);

            //новые скорости объекта 2
            obj2NewSpeed.X = (obj2.Speed.X * (obj2.Mass - obj1.Mass) + (2 * obj1.Mass * obj1.Speed.X)) / (obj1.Mass + obj2.Mass);
            obj2NewSpeed.Y = (obj2.Speed.Y * (obj2.Mass - obj1.Mass) + (2 * obj1.Mass * obj1.Speed.Y)) / (obj1.Mass + obj2.Mass);

            obj1.Speed = obj1NewSpeed;
            obj2.Speed = obj2NewSpeed;
        }

    }
}
