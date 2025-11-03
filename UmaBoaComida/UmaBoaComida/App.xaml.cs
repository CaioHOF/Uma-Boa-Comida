using Microsoft.Maui.Controls;

namespace UmaBoaComida
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell(); 
        }
    }
}
