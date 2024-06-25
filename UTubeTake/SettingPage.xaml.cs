using CommunityToolkit.Maui.Storage;
using UTubeTake.Code;

namespace UTubeTake;

public partial class SettingPage : ContentPage {

	public SettingPage() {
		InitializeComponent();
        UpdateTextForPath();
	}

    private async void PathVideo_Clicked(object sender, EventArgs e) {
       
        try {
            var folder = await FolderPicker.PickAsync(default);

            if (folder.Folder != null) {
                if (folder.Folder.Path  != null && folder.Folder.Path.ToString() != "") {
                    SettingStatic.SaveSetting(folder.Folder.Path.ToString(), SettingStatic.pathForImage);
                    UpdateTextForPath();
                }
            }

        }catch(Exception ex) {
            PathVideo_label.Text = "Error path";
        }
    }

    private async void PathImage_Clicked(object sender, EventArgs e) {

        try {
            var folder = await FolderPicker.PickAsync(default);
            
            if (folder.Folder != null) {
                if (folder.Folder.Path.ToString() != null && folder.Folder.Path.ToString() != "") {
                    SettingStatic.SaveSetting(SettingStatic.pathForVideo, folder.Folder.Path.ToString());
                    UpdateTextForPath();
                }
            }

        } catch (Exception ex) {

            PathImage_label.Text = "Error path";
        }
    }

    private void UpdateTextForPath() {
        PathVideo_label.Text = SettingStatic.pathForVideo;
        PathImage_label.Text = SettingStatic.pathForImage;
    }

    private async void Back_Clicked(object sender, EventArgs e) {
		await Navigation.PopModalAsync();
    }
}