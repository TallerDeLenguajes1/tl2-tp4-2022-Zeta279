namespace TP4.Models
{
    public class PedidoModel
    {
        public int Nro { get; }
        public string Detalles { get; }
        public ClienteModel cliente { get; }
        public CadeteModel Cadete { get; private set; }
        private bool EnCurso;
        private bool Entregado;

        public PedidoModel(int nro, string det, int id, string nom, string direc, long tel, string datos)
        {
            Nro = nro;
            Detalles = det;
            cliente = new ClienteModel(id, nom, direc, tel, datos);
            Entregado = false;
        }

        public void IniciarPedido(CadeteModel cadete)
        {
            EnCurso = true;
            Cadete = cadete;
        }

        public void EntregarPedido()
        {
            EnCurso = false;
            Entregado = true;
        }

        public void AsignarCadete(CadeteModel cadete)
        {
            Cadete = cadete;
        }

        public bool FueEntregado()
        {
            return Entregado;
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

        public string PasarACSV()
        {
            int auxA = 0, auxB = 0;
            if (EnCurso) auxA = 1;
            if (Entregado) auxB = 1;
            return $"{Nro};{Detalles};{cliente.id};{cliente.nombre};{cliente.direccion};{cliente.telefono};{cliente.DatosRef};{Cadete.id};{auxA};{auxB}\n";
        }
    }
}
