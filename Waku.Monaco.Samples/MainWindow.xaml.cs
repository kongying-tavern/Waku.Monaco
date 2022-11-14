using System.Windows;

namespace Waku.Monaco.Samples;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        //SizeChanged += MainWindow_SizeChanged;
    }
    //private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e) => _ = editor.Layout(editor.ActualWidth, editor.ActualHeight);

    private void Button_Click(object sender, RoutedEventArgs e) => _ = editor.GetPosition();
}
