namespace AllUp2.Services.AccountS
{
    public class AccountService
    {
        public string Random { get; set; }
        public AccountService(string random)
        {
            Random = random;
        }
        public void Login(string username, string password)
        {
            Console.WriteLine(username + ":" + password);
        }
    }
}
