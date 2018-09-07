using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyApp.Models
{
    public class SurveyCandidate
    {
        [Key]
        [Column(Order = 1)]
        public int SurveyId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int CandidateId { get; set; }

        public int SurveyQuesNo { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual Survey Survey { get; set; }
    }
}