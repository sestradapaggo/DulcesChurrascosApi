namespace DulcesChurrascosAPI.Models;

public enum TipoProducto
{
    Churrasco,
    Dulce
}

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public TipoProducto Tipo { get; set; }

    // Sólo para churrasco
    public ModalidadChurrasco? Modalidad { get; set; }
    public string? TerminoCoccion { get; set; }
    public string? GuarnicionesDisponibles { get; set; }

    // Sólo para dulces
    public int? CantidadPorCaja { get; set; }
    public decimal PrecioUnitario { get; set; }
}
