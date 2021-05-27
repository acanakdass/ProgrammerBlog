using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Shared.Entities.Abstract
{
    public abstract class IEntityRepository
    {
        //virtual : override edilebilir
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual DateTime ModifiedDate { get; set; } = DateTime.Now;
        public virtual bool IsDeleted { get; set; } = false;
        public virtual bool IsActive { get; set; } = true;
        public virtual string CreaterName { get; set; } = "Admin";
        public virtual string ModifierName { get; set; } = "Admin";
        public virtual string Note { get; set; }
    }
}
