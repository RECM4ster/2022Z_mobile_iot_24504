namespace Bryly6._0;

public partial class walec : ContentPage
{
    public walec()
    {
        InitializeComponent();
        h.TextChanged += (s, e) => walecValue();
        r1.TextChanged += (s, e) => walecValue();
        clearform.Clicked += (s, e) => clearForm();


    }

    void walecValue()
    {
        double hh, rr, pp, v;

        if ((Double.TryParse(h.Text, out hh) && hh > 0) && (Double.TryParse(r1.Text, out rr) && rr > 0) )

        {
            double pi = Math.PI;
             pp = Math.Round(2 * pi*rr*rr+2*pi*rr*hh, 2);
             v = Math.Round(pi * rr*rr*hh, 2);

            ppOut.Text = "Pole powierzchni = " + pp.ToString();
            vOut.Text = "Objetosc = " + v.ToString();

        }
    }

    void clearForm()
    {
        ppOut.Text = "";
        vOut.Text = "";
        r1.Text = "";
        h.Text = "";
    }
}

