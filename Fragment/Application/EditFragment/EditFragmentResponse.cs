namespace Fragment.Application.EditFragment;

public class EditFragmentResponse
{
    public string? Error { get; }

    public EditFragmentResponse(string? error = null)
    {
        Error = error;
    }
}