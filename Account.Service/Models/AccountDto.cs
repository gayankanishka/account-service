using Core.Enums;

namespace Account.Service.Models
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DataBaseOperationType OperationType { get; set; }
    }
}
