using Microsoft.AspNetCore.Mvc;
using Rest.Api.Models.Request;
using Rest.Api.Models.Response;
using Rest.Api.Models.Update;
using Rest.Domain.App;
using Rest.Domain.App.Interfaces;
using Rest.Domain.App.Interfaces.Repository;
using Rest.Domain.App.Interfaces.Service;

namespace Rest.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AddressController : BaseController<Address, AddressRequestModel, AddressUpdateModel, AddressModel>
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressService _addressService;

        public AddressController(ILogger<AddressController> logger, IAddressService addressService) : base(logger, addressService)
        {
            _logger = logger;
            _addressService = addressService;
        }

        
    }
}