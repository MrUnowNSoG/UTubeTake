using JikanDotNet;
using MyHikiList.Scripts;
using System.Net;

namespace MyHikiList.Pages;

public partial class SoloAnime : ContentPage {

    private Anime _animeData;

    public SoloAnime(Anime animeData) {
        InitializeComponent();

        _animeData = animeData;
        Title = _animeData.Title;

        MyInit();
    }

    private void MyInit() {

        //Картинка
        imgAnime.Source = _animeData.Images.JPG.LargeImageUrl;

        //Raiting
        scoreAnime.Text = "Score:" + _animeData.Score.ToString();
        raitingAnime.Text = "Ranked:" + _animeData.Rank.ToString();

        // Завантажте HTML-код у WebView
        youtubeWebView.Source = new HtmlWebViewSource {
            Html = GetHtmlForVideo(_animeData.Trailer.YoutubeId)
        };


        //Definition
        descriptionAnime.Text = _animeData.Synopsis.ToString();

    }
    

    private string GetHtmlForVideo(string IdVideo) {
        //Відео
        string youtubeUrl = $"https://www.youtube.com/embed/{IdVideo}?modestbranding=1&rel=0&controls=1&showinfo=0";

        //HTML-код для вбудовування відео
        string htmlSource = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            </head>
            <body style='margin:0; padding:0;'>
                <iframe width='100%' height='100%' src='{youtubeUrl}' frameborder='0' allowfullscreen></iframe>
            </body>
            </html>";

        return htmlSource;
    }


    //button
    private async void back_Clicked(object sender, EventArgs e) {
        await Navigation.PopAsync(true);
    }

    protected override bool OnBackButtonPressed() {
        // Блокуємо стандартну поведінку кнопки "Назад"
        return true;
    }

    private void WatchAnime_Clicked(object sender, EventArgs e) {
        if(_animeData != null) {
            long temp = (long)_animeData.MalId;
            DataUser.AddWatchAnime(temp);
        }

    }

    private void WillWatchAnime_Clicked(object sender, EventArgs e) {
        if (_animeData != null) {
            long temp = (long)_animeData.MalId;
            DataUser.AddWillWatchAnime(temp);
        }
    }

    private void WatchedAnime_Clicked(object sender, EventArgs e) {
        if (_animeData != null) {
            long temp = (long)_animeData.MalId;
            DataUser.AddWatchedAnime(temp);
        }
    }

    private void TrashAnime_Clicked(object sender, EventArgs e) {
        if (_animeData != null) {
            long temp = (long)_animeData.MalId;
            DataUser.AddBadAnime(temp);
        }
    }
}