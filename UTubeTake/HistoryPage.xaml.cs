namespace UTubeTake;

public partial class HistoryPage : ContentPage {

	public HistoryPage() {
		InitializeComponent();
	}

    private async void Back_Clicked(object sender, EventArgs e) {
		await Navigation.PopModalAsync();
    }
}