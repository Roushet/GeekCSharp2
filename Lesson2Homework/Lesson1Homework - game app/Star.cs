using System.Drawing;

namespace GameApp
{
	/// <summary>Звезда</summary>
	class Star : BaseObject
	{
		/// <summary>Инициализация новой звезды</summary>
		/// <param name="position">Положение объекта в пространстве</param>
		/// <param name="speed">Скорость перемещения объекта за единицу времени (за кадр)</param>
		/// <param name="size">Размер объекта</param>
		public Star(Point position, Point speed, Size size) : base(position, speed, size) { }

		/// <inheritdoc />
		public override void Draw()
		{
			var g = GameLogic.Buffer.Graphics;

			g.DrawLine(Pens.White, 
				_Position.X, 
				_Position.Y, 
				_Position.X + _Size.Width, 
				_Position.Y + _Size.Height);
			g.DrawLine(Pens.White, 
				_Position.X + _Size.Width, 
				_Position.Y, 
				_Position.X, 
				_Position.Y + _Size.Height);
		}

		/// <inheritdoc />
		public override void Update()
		{
			_Position.X += _Speed.X;
			if (_Position.X < 0) _Position.X = GameLogic.Width - _Size.Width;
		}
	}
}