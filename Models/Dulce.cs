// Models/Dulce.cs
using System.ComponentModel.DataAnnotations;

namespace DulcesChurrascosAPI.Models;

public class Dulce
{
  public int Id { get; set; }

  [Required]
  public string TipoDulce { get; set; }

  public bool PorUnidad { get; set; }

  [Range(6, 24)]
  public int? CantidadPorCaja { get; set; }
}
