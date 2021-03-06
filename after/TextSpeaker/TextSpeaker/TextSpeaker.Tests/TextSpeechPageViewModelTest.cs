using Moq;
using TextSpeaker.Model;
using Xunit;

// ReSharper disable once CheckNamespace
namespace TextSpeaker.ViewModels.Tests
{
    public class TextSpeechPageViewModelTest
    {
        [Fact]
        public void SpeechTest()
        {
            var service = new Mock<ITextToSpeechService>();
            var viewModel = new TextSpeechPageViewModel(service.Object);
            viewModel.Text = "Message";

            var command = viewModel.SpeechCommand;
            Assert.NotNull(command);
            Assert.True(command.CanExecute(null));
            command.Execute(null);

            service.Verify(m => m.Speech("Message"));
        }
    }
}
