using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class CallDAO
    {
        private IRepository repo;

        public CallDAO()
        {
            repo = new HelpdeskRepository();
        }
        public Call GetById(string id)
        {

            Call call = null;
            var builder = Builders<Call>.Filter;
            var filter = builder.Eq("Id", new ObjectId(id));


            try
            {
                call = repo.GetOne<Call>(filter);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "CallDAO", "GetById");
            }

            return call;
        }
        public Call Create(Call createCall)
        {
            try
            {
                createCall = repo.Create<Call>(createCall);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "CallDAO", "Create");
            }

            return createCall;
        }


        public long Delete(string id)
        {

            long deleted = 0;

            try
            {
                deleted = repo.Delete<Call>(id);
                return deleted;
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "CallDAO", "Update");
                return deleted;
            }

        }
        public UpdateStatus Update(Call call)
        {
            UpdateStatus status = UpdateStatus.Failed;
            HelpdeskRepository repo = new HelpdeskRepository(new DbContext());
            if (!(repo.Exists<Call>(call.GetIdAsString())))
            {
                return status;
            }
            try
            {
                var filter = Builders<Call>.Filter.Eq("Id", call.Id) & Builders<Call>.Filter.Eq("Version", call.Version);
                var update = Builders<Call>.Update
                    .Set("DateClosed", call.DateClosed)
                    .Set("DateOpened", call.DateOpened)
                    .Set("EmployeeId", call.EmployeeId)
                    .Set("TechId", call.TechId)
                    .Set("Id", call.Id)
                    .Set("ProblemId", call.ProblemId)
                    .Set("Notes", call.Notes)
                    .Set("OpenStatus", call.OpenStatus)
                    .Inc("Version", 1);

                status = repo.Update(call.GetIdAsString(), filter, update);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "CallDAO", "Update");
            }

            return status;
        }
        public List<Call> GetAll()
        {
            List<Call> call = new List<Call>();
            try
            {
                call = repo.GetAll<Call>();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "CallDAO", "GetAll");
            }

            return call;
        }
    }
}
