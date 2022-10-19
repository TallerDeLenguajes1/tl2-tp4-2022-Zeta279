namespace TP4.Models
{
    public class PedidoModel
    {
        public int Nro { get; set; }
        private string Detalles { get; set; }
        private ClienteModel cliente { get; set; }
        private bool EnCurso { get; set; }

        public PedidoModel()
        {

        }

        public PedidoModel(int nro, string det, int id, string nom, string direc, int tel, string datos)
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
    }
}
