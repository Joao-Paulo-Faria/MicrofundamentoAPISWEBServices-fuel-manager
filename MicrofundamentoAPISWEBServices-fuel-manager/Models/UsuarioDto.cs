﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MicrofundamentoAPISWEBServices_fuel_manager.Models
{
    public class UsuarioDto
    {
        public int? Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Perfil Perfil { get; set; }

    }
}
