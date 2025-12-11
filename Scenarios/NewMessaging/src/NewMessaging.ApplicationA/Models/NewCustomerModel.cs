using System;

namespace NewMessaging.ApplicationA.Models
{
    public class NewCustomerModel
    {
        public Guid TransactionId { get; set; }
        public string Name { get; set; }
        public string BillingPlan { get; set; }
        public string CompanyCode { get; set; }

        public bool IsCompleted
        {
            get
            {
                return !CompanyCode.IsNullOrWhiteSpace();
            }
        }
    }
}
