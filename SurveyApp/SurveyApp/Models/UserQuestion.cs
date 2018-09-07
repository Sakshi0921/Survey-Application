using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyApp.Models
{
    public class UserQuestion
    {
        [Key]
        public int UQuestionId { get; set; }

        [Required(ErrorMessage = "Please enter the Survey Id")]
        public int SurveyId { get; set; }

        public int SurveyQuesNo { get; set; }

        [Required(ErrorMessage = "Please enter the question")]
        public string Question { get; set; }
        public string Type { get; set; }

        public virtual Survey survey {get; set;}
    }
}