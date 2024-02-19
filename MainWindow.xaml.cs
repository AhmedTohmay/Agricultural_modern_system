using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;
using System.Text.RegularExpressions;
using System.Security.Cryptography.Xml;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System.Net.Http;
using static Agricultural_modern_system.AllOwners;
using static System.Net.Mime.MediaTypeNames;
using static Agricultural_modern_system.SensorReading;
using System.Windows.Media.Media3D;
namespace Agricultural_modern_system
{


    public partial class MainWindow : Window
    {
        int Irrigation=0,Pest=0,Fertilizer=0;
        int num_of_users=13;
        bool Break=false;
        public  MainWindow()
        {
            InitializeComponent();
            Irrigation_Prgress.Value=Irrigation;
            Pest_progress.Value=Pest;
            Fertilizer_Progress.Value = Fertilizer;     
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    IsMaximized = true;
                }
                else
                {
                    IsMaximized = true;
                }

            }
        }

        public bool Is_Valid_Email (String email)
        {
            Regex check=new Regex(@"[^@\s]+@[^@\s]+\.[^@\s]+$");
            bool valid = false;
            valid = check.IsMatch(email);
            if (valid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Is_Valid_Name(String MyName)
        {
            Regex check = new Regex(@"^([A-Z][a-z-A-Z]+)$");
            bool valid = false;
            valid = check.IsMatch(MyName);
            if (valid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void controls_Click(object sender, RoutedEventArgs e)
        {
            infogrid.Visibility = Visibility.Collapsed;
            About_Usgrid.Visibility = Visibility.Collapsed;
            Controlsgrid.Visibility = Visibility.Visible;

        }

        private void Informations_Click(object sender, RoutedEventArgs e)
        {
            About_Usgrid.Visibility = Visibility.Collapsed;
            Controlsgrid.Visibility = Visibility.Collapsed;
            infogrid.Visibility = Visibility.Visible;
        }
        private void About_us_Click(object sender, RoutedEventArgs e)
        {
            infogrid.Visibility = Visibility.Collapsed;
            Controlsgrid.Visibility = Visibility.Collapsed;
            About_Usgrid.Visibility = Visibility.Visible;
        }



        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (Password.PasswordChar == '*')
            {
                Password.PasswordChar = '\0'; // Show the pass
            }
            else
            {
                Password.PasswordChar = '*'; //Hide the pass
            }
        }
       
        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            bool chek = await signIN();

            if (chek)
            {
                SignInGrid.Visibility = Visibility.Collapsed;
                BaseGrid.Visibility = Visibility.Visible;
                Personal_Account.Visibility = Visibility.Visible;
                infogrid.Visibility= Visibility.Visible;
                await sensorsreadings();
            }
            else
            {
                Wrong_account.Visibility = Visibility.Visible;
            }
        }


        

        private void Show_Sign_Up_Password_Click(object sender, RoutedEventArgs e)
        {
            if (Sign_Up_Password.PasswordChar == '*')
            {
                Sign_Up_Password.PasswordChar = '\0'; // Show the pass
            }
            else
            {
                Sign_Up_Password.PasswordChar = '*'; //Hide the pass
            }
        }

        private void Show_Sign_Up_ConfirmPassword_Click(object sender, RoutedEventArgs e)
        {
            if (Sign_Up_ConfirmPassword.PasswordChar == '*')
            {
                Sign_Up_ConfirmPassword.PasswordChar = '\0'; // Show the pass
            }
            else
            {
                Sign_Up_ConfirmPassword.PasswordChar = '*'; //Hide the pass
            }
        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {

            if (Sign_Up_User.Text=="") 
            {
                Sign_Up_Warning.Visibility = Visibility.Visible;
                Sign_Up_Warning.Text = "Please Enter Your User Name..!";
               
            }
            else if (Is_Valid_Name(Sign_Up_User.Text)==false)
            {
                Sign_Up_Warning.Visibility = Visibility.Visible;
                Sign_Up_Warning.Text = "Please Enter Valid User Name..!";
            }
            else if (Sign_Up_Email.Text =="")
            {
                Sign_Up_Warning.Visibility = Visibility.Visible;
                Sign_Up_Warning.Text = "Please Enter Your Email..!";

            }
            else if (Is_Valid_Email(Sign_Up_Email.Text)==false)
            {
                Sign_Up_Warning.Visibility = Visibility.Visible;
                Sign_Up_Warning.Text = "Please Enter Valid Email..!";

            }
            else if (Sign_Up_Password.Password == "")
            {
                Sign_Up_Warning.Visibility = Visibility.Visible;
                Sign_Up_Warning.Text = "Please Enter Your Passowrd..!";

            }
            else if (Sign_Up_Password.Password.Length<6)
            {
                Sign_Up_Warning.Visibility = Visibility.Visible;
                Sign_Up_Warning.Text = "Password Should be At Least 6 Characters";

            }
            else if (Sign_Up_ConfirmPassword.Password == "")
            {
                Sign_Up_Warning.Visibility = Visibility.Visible;
                Sign_Up_Warning.Text = "Please Confirm Your Passowrd..!";

            }
            else if (Sign_Up_Password.Password!=Sign_Up_ConfirmPassword.Password)
            {
                Sign_Up_Warning.Visibility = Visibility.Visible;
                Sign_Up_Warning.Text = "Your Passwords Don't Match ..!";
            }
           else
            {
                Sign_Up_Warning.Foreground = Brushes.Green;
                Sign_Up_Warning.Visibility = Visibility.Visible;
                await AllOwners.Post(Sign_Up_User.Text, Sign_Up_Email.Text, Sign_Up_Password.Password);
                Sign_Up_Warning.Text = "Your Account Successfully Added";
                num_of_users++;
            }
           
        }

        private void Sign_Up_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (Sign_Up_Warning.Text == "Please Enter Your Passowrd..!" || Sign_Up_Warning.Text == "Your Passwords Don't Match ..!")
            {
                Sign_Up_Warning.Visibility = Visibility.Collapsed;
            }
        
        }

        private void Sign_Up_ConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (Sign_Up_Warning.Text == "Please Confirm Your Passowrd..!" || Sign_Up_Warning.Text == "Your Passwords Don't Match ..!")
            {
                Sign_Up_Warning.Visibility = Visibility.Collapsed;
            }

        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            Wrong_account.Visibility = Visibility.Collapsed;
        }

        private void Enter_To_Sign_Up_Click(object sender, RoutedEventArgs e)
        {
            SignInGrid.Visibility = Visibility.Collapsed;
            SignUpGrid.Visibility = Visibility.Visible;

        }

        private void UserName_KeyDown(object sender, KeyEventArgs e)
        {
            Wrong_account.Visibility = Visibility.Collapsed;

        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuBorder.Visibility==Visibility.Collapsed)
            {
                MenuBorder.Visibility = Visibility.Visible;
            }
            else
            {
                MenuBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void Sign_Up_User_KeyDown(object sender, KeyEventArgs e)
        {
            if (Sign_Up_Warning.Text == "Please Enter Your User Name..!"||Sign_Up_Warning.Text== "Please Enter Valid User Name..!")
            {
                Sign_Up_Warning.Visibility = Visibility.Collapsed;
            }
        }

        private void Sign_Up_Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (Sign_Up_Warning.Text == "Please use a Gmail address..!" || Sign_Up_Warning.Text == "Please Enter Your Email..!")
            {
                Sign_Up_Warning.Visibility = Visibility.Collapsed;
            }
        }

        private void Return_Sign_In_Click(object sender, RoutedEventArgs e)
        {
            Sign_Up_Warning.Visibility=Visibility.Collapsed;
            SignUpGrid.Visibility=Visibility.Collapsed;
            SignInGrid.Visibility = Visibility.Visible;
        }

        private void Return_to_main_Click(object sender, RoutedEventArgs e)
        {
            Your_Account_Border.Visibility=Visibility.Collapsed;
            BaseGrid.Visibility=Visibility.Visible;
            infogrid.Visibility=Visibility.Visible;
        }

        private async void Increase_Irrigation_Click(object sender, RoutedEventArgs e)
        {
            Irrigation = Irrigation + 1;
            Irrigation_Prgress.Value = Irrigation;
            float I = ((float)Irrigation_Prgress.Value);
            await SensorReading.Edit(33, "Temperature Sensor", I, "2024-02-08T06:00:00.000Z");
        }

        private async void decrease_Irrigation_Click(object sender, RoutedEventArgs e)
        {
            Irrigation = Irrigation -1 ;
            Irrigation_Prgress.Value = Irrigation;
            float I = ((float)Irrigation_Prgress.Value);
            await SensorReading.Edit(33,"Temperature Sensor", I,"2024-02-08T06:00:00.000Z");
        }

        private void Personal_Account_Click(object sender, RoutedEventArgs e)
        {
            if (Your_Account_Border.Visibility == Visibility.Collapsed && infogrid.Visibility==Visibility.Visible)
            {
                Your_Account_Border.Visibility = Visibility.Visible;
                BaseGrid.Visibility = Visibility.Collapsed;
                infogrid.Visibility = Visibility.Collapsed;
            }
            else if (Your_Account_Border.Visibility == Visibility.Collapsed && Controlsgrid.Visibility == Visibility.Visible)
            {
                Your_Account_Border.Visibility = Visibility.Visible;
                BaseGrid.Visibility = Visibility.Collapsed;
                Controlsgrid.Visibility = Visibility.Collapsed;
            }
            else if (Your_Account_Border.Visibility == Visibility.Collapsed && About_Usgrid.Visibility == Visibility.Visible)
            {
                Your_Account_Border.Visibility = Visibility.Visible;
                BaseGrid.Visibility = Visibility.Collapsed;
                About_Usgrid.Visibility = Visibility.Collapsed;
            }
            else if (Your_Account_Border.Visibility == Visibility.Visible )
            {
                Your_Account_Border.Visibility = Visibility.Collapsed;
                BaseGrid.Visibility = Visibility.Visible;
                infogrid.Visibility= Visibility.Visible;
            }
        }

        private void New_Account_Button_Click(object sender, RoutedEventArgs e)
        {
            BaseGrid.Visibility = Visibility.Collapsed;
            infogrid.Visibility = Visibility.Collapsed;
            Your_Account_Border.Visibility = Visibility.Collapsed;
            Personal_Account.Visibility = Visibility.Collapsed;
            SignUpGrid.Visibility = Visibility.Visible;
        }
        public async Task<bool> signIN()
        {
            int id;
            string ownerName;
            string email;
            int passcode;
            bool result = false;

            for (id = 1; id <=(20+num_of_users); id++)
            {
                var data = await AllOwners.Getone(id.ToString());

                email = data.email;
                ownerName = data.owner_name;
                passcode = data.passcode;
                if (UserName.Text == ownerName && Password.Password == passcode.ToString())
                {
                    result = true;
                    Your_Account_Id.Text = id.ToString();
                    Your_Account_Name.Text = ownerName;
                    Your_Account_Email.Text = email;
                    Your_Account_Password.Text = passcode.ToString();
                    break;
                }
                else 
                    result = false;


            }
            return result;
        }

        private async void refreshdata_Click(object sender, RoutedEventArgs e)
        {
            Break = true;

            Temprature_lable.Content = 0+ " °C";
            Humidity_Lable.Content = 0+" %";
            Soil_Moisture_Lable.Content = 0+" %";
            Light_Lable.Content = 0+" LUX";
            Ph_Lable.Content = 0 ;
            Conductivity_Lable.Content = 0 + " µS/cm";
            Rainfall_Lable.Content = 0 + " mm";
            Flow_Control_Lable.Content = 0 + " lit/m";

            await Task.Delay(1000);
            Break = false;
            await sensorsreadings();
        }

        private async void decrease_Pest_Click(object sender, RoutedEventArgs e)
        {
            Pest --;
            Pest_progress.Value = Pest;
            float I = ((float)Pest_progress.Value) + 0.8f;
            await SensorReading.Edit(34, "Humidity Sensor", I, "2024-02-08T06:00:00.000Z");
        }

        private async void Increase_Pest_Click(object sender, RoutedEventArgs e)
        {
            Pest++;
            Pest_progress.Value = Pest;
            float I = ((float)Pest_progress.Value) + 0.8f;
            await SensorReading.Edit(34,"Humidity Sensor", I, "2024-02-08T06:00:00.000Z");
        }

        private async void decrease_Fertilizer_Click(object sender, RoutedEventArgs e)
        {
            Fertilizer --;
            Fertilizer_Progress.Value = Fertilizer;
            float I = ((float)Fertilizer_Progress.Value) + 0.3f;
            await SensorReading.Edit(35,"Soil Moisture Sensor", I, "2024-02-08T06:00:00.000Z");
        }

        private async void Increase_Fertilizer_Click(object sender, RoutedEventArgs e)
        {
            Fertilizer++;
            Fertilizer_Progress.Value=Fertilizer;
            float I = ((float)Fertilizer_Progress.Value)+0.3f;
            await SensorReading.Edit(35, "Soil Moisture Sensor", I, "2024-02-08T06:00:00.000Z");
        }

        public async Task sensorsreadings()
        {
            int sensorID;
            string sensor_name;
            float sensor_value;
            for (sensorID = 1; sensorID <= 40; sensorID++)
            {
                if (Break) break;

                await Task.Delay(1000);
                var data = await SensorReading.GetSensor(sensorID.ToString());
                sensor_name = data.sensor_name;
                sensor_value = data.value;
                if (sensor_name == "Temperature Sensor")
                    Temprature_lable.Content = sensor_value + " °C";
                else if (sensor_name == "Humidity Sensor")
                    Humidity_Lable.Content = sensor_value + " %";
                else if (sensor_name == "Soil Moisture Sensor")
                    Soil_Moisture_Lable.Content = sensor_value + " %";
                else if (sensor_name == "Light Sensor")
                    Light_Lable.Content = sensor_value;
                else if (sensor_name == "pH Sensor")
                    Ph_Lable.Content = sensor_value;
                else if (sensor_name== "Conductivity Sensor")
                    Conductivity_Lable.Content = sensor_value + " µS/cm";
                else if (sensor_name == "Rainfall Sensor")
                    Rainfall_Lable.Content = sensor_value + " mm";
                else if (sensor_name == "Flow Control Sensor")
                    Flow_Control_Lable.Content = sensor_value + " lit/m";          
            }
        }     

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}