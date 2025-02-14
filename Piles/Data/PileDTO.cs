﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Piles.Data
{
    public class PileDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RuminationDTO> Ruminations { get; set; }
    }
}
