namespace ClientAPI.Domain.Messages
{
    public class ClientMessage
    {
        public string Operation { get; set; } // "Create", "Update", "Delete"
        public ClientDto Client { get; set; }
    }

    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
    }
}
