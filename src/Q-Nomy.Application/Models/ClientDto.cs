using System;

namespace QNomy.Application.Models
{
    public class ClientDto
    {
        public string FullName { get; set; }
        public int NumberInLine { get; set; }
        public int Status { get; set; }
        public DateTime CheckInTime { get; set; }
    }
}