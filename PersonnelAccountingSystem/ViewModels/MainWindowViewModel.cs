using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using PersonnelAccountingSystem.Infrastructure.Commands;
using PersonnelAccountingSystem.Models;
using PersonnelAccountingSystem.ViewModels.Base;
using System.Drawing;

namespace PersonnelAccountingSystem.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region depList
        private List<string> departments = GetDepartments();
        public List<string> Departments { get => departments; }
        private static List<string> GetDepartments()
        {
            var departmentsList = new List<string>() { };
            using (var context = new Personnel_Accounting_SystemEntities())
            {
                var data = context.Departments;
                foreach (var department in data)
                {
                    departmentsList.Add(department.Department_name);
                }
            }
            return departmentsList;
        }
        #endregion deplist

        #region ShowEmployeesComman
        public static List<string> employees;
        public List<string> Employees
        {
            get => employees;
            set
            {
                employees = value;
                OnPropertyChange();
            }
        }

        private static string selectedDepartment;
        public string SelectedDeparment
        {
            get => selectedDepartment;
            set
            {
                selectedDepartment = value;
                Employees = ShowEmployeesExecuted();
            }
        }

        private static List<string> ShowEmployeesExecuted()
        {
            var employeesNamesList = new List<string>() { };
            var employeesIdList = new List<int>() { };
            using (var context = new Personnel_Accounting_SystemEntities())
            {
                var DepartmentsList = context.Departments;
                var DepartmentsIdList = new List<int>() { };
                foreach (var departmentID in DepartmentsList)
                {
                    if (departmentID.Department_name == selectedDepartment)
                        DepartmentsIdList.Add(departmentID.Department_ID);
                }
                var employeesList = context.Employees;
                foreach (var employee in employeesList)
                {
                    if (DepartmentsIdList.Contains(employee.Department_ID))
                    {
                        employeesNamesList.Add(employee.First_name + " " + employee.Last_name + " " + employee.Surname);
                        employeesIdList.Add(employee.Employee_ID);
                    }
                }
            }
            return employeesNamesList;
        }

        #endregion ShowEmployeesCommand

        #region DisplayEmployeesData

        #region test

        

        #endregion test

        #endregion DisplayEmployeesData




        public MainWindowViewModel()
        {
            GetDepartments();
        }   
    }
}
