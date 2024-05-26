using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace practica03.Integration.dto
{
    public class Registro
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Job { get; set; }
        
    }
}