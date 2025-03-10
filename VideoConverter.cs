using System.Diagnostics;

namespace AnyConverter
{
    internal class VideoConverter
    {
        private const string ffmpegPath = "ffmpeg.exe";
        private readonly string inputFilePath;
        private readonly string fileName;
        private readonly string? scale;

        public VideoConverter(string inputFilePath, string fileName, string? scale)
        {
            this.inputFilePath = inputFilePath;
            this.fileName = fileName;
            this.scale = scale;
        }

        public bool TryConvert(string inptuFileExtension, string outputFileExtension)
        {
            inptuFileExtension = inptuFileExtension.ToLower();
            switch (outputFileExtension)
            {
                case "webm":
                    switch (inptuFileExtension)
                    {
                        case "gif":
                            GifToWebm();
                            return true;
                        case "png":
                            PngToWebm();
                            return true;
                    }
                    break;
            }
            return false;
        }

        public bool PngToWebm()
        {
            return GifToWebm();
        }

        public bool GifToWebm()
        {
            var outputFilePath = Path.Combine(inputFilePath, @"../", $"{fileName}.webm");

            var ffmpegStringBuilder = new List<string>() { "-y -i \"{0}\"" };
            if (scale != null)
            {
                ffmpegStringBuilder.Add($"-s {scale}");
            }
            ffmpegStringBuilder.Add("-b:v 0 -crf 25 \"{1}\"");

            var ffmpegString = String.Join(" ", ffmpegStringBuilder.ToArray());

            Console.WriteLine(string.Format(ffmpegString, inputFilePath, outputFilePath));

            using (var process = Process.Start(ffmpegPath, string.Format(ffmpegString, inputFilePath, outputFilePath)))
            {
                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
