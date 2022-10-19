namespace TP4.Models
{
    public class CadeteModel: PersonaModel
    {
        private List<PedidoModel> ListadoPedidos;

        public CadeteModel(int id, string nom, string direc, int tel): base(id, nom, direc, tel)
        {
            ListadoPedidos = new List<PedidoModel>();
        }

        public void IngresarPedido(PedidoModel pedido)
        {
            ListadoPedidos.Add(pedido);
        }
    }
}
