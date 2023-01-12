namespace Bryly6._0;

public partial class stozekSciety : ContentPage
{
    public stozekSciety()
    {
        InitializeComponent();
        h.TextChanged += (s, e) => stozeksValue();
        r1.TextChanged += (s, e) => stozeksValue();
        r2.TextChanged += (s, e) => stozeksValue();
        l.TextChanged += (s, e) => stozeksValue();
        clearform.Clicked += (s, e) => clearForm();


    }

    void stozeksValue()
    {
         double rr1, rr2, ll, hh, pp, v;

        if ((Double.TryParse(h.Text, out hh) && hh > 0) &&
            (Double.TryParse(r1.Text, out rr1) && rr1 > 0)&&
            (Double.TryParse(r2.Text, out rr2) && rr2 > 0) && 
            (Double.TryParse(l.Text, out ll) && ll > 0) 
            )
        {
            double pi = Math.PI;
            pp = Math.Round(pi * (rr1 * rr1 + rr2 * rr2 + rr1 + rr2 * ll), 2);
            v = Math.Round(((pi / 3) * (rr1 * rr1 + rr1 * rr2 + rr2 * rr2) * hh), 2);

            ppOut.Text = "Pole powierzchni = " + pp.ToString();
            vOut.Text = "Objetosc = " + v.ToString();

        }
    }

    void clearForm()
    {
        ppOut.Text = "";
        vOut.Text = "";
        r1.Text = "";
        r2.Text = "";
        h.Text = "";
        l.Text = "";
    }
}

