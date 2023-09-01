using System;
using System.Drawing;
using System.IO;
using System.Net;

/*
 * Reto #5
 * ASPECT RATIO DE UNA IMAGEN
 * Fecha publicación enunciado: 01/02/22
 * Fecha publicación resolución: 07/02/22
 * Dificultad: DIFÍCIL
 *
 * Enunciado: Crea un programa que se encargue de calcular el aspect ratio de una imagen a partir de una url.
 * - Nota: Esta prueba no se puede resolver con el playground online de Kotlin. Se necesita Android Studio.
 * - Url de ejemplo: https://raw.githubusercontent.com/mouredev/mouredev/master/mouredev_github_profile.png
 * - Por ratio hacemos referencia por ejemplo a los "16:9" de una imagen de 1920*1080px.
 *
 * Información adicional:
 * - Usa el canal de nuestro discord (https://mouredev.com/discord) "🔁reto-semanal" para preguntas, dudas o prestar ayuda a la acomunidad.
 * - Puedes hacer un Fork del repo y una Pull Request al repo original para que veamos tu solución aportada.
 * - Revisaré el ejercicio en directo desde Twitch el lunes siguiente al de su publicación.
 * - Subiré una posible solución al ejercicio el lunes siguiente al de su publicación.
 *
 */

namespace AspectRatioUrl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url;

            //url = "https://raw.githubusercontent.com/mouredev/mouredev/master/mouredev_github_profile.png";

            Console.WriteLine("Introduce una url que contenga una imagen:");

            url = Console.ReadLine();

            Image image = GetImage(url);

            Console.WriteLine(GetAspectRatio(image));
        }

        static int GetMCD(int a, int b)
        {
            // Implementación del algoritmo de Euclides para calcular el MCD
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static string GetAspectRatio(Image image)
        {
            if (image != null)
            {
                int width = image.Width;
                int height = image.Height;

                int mcd = GetMCD(width, height);

                int a = width / mcd;
                int b = height / mcd;

                return $"\nTamaño de la imagen:\nAncho: {width}px - Alto: {height}px\n\nAspect Ratio: {a}:{b}\n";
            }
            else
            {
                return "No se pudo acceder a la imagen.";
            }
        }

        static Image GetImage(string url)
        {
            var bytes = GetByteArray(url);

            if (bytes != null)
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    try
                    {
                        Image image = Image.FromStream(ms);

                        return image;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Problema al convertir a imagen: " + ex.Message);
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        static byte[] GetByteArray(string url)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    // Descarga los bytes de la imagen desde la URL.
                    byte[] bytes = webClient.DownloadData(url);
                    return bytes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al descargar la imagen: " + ex.Message);
                return null;
            }
        }
    }
}
