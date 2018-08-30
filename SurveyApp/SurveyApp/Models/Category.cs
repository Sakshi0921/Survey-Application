using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyApp.Models
{
    public class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            this.QuestionBanks = new HashSet<QuestionBank>();
        }
        [Key]

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionBank> QuestionBanks { get; set; }
    }
}