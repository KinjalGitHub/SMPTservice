namespace SMTPservice.Models
{
    public class SendMail
    {
        public string FromEmailAddress { get; set; }

        public string FromEmailName { get; set; }
         
        public  List<string> ToEmailAddresses { get; set; }
        public  string MailSubject { get; set; }

        public string MailBody { get; set; }

        public List<string> FilesToAttachPaths { get; set; }
    }
}
