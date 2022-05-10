namespace MotoStore.Services.Senders
{
    public class BaseSender
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public bool UseSSL { get; set; }

        public BaseSender(string host, int port, string @from, string password, bool useSsl)
        {
            Host = host;
            Port = port;
            From = @from;
            Password = password;
            UseSSL = useSsl;
        }
    }
}