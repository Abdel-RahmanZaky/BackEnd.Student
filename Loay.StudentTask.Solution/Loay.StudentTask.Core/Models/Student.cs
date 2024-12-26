using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loay.StudentTask.Core.Models
{
    public class Student : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
