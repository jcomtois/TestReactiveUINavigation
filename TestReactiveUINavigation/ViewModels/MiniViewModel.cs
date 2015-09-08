using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace TestReactiveUINavigation.ViewModels
{
    public class MiniViewModel : ReactiveObject
    {
        private static readonly Random _random = new Random();

        private readonly ObservableAsPropertyHelper<string> _buttonText;
        private readonly ObservableAsPropertyHelper<byte> _red;
        private readonly ObservableAsPropertyHelper<byte> _green;
        private readonly ObservableAsPropertyHelper<byte> _blue;
        public string ButtonText => _buttonText.Value;
        public byte Red => _red.Value;
        public byte Green => _green.Value;
        public byte Blue => _blue.Value;

        public IReactiveCommand<object> ChangeColorCommand { get; } = ReactiveCommand.Create();

        public MiniViewModel()
        {
            _buttonText =
                Observable.Timer(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(_random.NextDouble() * 2000))
                          .Select(_ => _random.Next().ToString("x8"))
                          .ObserveOnDispatcher()
                          .ToProperty(this, vm => vm.ButtonText);

            _red =
                ChangeColorCommand
                    .Select(_ => (byte)_random.Next(255))
                    .ToProperty(this, vm => vm.Red, (byte)_random.Next(255));

            _green =
                ChangeColorCommand
                    .Select(_ => (byte)_random.Next(255))
                    .ToProperty(this, vm => vm.Green, (byte)_random.Next(255));

            _blue =
                ChangeColorCommand
                    .Select(_ => (byte)_random.Next(255))
                    .ToProperty(this, vm => vm.Blue, (byte)_random.Next(255));
        }
    }
}