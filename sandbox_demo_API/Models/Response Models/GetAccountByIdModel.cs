using System;

namespace sandbox_demo_API.Models.Response_Models
{
    public class GetAccountByIdModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }
    }
}
