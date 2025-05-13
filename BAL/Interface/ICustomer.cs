using DAL.Models;
using DAL.ViewModels;

namespace BAL.Interface;

public interface ICustomer
{
    public Task<List<Customer>> GetAllCustomer();
    public Task<CustomerList> GetModalData(int id);

    public Task SaveCustomerDetail( CustomerList customerList);
    public Task<CustomerList> DeleteCustomerDetail(int id);


}
