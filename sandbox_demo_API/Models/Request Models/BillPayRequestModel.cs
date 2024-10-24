namespace sandbox_demo_API.Models.Request_Models
{
    public class BillPayRequestModel
    {
        public int accountId { get; set; }
        public decimal amount { get; set; }
        public Body body { get; set; }
    }

    public class Body
    {
        public string name { get; set; }
        public Address address { get; set; }
        public string phoneNumber { get; set; }
        public int accountNumber { get; set; }
    }

    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
    }


}
