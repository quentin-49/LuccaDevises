using LuccaDevises.Exception;
using LuccaDevises.Ressource;

namespace LuccaDevises.Service
{
    public class FileService : IFileService
    {
        public string[] ReadFiles(string path)
        {
            return File.ReadAllLines(path);
        }

        public void Exists(string path)
        {
            if (File.Exists(path) && Path.GetExtension(path).ToUpper().Equals(Constant.Extension))
                return;

            throw new CommandLineException(Constant.FileDoesNotExist);
        }
    }
}
