using Fragment.Application.AddFragment;
using Shouldly;
using NSubstitute;
using Fragment.Domain.Repositories;
using Fragment.Domain;

namespace Fragment.Application.Tests.AddFragment;

public class AddFragmentHandlerTests
{
    public class Handle
    {
        private class FakeAddFragmentHandler : AddFragmentHandler
        {
            public TextFragment Fragment { get; init; }

            public FakeAddFragmentHandler(
                IUnitOfWork unitOfWork,
                ITextFragmentRepository textFragmentRepository,
                ITagRepository tagRepository,
                TextFragment fragment)
                : base(unitOfWork, textFragmentRepository, tagRepository)
            {
                Fragment = fragment;
            }

            protected override TextFragment MakeFragment(string text)
            {
                return Fragment;
            }
        }

        [Fact]
        public async Task RequestMustBeSupplied()
        {
            // Arrange
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var textFragmentRepository = Substitute.For<ITextFragmentRepository>();
            var tagRepository = Substitute.For<ITagRepository>();

            var sut = new AddFragmentHandler(
                unitOfWork,
                textFragmentRepository,
                tagRepository);

            // Act
            var action = async () => { await sut.Handle(null!, CancellationToken.None); };

            // Assert
            await action.ShouldThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ReturnsAssignedId()
        {
            // Arrange
            const int expectedId = 100;
            var ct = CancellationToken.None;
            var fragment = new TextFragment("some_text");
            var request = new AddFragmentRequest(fragment.Text, []);

            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork
                .When(u => u.CommitChangesAsync(ct))
                .Do(x => fragment.GetType().GetProperty("Id")!.SetValue(fragment, expectedId));

            var textFragmentRepository = Substitute.For<ITextFragmentRepository>();
            var tagRepository = Substitute.For<ITagRepository>();

            var sut = new FakeAddFragmentHandler(
                unitOfWork,
                textFragmentRepository,
                tagRepository,
                fragment);

            // Act
            var result = await sut.Handle(request, ct);

            // Assert
            result.ShouldBe(expectedId);
        }

        [Fact]
        public async Task PersistsFragmentWithTextAndTags()
        {
            // Arrange
            var tag = new Tag("a_tag");
            tag.GetType().GetProperty("Id")!.SetValue(tag, 100);

            var ct = CancellationToken.None;
            var request = new AddFragmentRequest("some_text", [tag.Id]);
            var unitOfWork = Substitute.For<IUnitOfWork>();
            var textFragmentRepository = Substitute.For<ITextFragmentRepository>();

            var tagRepository = Substitute.For<ITagRepository>();
            tagRepository.GetByIdAsync(tag.Id, ct).Returns(tag);

            var sut = new AddFragmentHandler(
                unitOfWork,
                textFragmentRepository,
                tagRepository);

            // Act
            _ = await sut.Handle(request, ct);

            // Assert
            await textFragmentRepository
                .Received()
                .AddAsync(Arg.Is<TextFragment>(f => f.Text == request.Text && f.Tags.Contains(tag)), ct);

            await unitOfWork.Received().CommitChangesAsync(ct);
        }
    }
}