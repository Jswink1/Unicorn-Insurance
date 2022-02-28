using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Constants
{
    public static class SD
    {
        // Custom User ID Claim Type
        public const string Uid = "uid";

        // Payment Statuses
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusRejected = "Rejected";

        // Order Statuses
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
    }
}
