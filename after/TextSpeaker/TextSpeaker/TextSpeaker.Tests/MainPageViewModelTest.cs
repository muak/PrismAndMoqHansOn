// <copyright file="MainPageViewModelTest.cs">Copyright ©  2016</copyright>

using System.Threading.Tasks;
using Moq;
using Prism.Navigation;
using Prism.Services;
using Xunit;

// ReSharper disable once CheckNamespace
namespace TextSpeaker.ViewModels.Tests
{
    public class MainPageViewModelTest
    {
        [Fact]
        public void ExecuteNavigationCommand()
        {
            Mock<INavigationService> navigationService = new Mock<INavigationService>();
            Mock<IPageDialogService> pageDialogService = new Mock<IPageDialogService>();
            var viewModel = new MainPageViewModel(navigationService.Object, pageDialogService.Object);
            var command = viewModel.NavigationCommand;

            Assert.NotNull(command);
            Assert.True(command.CanExecute(null));

            command.Execute(null);
            navigationService.Verify(m => m.NavigateAsync("TextSpeechPage", null, null, true), Times.Once);
        }

        [Fact]
        public async Task NaivateToTextSpeechPageWhenPressedOk()
        {
            Mock<INavigationService> navigationService = new Mock<INavigationService>();
            Mock<IPageDialogService> pageDialogService = new Mock<IPageDialogService>();
            var viewModel = new MainPageViewModel(navigationService.Object, pageDialogService.Object);

            pageDialogService
                .Setup(m => m.DisplayAlertAsync("確認", "Text Speech画面へ遷移しますか？", "OK", "Cancel"))
                .Returns(Task.FromResult(true));

            Assert.True(await viewModel.CanNavigateAsync(null));
        }

        [Fact]
        public async Task NaivateToTextSpeechPageWhenPressedCancel()
        {
            Mock<INavigationService> navigationService = new Mock<INavigationService>();
            Mock<IPageDialogService> pageDialogService = new Mock<IPageDialogService>();
            var viewModel = new MainPageViewModel(navigationService.Object, pageDialogService.Object);

            pageDialogService
                .Setup(m => m.DisplayAlertAsync("確認", "Text Speech画面へ遷移しますか？", "OK", "Cancel"))
                .Returns(Task.FromResult(false));

            Assert.False(await viewModel.CanNavigateAsync(null));
        }
    }
}
