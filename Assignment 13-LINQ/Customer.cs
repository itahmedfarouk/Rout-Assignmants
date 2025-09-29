using System;
using System.Collections.Generic;

public class Customer
{
    public string CustomerID { get; set; }
    public string CompanyName { get; set; }
    public List<Order> Orders { get; set; }
}
