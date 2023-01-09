using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Astronomy.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AstronomicalBodiesPage : ContentPage
	{
        public AstronomicalBodiesPage()
        {
            InitializeComponent();

            btnComet.Clicked += async (s, e) => await Shell.Current.GoToAsync("astronomicalbodydetails?astroName=comet");
            btnEarth.Clicked += async (s, e) => await Shell.Current.GoToAsync("astronomicalbodydetails?astroName=earth");
            btnMoon.Clicked += async (s, e) => await Shell.Current.GoToAsync("astronomicalbodydetails?astroName=moon");
            btnSun.Clicked += async (s, e) => await Shell.Current.GoToAsync("astronomicalbodydetails?astroName=sun");
        }
    }
}