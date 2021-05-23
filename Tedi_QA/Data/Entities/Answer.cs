using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tedi_QA.Data.Entities
{
    public class Answer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int id { get; set; }

        public string UserID { get; set; }
        
        public string lecturerName { get; set; }
     
        public bool isPublished { get; set; }
        
        [Required]
        public string answer { get; set; }
        public DateTime answerDate { get; set; }

        [ForeignKey("Qid")]
        public Question question{get; set;}

        public int Qid { get; set; }
       
    }
}
