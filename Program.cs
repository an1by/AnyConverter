using AnyConverter;

namespace AnyController
{
    static class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = args[0];
            string outputExtension = args[1];
            string? scale = args.Length >= 3 ? args[2] : null;
            Console.WriteLine(scale);

            string fileName = Path.GetFileName(inputFilePath);
            string[] split = fileName.Split('.');
            int lastIndex = split.Length - 1;
            string fileExtension = split[lastIndex];
            split = split.Take(lastIndex).ToArray();
            string fileNameWithoutExt = String.Join(".", split);

            var videoConverter = new VideoConverter(inputFilePath, fileNameWithoutExt, scale);
            if (videoConverter.TryConvert(fileExtension, outputExtension))
            {
                return;
            }

            Console.WriteLine("There is no variant to convert it!");
        }
    }
}