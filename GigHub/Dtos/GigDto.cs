using GigHub.Models;
using System;
using System.Collections.Generic;

namespace GigHub.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; private set; }
        public UserDto Artist { get; set; }
        public DateTime DatetTime { get; set; }
        public string Venue { get; set; }
        public GenreDto Genre { get; set; }


    }
}