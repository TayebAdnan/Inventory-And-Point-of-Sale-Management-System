using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS.Models.Validation
{
    [MetadataType(typeof(RoleMetadata))]
    public partial class Role
    {
    }
}

public class RoleMetadata
{
    [Display(Name ="Role Name")]
    [Required]
    public int RoleId { get; set; }
    public string RoleName { get; set; }
}