using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using DAL.Models;

namespace DAL.ViewModels;

public class CustomerViewModel
{
    public List<Customer> Customers {get; set;} = new List<Customer>();
   public CustomerList? AddEditCustomerLists {get; set;} 

}
public class CustomerList
{
    public int Id {get; set;}

    [Required]
    public string? CustomerName {get; set;}

    [Required]
    public string? CustomerEmail {get; set;}

    [Required]
    [MaxLength(10)]
    public string? PhoneNumber {get; set;}

}
