using Microsoft.Win32;
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
using EmployeeDatabase;
using System.Windows;
using System.Reflection;

namespace EmployeeDatabase.Data
{
    [Serializable]
    [DebuggerDisplay("Предприятие {Name}, число отделов {Departaments.Count}")]
    public class Enterprise : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static Enterprise _instance;

        private string _Name;

        public static Enterprise GetInstance()
        {
            if (_instance == null)
                return Enterprise.LoadFromFile("data\\enterprise.xml");
            return _instance;
        }

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

        public void SaveToFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";
            saveFileDialog.DefaultExt = "xml";
            //TODO переделать относительный путь из пропертей
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            saveFileDialog.ShowDialog();

            if (String.IsNullOrEmpty(saveFileDialog.FileName)) return;

            var serializer = new XmlSerializer(typeof(Enterprise));
            using (var writer = File.CreateText(saveFileDialog.FileName))
                serializer.Serialize(writer, this);
        }

        public void LoadFromFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML file (*.xml)|*.xml";
            openFileDialog.DefaultExt = "xml";
            openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            openFileDialog.ShowDialog();

            if (String.IsNullOrEmpty(openFileDialog.FileName)) return;

            var serializer = new XmlSerializer(typeof(Enterprise));
            using (var writer = File.CreateText(openFileDialog.FileName))
                serializer.Serialize(writer, this);
        }

    }
}
