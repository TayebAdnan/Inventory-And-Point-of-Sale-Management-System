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
    public string ProductName { get; set; }
    public int CategoryId { get; set; }
    public int ColorId { get; set; }
    public int SizeId { get; set; }
    public Nullable<int> ProductQuantity { get; set; }
    public Nullable<int> AlertQuantity { get; set; }
    public string SellingPrice { get; set; }
    //public byte[] Image { get; set; }

    public String ProductImage { get; set; }
    public int ManufactureId { get; set; }
    public Nullable<System.DateTime> ProductDate { get; set; }

    


}
