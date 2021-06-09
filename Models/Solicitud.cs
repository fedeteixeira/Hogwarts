using System;
using System.ComponentModel.DataAnnotations;
namespace Hogwarts.Models
{
    public class Solicitud
    {
        public long Id {get; set;}

        [Required]
        [StringLength(20)]        
        public string nombre {get; set;}

        [Required]
        [StringLength(20)]  
        public string apellido {get; set;}

        [Required]
        [Range(0, ((10E9)-1))] 
        public long identificación {get; set;}

        [Required]
        [Range(0, 99)]  //2 dígitos
        public short edad {get;set;}

        [Required]
        [RegularExpression("Gryffindor|Hufflepuff|Ravenclaw|Slytherin", ErrorMessage = "Esa casa no pertence a Hogwarts")] 
        public string casa{get;set;}
    }
}