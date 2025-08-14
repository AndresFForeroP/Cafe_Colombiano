using System;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Cafe_Colombiano.src.Modules.Usuario.Ui
{
    public static class ImagenConsola
    {
        public static void MostrarImagenEnConsola(string rutaImagen, int anchoMax = 60)
        {
            try
            {
                using (var imagen = Image.Load<Rgba32>(rutaImagen))
                {
                    // Ajustar brillo y contraste para mejor visualización
                    imagen.Mutate(x =>
                        x.Contrast(1.8f) // aumenta contraste
                         .Brightness(2f) // aumenta brillo
                    );

                    // Mantener proporción
                    double relacion = (double)imagen.Height / imagen.Width;
                    int nuevoAlto = (int)(anchoMax * relacion);
                    imagen.Mutate(x => x.Resize(anchoMax, nuevoAlto));

                    // Mapa de caracteres para sombreado (de oscuro a claro)
                    string caracteres = "@%#*+=-:. ";

                    // Calcular margen izquierdo para centrar la imagen
                    int margenIzquierdo = Math.Max(0, (Console.WindowWidth - anchoMax) / 2);

                    for (int y = 0; y < imagen.Height; y++)
                    {
                        StringBuilder linea = new StringBuilder();
                        linea.Append(' ', margenIzquierdo); // añadir espacios para centrar
                        for (int x = 0; x < imagen.Width; x++)
                        {
                            var pixel = imagen[x, y];
                            var gris = (pixel.R + pixel.G + pixel.B) / 3;
                            int indice = gris * (caracteres.Length - 1) / 255;
                            linea.Append(caracteres[indice]);
                        }
                        Console.WriteLine(linea.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error mostrando imagen: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
