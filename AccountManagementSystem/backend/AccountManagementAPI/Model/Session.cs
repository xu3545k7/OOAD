namespace AccountManagementAPI.Models
{
    public class Session
    {
        public string Token { get; set; }
        public int AccountId { get; set; }
        public DateTime Expiration { get; set; }
    }
}
