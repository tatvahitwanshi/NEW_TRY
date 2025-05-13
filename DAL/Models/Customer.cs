using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string? Customername { get; set; }

    public string? Emailaddress { get; set; }

    public string? Phonenumber { get; set; }
}
