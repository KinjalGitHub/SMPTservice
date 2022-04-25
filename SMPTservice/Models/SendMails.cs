namespace SMPTservice.Models
{
    public class SendMails
    {
        public string fromEmailAddress { get; set; }

        public string fromEmailName { get; set; }
         
        public  List<string> lstToEmailAddress { get; set; }
        public  string mailSubject { get; set; }

        public string mailBody { get; set; }

        public List<string> lstFilesToAttachPaths { get; set; }


    }
}
