namespace Rest.Api.Models.Request
{
    public class AddressRequestModel : ModelBase
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string State { get; set; }

        public string Number { get; set; }

        public string SuplementarInfo { get; set; }
    }
}
