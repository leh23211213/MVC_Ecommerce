namespace MVC_Ecommerce.Models
{
    public class Account
    {
        public Account()
        {
            this.Customers = new HashSet<Customer>();
        }

        public int AccountId { get; set; }
        public int LoginAccount { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Status { get; set; }
        public string AccountTypeId { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual AccountType AccountType { get; set; }
    }
}