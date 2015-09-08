using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ReactiveUI;
using TestReactiveUINavigation.ViewModels;

namespace TestReactiveUINavigation.Views
{
    /// <summary>
    /// Interaction logic for MiniView.xaml
    /// </summary>
    public partial class MiniView : UserControl, IViewFor<MiniViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
                                                                                                  "ViewModel",
                                                                                                  typeof (MiniViewModel),
                                                                                                  typeof (MiniView),
                                                                                                  new PropertyMetadata(default(MiniViewModel)));

        public MiniView()
        {
            InitializeComponent();
            this.WhenActivated(ApplyBindings);

            
        }

        private IEnumerable<IDisposable> ApplyBindings()
        {
            yield return
                this.OneWayBind(ViewModel, vm => vm.ButtonText, v => v.MiniButton.Content);

            yield return
                this.BindCommand(ViewModel, vm => vm.ChangeColorCommand, v => v.MiniButton);

            yield return
                this.WhenAnyValue(t => t.ViewModel.Red,
                                  t => t.ViewModel.Green,
                                  t => t.ViewModel.Blue,
                                  (r, g, b) => new SolidColorBrush(Color.FromRgb(r, g, b)))
                    .BindTo(this, v => v.MiniButton.Background, () => Brushes.DarkGray);
        }

        public MiniViewModel ViewModel
        {
            get
            {
                return (MiniViewModel)GetValue(ViewModelProperty);
            }
            set
            {
                SetValue(ViewModelProperty, value);
            }
        }

        object IViewFor.ViewModel
        {
            get
            {
                return ViewModel;
            }
            set
            {
                ViewModel = (MiniViewModel)value;
            }
        }
    }
}