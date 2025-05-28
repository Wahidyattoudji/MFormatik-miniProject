using MFormatik.ViewModels;
using System.Windows;

namespace MFormatik;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowVM mainWindowVM)
    {
        InitializeComponent();
        DataContext = mainWindowVM;
    }
}