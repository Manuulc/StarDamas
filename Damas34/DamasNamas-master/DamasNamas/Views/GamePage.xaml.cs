using DamasNamas.ViewModels;

namespace DamasNamas.Views;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
 
	}


    protected override async void OnAppearing()
    {
		InitializeComponent();
        base.OnAppearing();
    }

}