using SkiaSharp;

public static class GifRenderer
{
    public static void MostrarGifEnConsola(string rutaGif)
    {
        using var stream = File.OpenRead(rutaGif);
        using var codec = SKCodec.Create(stream);

        var frameInfos = codec.FrameInfo;
        var info = new SKImageInfo(codec.Info.Width, codec.Info.Height);
        using var bitmap = new SKBitmap(info);

        // Paleta de caracteres para distintos niveles de gris
        string charset = "@%#*+=-:. ";

        // Tamaño dinámico según la consola
        int ancho = Console.WindowWidth - 1;
        int alto = (Console.WindowHeight - 1) * 2; // compensar aspecto

        Console.CursorVisible = false;

        for (int i = 0; i < frameInfos.Length; i++)
        {
            var opts = new SKCodecOptions(i);
            codec.GetPixels(info, bitmap.GetPixels(), opts);

            Console.SetCursorPosition(0, 0); // más fluido que Clear()

            for (int y = 0; y < alto; y++)
            {
                for (int x = 0; x < ancho; x++)
                {
                    int srcX = x * bitmap.Width / ancho;
                    int srcY = y * bitmap.Height / alto;

                    SKColor color = bitmap.GetPixel(srcX, srcY);

                    int gray = (color.Red + color.Green + color.Blue) / 3;
                    int idx = gray * (charset.Length - 1) / 255;

                    Console.Write(charset[idx]);
                }
                Console.WriteLine();
            }

            int delay = frameInfos[i].Duration;
            if (delay <= 0) delay = 33; // default más rápido
            Thread.Sleep(delay);
        }

        Console.CursorVisible = true;
    }
}
