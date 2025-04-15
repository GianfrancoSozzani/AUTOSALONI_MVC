using AUTOSALONI_MVC.Data;
using AUTOSALONI_MVC.Models;
using AUTOSALONI_MVC.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AUTOSALONI_MVC.Controllers
{
    public class MarcheController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public MarcheController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMarcheViewModel viewModel)
        {
            var marca = new MARCHE
            {
                Marca = viewModel.name.ToString().Trim(),
                Paese = viewModel.country.ToString().Trim()
            };
            await dbContext.Marche.AddAsync(marca);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Marche");
        }


        //LIST
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var marca = await dbContext.Marche.OrderBy(m => m.Marca).ToListAsync();
            return View(marca); //non siamo passati dal view model
        }

        //EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var marca = await dbContext.Marche.FindAsync(Id);
            return View(marca);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MARCHE viewModel)
        {
            var marca = await dbContext.Marche.FindAsync(viewModel.IdMarca);
            //si rifa questa ricerca di nuovo perché il record potrebbe essere stato cancellato
            if (marca is not null)
            {
                marca.Marca = viewModel.Marca;
                marca.Paese = viewModel.Paese;
                await dbContext.SaveChangesAsync();
            }


            return RedirectToAction("List", "Marche"); //azione //controller 
        }

        //DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(MARCHE viewModel)
        {
            var facolta = await dbContext.Marche
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdMarca == viewModel.IdMarca);

            if (facolta is not null)
            {
                dbContext.Marche.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Marche"); //azione //controller 
        }

    }
}
