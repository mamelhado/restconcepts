namespace Rest.Api.Models.Update
{
    public class CustomerUpdateModel : ModelBase
    {
        public string Name { get; set; }
        
        public string LastName { get; set; }

        public DateTime Birth { get; set; }

        public int AddressId { get; set; }

        public AddressUpdateModel Address { get; set; }
    }
}
