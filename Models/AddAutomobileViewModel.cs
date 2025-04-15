using AUTOSALONI_MVC.Models.Entities;

namespace AUTOSALONI_MVC.Models
{
    public class AddAutomobileViewModel
    {
        public Guid IdAuto { get; set; }
        public Guid IdModello { get; set; }
        public DateTime data { get; set; }
        public string targa { get; set; }
        public decimal prezzo { get; set; }
    }
}
