using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;

namespace DotNetApi.WebSockets;
public class RoutesGateway
{
    private readonly Socket socket;

    public RoutesGateway(string serverUrl)
    {
        socket = IO.Socket(serverUrl);
    }

    public void Connect()
    {
        socket.On(Socket.EVENT_CONNECT, () =>
        {
            Console.WriteLine("Connected to Socket.IO server.");
        });

        socket.On("admin-new-points", (data) =>
        {
            Console.WriteLine("Received admin-new-points: " + data);
        });

        socket.On("new-points", (data) =>
        {
            Console.WriteLine("Received new-points: " + data);
        });

        socket.Connect();
    }

    public void SendMessage(string routeId, double lat, double lng)
    {
        var payload = new
        {
            route_id = routeId,
            lat,
            lng
        };

        socket.Emit($"new-points/{routeId}", JObject.FromObject(payload));
        socket.Emit("admin-new-points", JObject.FromObject(payload));
        
    }

    public void Disconnect()
    {
        socket.Disconnect();
    }
}
