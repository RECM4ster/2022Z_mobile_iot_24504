using RestSharp;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace PaswordManagerMobileApp;

public partial class NewPassword : ContentPage
{
    public NewPassword()
	{
        InitializeComponent();
    }

    [Obsolete]
    private void AddPasswordButtonClicked(object sender, EventArgs e)
    {
        var serviceName = ServiceName.Text;
        var login = Login.Text;
        var passwordValue = PasswordValue.Text;
        var serviceUrl = ServiceUrl.Text;
        string empty = "Field can't be entry";

        if (!Preferences.Default.ContainsKey("userHash"))
        {
            App.Current.MainPage.Navigation.PushAsync(new Login(), true);
            return;
        }
        var hash = Preferences.Default.Get("userHash", "Unknown");

        if (serviceName == null)
        {
            ServiceName.Placeholder = empty;
            ServiceName.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }
        if (login == null)
        {
            Login.Placeholder = empty;
            Login.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }
        if (passwordValue == null)
        {
            PasswordValue.Placeholder = empty;
            PasswordValue.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }



        var client = new RestClient("https://szczepaniak-haenel-passwordmanager.azurewebsites.net");
        var request = new RestRequest("/password", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        string tempstr = hash.Replace("\"", "");
        request.AddHeader("hash", tempstr);
        var body = @"{""ServiceName"":""" + serviceName+@""",""Login"":"""+login+ @""",""PasswordValue"":""" + passwordValue + @""",""ServiceUrl"":""" + serviceUrl+@"""}";
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        RestResponse response = client.Execute(request);

        if(!response.IsSuccessStatusCode)
        {
            if(response.Content == "Password Exist")
            {
                DisplayAlert("Alert", "Password Exist.", "OK");
            }
            DisplayAlert("Alert", "Something went wrong", "OK");
            return;

        }
        else
        {
            DisplayAlert("Alert", "Password Saved", "OK");
            Application.Current.MainPage.Navigation.PushAsync(new MainPage(), true);
            return;
        }
    }




}

