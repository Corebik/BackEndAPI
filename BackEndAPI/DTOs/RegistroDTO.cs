namespace BackEndAPI.DTOs
{
    public class RegistroDTO
    {
        public int IdRegistro { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public int? Identificacion { get; set; }

        public int? Edad { get; set; }

        public int? RefCasa { get; set; }

        public string? NombreCasa { get; set; }
    }
}
