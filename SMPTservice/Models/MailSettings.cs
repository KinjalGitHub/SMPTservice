namespace SMPTservice.Models
{
    public static class MailSettings
    {
        public  static string DomainUserName { get; set; }
        public static string DomainPassword { get; set; }
        public static string SMTPServerName { get; set; }
        public static int Port { get; set; }

        public static string log_path { get; set; }
    }
}
