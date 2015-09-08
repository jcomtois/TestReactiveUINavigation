using System.Linq;
using ReactiveUI;

namespace TestReactiveUINavigation.ViewModels
{
    public class ComplexViewModel : ReactiveObject, IRoutableViewModel
    {
        public ComplexViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            UrlPathSegment = nameof(ComplexViewModel);

            NavigateBackCommand
                .InvokeCommand(HostScreen.Router, r => r.NavigateBack);

            MiniViewModels =
                new ReactiveList<MiniViewModel>(Enumerable.Range(0, 100).Select(_ => new MiniViewModel()))
                {ChangeTrackingEnabled = true};
        }

        public IReactiveCollection<MiniViewModel> MiniViewModels { get; }
        public string ButtonText { get; } = "Click to Navigate Back";
        public IReactiveCommand<object> NavigateBackCommand{ get; } = ReactiveCommand.Create();
        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }
}