using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Constants
{
    public static class SD
    {
        public const string CartSesh = "UnicornShoppingCart";

        // User Roles
        public const string AdminRole = "Admin";
        public const string CustomerRole = "Customer";

        // Payment Statuses
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusRejected = "Rejected";

        // Order Statuses
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
    }
}
