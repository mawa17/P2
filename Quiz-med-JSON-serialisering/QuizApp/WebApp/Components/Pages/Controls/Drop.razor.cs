using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace WebApp.Components.Pages.Controls;

public partial class Drop
{
    [Parameter]
    public string DefaultStatus { get; init; } = "Upload file...";

    private readonly string _fileExtensions;
    [Parameter] // .gif,.jpg,.jpeg,.png,.doc,.docx
    public string? FileExtensions
    {
        get => this._fileExtensions;
        init
        {
        }
    }


    private IBrowserFile file;
    private string dragStatus;

    protected override void OnInitialized()
    {
        dragStatus = DefaultStatus;
        base.OnInitialized();
    }
    private void OnDragOver(DragEventArgs e)
    {
        dragStatus = "Drop Here...";
    }

    private void OnDragLeave()
    {
        dragStatus = String.IsNullOrEmpty(file?.Name) ? DefaultStatus : file.Name;
    }

    private void FilesDropped(InputFileChangeEventArgs e)
    {
        Console.WriteLine("DROPPP");
        file = e.File;
        string ext = Path.GetExtension(file.Name);
        Console.WriteLine(ext);

        dragStatus = file.Name;
    }
}
