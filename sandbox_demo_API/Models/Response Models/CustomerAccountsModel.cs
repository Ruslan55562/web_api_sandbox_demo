using System;

namespace sandbox_demo_API.Models.Response_Models
{
    public class CustomerAccountsModel
    {
        public int id { get; set; }
        public int customerId { get; set; }
        public string type { get; set; }
        public decimal balance { get; set; }
    }
}
