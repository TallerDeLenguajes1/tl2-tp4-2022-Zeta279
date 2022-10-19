using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP4.Models;

namespace TP4.Controllers
{
    public class CadeteController : Controller
    {
        // GET: CadeteController
        public ActionResult Index()
        {
            string[] array;
            ViewData["cadetes"] = new List<CadeteModel>();

            foreach (string s in System.IO.File.ReadAllLines("CSV/cadetes.csv"))
            {
                if (s != "")
                {
                    array = s.Split(";");
                    ((List<CadeteModel>)ViewData["cadetes"]).Add(new CadeteModel(Int32.Parse(array[0]), array[1], array[2], Int32.Parse(array[3])));
                }
            }

            return View();
        }

        // GET: CadeteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadeteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadeteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CadeteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CadeteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CadeteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadeteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
