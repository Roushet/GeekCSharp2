using Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EmployeesWcf
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IEmployeeService
    {

        //Количество сотрудников в базе
        [OperationContract]
        int GetEmployeesCount();

        [OperationContract]
        void AddEmployee(string name, int age, decimal salary, string comment = "");

        //[OperationContract]
        //EmployeeData GetDataUsingDataContract(EmployeeData employee);

        // TODO: Добавьте здесь операции служб
    }


    ////// Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    //[DataContract]
    //public class EmployeeData : EmployeeList
    //{
    //    [DataMember]
    //    public EmployeeList List
    //    {
    //        get { return List; }
    //        set { List = value; }
    //    }
    //}
}
