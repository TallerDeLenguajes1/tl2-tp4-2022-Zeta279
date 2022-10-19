using Microsoft.AspNetCore.Mvc;
using TP4.Models;

namespace TP4.Controllers
{
    public class PedidoController : Controller
    {
        public ActionResult Index()
        {
            ViewData["pedidos"] = PedidoModel.ObtenerPedidos();

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int nro, string det, int id, string nom, string direc, long tel, string datos){
            
            
            return View();
        }
    }
}
