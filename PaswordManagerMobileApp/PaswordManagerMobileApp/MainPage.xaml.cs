using RestSharp;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using PaswordManagerMobileApp.Models;

namespace PaswordManagerMobileApp;

public partial class MainPage : ContentPage
{
    private ObservableCollection<GetPasswords> getpasswords;
	public MainPage()
	{
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();


        if (!Preferences.Default.ContainsKey("userHash"))
        {
        App.Current.MainPage.Navigation.PushAsync(new Login(), true);
        return;
        }
        int unixTimestamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        
        if (Preferences.Default.ContainsKey("HashDieTime"))
        {
            int hashDieTime = int.Parse(Preferences.Default.Get("HashDieTime", "Unknown"));
            if (hashDieTime > unixTimestamp)
            {
                var hash = Preferences.Default.Get("userHash", "Unknown");
                SyncPasswords(hash);
                var passwords = Preferences.Default.Get("passwordsList", "Unknown");
                DisplayPasswords(passwords);
            }
            else
            {
                Preferences.Default.Clear();
                App.Current.MainPage.Navigation.PushAsync(new Login(), true);
            }
        }
        else
        {
            Preferences.Default.Clear();
            App.Current.MainPage.Navigation.PushAsync(new Login(), true);
        }


    }

    private void Logout(object sender, EventArgs e)
    {
        Preferences.Default.Clear();
        App.Current.MainPage.Navigation.PushAsync(new Login(), true);
    }
    private void SyncPass(object sender, EventArgs e)
    {
        var hash = Preferences.Default.Get("userHash", "Unknown");
        SyncPasswords(hash);
        DisplayAlert("Sync Password", "Done", "OK");

    }

    private void SyncPasswords(string hash)
	{
        var client = new RestClient("https://szczepaniak-haenel-passwordmanager.azurewebsites.net");
        var request = new RestRequest("/password", Method.Get);
        string tempstr = hash.Replace("\"", "");
        request.AddHeader("hash", tempstr);
        RestResponse response = client.Execute(request);
        Preferences.Default.Set("passwordsList", response.Content);
    }

    private void DisplayPasswords(string passwordsList)
    {
        if(passwordsList != null && passwordsList != string.Empty && passwordsList != "\"User have not any password\"")
        {
            List<GetPasswords> passList = JsonConvert.DeserializeObject<List<GetPasswords>>(passwordsList);
            getpasswords = new ObservableCollection<GetPasswords>(passList);
            ItemlistView.ItemsSource = getpasswords;
            base.OnAppearing();
        }

        if(passwordsList == "\"User have not any password\"")
        {
            DisplayAlert("Password", "User have not any password", "OK");

        }

    }

    private async void ItemlistView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var mySelectedItem = e.Item as GetPasswords;
        await Navigation.PushAsync(new DeletePassword(mySelectedItem.serviceName, mySelectedItem.login, mySelectedItem.passwordValue));
        ((ListView)sender).SelectedItem = null;
    
    }

    private void AddNewPassword(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PushAsync(new NewPassword(), true);

    }
}



