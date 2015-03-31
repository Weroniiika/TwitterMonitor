using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TwitterMonitor.Models
{
    public class RequestSpec
    {
        [Required]
        [DisplayName("Number of tweets")]
        [Range(10, 10000, ErrorMessage="You can request from 10 to 10000 tweets")]
        public int NumberTweets { get; set; }
        
        [Required]
        [DisplayName("Filtering word")]
        [StringLength(20, MinimumLength=3, ErrorMessage="Track word must be between 3 and 20 characters long")]
        public string TrackWord { get; set; }
        
        [DisplayName("CODE")]
        public string Code { get; set; }

        public string ExceptionMessage { get; set; }
    }
}