using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALEmployeesEF : IDALEmployees
    {
        private Model.Employee Casteo(Employee emp)
        {
            if (emp == null)
            {
                return null;
            }
            if (emp.GetType().Name == "FullTimeEmployee")
            {
                FullTimeEmployee employee = (FullTimeEmployee)emp;
                Model.FullTimeEmployee employeeBase = new Model.FullTimeEmployee()
                {
                    Name = employee.Name,
                    StartDate = employee.StartDate,
                    Salary = employee.Salary
                };
                return employeeBase;
            }
            else
            {
                PartTimeEmployee employee = (PartTimeEmployee)emp;
                Model.PartTimeEmployee employeeBase = new Model.PartTimeEmployee()
                {
                    Name = employee.Name,
                    StartDate = employee.StartDate,
                    HourlyRate = employee.HourlyRate
                };
                return employeeBase;
            }

        }

        private Employee Casteo(Model.Employee emp)
        {
            if (emp == null)
            {
                return null;
            }
            if (emp.GetType().Name == "FullTimeEmployee")
            {
                Model.FullTimeEmployee Memp = (Model.FullTimeEmployee)emp;
                FullTimeEmployee employeeBase = new FullTimeEmployee()
                {
                    Id = Memp.EmployeeId,
                    Name = Memp.Name,
                    StartDate = Memp.StartDate,
                    Salary = Memp.Salary
                };
                return employeeBase;
            }
            else
            {
                Model.PartTimeEmployee Memp = (Model.PartTimeEmployee)emp;
                PartTimeEmployee employeeBase = new PartTimeEmployee()
                {
                    Id = Memp.EmployeeId,
                    Name = Memp.Name,
                    StartDate = Memp.StartDate,
                    HourlyRate = Memp.HourlyRate
                };
                return employeeBase;
            }

        }
        public void AddEmployee(Employee emp)
        {
            if (emp == null)
            {
                return;
            }
            using (Model.PracticoNetEntities en = new Model.PracticoNetEntities())
            {
                en.Employee.Add(Casteo(emp));
                en.SaveChanges();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var context = new Model.PracticoNetEntities())
            {
                Model.Employee emp = context.Employee.FirstOrDefault(x => x.EmployeeId == id);
                if (emp != null)
                {
                    if (emp.GetType().Name == "FullTimeEmployee")
                    {
                        Model.FullTimeEmployee fte = (Model.FullTimeEmployee)emp;
                        context.Employee.Remove(fte);
                        context.SaveChanges();
                    }
                    else
                    {
                        Model.PartTimeEmployee pte = (Model.PartTimeEmployee)emp;
                        context.Employee.Remove(pte);
                        context.SaveChanges();
                    }
                }
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            if (emp == null)
            {
                return;
            }
            using (Model.PracticoNetEntities en = new Model.PracticoNetEntities())
            {
                Model.Employee em = en.Employee.Find(emp.Id);
                em.Name = emp.Name;
                em.StartDate = emp.StartDate;
                if (emp.GetType().Name == "FullTimeEmployee")
                {
                    Model.FullTimeEmployee emCast = (Model.FullTimeEmployee)em;
                    emCast.Salary = ((FullTimeEmployee)emp).Salary;
                }
                else
                {
                    Model.PartTimeEmployee emCast = (Model.PartTimeEmployee)em;
                    emCast.HourlyRate = ((PartTimeEmployee)emp).HourlyRate;
                }
                en.SaveChanges();
            }
        }

        public List<Employee> GetAllEmployees()
        {
            using (Model.PracticoNetEntities en = new Model.PracticoNetEntities())
            {
                List<Employee> listaEmpleados = new List<Employee>();
                en.Employee.ToList().ForEach(x =>
                {
                    listaEmpleados.Add(Casteo(x));
                });
                return listaEmpleados;
            }
        }

        public Employee GetEmployee(int id)
        {
            using (Model.PracticoNetEntities en = new Model.PracticoNetEntities())
            {
                return Casteo(en.Employee.Find(id));
            }
        }
    }
    
}
