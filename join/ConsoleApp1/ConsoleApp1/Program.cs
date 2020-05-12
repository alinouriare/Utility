using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ConsoleApp1
{
    public class Department
    {

        public int DepId { get; set; }

        public string DepName { get; set; }
    }

    public class Employee
    {

        public int EmpId { get; set; }

        public string Name { get; set; }

        public int DeptId { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Department> objDept = new List<Department>(){
                new Department{DepId=1,DepName="Software"},
                new Department{DepId=2,DepName="Finance"},
                new Department{DepId=3,DepName="Health"}
            };
            List<Employee> objEmp = new List<Employee>()
            {
                new Employee { EmpId=1,Name = "Suresh Dasari", DeptId=1 },
                new Employee { EmpId=2,Name = "Rohini Alavala", DeptId=1 },
                new Employee { EmpId=3,Name = "Praveen Alavala", DeptId=2 },
                new Employee { EmpId=4,Name = "Sateesh Alavala", DeptId =2},
                new Employee { EmpId=5,Name = "Madhav Sai"}};
            var result = from d in objDept
                         join e in objEmp on d.DepId equals e.DeptId into empDept
                         select new
                         {
                             DepartmentName = d.DepName,
                             Employees = from emp2 in empDept
                                         orderby emp2.Name
                                         select emp2
                         };
            foreach (var empGroup in result)
            {
                Console.WriteLine(empGroup.DepartmentName);
                foreach (var item in empGroup.Employees)
                {
                    Console.WriteLine("    {0}", item.Name);
                }
            }

            Console.ReadLine();
        }

        private static void leftjoin(List<Department> objDept, List<Employee> objEmp)
        {
            var leftjoin = (from e in objEmp
                            join d in objDept
                            on e.DeptId equals d.DepId into empDept
                            from ed in empDept.DefaultIfEmpty()
                            select new
                            {
                                EmployeeName = e.Name,
                                DepartmentName = ed == null ? "No Department" : ed.DepName
                            }).ToList();

            foreach (var item in leftjoin)
            {
                Console.WriteLine(item.EmployeeName + "\t | " + item.DepartmentName);

            }
        }

        private static void join(List<Department> objDept, List<Employee> objEmp)
        {
            var join = (from d in objDept
                        join e in objEmp
                        on d.DepId equals e.DeptId
                        select new
                        {
                            EmployeeName = e.Name,
                            DepartmentName = d.DepName
                        }
                ).ToList();

            foreach (var item in join)
            {
                Console.WriteLine(item.EmployeeName + "\t | " + item.DepartmentName);

            }
        }
    }
}
