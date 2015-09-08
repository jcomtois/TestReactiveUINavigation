using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using TestReactiveUINavigation.ViewModels;

namespace TestReactiveUINavigation.Views
{
    /// <summary>
    /// Interaction logic for SimpleView.xaml
    /// </summary>
    public partial class SimpleView : UserControl, IViewFor<SimpleViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
                                                                                                  "ViewModel",
                                                                                                  typeof (SimpleViewModel),
                                                                                                  typeof (SimpleView),
                                                                                                  new PropertyMetadata(default(SimpleViewModel)));

        public SimpleView()
        {
            InitializeComponent();

            this.WhenActivated(ApplyBindings);
        }

        private IEnumerable<IDisposable> ApplyBindings()
        {
            // I know ButtonText is not going to change -- for demo purposes only
            yield return
                this.OneWayBind(ViewModel, vm => vm.ButtonText, v => v.NavigateButton.Content);

            yield return
                this.BindCommand(ViewModel, vm => vm.NavigateCommand, v => v.NavigateButton);
        }

        public SimpleViewModel ViewModel
        {
            get
            {
                return (SimpleViewModel)GetValue(ViewModelProperty);
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
                ViewModel = (SimpleViewModel)value;
            }
        }
    }
}