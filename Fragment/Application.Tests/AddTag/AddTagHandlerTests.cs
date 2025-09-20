using Shouldly;
using NSubstitute;
using Fragment.Application.AddTag;
using Fragment.Domain.Repositories;
using Fragment.Domain;
using NSubstitute.ExceptionExtensions;

namespace Fragment.Application.Tests;

public class AddTagHandlerTests
{
    public class Handle
    {
        private class FakeAddTagHandler : AddTagHandler
        {
            public Tag Tag { get; init; }
            public FakeAddTagHandler(
                IUnitOfWork unitOfWork,
                ITagRepository tagRepository,
                Tag tag)
                : base(unitOfWork, tagRepository)
            {
                Tag = tag;
            }

            protected override Tag MakeTag(string name)
            {
                return Tag;
            }
        }

        [Fact]
        public async Task RequestMustBeSupplied()
        {
            // Arrange
            var ct = CancellationToken.None;
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var tagRepository = Substitute.For<ITagRepository>();

            var sut = new AddTagHandler(
                unitOfWork,
                tagRepository);

            // Act
            var action = async () => { _ = await sut.Handle(null!, ct); };

            // Assert
            await action.ShouldThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ReturnsAssignedId()
        {
            // Arrange
            const int expectedId = 100;
            var ct = CancellationToken.None;
            var tag = new Tag("a_tag");
            var request = new AddTagRequest(tag.Name);

            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork
                .When(u => u.CommitChangesAsync(ct))
                .Do(x => tag.GetType().GetProperty("Id")!.SetValue(tag, expectedId));

            var tagRepository = Substitute.For<ITagRepository>();

            var sut = new FakeAddTagHandler(
                unitOfWork,
                tagRepository,
                tag);

            // Act
            var result = await sut.Handle(request, ct);

            // Assert
            result.ShouldBe(expectedId);
        }

        [Fact]
        public async Task PersistsTagWithName()
        {
            // Arrange
            var ct = CancellationToken.None;
            var request = new AddTagRequest("a_tag");
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var tagRepository = Substitute.For<ITagRepository>();

            var sut = new AddTagHandler(
                unitOfWork,
                tagRepository);

            // Act
            _ = await sut.Handle(request, ct);

            // Assert
            await unitOfWork.Received().CommitChangesAsync(ct);
            await tagRepository.Received().AddAsync(Arg.Is<Tag>(t => t.Name == request.Name), ct);
        }
    }
}