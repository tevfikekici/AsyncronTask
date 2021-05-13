using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AsyncronTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        private ObservableCollection<string> numberDescriptions;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            numberDescriptions = new ObservableCollection<string>();

            listBox.ItemsSource = numberDescriptions;

            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new LoadNumberDelegate(LoadNumber), 1);

        }
        private delegate void LoadNumberDelegate(int number);

        private void LoadNumber(int number)
        {
            numberDescriptions.Add("Number " + number.ToString());
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new LoadNumberDelegate(LoadNumber), ++number);
            Thread.Sleep(200);
        }

        Task CountX()
        {
            return Task.Run(() =>
            {
                int Counter = 1;
                while (true)
                {
                    
                    label1.Dispatcher.Invoke(new Action(() => { label1.Content = Counter; }));
                    Counter++;
                }

            });
        }

        private async void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            await CountX();          
        }

    }
}