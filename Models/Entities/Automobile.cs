using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUTOSALONI_MVC.Models.Entities
{
    public class Automobile
    {
        [Key]
        public Guid IdAuto { get; set; }
        public Guid IdModello { get; set; }
        [ForeignKey("IdModello")]
        [ValidateNever]
        public Modello modello { get; set; }
        public DateTime DataAcquisto { get; set; }
        public string Targa { get; set; }
        public decimal PrezzoOfferto { get; set; }
    }
}
