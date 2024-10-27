using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpeedShip.Model.Models;

public partial class Category
{
    public long CategoryId { get; set; }

    [DisplayName("Category Name")]
    [MaxLength(50)]
    public string CategoryName { get; set; } = null!;

    [MaxLength(50)]
    public string? Description { get; set; }

	[DisplayName("Display Order")]
    [Range(1,100)]
    public int DisplayOrder { get; set; }

    [DisplayName("Created By")]
	public string? CreatedBy { get; set; }

    [DisplayName("Date Created")]
	public DateTime? DateCreated { get; set; }

    [DisplayName("Updated By")]
	public string? UpdatedBy { get; set; }

    [DisplayName("Date Updated")]
	public DateTime? DateUpdated { get; set; }

	public string? DeletedBy { get; set; }

    public DateTime? DateDeleted { get; set; }

    public bool IsDeleted { get; set; }
}
