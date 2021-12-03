using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Dtos
{
    public class MovieUpdateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(255, ErrorMessage = "Title can't be longer than 255 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Year Released is required")]
        [StringLength(4, ErrorMessage = "Year Released can't be longer than 4 numbers")]
        public string YearReleased { get; set; }
        [Required(ErrorMessage = "Media Type Primary is required")]
        public string MediaTypePri { get; set; }
        public string MediaTypeSec { get; set; }
        public string Genre { get; set; }
        [Required(ErrorMessage = "Movie/Tv entry is required")]
        public string MovTv { get; set; }
        public byte[] Image { get; set; }
    }
}
