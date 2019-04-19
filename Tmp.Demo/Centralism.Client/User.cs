using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centralism.Client
{
    [Serializable]
    public class User
    {
        public string Code { get; set; }
        public string CorpCode { get; set; }
        public string CorpName { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PrimaryOrgCode { get; set; }
        public string PrimaryOrgName { get; set; }
        public int Sequence { get; set; }
        public string UserType { get; set; }
        public string UserTypeName { get; set; }
    }
}
