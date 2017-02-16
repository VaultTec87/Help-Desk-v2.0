using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskViewModels
{
    public class ProblemViewModel
    {
        private ProblemDAO _dao;

        public ProblemViewModel()
        {
            _dao = new ProblemDAO();
        }

        public string Description { get; set; }
        public string Id { get; set; }
        public int Version { get; set; }

        public void Create()
        {
            try
            {
                Problem prob = new Problem();
                prob.Description = Description;
                prob.Version = Version;
                Id = _dao.Create(prob).GetIdAsString();
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModel", "Create");
            }
        }
        public long Delete()
        {
            long deleted = 0;
            try
            {
                deleted = _dao.Delete(Id);
                return deleted;
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModel", "Delete()");
                return deleted;
            }
        }

        public void GetById()
        {
            try
            {
                Problem prob = _dao.GetById(Id);
                Description = prob.Description;
                Version = prob.Version;
                Id = prob.GetIdAsString();
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModel", "GetById");
            }
        }

        public List<ProblemViewModel> GetAll()
        {
            List<ProblemViewModel> viewModels = new List<ProblemViewModel>();
            try
            {
                List<Problem> problem = _dao.GetAll();
                foreach (Problem prob in problem)
                {
                    ProblemViewModel viewModel = new ProblemViewModel();
                    viewModel.Description = prob.Description;
                    viewModel.Version = prob.Version;
                    viewModel.Id = prob.GetIdAsString();
                    viewModels.Add(viewModel);
                }

            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemViewModel", "GetAll");
            }
            return viewModels;
        }

        public int Update()
        {
            int opStatus;

            try
            {
                Problem prob = new Problem();
                prob.SetIdFromString(Id);
                prob.Description = Description;
                prob.Version = Version;
                opStatus = _dao.Update(prob);
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModel", "Update");
                opStatus = Convert.ToInt16(UpdateStatus.Failed);
            }
            return Convert.ToInt16(opStatus);
        }

        public void GetByDescription()
        {
            try
            {

                Problem prob = _dao.GetByDescription(Description);
                Description = prob.Description;
                Id = prob.GetIdAsString();
                Version = prob.Version;
            }
            catch(Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModels", "GetByDescription");
            }
        }
   
    }
}
