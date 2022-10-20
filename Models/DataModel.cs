namespace TP4.Models
{
    public class DataModel
    {
        static public Dictionary<int, CadeteModel> CadeteList = ObtenerCadetes();
        static public Dictionary<int, PedidoModel> PedidoList = ObtenerPedidos();
        static public int IDCadetes { get; private set; }
        static public int IDPedidos { get; private set; }

        public DataModel()
        {
            if (CadeteList.Count > 0) IDCadetes = CadeteList[CadeteList.Count - 1].id;
            else IDCadetes = 0;

            if (PedidoList.Count > 0) IDPedidos = PedidoList[PedidoList.Count - 1].Nro;
            else IDPedidos = 0;
        }

        static private Dictionary<int, CadeteModel> ObtenerCadetes()
        {
            string[] array;
            Dictionary<int, CadeteModel> cadetes = new();

            foreach (string s in File.ReadAllLines("CSV/cadetes.csv"))
            {
                if (s != "")
                {
                    try
                    {
                        array = s.Split(";");
                        cadetes.Add(Int32.Parse(array[0]), new CadeteModel(Int32.Parse(array[0]), array[1], array[2], long.Parse(array[3])));
                        IDCadetes = Int32.Parse(array[0]) + 1;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ha ocurrido un error (ObtenerCadetes): " + ex.Message);
                    }
                }
            }

            return cadetes;
        }

        static private Dictionary<int, PedidoModel> ObtenerPedidos()
        {
            string[] aux;
            Dictionary<int, PedidoModel> lista = new();

            foreach (var s in File.ReadAllLines("CSV/pedidos.csv"))
            {
                if (s != "")
                {
                    aux = s.Split(";");
                    try
                    {
                        lista.Add(Int32.Parse(aux[0]), new PedidoModel(Int32.Parse(aux[0]), aux[1], Int32.Parse(aux[2]), aux[3], aux[4], long.Parse(aux[5]), aux[6]));
                        if (Int32.Parse(aux[7]) != 0)
                        {
                            lista[Int32.Parse(aux[0])].IniciarPedido(CadeteList[Int32.Parse(aux[7])]);
                            if (Int32.Parse(aux[8]) == 0)
                            {
                                lista[Int32.Parse(aux[0])].EntregarPedido();
                            }
                        }
                        IDPedidos = Int32.Parse(aux[7]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ha ocurrido un error (ObtenerPedidos): " + ex.Message);
                    }
                }
            }

            return lista;
        }

        static public void ActualizarCadete(int id, string nom, string direc, long tel)
        {

            CadeteList[id] = new CadeteModel(id, nom, direc, tel);
            File.WriteAllText("CSV/cadetes.csv", "");

            foreach(var cadete in CadeteList.Values)
            {
                IngresarCadete(cadete);
            }
        }

        static public void IngresarCadete(string nom, string direc, long tel)
        {
            IDCadetes = CadeteList.Keys.Max() + 1;
            IngresarCadete(new CadeteModel(IDCadetes, nom, direc, tel));
        }

        static public void IngresarPedido(string det, int id, string nom, string direc, long tel, string datos)
        {
            IDPedidos++;
            PedidoList.Add(IDPedidos, new PedidoModel(IDPedidos, det, id, nom, direc, tel, datos));
        }

        static public void BorrarCadete(int id)
        {
            CadeteList.Remove(id);

            File.WriteAllText("CSV/cadetes.csv", "");

            foreach(var cadetes in CadeteList)
            {
                IngresarCadete(cadetes.Value);
            }
        }

        static public void IngresarCadete(CadeteModel cadete)
        {
            try
            {
                string csv = $"{cadete.id};{cadete.nombre};{cadete.direccion};{cadete.telefono}\n";

                File.AppendAllText("CSV/cadetes.csv", csv);

                if(!CadeteList.ContainsKey(cadete.id)) CadeteList.Add(cadete.id, cadete);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error (IngresarCadete): " + ex.Message);
            }
        }

        static public void IngresarPedido(int nro, string det, int idc, string nomc, string direc, long tel, string datos)
        {
            var lista = PedidoList.Keys.ToList<int>();
            lista.Sort();
            IDPedidos = lista.Min();

            foreach (var num in lista)
            {
                if(num > IDPedidos + 1)
                {
                    IDPedidos += 1;
                    break;
                }
                else
                {
                    IDPedidos = num;
                }
            }

            IngresarPedido(new PedidoModel(nro, det, idc, nomc, direc, tel, datos));
        }

        static public void IngresarPedido(PedidoModel pedido)
        {

            try
            {
                File.AppendAllText("CSV/pedidos.csv", pedido.PasarACSV());
                if (!PedidoList.ContainsKey(pedido.Nro)) PedidoList[pedido.Nro] = pedido;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error (IngresarPedido): " + ex.Message);
            }
        }

        static public void EntregarPedido(int id)
        {
            PedidoList[id].EntregarPedido();

            File.WriteAllText("CSV/pedidos.csv", "");

            foreach(var pedido in PedidoList.Values)
            {
                IngresarPedido(pedido);
            }
        }
    }
}
