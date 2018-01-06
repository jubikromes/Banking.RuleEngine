using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
