namespace Bryly6._0;

public partial class kula : ContentPage
{
    public kula()
    {
        InitializeComponent();
        r1.TextChanged += (s, e) => kulavalue();
        clearform.Clicked += (s, e) => clearForm();


    }

    void kulavalue()
    {
        double rr1, pp, v;
        if ((Double.TryParse(r1.Text, out rr1) && rr1 > 0))
        {
            double pi = Math.PI;
            pp = Math.Round(4*pi*rr1*rr1,2);
            v = Math.Round(4*pi*rr1*rr1*rr1/3, 2);
            ppOut.Text = "Pole powierzchni = " + pp.ToString();
            vOut.Text = "Objetosc = " + v.ToString();
        }
    }

    void clearForm()
    {
        ppOut.Text = "";
        vOut.Text = "";
        r1.Text = "";
    }
}

