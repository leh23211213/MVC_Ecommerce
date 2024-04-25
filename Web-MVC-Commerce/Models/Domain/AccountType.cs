namespace MVC_Ecommerce.Models
{
    public class AccountType
    {
        public int AccountTypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}