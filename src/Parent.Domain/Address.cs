using Parent.Common;

namespace Parent.Domain
{
    public record Address : SearchableValueObject
    {
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }

        public Address(string address1, string address2, string city, string state, string zip)
        {
            if (string.IsNullOrWhiteSpace(address1))
            {
                throw new ArgumentNullException(nameof(address1));
            }
            
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }
            
            if (string.IsNullOrWhiteSpace(state)) 
            {
                throw new ArgumentNullException(nameof(state));
            }
            
            if (string.IsNullOrWhiteSpace(zip))
            {
                throw new ArgumentNullException(nameof(zip));
            }

            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            Zip = zip;
        }

        public override bool Contains(string searchText)
        {
            var normalizedSearchText = searchText.ToUpper();

            return City.ToUpper().Contains(normalizedSearchText)
                   || State.ToUpper().Contains(normalizedSearchText);
        }
    }
}

