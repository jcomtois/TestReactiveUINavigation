using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using Splat;
using TestReactiveUINavigation.ViewModels;

namespace TestReactiveUINavigation.Views
{
    /// <summary>
    /// Interaction logic for ComplexView.xaml
    /// </summary>
    public partial class ComplexView : UserControl, IViewFor<ComplexViewModel>, IEnableLogger
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
                                                                                                  "ViewModel",
                                                                                                  typeof (ComplexViewModel),
                                                                                                  typeof (ComplexView),
                                                                                                  new PropertyMetadata(default(ComplexViewModel)));

        public ComplexView()
        {
            InitializeComponent();
            this.WhenActivated(ApplyBindings);
        }

        private IEnumerable<IDisposable> ApplyBindings()
        {
            yield return 
            this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext);

            // I know ButtonText is not going to change -- for demo purposes only
            yield return
                this.OneWayBind(ViewModel, vm => vm.ButtonText, v => v.NavigateBackButton.Content);

            yield return
                this.BindCommand(ViewModel, vm => vm.HostScreen.Router.NavigateBack, v => v.NavigateBackButton);
        }

        public ComplexViewModel ViewModel
        {
            get
            {
                return (ComplexViewModel)GetValue(ViewModelProperty);
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
                ViewModel = (ComplexViewModel)value;
            }
        }
    }
}