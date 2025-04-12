using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedLibrary.SharedItems.Shared;

namespace DomainLayer.Models
{
	public class ProcessEvents
	{
        public ProcessEvents()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }


        public DateTime CreatedDate { get; set; }


    }
}

