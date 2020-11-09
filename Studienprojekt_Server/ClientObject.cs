using System;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Studienprojekt_Server
{
    public class ClientObject
    {
        public TcpClient socket;
        public NetworkStream myStream; // to receive and send data from/to client
        public int connectionID; // ID of client
        public byte[] receiveBuffer; // stores the data the server receives from the client on the server
        public ByteBuffer buffer; // vorerst externe Ressource von Kevin Kaymak

        /* Constructor to create new ClientObject, also shortens the reaction time of the connection and starts the stream.
         * @param TcpClient newSocket
         * @param newConnectionID: an integer in which the identification number of the client-server-connection is saved
         * */
        public ClientObject(TcpClient newSocket, int newConnectionID)
        {
            if (newSocket == null) return;
            socket = newSocket;
            connectionID = newConnectionID;

            socket.NoDelay = true; // changes Nagle algorithm: shortens reaction time (tcp fires one package after another, without awaiting the acknowledgement of the prior package)
            socket.ReceiveBufferSize = 4096; // package size in bytes - makes tcp faster
            socket.SendBufferSize = 4096;

            myStream = socket.GetStream(); // get stream from player
            receiveBuffer = new byte[4096]; // resize receiveBuffer array in accordance to package size

            myStream.BeginRead(receiveBuffer, 0, socket.ReceiveBufferSize, ReceiveCallback, null); // arguments: buffer, offset, intsize, async callback,object 
            
        }

        /*
         * If we get information from the stream through "myStream", ReceiveCallback is called and reads the full package.
         * @param IAsyncResult result: connected client
         */
        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int readBytes = myStream.EndRead(result); // how much data are we getting in the current stream call? = getting exact length of current stream call
                if (readBytes <= 0) // if no data is received, close connection and end method
                {
                    CloseConnection();
                    return; 
                }
                byte[] newBytes = new byte[readBytes];
                Buffer.BlockCopy(receiveBuffer, 0, newBytes, 0, readBytes); // copies data we are getting into newBytes
                ServerHandleData.HandleData(connectionID, newBytes);

                myStream.BeginRead(receiveBuffer, 0, socket.ReceiveBufferSize, ReceiveCallback, null); // listen to stream call again for new data
            }
            catch (Exception)
            {
                CloseConnection(); // if there is an error, close connection
                throw;
            }
        }

        /* Closing the connection between client and server properly and emptying the occupied socket.
         * */
        private void CloseConnection()
        {
            Console.WriteLine("Connection from {0} has been terminated.", socket.Client.RemoteEndPoint.ToString());
            socket.Close();
            socket = null; // to empty socket
        }
    }
}
