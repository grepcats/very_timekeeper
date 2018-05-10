using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeryTimekeeper.Models
{
    [Table("Tasks")]
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public string Content { get; set; }
        public string timeTotal { get; set; }
        public string timeRemaining { get; set; }
        public DateTime timeToStart { get; set; }
        public DateTime timeToFinish { get; set; }
    }
}
