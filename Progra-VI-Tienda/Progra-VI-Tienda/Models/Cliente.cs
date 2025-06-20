namespace Progra_VI_Tienda.Models
{
    public class Cliente
    {
        public int Id_cliente { get; set; }
        public required string Nombre { get; set; }
        public required string Correo { get; set; }
        public required string Contraseña { get; set; }

        public required string Direccion { get; set; }

    }
}
