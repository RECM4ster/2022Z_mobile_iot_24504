namespace Bryly6._0;

public partial class szescian : ContentPage
{
    public szescian()
    {
        InitializeComponent();

        krawedz.TextChanged += (s, e) => szescianValue();
        clearform.Clicked += (s, e) => clearForm();

    }

    void szescianValue()
    {
        double a;
        if (Double.TryParse(krawedz.Text, out a) && a > 0)
        {
            double pp = Math.Round(a * a * 6 ,2);
            double v = Math.Round(a * a * a,2);

            ppOut.Text = "Pole powierzchni = " + pp.ToString();
            vOut.Text = "Objetosc = " + v.ToString();

        }

    }

    void clearForm()
    {
        ppOut.Text = "";
        vOut.Text = "";
        krawedz.Text = "";
    }
}
