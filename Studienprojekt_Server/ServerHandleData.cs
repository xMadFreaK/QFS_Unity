using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Studienprojekt_Server
{
    public class ServerHandleData
    {
        public delegate void Package_(int connectionID, byte[] data); // only data with connection ID can pass to find out which player sent which package
        public static Dictionary<int, Package_> packageListener;
        private static int pLength;

        /*  sorts packages into 
         */
        public static void InitializePackageListener()
        {
            packageListener = new Dictionary<int, Package_>();
            packageListener.Add((int)ClientPackages.CLogin, HandleLogin); 
            // ^ once the client sent the package CLogin to the server, the server checks everything 
            // (whether it received full package size and whether it is listening to the package)
            // and if we are listening to this package identifier, the Server will execute the method HandleLogin
        }

        /* Listens to stream until it has received full package size of bytes until it processes the package.
         * @param int connectionID: the ID of the connection
         * @param byte[] data: byte array with data sent through connection
        */
        public static void HandleData(int connectionID, byte[] data)
        {
            byte[] buffer = (byte[])data.Clone(); // clones method argument data into temporary array so it can be edited savely

            // checking if the connected player which sent this package has an instance of the bytebuffer to read out the information of the byte-array buffer.
            if(ServerTCP.clientObjects[connectionID].buffer == null)
            {
                // if there is no instance, create one
                ServerTCP.clientObjects[connectionID].buffer = new ByteBuffer();
            }

            // Reading out the package from the player to check which package it is
            ServerTCP.clientObjects[connectionID].buffer.WriteBytes(buffer);

            if(ServerTCP.clientObjects[connectionID].buffer.Count() == 0)
            { // checking if received package is empty. If so, stops to execute this code.
                ServerTCP.clientObjects[connectionID].buffer.Clear();
                return;
            }

            // checking whether package contains information
            if(ServerTCP.clientObjects[connectionID].buffer.Length() >= 4)
            { // checks if size of package is greater than 4 (4 bytes, size of int) = if it contains more information than just packet identifier
                pLength = ServerTCP.clientObjects[connectionID].buffer.ReadInteger(false);
                if (pLength <= 0) // check if package is 0 (= invalid)
                {
                    ServerTCP.clientObjects[connectionID].buffer.Clear();
                    return;
                }
            }

            // prevents network stream which splits a package from dropping the package
            while(pLength > 0 & pLength <= ServerTCP.clientObjects[connectionID].buffer.Length()-4)
            {
                if (pLength <= ServerTCP.clientObjects[connectionID].buffer.Length()-4)
                {
                    ServerTCP.clientObjects[connectionID].buffer.ReadInteger(); //reading out server information
                    data = ServerTCP.clientObjects[connectionID].buffer.ReadBytes(pLength);
                    HandleDataPackages(connectionID, data);
                }
                pLength = 0;
                if (ServerTCP.clientObjects[connectionID].buffer.Length() >= 4)
                {
                    pLength = ServerTCP.clientObjects[connectionID].buffer.ReadInteger(false);
                    if (pLength <= 0) // check if package is 0 (= invalid)
                    {
                        ServerTCP.clientObjects[connectionID].buffer.Clear();
                        return;
                    }
                }

                if (pLength <= 1)
                {
                    ServerTCP.clientObjects[connectionID].buffer.Clear();
                }
            }
        }

        private static void HandleDataPackages(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            int packageID = buffer.ReadInteger(); // which package did client actually send

            if (packageListener.TryGetValue(packageID, out Package_ package))
            {
                package.Invoke(connectionID, data);
            }
        }

        private static void HandleLogin (int connectionID, byte[] data)
        {

        }
    }
}
