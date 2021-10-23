using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetAndSavory.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly SweetAndSavoryContext _db;

    public TreatsController(SweetAndSavoryContext db)
    {
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index() 
    { 
      List<Treat> model = _db.Treats.ToList();
      return View(model); 
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var model = _db.Treats
        .Include(treat => treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      return View(model);
    }

    public ActionResult AddFlavor(int id)
    {
      var model = _db.Treats
        .Include(treat => treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View(model);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      if(FlavorId != 0 && !_db.FlavorTreat.Any(model => model.TreatId == treat.TreatId && model.FlavorId == FlavorId))
      {
        _db.FlavorTreat.Add(new FlavorTreat() {FlavorId = FlavorId, TreatId = treat.TreatId});
        _db.SaveChanges();
      }
      return RedirectToAction("AddFlavor");
    }

    [HttpPost]
    public ActionResult DeleteFlavor(int joinId, int id) 
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      var model = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View("Details", model);
    }

    public ActionResult Edit(int id)
    {
      var model = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var model = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var model = _db.Treats.FirstOrDefault(treat => treat.TreatId == id );
      _db.Treats.Remove(model);
      _db.SaveChanges();
      return RedirectToAction("Index");  
    }
  }
}