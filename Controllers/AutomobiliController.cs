using AUTOSALONI_MVC.Data;
using AUTOSALONI_MVC.Models;
using AUTOSALONI_MVC.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AUTOSALONI_MVC.Controllers
{
    public class AutomobiliController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public AutomobiliController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void PopolaModelli()
        {
            IEnumerable<SelectListItem> listaModelli = dbContext.Modelli.Select(i => new SelectListItem
            {
                Text = i.modello,
                Value = i.IdModello.ToString()
            });
            ViewBag.ModelliList = listaModelli;
        }
        //LIST
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var automobili = await dbContext.Automobili.ToListAsync();
            foreach (var record in automobili)
            {
                record.modello = dbContext.Modelli.FirstOrDefault(u => u.IdModello == record.IdModello);
            }
            return View(automobili);
        }

        //ADD
        [HttpGet]
        public IActionResult Add()
        {
            PopolaModelli();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddAutomobileViewModel viewModel)
        {
            var automobile = new Automobile
            {
                IdModello = viewModel.IdModello,
                DataAcquisto = viewModel.data,
                Targa = viewModel.targa,
                PrezzoOfferto = viewModel.prezzo
            };
            await dbContext.Automobili.AddAsync(automobile);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Automobili");
        }
        //EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var automobile = await dbContext.Automobili.FindAsync(id);
            PopolaModelli();
            return View(automobile);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(Automobile viewModel)
        {
            var automobile = await dbContext.Automobili.FindAsync(viewModel.IdAuto);
            if (automobile is not null)
            {
                automobile.IdModello = viewModel.IdModello;
                automobile.DataAcquisto = viewModel.DataAcquisto;
                automobile.Targa = viewModel.Targa;
                automobile.PrezzoOfferto = viewModel.PrezzoOfferto;
                
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Automobili");
        }
        //DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(Automobile viewModel)
        {
            var automobile = await dbContext.Automobili
                .AsNoTracking()
                //costruisco viewModel come entità separata
                .FirstOrDefaultAsync(x => x.IdAuto == viewModel.IdAuto);
            if (automobile is not null)
            {
                dbContext.Automobili.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Automobili");

        }
    }
}
