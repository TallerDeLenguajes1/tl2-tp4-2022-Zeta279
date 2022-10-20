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
            ViewData["cadetes"] = DataModel.CadeteList;

            return View();
        }

        // GET: CadeteController/Details/5
        public ActionResult Details(int id)
        {

            if(DataModel.CadeteList.ContainsKey(id)) return View(DataModel.CadeteList[id]);

            return RedirectToAction("Error", new {error = "No se ha encontrado el cadete solicitado"});
        }

        public ActionResult Error(string error)
        {
            ViewData["error"] = error;
            return View();
        }

        // GET: CadeteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadeteController/Create
        [HttpPost]
        public ActionResult Create(string nombre, string direccion, long tel)
        {
            DataModel.IngresarCadete(nombre, direccion, tel);

            return RedirectToAction("Index");
        }

        // GET: CadeteController/Edit/5
        public ActionResult Edit(int id)
        {
            if (DataModel.CadeteList.ContainsKey(id)) return View(DataModel.CadeteList[id]);
            return RedirectToAction("Error", new {error = "No se ha encontrado el cadete solicitado"});
        }

        // POST: CadeteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string nombre, string direccion, long tel)
        {
            DataModel.ActualizarCadete(id, nombre, direccion, tel);

            return RedirectToAction("Index");
        }

        // GET: CadeteController/Delete/5
        public ActionResult Delete(int id)
        {
            if (DataModel.BorrarCadete(id)) return RedirectToAction("Index");
            else return RedirectToAction("Error", new { error = "No es posible eliminar un cadete con un pedido en curso" });
        }
    }
}
