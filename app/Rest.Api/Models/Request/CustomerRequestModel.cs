namespace Rest.Api.Models.Request
{
    public class CustomerRequestModel : ModelBase
    {
        public string Name { get; set; }
        
        public string LastName { get; set; }

        public DateTime Birth { get; set; }

        public int AddressId { get; set; }

        public AddressRequestModel Address { get; set; }
    }
}
