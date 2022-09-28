using Rest.Domain.App;
using Rest.Domain.App.Interfaces.Repository;
using Rest.Domain.App.Interfaces.Service;

namespace Rest.Service
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }
    }
}
