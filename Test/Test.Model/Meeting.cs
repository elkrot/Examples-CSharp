using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public class Meeting
    {
        public Meeting()
        {
            Tests = new Collection<TestEntity>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public ICollection<TestEntity> Tests { get;  set; }
    }
}
