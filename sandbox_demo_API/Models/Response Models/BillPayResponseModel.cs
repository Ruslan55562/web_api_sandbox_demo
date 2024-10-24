using System;

namespace sandbox_demo_API.Models.Response_Models
{
    public class BillPayResponseModel
    {
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public string PayeeName { get; set; }
    }

}
