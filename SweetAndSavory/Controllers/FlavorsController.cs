using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;

namespace SweetAndSavory.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly SweetAndSavoryContext _db;

    public FlavorsController(SweetAndSavoryContext db)
    {
      _db = db;
    }

    [AllowAnonymous]
    public ActionResult Index() 
    { 
      List<Flavor> model = _db.Flavors.ToList();
      return View(model); 
    }
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var model = _db.Flavors
        .Include(treat => treat.JoinEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(model);
    }

    public ActionResult AddTreat(int id)
    {
      var model = _db.Flavors
        .Include(flavor => flavor.JoinEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
      return View(model);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int TreatId)
    {
      if(TreatId != 0 && !_db.FlavorTreat.Any(model => model.FlavorId == flavor.FlavorId && model.TreatId == TreatId))
      {
        _db.FlavorTreat.Add(new FlavorTreat() {TreatId = TreatId, FlavorId = flavor.FlavorId});
        _db.SaveChanges();
      }
      return RedirectToAction("AddTreat");
    }

    [HttpPost]
    public ActionResult DeleteTreat(int joinId, int id) 
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      var model = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View("Details", model);
    }

    public ActionResult Edit(int id)
    {
      var model = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var model = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var model = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id );
      _db.Flavors.Remove(model);
      _db.SaveChanges();
      return RedirectToAction("Index");  
    }
  }
}
