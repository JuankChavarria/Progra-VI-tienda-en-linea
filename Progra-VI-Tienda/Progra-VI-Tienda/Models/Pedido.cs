namespace Progra_VI_Tienda.Models
{
    public class Pedido
    {
        public int Id_pedido{ get; set; }
        public int Id_cliente { get; set; }
        public DateTime Fecha { get; set; }
        public required decimal Total { get; set; }
        public required string Estado { get; set; }
    }
}
