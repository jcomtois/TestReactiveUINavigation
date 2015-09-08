using System.Windows;
using TestReactiveUINavigation.ViewModels;

namespace TestReactiveUINavigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppBootstrapper = new AppBootStrapper();
            DataContext = AppBootstrapper;
        }

        public AppBootStrapper AppBootstrapper { get; protected set; }
    }
}