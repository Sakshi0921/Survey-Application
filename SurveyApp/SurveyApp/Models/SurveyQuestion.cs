﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SurveyApp.Models
{
    public class SurveyQuestion
    {
        [Key]
        [Column(Order=1)]
        public int SurveyId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int QuestionId { get; set; }

        public virtual Survey Survey { get; set; }
        public virtual UserQuestion UserQuestion { get; set; }
    }
}