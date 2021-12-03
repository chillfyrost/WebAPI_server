using Microsoft.AspNetCore.Http;
using System;

namespace Entities.Dtos
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string YearReleased { get; set; }
        public string MediaTypePri { get; set; }
        public string MediaTypeSec { get; set; }
        public string Genre { get; set; }
        public string MovTv { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
