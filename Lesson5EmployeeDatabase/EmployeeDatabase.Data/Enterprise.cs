using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeeDatabase.Data
{
    [Serializable]
    [DebuggerDisplay("Предприятие {Name}, число отделов {Departaments.Count}")]
    public class Enterprise : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Name;

        [XmlAttribute]
        public string Name
        {
            get => _Name;
            set
            {
                if(string.Equals(value, _Name)) return;
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private ObservableCollection<Departament> _Departaments;

        [XmlElement("Departament")]
        public ObservableCollection<Departament> Departaments
        {
            get => _Departaments ?? (_Departaments = new ObservableCollection<Departament>());
            set => _Departaments = value;
        }

        public Enterprise() { }

        public Enterprise(string name) => Name = name;

        public Enterprise(string name, IEnumerable<Departament> departaments) : this(name) => 
            _Departaments = new ObservableCollection<Departament>(departaments);

        public static Enterprise LoadFromFile(string file)
        {
            var serializer = new XmlSerializer(typeof(Enterprise));
            using (var reader = File.OpenText(file))
                return (Enterprise)serializer.Deserialize(reader);
        }

        public void SaveToFile(string file)
        {
            var serializer = new XmlSerializer(typeof(Enterprise));
            using (var writer = File.CreateText(file))
                serializer.Serialize(writer, this);
        }
    }
}
