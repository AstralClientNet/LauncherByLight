using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using DiscordRPC;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Pizzaria1
{
    public partial class MainWindow : Window
    {
        public DiscordRpcClient client;
        string discordTime = "";
        public static void versionFinderForLabel(string script, Label version)
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                powerShell.AddScript(script);
                powerShell.AddCommand("Out-String");
                Collection<PSObject> PSOutput = powerShell.Invoke();
                StringBuilder stringBuilder = new StringBuilder();
                foreach (PSObject pSObject in PSOutput)
                    stringBuilder.AppendLine(pSObject.ToString());
                version.Content = stringBuilder.ToString();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            versionFinderForLabel("Get-AppPackage -name Microsoft.MinecraftUWP | select -expandproperty Version", discordRpc);
            InitializeDiscordHome("Testing new app");
        }
        public void InitializeDiscordHome(string status)
        {
            int TimestampStart = 0;
            int TimestampEnd = 0;
            dynamic DateTimestampEnd = null;

            if (discordTime != "" && Int32.TryParse(discordTime, out TimestampEnd))
                DateTimestampEnd = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(TimestampEnd);

            client = new DiscordRpcClient("722349140180205611");
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Details = status,
                State = "on " + discordRpc.Content,
                Assets = new Assets()
                {
                    LargeImageKey = "logonewdiscord",
                    LargeImageText = "Astral Launcher",
                    SmallImageKey = "minecraft"
                },
                Timestamps = new Timestamps()
                {
                    Start = discordTime != "" && Int32.TryParse(discordTime, out TimestampStart) ? new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(TimestampStart) : DateTime.UtcNow,
                    End = DateTimestampEnd
                }

            });
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);

            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlInicio());
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new UserControlEscolha());
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Update());
                    break;
                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new AboutUs());
                    break;
                default:
                    break;
            }
        }

        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/AstralClientNet");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("https://astralclient.net");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Process.Start("https://twitter.com/AstralClient");
        }

        private void patreoncllick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.patreon.com/astralclient");
        }
    }
}
