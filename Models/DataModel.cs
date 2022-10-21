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
                        if(Int32.Parse(aux[7]) == -1){
                            lista[Int32.Parse(aux[0])].AsignarCadete(null);
                            lista[Int32.Parse(aux[0])].EntregarPedido();
                        }
                        else if (Int32.Parse(aux[7]) != 0)
                        {
                            lista[Int32.Parse(aux[0])].IniciarPedido(CadeteList[Int32.Parse(aux[7])]);
                            CadeteList[Int32.Parse(aux[7])].IngresarPedido(lista[Int32.Parse(aux[0])]);
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

            ActualizarCadetes();
        }

        static public void IngresarCadete(string nom, string direc, long tel)
        {
            try{
                if(CadeteList.Count > 0) IDCadetes = CadeteList.Keys.Max() + 1;
                else IDCadetes = 1;
                IngresarCadete(new CadeteModel(IDCadetes, nom, direc, tel));
            }
            catch(Exception ex){
                Console.WriteLine("Ha ocurrido un error (IngresarCadete): " + ex.Message);
            }
        }

        static private void ActualizarCadetes()
        {
            File.WriteAllText("CSV/cadetes.csv", "");

            foreach (var cadetes in CadeteList)
            {
                IngresarCadete(cadetes.Value);
            }
        }

        static public void RestaurarDatos()
        {
            string datosCadete = File.ReadAllText("CSV/Restaurar/cadetes.csv");
            string datosPedido = File.ReadAllText("CSV/Restaurar/pedidos.csv");
            
            File.WriteAllText("CSV/cadetes.csv", datosCadete);
            File.WriteAllText("CSV/pedidos.csv", datosPedido);

            CadeteList = ObtenerCadetes();
            PedidoList = ObtenerPedidos();
        }

        static public bool BorrarCadete(int id)
        {
            if (!CadeteList[id].TienePedidoEnCurso())
            {

                foreach(var pedido in CadeteList[id].ObtenerPedidos())
                {
                    PedidoList[pedido.Nro].BorrarCadete();
                }

                CadeteList.Remove(id);
                
                ActualizarCadetes();
                ActualizarPedidos();

                return true;
            }
            else
            {
                return false;
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
            IngresarPedido(new PedidoModel(nro, det, idc, nomc, direc, tel, datos));
        }

        static public void IngresarPedido(string det, int idc, string nomc, string direc, long tel, string datos)
        {
            var lista = PedidoList.Keys.ToList<int>();
            int aux = 0;
            lista.Sort();
            IDPedidos = lista.Min();

            foreach (var num in lista)
            {
                aux = num;
                if (num > IDPedidos + 1)
                {
                    IDPedidos += 1;
                    break;
                }
                else
                {
                    IDPedidos = num;
                }
            }

            if (IDPedidos == aux) IDPedidos += 1;

            IngresarPedido(new PedidoModel(IDPedidos, det, idc, nomc, direc, tel, datos));
        }

        static private void ActualizarPedidos()
        {
            File.WriteAllText("CSV/pedidos.csv", "");

            foreach (var pedido in PedidoList.Values)
            {
                IngresarPedido(pedido);
            }
        }

        static public void AsignarPedidoACadete(int idPedido, int idCadete)
        {
            PedidoList[idPedido].IniciarPedido(CadeteList[idCadete]);
            CadeteList[idCadete].IngresarPedido(PedidoList[idPedido]);

            ActualizarPedidos();
        }

        static public void BorrarPedido(int id)
        {
            if (PedidoList[id].EstaEnCurso() || PedidoList[id].FueEntregado())
            {
                if(PedidoList[id].Cadete is not null) CadeteList[PedidoList[id].Cadete.id].EliminarPedido(id);
            }
            PedidoList.Remove(id);

            ActualizarPedidos();
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

            ActualizarPedidos();
        }
    }
}
