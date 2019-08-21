using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
}


public class ProductMetaData
{
    [Display(Name = "Product Code")]
    [Required]
    public string ProductCode { get; set; }

    [Display(Name = "Product Name")]
    [Required]
    [RegularExpression(@"^[a-zA-Z-]+$", ErrorMessage = "Use a valid name please")]
    public string ProductName { get; set; }
    [Display(Name = "Category")]
    [Required]
    public int CategoryId { get; set; }
    [Required]
    [Display(Name = "Color")]
    public int ColorId { get; set; }
    [Display(Name = "Size")]
    public int SizeId { get; set; }
    [Display(Name = "Product Quantity")]
    [Required(ErrorMessage = "Product Quantity must be at least 1")]
    [Range(1, Int32.MaxValue)]
    
    public Nullable<int> ProductQuantity { get; set; }

    [Required(ErrorMessage = "Product Quantity must be at least 1")]
    [Range(1, Int32.MaxValue)]
    [Display(Name = "Alert Quantity")]
    public Nullable<int> AlertQuantity { get; set; }
    [Display(Name = "Selling Price")]
    public string SellingPrice { get; set; }
    //public byte[] Image { get; set; }
    [Display(Name = "Product Image")]
    
    public String ProductImage { get; set; }
    public int ManufactureId { get; set; }
    public Nullable<System.DateTime> ProductDate { get; set; }

    


}
