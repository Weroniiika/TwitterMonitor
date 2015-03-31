using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TwitterMonitor.Models
{
    public class PresentationType
    {
        public int Id {get;set;}
        public string Text {get; set;}
    }

    public class PresentationSpec
    {

        public PresentationSpec() 
        {
            Presentations = new List<PresentationType>();
        }

        public List<PresentationType> Presentations {get;set;}
        
        [Required]
        [DisplayName("Presentation")]
        [Range(1 ,3, ErrorMessage="You have to choose presentation type")]
        public int selectedPresentationId { get; set; }
        
        [Required]
        [StringLength(4, MinimumLength=4, ErrorMessage="Code must be 4 characters long")]
        public string CODE {get; set;}
        
        public string ExceptionMessage { get; set; }
    }
}