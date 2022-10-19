namespace TP4.Models
{
    public class PedidoModel
    {
        public int Nro { get; }
        private string Detalles;
        private ClienteModel cliente;
        private bool EnCurso;

        public PedidoModel(int nro, string det, int id, string nom, string direc, long tel, string datos)
        {
            Nro = nro;
            Detalles = det;
            cliente = new ClienteModel(id, nom, direc, tel, datos);
        }

        public void IniciarPedido()
        {
            EnCurso = true;
        }

        public void EntregarPedido()
        {
            EnCurso = false;
        }

        public bool EstaEnCurso()
        {
            return EnCurso;
        }

        public override string ToString()
        {
            string curso;
            if (EnCurso) curso = "Si";
            else curso = "No";
            return $"Número: {Nro}\nDetalles: {Detalles}\nEn curso: {curso}\nCliente: \n{cliente}";
        }

        static public List<PedidoModel> ObtenerPedidos()
        {
            string[] aux;
            List<PedidoModel> lista = new List<PedidoModel>();

            foreach (var s in System.IO.File.ReadAllLines("CSV/pedidos.csv"))
            {
                if (s != "")
                {
                    aux = s.Split(";");
                    try
                    {
                        lista.Add(new PedidoModel(Int32.Parse(aux[0]), aux[1], Int32.Parse(aux[2]), aux[3], aux[4], Int32.Parse(aux[5]), aux[6]));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ha ocurrido un error: " + ex.Message);
                    }
                }
            }

            return lista;
        }

        public string PasarACSV()
        {
            return $"{Nro};{Detalles};{cliente.id};{cliente.nombre};{cliente.direccion};{cliente.telefono};{cliente.DatosRef}\n";
        }

        static public void IngresarPedido(PedidoModel pedido)
        {
            int index = 0;

            foreach (var s in System.IO.File.ReadAllLines("CSV/pedidos.csv"))
            {
                if (s != "")
                {
                    try
                    {
                        index = Int32.Parse(s.Split(";")[0]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ha ocurrido un error: " + ex.Message);
                    }
                }
            }

            try
            {
                System.IO.File.AppendAllText("CSV/pedidos.csv", pedido.PasarACSV());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error: " + ex.Message);
            }
        }
    }
}
