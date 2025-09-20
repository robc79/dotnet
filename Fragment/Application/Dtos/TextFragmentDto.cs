namespace Fragment.Application.Dtos;

public class TextFragmentDto
{
    public int Id { get; set; }

    public string Text { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public TagDto[] Tags { get; set; }
}