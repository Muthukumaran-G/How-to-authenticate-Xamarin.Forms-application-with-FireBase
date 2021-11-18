using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FirebaseContacts
{
    public class SignUpPageViewModel : INotifyPropertyChanged
    {
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        public string webAPI = "AIzaSyBYoiyOBRLDiXbA1eU26V3bNK36UGQh3r0";
        public string email;
        public string password;

        #endregion

        #region Properties
        public Command LoginCommand { get; set; }
        public Command SignUpCommand { get; set; }
        public string Email
        {
            get
            { return email; 
            }
            set
            {
                email = value;
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

        public string confirmPassword;
        public string ConfirmPassword
        {
            get
            { return confirmPassword; }
            set
            {
                confirmPassword = value;
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


        #endregion

        #region Constructors
        public SignUpPageViewModel()
        {
            LoginCommand = new Command(ExecuteLoginCommand);
            SignUpCommand = new Command(ExecuteSignUpCommand);
        }

        private void ExecuteLoginCommand(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        #endregion

        #region Methods

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void ExecuteSignUpCommand(object obj)
        {
            if (Password == null || Email == null)
            {
                await App.Current.MainPage.DisplayAlert("!!", "Password/Email cannot be empty.", "OK");
                return;
            }
            if (!Password.Equals(ConfirmPassword))
            {
                await App.Current.MainPage.DisplayAlert("!!", "Password doesn't match. Please re-enter password", "OK");
                return;
            }

            try
            {
                IsBusy = true;
                await DependencyService.Get<IFirebaseAuthentication>().SignUpWithEmailAndPassword(Email, Password);
            }
            catch(Exception ex)
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("!!", ex.Message.ToString(), "OK");
            }

            IsBusy = false;
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());

        }
        #endregion

    }

    public interface IFirebaseAuthentication
    {
        Task<object> LoginWithEmailAndPassword(string email, string password);
        Task<object> SignUpWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool IsSignIn();
    }
}
