namespace DulcesChurrascosAPI.Models;

public class Inventario
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;
    public decimal Cantidad { get; set; }
    public string UnidadMedida { get; set; } = null!;
}
