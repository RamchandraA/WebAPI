using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Entities
{
  public class Product
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    [Required(ErrorMessage ="Product Name cannot be empty"), StringLength(maximumLength:250, MinimumLength =4)]
    public string Name { get; set; }
    [StringLength(maximumLength:250, MinimumLength =4)]
    public string Description { get; set; }
    [Required(ErrorMessage ="Price cannot be empty"), Column(TypeName ="float(8,2)"), Range(minimum:0.01, maximum:float.MaxValue)]
    public float Price { get; set; }
    [Required(ErrorMessage ="Category cannot be empty"), StringLength(maximumLength:150, MinimumLength =4)]
    public string Category { get; set; }
  }
}