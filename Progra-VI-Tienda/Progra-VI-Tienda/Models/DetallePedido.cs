namespace Progra_VI_Tienda.Models
{
    public class DetallePedido
    {
        public int Id_detalle { get; set; }
        public int Id_pedido { get; set; }
        public int Id_producto{ get; set; }
        public required int Cantidad { get; set; }
        public required decimal Total { get; set; }
    }
}