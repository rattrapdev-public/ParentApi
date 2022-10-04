using Parent.Common;

namespace Parent.Domain
{
    public record Address : ValueObject
    {
        public string Address1 { get; }
        public string Address2 { get; }
        public string City { get; }
        public string State { get; }
        public string Zip { get; }

        public Address(string address1, string address2, string city, string state, string zip)
        {
            if (string.IsNullOrWhiteSpace(address1))
            {
                throw new ArgumentException("The address1 field can not be null or white space");
            }
            
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            Zip = zip;
        }
    
    }
}

