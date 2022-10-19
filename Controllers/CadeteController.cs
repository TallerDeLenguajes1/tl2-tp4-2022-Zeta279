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
            ViewData["cadetes"] = CadeteModel.ObtenerCadetes();

            return View();
        }

        // GET: CadeteController/Details/5
        public ActionResult Details(int id)
        {

            foreach(var cadete in CadeteModel.ObtenerCadetes()){
                if(cadete.id == id) return View(cadete);
            }

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
            int id = 0;

            try{
                foreach(var cadete in CadeteModel.ObtenerCadetes()){
                    id = cadete.id;
                }

                string csv = $"{id + 1};{nombre};{direccion};{tel}\n";

                System.IO.File.AppendAllText("CSV/cadetes.csv", csv);
            }
            catch(Exception ex){
                Console.WriteLine("Ha ocurrido un error: " + ex.Message);
                return RedirectToAction("Error", new {error = "Ha ocurrido un error al crear el objeto"});
            }
            

            return RedirectToAction("Create");
        }

        // GET: CadeteController/Edit/5
        public ActionResult Edit(int id)
        {
            var lista = CadeteModel.ObtenerCadetes();

            foreach(var cadete in lista){
                if(cadete.id == id) return View(cadete);
            }

            return RedirectToAction("Error", new {error = "No se ha encontrado el cadete solicitado"});
        }

        // POST: CadeteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string nombre, string direccion, long tel)
        {
            var lista = CadeteModel.ObtenerCadetes();
            CadeteModel aux;

            for(int i = 0; i < lista.Count; i++){
                if(lista[i].id == id){
                    aux = lista[i];
                    lista[i] = new CadeteModel(aux.id, nombre, direccion, tel);
                }
            }

            CadeteModel.IngresarCadetes(lista);

            return RedirectToAction("Index");
        }

        // GET: CadeteController/Delete/5
        public ActionResult Delete(int id)
        {
            var lista = CadeteModel.ObtenerCadetes();
            bool encontrado = false;

            for(int i = 0; i < lista.Count && !encontrado; i++){
                if(lista[i].id == id){
                    lista.RemoveAt(i);
                    encontrado = true;
                }
            }

            if(!encontrado){
                return View("Error", new {error = "No se encontro el cadete solicitado"});
            }
            else{
                CadeteModel.IngresarCadetes(lista);
            }

            return RedirectToAction("Index");
        }
    }
}
