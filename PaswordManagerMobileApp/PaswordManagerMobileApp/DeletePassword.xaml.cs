using RestSharp;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace PaswordManagerMobileApp;

public partial class DeletePassword : ContentPage
{

    public DeletePassword(string serviceName, string login, string password) 
    {
    
        InitializeComponent();
        ServiceName.Text = serviceName;
        Login.Text = login;
        PasswordValue.Text = password;
        if (!Preferences.Default.ContainsKey("userHash"))
        {
            App.Current.MainPage.Navigation.PushAsync(new Login(), true);
            return;
        }

    }

    private void DeletePasswordButtonClicked(object sender, EventArgs e)
    {
        var serviceName = ServiceName.Text;
        var login = Login.Text;
        var passwordValue = PasswordValue.Text;
        var hash = Preferences.Default.Get("userHash", "Unknown");


        var client = new RestClient("https://szczepaniak-haenel-passwordmanager.azurewebsites.net");
        var request = new RestRequest("/password", Method.Delete);
        request.AddHeader("Content-Type", "application/json");
        string tempstr = hash.Replace("\"", "");
        request.AddHeader("hash", tempstr);
        var body = @"{""ServiceName"":""" + serviceName + @""",""Login"":""" + login + @""",""PasswordValue"":""" + passwordValue + @"""}";
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        RestResponse response = client.Execute(request);


        if (!response.IsSuccessStatusCode)
        {
            DisplayAlert("Alert", "Something went wrong", "OK");
            return;

        }
        else
        {
            DisplayAlert("Alert", "Password Deleted", "OK");
            Application.Current.MainPage.Navigation.PushAsync(new MainPage(), true);
            return;
        }
    }

}

