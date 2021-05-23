using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tedi_QA.Data.Entities
{
    public class Question
    {
        [Key]
        public int Qid { get; set; }

        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        public string phone { get; set; }
        public bool isRead { get; set; }
        public bool isAnswered { get; set; }
        [Required]
        public string message { get; set; }
        public DateTime questionDate { get; set; }
    }
}
