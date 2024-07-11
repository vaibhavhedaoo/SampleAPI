using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SampleAPI.Entities
{
    public class Order
    {
        //[Index(IsUnique =true)]
        [ScaffoldColumn(false)]
        [HiddenInput(DisplayValue =false)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Required")]
        [MaxLength(length:100)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Required")]
        [MaxLength(length: 100)]
        public string Description { get; set; }        
        public DateTime EntryDate { get; set; }
        [DefaultValue(true)]
        public bool Invoiced { get; set; } = true;
        [DefaultValue(false)]
        public bool Deleted { get; set; } = false;
    }
}
