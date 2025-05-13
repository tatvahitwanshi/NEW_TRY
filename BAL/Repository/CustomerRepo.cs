using System.Security.Cryptography.X509Certificates;
using BAL.Interface;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BAL.Repository;

public class CustomerRepo : ICustomer
{
    private readonly CustomersContext _db;

    public CustomerRepo(CustomersContext db)
    {
        _db=db;
    }

    public async Task<List<Customer>> GetAllCustomer()
    {
        return await _db.Customers.ToListAsync();
    }
    public async Task<CustomerList> GetModalData(int id)
    {
        CustomerList customerList = new CustomerList();
        if(id != 0)
        {
            Customer? customer = await _db.Customers.FirstOrDefaultAsync(x=> x.Customerid == id);
            if(customer != null)
            {
                customerList.CustomerName=customer.Customername;
                customerList.CustomerEmail= customer.Emailaddress;
                customerList.PhoneNumber= customer.Phonenumber;
            }
        }
        return customerList;
    }

    public async Task SaveCustomerDetail( CustomerList customerList)
    {
        var email = customerList.CustomerEmail!.ToLower().Trim();
        using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Emailaddress!.ToLower().Trim() == email);
            if(customer == null)
            {
                customer= new Customer{
                    Customername= customerList.CustomerName,
                    Emailaddress= customerList.CustomerEmail,
                    Phonenumber= customerList.PhoneNumber
                };
                _db.Customers.Add(customer);
            }
            else
            {
                 
                customer.Customername= customerList.CustomerName;
                customer.Emailaddress= customerList.CustomerEmail;
                customer.Phonenumber= customerList.PhoneNumber;
             
                _db.Customers.Update(customer);
            }
            await _db.SaveChangesAsync();
            await transaction.CommitAsync(); 
    

        }
        catch
        {
            await transaction.RollbackAsync(); 
            throw; 
        }
    }
    public async Task<CustomerList> DeleteCustomerDetail(int id)
    {
        Customer? customer = await _db.Customers.FirstOrDefaultAsync(x => x.Customerid == id);
        if (customer != null)
        {
            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
        }
        return new CustomerList();
    }



}
