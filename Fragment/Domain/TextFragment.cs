namespace Fragment.Domain;

public class TextFragment
{
    public int Id { get; protected set; }
    
    private string _text;

    public string Text
    {
        get { return _text; }
        
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Text must be supplied.");
            }

            _text = value;
        }
    }

    public DateTimeOffset CreatedOn { get; protected set; }
    
    public ICollection<Tag> Tags { get; protected set; }
    
    protected TextFragment()
    {
        Tags = new List<Tag>();
        CreatedOn = DateTimeOffset.UtcNow;
    }

    public TextFragment(string text) : this()
    {
        Text = text;
    }
}