using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Domain.App
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public int AddressId { get; set; }
        
        public DateTime Birth { get; set; }

        public virtual Address Address { get; set; }
    }
}
