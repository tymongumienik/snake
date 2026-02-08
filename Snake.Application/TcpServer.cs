using System.Net;
using System.Net.Sockets;
using Snake.Application.Core;

// inspired by https://medium.com/@jm.keuleyan/c-tcp-communications-building-a-client-server-chat-a2155d585191
// made async because learn.microsoft.com says so
public class TcpServer(GameManager manager)
{
    private readonly GameManager _manager = manager;

    public async Task StartAsync(int port)
    {
        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();

        Console.WriteLine($"Server started on port {port}!");

        while (true)
        {
            try
            {
                var client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected!");

                _ = HandleClientAsync(client);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accepting client: {ex}");
            }
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        using var stream = client.GetStream();
        var buffer = new byte[1024];

        try
        {
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    Console.WriteLine("Client disconnected.");
                    break;
                }

                var message = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"pong {message}");

                await stream.WriteAsync(buffer, 0, bytesRead); // echo
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Client connection error: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }
}
