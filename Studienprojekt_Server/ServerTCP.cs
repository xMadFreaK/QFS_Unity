using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace Studienprojekt_Server
{
    class ServerTCP
    {
        private static TcpListener serverSocket; // basically the server
        public static ClientObject[] clientObjects;

        /* Starts client array and Serversockets/Connection from client to server.
         * */ 
        public static void InitializeServer()
        {
            InitializeClientObjects();
            InitializeServerSocket();
        }

        /* Starts sockets and the connection from client to server.
         * */
        private static void InitializeServerSocket()
        {
            serverSocket = new TcpListener(IPAddress.Any, 5555); // IP-Address, Port
            serverSocket.Start();
            serverSocket.BeginAcceptTcpClient(new AsyncCallback(ClientConnectCallback), null); // Client connects to server
        }

        /* Creates array of client objects and clears it for use.
         * */
        private static void InitializeClientObjects() // initialize client array
        {
            clientObjects = new ClientObject[Constants.MAX_PLAYERS]; // number of players
            for (int i = 1; i < Constants.MAX_PLAYERS; i++) // for-loop for filling array, client id 0 always empty
            {
                clientObjects[i] = new ClientObject(null, 0);
            }
        }

        /* Assigns connected player clients to the clientObjects array and fills the clientObject with the player data.
         * @param IAsyncResult result: connected client
         * */
        private static void ClientConnectCallback(IAsyncResult result) // result = connected client
        {
            TcpClient tempClient = serverSocket.EndAcceptTcpClient(result); // puts client into server as current server client
            serverSocket.BeginAcceptTcpClient(new AsyncCallback(ClientConnectCallback), null); // open connection again for everyone, so multiple people can connect to the server

            for (int i = 1; i < Constants.MAX_PLAYERS; i++)
            {
                if(clientObjects[i].socket == null) // if player assigned to this object
                {
                    clientObjects[i] = new ClientObject(tempClient, i); // filling in player data
                    return;
                }
            }
        }

        /* A method to send data from client to server or vice versa, the data packages are split up
         * and a ByteBuffer is created to prevent data loss.
         * @param int connectionID: the ID of the client-server connection
         * @param byte[] data: a byte array with the data from the client or from the server
         * */
        public static void SendDataTo(int connectionID, byte[] data)
        {
            // create bytebuffer within bytebuffer to prevent data loss while splitting packages
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteInteger((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1); // add size of package data
            buffer.WriteBytes(data); // add method argument package to new package
            clientObjects[connectionID].myStream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null); // send package/data from client to server or vice versa
            buffer.Dispose(); // clear buffer
        }
    }
}
