using RestSharp;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace PaswordManagerMobileApp;

public partial class Register : ContentPage
{
    public Register()
	{
        InitializeComponent();
    }

    [Obsolete]
    private void RegisterButtonClicked(object sender, EventArgs e)
    {
        var nameValue = Username.Text;
        var passwordValue = Password.Text;
        var emailValue = Email.Text;
        string empty = "Field can't be entry";

        if(nameValue== null)
        {
            Username.Text = "";
            Username.Placeholder = empty;
            Username.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }
        if (emailValue == null)
        { 
            Email.Text = ""; 
            Email.Placeholder = empty;
            Email.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(emailValue);

        if (match.Success == false)
        {
            Email.Text = "";
            Email.Placeholder = "Value isn't email";
            Email.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }

        if (passwordValue == null)
        {
            Password.Text = "";
            Password.Placeholder = empty;
            Password.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }
        if (passwordValue.Length < 8)
        {
            Password.Text = "";
            Password.Placeholder = "Password should be longer than 8 char";
            Password.PlaceholderColor = Color.FromHex("#F01D09");
            return;
        }
        var client = new RestClient("https://szczepaniak-haenel-passwordmanager.azurewebsites.net");
        var request = new RestRequest("/signup/", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        var body = @"{""Username"":"""+nameValue+@""",""Password"":"""+passwordValue+@""",""Email"":"""+emailValue+@"""}";
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        RestResponse response = client.Execute(request);

        if(!response.IsSuccessStatusCode)
        {
            if(response.Content == "UserExist")
            {
                DisplayAlert("Alert", "Account Exist.", "OK");
            }
            DisplayAlert("Alert", "Something went wrong", "OK");
            return;

        }
        else
        {
            DisplayAlert("Alert", "Account Created", "OK");
            Application.Current.MainPage.Navigation.PushAsync(new Login(), true);
            return;
        }
    }




}

