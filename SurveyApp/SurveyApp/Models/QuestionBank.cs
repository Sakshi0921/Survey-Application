using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyApp.Models
{
    public class QuestionBank
    {
        [Key]
        public int QuestionId { get; set; }

        [Required(ErrorMessage="Please enter the question")]
        public string Question { get; set; }
        [Required]
        public string Type { get; set; }
        public Nullable<int> CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}