namespace Rest.Api.Models.Update
{
    public class AddressUpdateModel : ModelBase
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string State { get; set; }

        public string Number { get; set; }

        public string SuplementarInfo { get; set; }
    }
}
