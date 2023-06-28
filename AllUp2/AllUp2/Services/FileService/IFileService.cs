namespace AllUp2.Services.FileService
{
    public interface IFileService
    {
        string ReadFile(string path, string body); // this is mainly will capture out httml and send to respectie email
    }
}
