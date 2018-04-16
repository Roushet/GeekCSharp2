using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Employees.Annotations;

namespace Employees
{
	/// <summary>Модель-представления</summary>
	abstract class ViewModel :
		MarkupExtension, 		 // Говорим, что модель является расширением разметки XAML
		INotifyPropertyChanged	 // Поддерживает уведомления изменений свйоств
	{
		/// <summary>Метод, вызвыаемый процессором XAML при создании логического дерева интерфейса</summary>
		public override object ProvideValue(IServiceProvider sp) => this;

		/// <summary>Событие, возникающие когда объект изменяет значение своего свойства</summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>Метод, генерирующий событие <see cref="PropertyChanged"/></summary>
		/// <param name="PropertyName">Имя изменившегося свойства (оставить пустым что бы система сама его определила)</param>
		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

		/// <summary>Метод, упрощающий установку значений полей свойств в сеттерах</summary>
		/// <typeparam name="T">Тип значения поля</typeparam>
		/// <param name="field">Поле, хранящее значение свйоства</param>
		/// <param name="value">Устанавливаемое значение</param>
		/// <param name="PropertyName">Имя изменяемого свойства</param>
		/// <returns>Истина, если значение поля было изменено</returns>
		[NotifyPropertyChangedInvocator]
		protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
		{
			if(Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(PropertyName);
			return true;
		}
	}
}
