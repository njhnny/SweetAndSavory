using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using SweetAndSavory.Models;
using System.Linq;

namespace SweetAndSavory.Controllers
{
  public class HomeController : Controller
  {
    private readonly SweetAndSavoryContext _db;
    public HomeController(SweetAndSavoryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      dynamic model = new ExpandoObject();
      model.Flavors = _db.Flavors.ToList();
      model.Treats = _db.Treats.ToList();
      return View(model);
    }
    // [HttpGet("/")]
    //   public ActionResult Index()
    //   {
    //     return View();
    //   }
  }
}