using System.ComponentModel.DataAnnotations;
using SharedLibrary.SharedItems.Shared;

namespace SecurityDomain.Models
{
	
    public class Action : BaseEntity<Guid>
    {

        public Action()
        {

        }


        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Code { get; set; }


    }
}

