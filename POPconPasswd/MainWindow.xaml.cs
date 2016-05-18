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
using Microsoft.Win32;

namespace POPconPasswd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        POPcondecrypt pd = new POPcondecrypt();
        List<passwordList> list = new List<passwordList>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb == null)
                return;
            if (rb.Name.Equals("rbmanually"))
            {
                TBpassword.IsEnabled = true;
            }
            else
            {
                TBpassword.IsEnabled = false;
            }
        }

        private void decrypt(object sender, RoutedEventArgs e)
        {
            if (rbmanually.IsChecked == true)
            {
                pd.setPassword(TBpassword.Text);
                string pwd = pd.decrypt();
                lbdecrypted.Content = pwd;
            }
            else
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\POPcon\\POP3");
                foreach(var k in key.GetSubKeyNames())
                {
                    RegistryKey rkey = key.OpenSubKey(k);
                    if (rkey != null)
                    {
                        string isencrypted = Convert.ToString(rkey.GetValue("PW_is_encrypted"));                            
                        if (isencrypted.Equals("2"))
                        {
                            string username = Convert.ToString(rkey.GetValue("Username"));
                            string password = Convert.ToString(rkey.GetValue("Password"));
                            pd.setPassword(password);
                            password = pd.decrypt();
                            list.Add(new passwordList(username, password));
                        }
                        
                    }
                }
            }
            DGpasswords.ItemsSource = list;
        }
    }

    public class passwordList
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public passwordList(string username,string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
