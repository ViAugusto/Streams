using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        // Alterar o diretório para o lugar onde o projeto foi feito na máquina.
        string filePath = @"C:\Users\Vini\Documents\Dev\Streams\example.txt";

        await WriteToFileStreamAsync(filePath);

        await ReadFromFileStreamAsync(filePath);

        UseMemoryStream();
    }

    static async Task WriteToFileStreamAsync(string filePath)
    {
        string content = "Este é um exemplo de uso de Streams em .NET 8.0.\n";

        await using (FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
        {
            byte[] data = Encoding.UTF8.GetBytes(content);
            await fileStream.WriteAsync(data, 0, data.Length);
            Console.WriteLine("Dados escritos no arquivo.");
        }
    }

    static async Task ReadFromFileStreamAsync(string filePath)
    {
        await using (FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true))
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                string content = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Dados lidos do arquivo:");
                Console.WriteLine(content);
            }
        }
    }

    static void UseMemoryStream()
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            string content = "Dados armazenados em memória.";
            byte[] data = Encoding.UTF8.GetBytes(content);

            memoryStream.Write(data, 0, data.Length);

            memoryStream.Position = 0;

            byte[] buffer = new byte[data.Length];
            int bytesRead = memoryStream.Read(buffer, 0, buffer.Length);
            string readContent = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine("Dados lidos do MemoryStream:");
            Console.WriteLine(readContent);
        }
    }
}
