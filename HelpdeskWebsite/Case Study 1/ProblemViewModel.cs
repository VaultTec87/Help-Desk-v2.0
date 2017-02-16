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
        public string ProblemId { get; set; }
        public int Version { get; set; }

        public void Create()
        {
            try
            {
                Problem prob = new Problem();
                prob.Description = Description;
                prob.Version = Version;
                ProblemId = _dao.Create(prob).GetIdAsString();
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
                deleted = _dao.Delete(ProblemId);
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
                Problem prob = _dao.GetById(ProblemId);
                Description = prob.Description;
                Version = prob.Version;
                ProblemId = prob.GetIdAsString();
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
                    viewModel.ProblemId = prob.GetIdAsString();
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
            UpdateStatus opStatus;

            try
            {
                Problem prob = new Problem();
                prob.SetIdFromString(ProblemId);
                prob.Description = Description;
                prob.Version = Version;
                opStatus = _dao.Update(prob);
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "ProblemViewModel", "Update");
                opStatus = UpdateStatus.Failed;
            }
            return Convert.ToInt16(opStatus);
        }

    }
}
