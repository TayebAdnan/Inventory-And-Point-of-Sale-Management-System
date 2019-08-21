using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }
}
public class UserMetaData
{
    public int UserId { get; set; }
    [Display(Name = "Name")]
    [RegularExpression(@"^[a-zA-Z-]+$", ErrorMessage = "Use a valid name please")]
    [Required]
    public string UserName { get; set; }
    [Display(Name = "Email")]
    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
         ErrorMessage = "Invalid email format")]
    [Required]
    public string UserEmail { get; set; }
    [Display(Name = "Phone")]
    [Required]
    
    [DataType(DataType.PhoneNumber)]
    //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    [StringLength(14, MinimumLength = 11,ErrorMessage = "You must provide a phone number")]
    public string UserPhone { get; set; }
    [Display(Name = "Address")]
    [Required]
    public string UserAddress { get; set; }
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string UserPassword { get; set; }
    [Display(Name = "Image")]
    public string UserImage { get; set; }
    [Display(Name = "Role Name")]
    public int RoleId { get; set; }

}