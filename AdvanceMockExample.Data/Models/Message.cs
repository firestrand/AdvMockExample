namespace AdvanceMockExample.Models
{
    public class Message
    {
        public string Destination { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}