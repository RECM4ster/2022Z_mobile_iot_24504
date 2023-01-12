namespace Bryly6._0;

public partial class prostopadloscian : ContentPage
{
    public prostopadloscian()
    {
        InitializeComponent();
        a.TextChanged += (s, e) => prostopadloscianValue();
        b.TextChanged += (s, e) => prostopadloscianValue();
        c.TextChanged += (s, e) => prostopadloscianValue();
        clearform.Clicked += (s, e) => clearForm();

    }

    void prostopadloscianValue()
    {
        double aa, bb, cc, pp, v;
        if((Double.TryParse(a.Text, out aa) && aa > 0) && (Double.TryParse(b.Text, out bb) && bb > 0) && (Double.TryParse(c.Text, out cc) && cc > 0))
        {
            pp = Math.Round(2 * (aa * bb + aa * cc + bb * cc),2);
            v = Math.Round(aa * bb * cc ,2);
            ppOut.Text = "Pole powierzchni = " + pp.ToString();
            vOut.Text = "Objetosc = " + v.ToString();
        }
    }

    void clearForm()
    {
        ppOut.Text = "";
        vOut.Text = "";
        a.Text = "";
        b.Text = "";
        c.Text = "";
    }
}

