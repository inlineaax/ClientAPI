namespace ClientAPI.Domain.Entities
{
    public sealed class Client : Entity
    {
        public string? Name { get; private set; }
        public string? Gender { get; private set; }
        public string? Email { get; private set; }
        public bool? IsActive { get; private set; }

        public Client() { }

        public void Update(string? name, string? gender, string? email, bool? active)
        {
            Name = name;
            Gender = gender;
            Email = email;
            IsActive = active;
        }

        public Client(string? name, string? gender, string? email, bool? isActive)
        {
            Name = name;
            Gender = gender;
            Email = email;
            IsActive = isActive;
        }

        public Client(int id)
        {
            Id= id;
        }
    }
}
