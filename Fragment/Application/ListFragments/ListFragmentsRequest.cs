using Fragment.Application.Dtos;
using MediatR;

namespace Fragment.Application.ListFragments;

public class ListFragmentsRequest : IRequest<List<TextFragmentDto>>
{
    public int Skip { get; }

    public int Take { get; }

    public ListFragmentsRequest(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }
}