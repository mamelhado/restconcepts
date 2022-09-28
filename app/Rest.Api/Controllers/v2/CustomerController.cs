using Microsoft.AspNetCore.Mvc;
using Rest.Api.Models.Request;
using Rest.Api.Models.Response;
using Rest.Api.Models.Update;
using Rest.Domain.App;
using Rest.Domain.App.Interfaces.Repository;
using Rest.Domain.App.Interfaces.Service;

namespace Rest.Api.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomerController : BaseController<Customer, CustomerRequestModel, CustomerUpdateModel, CustomerModel>
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService) : base(logger, customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        
    }
}