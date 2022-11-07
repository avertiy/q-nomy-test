using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QNomy.Domain.Entities
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public int NumberInLine { get; set; }
        [Required]
        public int Status { get; set; }

        [Required]
        public DateTime CheckInTime { get; set; }

    }
}