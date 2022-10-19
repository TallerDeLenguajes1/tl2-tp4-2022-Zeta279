using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TP4.Controllers
{
    public class VerController : Controller
    {
        // GET: VerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VerController/Create
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

        // GET: VerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VerController/Edit/5
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

        // GET: VerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VerController/Delete/5
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
