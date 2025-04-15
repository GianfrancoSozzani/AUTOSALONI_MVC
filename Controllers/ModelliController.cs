using AUTOSALONI_MVC.Data;
using AUTOSALONI_MVC.Models;
using AUTOSALONI_MVC.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AUTOSALONI_MVC.Controllers
{
    public class ModelliController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public ModelliController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void PopolaMarca()
        {
            IEnumerable<SelectListItem> listaMarche = dbContext.Marche.Select(i => new SelectListItem
            {
                Text = i.Marca,
                Value = i.IdMarca.ToString()
            });
            ViewBag.MarcheList = listaMarche;
        }

        //LIST
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var modelli = await dbContext.Modelli.ToListAsync();
            foreach(var record in modelli)
            {
                record.Marca = dbContext.Marche.FirstOrDefault(u => u.IdMarca == record.IdMarca);
            }
            return View(modelli);
        }

        //ADD
        [HttpGet]
        public IActionResult Add()
        {
            PopolaMarca();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddModelloViewModel viewModel)
        {
            var modello = new Modello
            {
                modello = viewModel.nome,
                IdMarca = viewModel.IdMarca,
            };
            await dbContext.Modelli.AddAsync(modello);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Modelli");
        }


        //EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var modello = await dbContext.Modelli.FindAsync(id);
            PopolaMarca();
            return View(modello);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Modello viewModel)
        {
            var modello = await dbContext.Modelli.FindAsync(viewModel.IdModello);
            if (modello is not null)
            {
                modello.modello = viewModel.modello;
                modello.IdMarca = viewModel.IdMarca;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Modelli");
        }
        //DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(Modello viewModel)
        {
            var modello = await dbContext.Modelli
                .AsNoTracking()
                //costruisco viewModel come entità separata
                .FirstOrDefaultAsync(x => x.IdModello == viewModel.IdModello);
            if (modello is not null)
            {
                dbContext.Modelli.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            
            return RedirectToAction("List", "Modelli");

        }
    }
}
