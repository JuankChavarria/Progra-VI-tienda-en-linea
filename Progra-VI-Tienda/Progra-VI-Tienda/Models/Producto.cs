namespace Progra_VI_Tienda.Models
{
    public class Producto
    {
        public int Id_producto { get; set; }
        public required string Nombre { get; set; }
        public string Descripcion { get; set; }
        public required decimal Precio { get; set; }
        public required int Stock { get; set; }
        public int Id_categoria { get; set; }

    }
}
