using System.ComponentModel.DataAnnotations;

namespace DulcesChurrascosAPI.Models
{
    public enum ModalidadChurrasco
    {
        Individual,
        Familiar3,
        Familiar5
    }

    public class Churrasco
{
    public int Id { get; set; }

    [Required]
    public string TipoCarne { get; set; }

    [Required]
    public string TerminoCoccion { get; set; }

    [Required]
    public string Guarniciones { get; set; }

    [Range(1,5)]
    public int Porciones { get; set; }

    [Range(0,int.MaxValue)]
    public int PorcionesExtra { get; set; }

    public ModalidadChurrasco Modalidad { get; set; }
}
    
}
