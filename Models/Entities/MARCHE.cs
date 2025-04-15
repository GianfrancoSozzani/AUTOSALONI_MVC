using System.ComponentModel.DataAnnotations;

namespace AUTOSALONI_MVC.Models.Entities
{
    public class MARCHE
    {
        [Key]
        public Guid IdMarca { get; set; }
        public string Marca { get; set; }
        public string Paese { get; set; }
    }
}
