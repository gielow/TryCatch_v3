using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.Models
{
    public class Record
    {
        [Key]
        public long Id { get; set; }

        public override bool Equals(object obj)
        {
            var obj2 = obj as Record;

            if (obj2 == null)
                return false;

            return this.Id.Equals(obj2.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
