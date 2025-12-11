using System;

namespace NewMessaging.ApplicationB.Models
{
    public class NewCustomerModel
    {
        public Guid TransactionId { get; set; }
        public string Name { get; set; }
        public string BillingPlan { get; set; }
        public string CompanyCode { get; set; }
    }
}
