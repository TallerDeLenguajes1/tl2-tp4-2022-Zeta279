using Microsoft.AspNetCore.Mvc;
using TP4.Models;

namespace TP4.Controllers
{
    public class PedidoController : Controller
    {
        public ActionResult Index()
        {
            ViewData["pedidos"] = DataModel.PedidoList;

            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string det, int idc, string nomc, string direc, long tel, string datos)
        {
            DataModel.IngresarPedido(det, idc, nomc, direc, tel, datos);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            if (DataModel.PedidoList.ContainsKey(id)) return View(DataModel.PedidoList[id]);
            else return RedirectToAction("Error", new { error = "No se ha encontrado el pedido solicitado" });
        }

        public ActionResult Delete(int id)
        {
            if (DataModel.PedidoList.ContainsKey(id))
            {
                DataModel.BorrarPedido(id);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Error", new { error = "No se ha encontrado el pedido solicitado" });
        }

        public ActionResult Estado(int id)
        {
            if (DataModel.PedidoList.ContainsKey(id))
            {
                DataModel.EntregarPedido(id);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Error", new { error = "No se ha encontrado el pedido solicitado" });
        }

        public ActionResult Asignar(int id)
        {
            if (DataModel.PedidoList.ContainsKey(id)) return View(DataModel.PedidoList[id]);
            else return RedirectToAction("Error", new { error = "No se ha encontrado el pedido solicitado" });
        }

        [HttpPost]
        public ActionResult Asignar(int idp, int idc)
        {
            DataModel.AsignarPedidoACadete(idp, idc);
            return RedirectToAction("Index");
        }

        public ActionResult Error(string error)
        {
            ViewData["error"] = error;

            return View();
        }
    }
}
