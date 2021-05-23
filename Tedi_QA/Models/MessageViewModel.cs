using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tedi_QA.Models
{
    public class MessageViewModel
    {
        public int Qid { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        [EmailAddress]
        public string email{ get; set; }
        public string phone { get; set; }
        [MinLength(15,ErrorMessage = "Message should not be less than 15 characters.")]
        [MaxLength(1000,ErrorMessage ="Message should not exceed 1000 characters.")]
        [Required]
        public string message { get; set; }
        public string answer { get; set; }
        public DateTime Qdate { get; set; }
        public DateTime Adate { get; set; }
    } 
}
