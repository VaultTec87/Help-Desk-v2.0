using MongoDB.Bson;

namespace HelpdeskDAL
{
    public class Employee : HelpdeskEntity
    {

        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string StaffPicture64 { get; set; }
        public bool IsTech { get; set; }
        public ObjectId DepartmentId { get; set; }

        public string GetDepartmentIdAsString()
        {
            return this.DepartmentId.ToString();
        }

        public void SetDepartmentIdFromString(string id)
        {
            this.DepartmentId = new ObjectId(id);
        }
    }
}
