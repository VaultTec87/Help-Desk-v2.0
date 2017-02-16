using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class Department : HelpdeskEntity
    {
        public ObjectId ManagerId { get; set; }
        public string DepartmentName { get; set; }
        public string GetManagerIdAsString()
        {
            return this.ManagerId.ToString();
        }
        public void SetManagerIdFromString(string id)
        {
            this.ManagerId = new ObjectId(id);
        }
    }
}
