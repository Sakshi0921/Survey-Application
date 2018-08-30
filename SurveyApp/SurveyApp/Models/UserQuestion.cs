﻿using System;
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
        public string Question { get; set; }
        public string Type { get; set; }
    }
}