using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskViewModels
{
    public class EmployeeViewModel
    {
        private EmployeeDAO _dao;

        public EmployeeViewModel()
        {
            _dao = new EmployeeDAO();
        }

        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public int Version { get; set; }
        public string StaffPicture64 { get; set; }
        public bool IsTech { get; set; }
        public string DepartmentId { get; set; }
        public string Id { get; set; }


        public void Create()
        {
            try
            {
                Employee emp = new Employee();
                emp.Title = Title;
                emp.Firstname = Firstname;
                emp.Lastname = Lastname;
                emp.Email = Email;
                emp.Phoneno = Phoneno;
                emp.Version = Version;
                emp.StaffPicture64 = StaffPicture64;
                emp.IsTech = IsTech;
                emp.SetDepartmentIdFromString(DepartmentId);
                Id = _dao.Create(emp).GetIdAsString();
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "Create");
            }

        }

        public long Delete()
        {
            long deleted = 0;

            try
            {
                deleted = _dao.Delete(Id);
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "Delete");
            }
            return deleted;
        }
        public void GetById()
        {
            try
            {
                Employee emp = _dao.GetById(Id);
                Title = emp.Title;
                Firstname = emp.Firstname;
                Lastname = emp.Lastname;
                Phoneno = emp.Phoneno;
                Email = emp.Email;
                Id = emp.GetIdAsString();
                IsTech = emp.IsTech;
                StaffPicture64 = emp.StaffPicture64;
                DepartmentId = emp.GetDepartmentIdAsString();
                Version = emp.Version;
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "GetByID");
            }
        }

        public void GetByLastname()
        {
            try
            {
                Employee emp = _dao.GetByLastname(Lastname);
                Title = emp.Title;
                Firstname = emp.Firstname;
                Lastname = emp.Lastname;
                Phoneno = emp.Phoneno;
                Email = emp.Email;
                IsTech = emp.IsTech;
                StaffPicture64 = emp.StaffPicture64;
                Id = emp.GetIdAsString();
                DepartmentId = emp.GetDepartmentIdAsString();
                Version = emp.Version;
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "GetByLastname");
            }
        }

        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                Employee emp = new Employee();
                emp.SetIdFromString(Id);
                emp.SetDepartmentIdFromString(DepartmentId);
                emp.Title = Title;
                emp.Firstname = Firstname;
                emp.Lastname = Lastname;
                emp.Phoneno = Phoneno;
                emp.Email = Email;
                emp.IsTech = IsTech;
                emp.StaffPicture64 = StaffPicture64;
                emp.Version = Version;
                opStatus = _dao.Update(emp);
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "Update");
            }
            return Convert.ToInt16(opStatus);
        }
        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> viewModels = new List<EmployeeViewModel>();
            try
            {
                List<Employee> employees = _dao.GetAll();
                foreach (Employee e in employees)
                {
                    EmployeeViewModel viewModel = new EmployeeViewModel();
                    viewModel.Id = e.GetIdAsString();
                    viewModel.Title = e.Title;
                    viewModel.Firstname = e.Firstname;
                    viewModel.Lastname = e.Lastname;
                    viewModel.Phoneno = e.Phoneno;
                    viewModel.Email = e.Email;
                    viewModel.IsTech = e.IsTech;
                    viewModel.StaffPicture64 = e.StaffPicture64;
                    viewModel.Version = e.Version;
                    viewModel.DepartmentId = e.GetDepartmentIdAsString();
                    viewModels.Add(viewModel);
                }

            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeViewModel", "GetAll");
            }
            return viewModels;
        }
        public List<EmployeeViewModel> GetTechs()
        {
            List<EmployeeViewModel> viewModels = new List<EmployeeViewModel>();
            try
            {
                List<Employee> techs = _dao.GetAll();
                foreach (Employee e in techs)
                {
                    if (e.IsTech)
                    {
                        EmployeeViewModel viewModel = new EmployeeViewModel();
                        viewModel.Id = e.GetIdAsString();
                        viewModel.Title = e.Title;
                        viewModel.Firstname = e.Firstname;
                        viewModel.Lastname = e.Lastname;
                        viewModel.Phoneno = e.Phoneno;
                        viewModel.Email = e.Email;
                        viewModel.IsTech = e.IsTech;
                        viewModel.StaffPicture64 = e.StaffPicture64;
                        viewModel.Version = e.Version;
                        viewModel.DepartmentId = e.GetDepartmentIdAsString();
                        viewModels.Add(viewModel);
                    }
                }

            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeViewModel", "GetAll");
            }
            return viewModels;
        }
    }
}