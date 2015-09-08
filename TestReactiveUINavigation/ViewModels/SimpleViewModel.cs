using System.Reactive.Linq;
using ReactiveUI;

namespace TestReactiveUINavigation.ViewModels
{
    public class SimpleViewModel : ReactiveObject, IRoutableViewModel
    {
        public SimpleViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            UrlPathSegment = nameof(SimpleViewModel);

            NavigateCommand
                .Select(_ => new ComplexViewModel(HostScreen))
                .InvokeCommand(HostScreen.Router, r => r.Navigate);
        }

        public IReactiveCommand<object> NavigateCommand { get; } = ReactiveCommand.Create();
        public string ButtonText { get; } = "Click Me To Navigate";
        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }
}