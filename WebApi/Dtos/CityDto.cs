using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Name is mandatory field")]
        [StringLength(50,MinimumLength=2)]
        [RegularExpression(".*[a-zA-Z]+.*",ErrorMessage="Only numeric value is not allowed")]
        public string Name { get; set; }
        [Required(ErrorMessage="Country is mandatory field")]
        public string Country { get; set; }
    }
}