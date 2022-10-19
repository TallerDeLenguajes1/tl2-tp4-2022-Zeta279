namespace TP4.Models
{
    public class ClienteModel: PersonaModel
    {
        private string DatosReferenciaDireccion;

        public ClienteModel(int id, string nom, string direc, int tel, string datos): base(id, nom, direc, tel)
        {
            DatosReferenciaDireccion = datos;
        }
    }
}
