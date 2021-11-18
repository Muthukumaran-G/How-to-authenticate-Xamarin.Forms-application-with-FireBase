# How-to-authenticate-Xamarin.Forms-application-with-FireBase
Authentication using Mail ID and password involves a simple step of getting the necessary details such as mail ID and password. It involves the installation of **Xamarin.Firebase.Auth** package in Android renderer. Once the details are obtained create the user account using CreateUserWithEmailAndPasswordAsync method by passing the ID and password as parameters. Now you can login to your page by validating the details using SignInWithEmailAndPasswordAsync method. Note that the following methods are renderer based and not associated with PCL. So interface conjunction can be used for effective connection. Generate and add the google-services.json from the firebase console project.

```
<ItemGroup>
  <GoogleServicesJson Include="google-services.json" />
</ItemGroup>
```

```
public async Task<object> LoginWithEmailAndPassword(string email, string password)
{
    try
    {
        var user = await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
        var token = await user.User.GetIdToken(false);
        return token;
    }
}

public async Task<object> SignUpWithEmailAndPassword(string email, string password)
{
    try
    {
        var user = await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
        var token = await user.User.GetIdToken(false);
        return token;
    }
    catch (FirebaseAuthInvalidUserException e)
    {
        e.PrintStackTrace();
        throw;
    }
}
```
