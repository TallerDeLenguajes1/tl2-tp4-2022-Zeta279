namespace TP4.Models
{
    public class CadeteModel: PersonaModel
    {
        private List<PedidoModel> ListadoPedidos;

        public CadeteModel(int id, string nom, string direc, long tel): base(id, nom, direc, tel)
        {
            ListadoPedidos = new List<PedidoModel>();
        }

        public void IngresarPedido(PedidoModel pedido)
        {
            ListadoPedidos.Add(pedido);
        }

        static public List<CadeteModel> ObtenerCadetes(){
            string[] array;
            List<CadeteModel> cadetes = new List<CadeteModel>();

            foreach (string s in System.IO.File.ReadAllLines("CSV/cadetes.csv"))
            {
                if (s != "")
                {
                    try{
                        array = s.Split(";");
                        cadetes.Add(new CadeteModel(Int32.Parse(array[0]), array[1], array[2], long.Parse(array[3])));
                    }
                    catch(Exception ex){
                        Console.WriteLine("Ha ocurrido un error: " + ex.Message);
                    }
                    
                }
            }

            return cadetes;
        }

        public bool TienePedidoEnCurso()
        {
            if (ListadoPedidos.Count > 0) return ListadoPedidos[ListadoPedidos.Count - 1].EstaEnCurso();
            return false;
        }

        public void EliminarPedido(int id)
        {
            int index = 0;
            foreach(var pedido in ListadoPedidos)
            {
                if (pedido.Nro == id) break;
                index++;
            }

            if(ListadoPedidos.Count > 0) ListadoPedidos.RemoveAt(index);
        }

        public List<PedidoModel> ObtenerPedidos()
        {
            return ListadoPedidos;
        }

        public PedidoModel UltimoPedido()
        {
            if (ListadoPedidos.Count > 0) return ListadoPedidos[ListadoPedidos.Count - 1];
            return null;
        }

        static public void IngresarCadetes(List<CadeteModel> ListaCadetes)
        {
            string csv = "";

            foreach(var cadete in ListaCadetes){
                csv += $"{cadete.id};{cadete.nombre};{cadete.direccion};{cadete.telefono}\n";
            }

            System.IO.File.WriteAllText("CSV/cadetes.csv", csv);
        }

        public PedidoModel PedidoEnCurso(){
            if(ListadoPedidos.Count > 1 && ListadoPedidos[ListadoPedidos.Count - 1].EstaEnCurso()) return ListadoPedidos[ListadoPedidos.Count - 1];
            return null;
        }
    }
}
