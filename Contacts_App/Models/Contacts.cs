namespace Contacts_App.Models
{
    public class Contacts
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? number { get; set; }
        public string? email { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdAt { get; set; }
        public bool? isSpam { get; set; }
        public Users? user { get; set; }
    }
}
