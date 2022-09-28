namespace Rest.Api.Models.Response
{
    public class CustomerModel : ModelBase
    {
        public string Name { get; set; }
        
        public string LastName { get; set; }

        public DateTime Birth { get; set; }

        public int AddressId { get; set; }

        public AddressModel Address { get; set; }
    }
}
