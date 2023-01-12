namespace Bryly6._0;

public partial class stozek : ContentPage
{
    public stozek()
    {
        InitializeComponent();
        r1.TextChanged += (s, e) => stozekValue();
        l.TextChanged += (s, e) => stozekValue();
        h.TextChanged += (s, e) => stozekValue();
        clearform.Clicked += (s, e) => clearForm();


    }

    void stozekValue()
    {
        double rr1, ll, hh, pp, v;
        if ((Double.TryParse(r1.Text, out rr1) && rr1 > 0)&&
            (Double.TryParse(l.Text, out ll) && ll > 0) && 
            (Double.TryParse(h.Text, out hh) && hh > 0))
        {
            double pi = Math.PI;
            pp = Math.Round(pi*rr1*rr1 + pi*rr1*ll);
            v = Math.Round(((pi*rr1*rr1 / 3) * hh), 2);

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
        l.Text = "";
    }
}

