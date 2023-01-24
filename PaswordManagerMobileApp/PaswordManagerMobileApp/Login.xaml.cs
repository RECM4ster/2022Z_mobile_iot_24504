using RestSharp;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace PaswordManagerMobileApp;

public partial class Login : ContentPage
{
    public Login()
	{
        InitializeComponent();
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();

        if (Preferences.Default.ContainsKey("userHash"))
        {
            await DisplayAlert("Logged Alert", "You Are Logged into app. You have been redirected now", "OK");
            App.Current.MainPage.Navigation.PushAsync(new MainPage(), true);
        }

    }

    private void RedirectToRegister(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PushAsync(new Register(), true);
    }

    [Obsolete]
    private void LoginButtonClicked(object sender, EventArgs e)
    {
        var nameValue = Username.Text;
        var passwordValue = Password.Text;
        var emailValue = Email.Text;
        string empty = "Field can't be entry";

        if(nameValue== null)
        {
            Username.Placeholder = empty;
            Username.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }
        if (emailValue == null)
        {
            Email.Placeholder = empty;
            Email.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }
        if (passwordValue == null)
        {
            Password.Placeholder = empty;
            Password.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }

        var client = new RestClient("https://szczepaniak-haenel-passwordmanager.azurewebsites.net");
        var request = new RestRequest("/login/", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        var body = @"{""Username"":"""+nameValue+@""",""Password"":"""+passwordValue+@""",""Email"":"""+emailValue+@"""}";
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        RestResponse response = client.Execute(request);

        if(!response.IsSuccessStatusCode)
        {
            DisplayAlert("Alert", "Something went wrong", "OK");
            return;

        }
        else
        {
            long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            int dieTime = (int)unixTimestamp+600;

            DisplayAlert("Alert", "You have been Logged", "OK");
            string hash = response.Content;
            Preferences.Default.Set("userHash", hash);
            Preferences.Default.Set("HashDieTime", dieTime);

            Application.Current.MainPage.Navigation.PushAsync(new MainPage(), true);
            return;
        }
        return;
    }




}

