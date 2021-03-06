using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnicornInsurance.MVC.Constants
{
    public static class SD
    {
        public const string CartSesh = "UnicornShoppingCart";
        public const string Token = "UnicornToken";

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

        // Pagination Items Per Page
        public const int MobileSuitsPerPage = 3;
        public const int WeaponsPerPage = 3;

        // Insurance Plans
        public const string StandardInsurancePlan = "Standard";
        public const string SuperInsurancePlan = "Super";
        public const string UltraInsurancePlan = "Ultra";

        // Deployment Result Types
        public const string GoodDeploymentResult = "Good";
        public const string BadDeploymentResult = "Bad";
    }
}
