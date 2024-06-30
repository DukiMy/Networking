/**
* Explanation of the program:
* This is a simple chat client that connects to a server.
* It sends a message to the server and then reads a message from the server.
* It then prints the message received from the server.
*/
using System;
using System.Net.Sockets;
using System.Text;

namespace client;
/**
* Class to represent a chat client.
* It has a main method, and everything is static due to that.
* Most other things are ready to convert to an object type of its own.
* You need remove the static specifiers and add a constructor to do so.
*/
class ChatClient
{
    private static string server = "127.0.0.1";
    private static int port = 13000;
    private static string msg = string.Empty;
/**
* Main method to run the client.
* NOTE: Remove this and replace it with a constructor.
*/
    static void Main()
    {
        using TcpClient client = new TcpClient(server, port);
        using NetworkStream stream = client.GetStream();

        while (true)
        {
            Console.WriteLine("Enter a message to send to the server: ");
            msg = Console.ReadLine();

            WriteBytesToStream(stream, msg!);

            Console.WriteLine(
                "Received: {0}",
                ReadBytesFromStream(stream)
            );
        }
    }
/**
* Method to write Bytes to a stream.
* @param stream The stream to write to.
* @param message The message to write to the stream.
*/
    private static void WriteBytesToStream(NetworkStream stream, string message)
    {
        byte[] ASCIIBytes = Encoding.ASCII.GetBytes(message);

        stream.Write(
            buffer: ASCIIBytes,
            offset: 0,
            count:  ASCIIBytes.Length
        );
    } 
/**
* Method to read Bytes from a stream.
* @param stream The stream to read from.
* @return The Bytes read from the stream.
*/
    private static string ReadBytesFromStream(NetworkStream stream)
    {
        byte[] bytes = new byte[256];

        return Encoding.ASCII.GetString(
            bytes: bytes,
            index: 0,
            count: stream.Read(bytes, 0, bytes.Length)
        );
    }
}
