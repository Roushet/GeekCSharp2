using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//Владимир Евдокимов - не смог открыть проект с урока, пересобрал 

//1. Подправил формулу отскока объектов от краёв экрана
//2. Гружу картинки вместо нарисованных фигур
//3. Добавил класс игрока в проект, спавню его. Игрок управляется с клавиатуры: вверх вниз вправо влево
//4. Для игрока добавил небольшой статический класс Input, где обрабатываю нажатия кнопок клавиатуры.
//5. Начал делать колижн на кругах, но не успел сделать.
//6. Сделал поворот картинки корабля игрока, но как то неудачно - он сам себя затирает. Моя ошибка в неверном определении размера картинки

namespace GameApp
{
    /// <summary>Класс главной программы</summary>
    static class Program
    {
        /// <summary>Точка входа</summary>
        [STAThread]
        static void Main()
        {
            // Инициализация визуальных стилей WinForms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form game_form = new Form();
            game_form.Width = 800;
            game_form.Height = 600;

            game_form.KeyPreview = true;
            game_form.KeyDown += new KeyEventHandler(Input.PlayerKey);

            GameLogic.Load();
            GameLogic.Initalize(game_form);
            game_form.Show();
            GameLogic.Draw();

            Application.Run(game_form);
        }
    }
}
