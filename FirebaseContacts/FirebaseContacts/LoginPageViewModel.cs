using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FirebaseContacts
{
   public class LoginPageViewModel : INotifyPropertyChanged
    {
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        public string userName;
        public string password;

        public string webAPI = "AIzaSyBYoiyOBRLDiXbA1eU26V3bNK36UGQh3r0";

        #endregion

        #region Properties
        public string UserName
        {
            get
            { return userName; }
            set
            {
                userName = value;
                NotifyPropertyChanged();
            }
        }

        public bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }

            set
            {
                isBusy = value; 
                NotifyPropertyChanged();
            }
        }

        public string Password
        {
            get
            { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged();
            }
        }
        public Command SignUpCommand { get; set; }
        public Command LoginCommand { get; set; }
        public Command<Image> KeepLoggedInCommand { get; set; }
        public Command<Image> ForgotPasswordCommand { get; set; }
        FirebaseHelper firebaseHelper;
        #endregion

        #region Constructors
        public LoginPageViewModel()
        {
            SignUpCommand = new Command(ExecuteSignUpCommand);
            LoginCommand = new Command(ExecuteLoginCommand);
            KeepLoggedInCommand = new Command<Image>(ExecuteKeepLoggedInCommand);
            ForgotPasswordCommand = new Command<Image>(ExecuteForgotPasswordCommand);
            firebaseHelper = new FirebaseHelper();
        }

        #endregion

        #region Methods
        private void ExecuteSignUpCommand(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new SignupPage());

        }

        private async void ExecuteLoginCommand(object obj)
        {
            try
            {
                IsBusy = true;
                var loginDetails = await DependencyService.Get<IFirebaseAuthentication>().LoginWithEmailAndPassword(UserName, Password);
                if (!string.IsNullOrEmpty(loginDetails.ToString()))
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Success!", "Login success", "Ok");
                    App.Current.MainPage = new MainPage();
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("!!", ex.Message.ToString(), "OK");
            }

        }

        private void ExecuteForgotPasswordCommand(Image obj)
        {

        }

        private void ExecuteKeepLoggedInCommand(Image image)
        {
            if (image.Source.ToString().Contains("graytick.png"))
            {
                image.Source = "greentick.png";
            }
            else
            {
                image.Source = "graytick.png";
            }
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
