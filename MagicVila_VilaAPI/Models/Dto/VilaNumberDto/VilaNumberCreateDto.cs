﻿using System.ComponentModel.DataAnnotations;

namespace MagicVila_VilaAPI.Models.Dto.VilaNumberDto
{
    public class VilaNumberCreateDto
    {
        [Required]
        public int VilaNo { get; set; }
        public string SpecialDetails { get; set; }
    }
}