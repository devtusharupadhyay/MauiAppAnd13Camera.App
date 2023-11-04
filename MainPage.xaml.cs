using System.Diagnostics;

namespace MauiAppAnd13Camera;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    async void Button_Clicked(System.Object sender, System.EventArgs e) 
    { 
        
    }


    public async void CounterBtn_Clicked(object sender, EventArgs e)
    {
        try
        {

            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(new string('=', 30));

            Debug.WriteLine(ex.Message);
        }

    }

}



