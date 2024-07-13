namespace Contacts_App.Models
{
    public class Users
    {
        public string? id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public List<Contacts>? contacts { get; set; }
    }
}
