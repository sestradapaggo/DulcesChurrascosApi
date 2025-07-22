using System.ComponentModel.DataAnnotations;

namespace DulcesChurrascosAPI.Models;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }

    [Required]
    public string Detalle { get; set; } = null!;

    public decimal Total { get; set; }
}
