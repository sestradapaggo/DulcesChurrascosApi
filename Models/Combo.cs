namespace DulcesChurrascosAPI.Models;

public class Combo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string ProductosIncluidos { get; set; } = null!;
}
