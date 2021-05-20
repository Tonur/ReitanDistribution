using System;

namespace ReitanDistribution.Core
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string ContactPerson { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
    }
}
