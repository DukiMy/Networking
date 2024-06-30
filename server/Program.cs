/**
* This is a simple chat server that listens for a client to connect to it.
* Once a client connects, the server will wait for the client to send a message.
* The server will then convert the message to uppercase and send it back to the client.
* The server will continue to wait for messages from the client until the client sends an empty message.
* The server will then close the connection to the client and stop listening for new connections.
*/
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server;
/**
* The ChatServer class is the main class for the server.
*/
class ChatServer
{
    private static int port = 13000;
    private static IPAddress localAddr = IPAddress.Parse("127.0.0.1");
    static void Main()
    {
        using TcpListener server = new TcpListener(localAddr, port);
        server.Start();
        Console.WriteLine("Server started");

        Console.WriteLine("Waiting...");
        using TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Connected to client");

        using NetworkStream stream = client.GetStream();
        Console.WriteLine("Waiting for data from client");
        string? msg = string.Empty;

        while (true)
        {
            // Fills the stream and the variable with data if data is supplied by the client
            msg = ReadBytesFromStream(stream).ToUpper();
            Console.WriteLine("Received data from client: " + msg);

            if (string.IsNullOrEmpty(msg)) { break; }

            // Empties the stream
            WriteBytesToStream(stream, msg);
            Console.WriteLine("Sent data to client");
        }
    }
/**
* Reads bytes from the stream and converts them to a string.
* @param stream The stream to read from.
* @return The string representation of the bytes read from the stream.
*/
    private static string ReadBytesFromStream(NetworkStream stream)
    {
        byte[] bytes = new byte[256];

        return Encoding.ASCII.GetString(
            bytes,
            0,
            stream.Read(bytes, 0, bytes.Length)
        );
    }
/**
* Writes bytes to the stream.
* @param stream The stream to write to.
* @param strData The string to write to the stream.
*/
    private static void WriteBytesToStream(NetworkStream stream, string strData)
    {
        byte[] ASCIIBytes = Encoding.ASCII.GetBytes(strData);
        
        stream.Write(
            ASCIIBytes,
            0,
            ASCIIBytes.Length
        );
    }
}
