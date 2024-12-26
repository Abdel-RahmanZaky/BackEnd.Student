using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loay.StudentTask.Core.Models
{
    public class StudentSubject : BaseModel
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
