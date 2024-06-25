using JikanDotNet;
using MyHikiList.Scripts;

namespace MyHikiList.Pages;

public partial class ProfilPage : ContentPage {

    private IJikan _jikan = new Jikan();

    public ProfilPage() {
		InitializeComponent();

        Title = "Account";
        WatchAnime_Clicked(new object(), new EventArgs());
    }

    private async void WatchAnime_Clicked(object sender, EventArgs e) {
        AddStartPanel();
        foreach(long item in DataUser.idWatchAnime) {
            var anim = await _jikan.GetAnimeAsync(item);
            ProfileLayout.Children.Add(CreateFrame(anim.Data));
        }
    }

    private async void WillWatchAnime_Clicked(object sender, EventArgs e) {
        AddStartPanel();
        foreach (long item in DataUser.idWillWatchAnime) {
            var anim = await _jikan.GetAnimeAsync(item);
            ProfileLayout.Children.Add(CreateFrame(anim.Data));
        }
    }

    private async void WatchedAnime_Clicked(object sender, EventArgs e) {
        AddStartPanel();
        foreach (long item in DataUser.idWathedAnime) {
            var anim = await _jikan.GetAnimeAsync(item);
            ProfileLayout.Children.Add(CreateFrame(anim.Data));
        }
    }

    private async void TrashAnime_Clicked(object sender, EventArgs e) {
        AddStartPanel();
        foreach (long item in DataUser.idBadAnime) {
            var anim = await _jikan.GetAnimeAsync(item);
            ProfileLayout.Children.Add(CreateFrame(anim.Data));
        }
    }

    private void AddStartPanel() {
        // Зберігаємо Grid елемент
        var grid = ProfileLayout.Children[0];

        // Очищаємо всі елементи після Grid
        ProfileLayout.Children.Clear();
        ProfileLayout.Children.Add(grid);
    }

    private Frame CreateFrame(Anime anime) {
        // Створення Frame
        var frame = new Frame {
            HeightRequest = 150,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            CornerRadius = 10,
            Padding = new Thickness(0),
            BackgroundColor = Color.FromArgb("#2e2e2e"),
            BorderColor = Colors.Transparent
        };

        // Створення Grid
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        // Створення Image
        var image = new Microsoft.Maui.Controls.Image {
            Aspect = Aspect.AspectFill,
            Source = anime.Images.JPG.LargeImageUrl
        };
        Grid.SetRowSpan(image, 3);
        grid.Children.Add(image);
        Grid.SetRow(image, 0);
        Grid.SetColumn(image, 0);

        // Створення Label
        var label = new Label {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.StartAndExpand,
            Padding = new Thickness(10, 0, 0, 0),
            FontSize = 20, // Medium
            TextColor = Colors.White,
            Text = anime.Titles.First().Title
        };
        grid.Children.Add(label);
        Grid.SetRow(label, 0);
        Grid.SetColumn(label, 1);

        // Створення HorizontalStackLayout
        var horizontalStack = new HorizontalStackLayout {
            VerticalOptions = LayoutOptions.StartAndExpand,
            HorizontalOptions = LayoutOptions.StartAndExpand,
            Padding = new Thickness(10, 0, 0, 0),
            Spacing = 10
        };

        // Додавання Frame з Label в HorizontalStackLayout
        var subFrame1 = new Frame {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            CornerRadius = 5,
            Padding = new Thickness(7, 2),
            BackgroundColor = Color.FromArgb("#2e2e2e"),
            BorderColor = Colors.White
        };
        subFrame1.Content = new Label {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.StartAndExpand,
            FontSize = 10, // Micro
            TextColor = Colors.White,
            Text = ShortAge(anime.Rating.ToString())
        };
        horizontalStack.Children.Add(subFrame1);

        var subFrame2 = new Frame {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            CornerRadius = 5,
            Padding = new Thickness(7, 2),
            BackgroundColor = Color.FromArgb("#2e2e2e"),
            BorderColor = Colors.White
        };
        subFrame2.Content = new Label {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.StartAndExpand,
            FontSize = 10, // Micro
            TextColor = Colors.White,
            Text = CutDate(anime.Aired.From.ToString())
        };
        horizontalStack.Children.Add(subFrame2);

        var subFrame3 = new Frame {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
            CornerRadius = 5,
            Padding = new Thickness(7, 2),
            BackgroundColor = Color.FromArgb("#2e2e2e"),
            BorderColor = Colors.White
        };
        subFrame3.Content = new Label {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.StartAndExpand,
            FontSize = 10, // Micro
            TextColor = Colors.White,
            Text = anime.Status.ToString()
        };
        horizontalStack.Children.Add(subFrame3);

        grid.Children.Add(horizontalStack);
        Grid.SetRow(horizontalStack, 1);
        Grid.SetColumn(horizontalStack, 1);

        // Створення Grid для останнього рядка
        var bottomGrid = new Grid();
        bottomGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
        bottomGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.6, GridUnitType.Star) });
        bottomGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.4, GridUnitType.Star) });

        var rankLabel = new Label {
            TextColor = Colors.White,
            Margin = new Thickness(10, 0, 0, 0),
            FontSize = 18, // Body
            Text = "Ranked:#" + anime.Rank.ToString()
        };
        bottomGrid.Children.Add(rankLabel);
        Grid.SetRow(rankLabel, 0);
        Grid.SetColumn(rankLabel, 0);

        var watchButton = new Button {
            Text = "Watch anime"
        };
        watchButton.Clicked += OpenAnime_Clicked;
        watchButton.BindingContext = anime.MalId.ToString();
        bottomGrid.Children.Add(watchButton);
        Grid.SetRow(watchButton, 0);
        Grid.SetColumn(watchButton, 1);

        grid.Children.Add(bottomGrid);
        Grid.SetRow(bottomGrid, 2);
        Grid.SetColumn(bottomGrid, 1);

        // Додавання Grid до Frame
        frame.Content = grid;

        return frame;
    }

    private string CutDate(string time) {

        if (time == null) { return " "; }
        if (time.Length < 11) { return ""; }

        return time.Substring(6, 4);

    }

    private string ShortAge(string str) {

        List<string> text = new List<string>() { "Teens 13 or older", "(violence & profanity)", "Mild Nudity" };
        List<string> age = new List<string>() { "PG-13+", "R-17+", "R-18+" };

        for (int i = 0; i < text.Count; i++) {
            if (str.Contains(text[i])) {
                return age[i];
            }
        }

        return str;
    }

    private async void OpenAnime_Clicked(object sender, EventArgs e) {
        var button = sender as Button;

        var value = button?.BindingContext as string;
        if (value != null && int.TryParse(value, out int code)) {

            var animeRealese = await _jikan.GetAnimeAsync(code);

            await Navigation.PushAsync(new SoloAnime(animeRealese.Data), false);
        }
    }

}