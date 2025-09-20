using Fragment.Application.Dtos;
using MediatR;

namespace Fragment.Application.ListTags;

public class ListTagsRequest : IRequest<List<TagDto>>
{
}