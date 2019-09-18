using DataAccessLayer;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLEmployees : IBLEmployees
    {
       private IDALEmployees _dal;

        public BLEmployees(IDALEmployees dal)
        {
            _dal = dal;
        }

        public void AddEmployee(Employee emp)
        {
            _dal.AddEmployee(emp);
        }

        public void DeleteEmployee(int id)
        {
            _dal.DeleteEmployee(id);
        }

        public void UpdateEmployee(Employee emp)
        {
            _dal.UpdateEmployee(emp);
        }

        public List<Employee> GetAllEmployees()
        {
            return _dal.GetAllEmployees();
        }

        public Employee GetEmployee(int id)
        {
            DALEmployeesEF emp = new DALEmployeesEF();
            if (emp == null)
            {
                throw new Exception("El usuario no ha sido encontrado");
            }
            return emp.GetEmployee(id);
        }

        public double CalcPartTimeEmployeeSalary(int idEmployee, int hours)
        {
            DALEmployeesEF empEF = new DALEmployeesEF();

            if (empEF == null)
            {
                throw new Exception("El usuario no ha sido encontrado");
            }
            Employee mEmploy = empEF.GetEmployee(idEmployee);
            if (mEmploy.GetType().Name == "FullTimeEmployee")
            {
                throw new Exception("El usuario no esta identificado como part-time");
            }
            else
            {
                return ((PartTimeEmployee)mEmploy).HourlyRate * hours;
            }
        }
    }
}
