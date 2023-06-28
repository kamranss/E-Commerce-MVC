namespace AllUp2.Services.FileService
{
    public class FileService : IFileService
    {
        public string ReadFile(string path, string body)
        {
            using (StreamReader reader = new StreamReader(path)) // this method takes path and reades the file content after we assign this value to the body adn return
            {
                body = reader.ReadToEnd();// this means Stream redader will read all body untill the end

            }
            return body;
        }
    }

   
}
