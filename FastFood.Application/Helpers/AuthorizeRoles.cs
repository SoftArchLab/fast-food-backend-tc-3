namespace FastFood.Application.Helpers
{
    public static class AuthorizeRoles
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Guest = "Guest";
        public const string AllRoles = $"{Admin}, {Guest}, {Customer}";
        public const string GuestAndCustomerRoles = $"{Guest}, {Customer}";
        public const string AdminAndCustomerRoles = $"{Admin}, {Customer}";
        public const string AdminAndGuestRoles = $"{Admin}, {Guest}";
    }
}
