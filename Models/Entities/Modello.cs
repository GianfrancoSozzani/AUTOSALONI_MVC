using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AUTOSALONI_MVC.Models.Entities
{
    public class Modello
    {
        [Key]
        public Guid IdModello { get; set; }
        public string modello { get; set; }

        public Guid IdMarca { get; set; }
        [ForeignKey("IdMarca")]
        [ValidateNever]
        public MARCHE Marca { get; set; }
    }
}
