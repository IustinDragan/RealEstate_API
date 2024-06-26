﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.DataAccess.Entities;

public class Company
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //pt autoincrement
    public int Id { get; set; }

    public string CompanyName { get; set; }
    public string CUI { get; set; }
    
    public ICollection<User> Users { get; set; }
}