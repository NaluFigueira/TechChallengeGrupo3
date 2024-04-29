namespace PosTech.TechChallenge.Contacts.Application;

public record class DeleteContactDTO
{
    public DeleteContactDTO(Guid id)
    {
        this.Id = id;
    }

    public Guid Id { get; set; }
}
