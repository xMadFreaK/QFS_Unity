using System;
using System.Threading;

namespace Studienprojekt_Server

{
    class Program
    {
        private static Thread consoleThread;

        /* Connects to server, starts receiving packages, starts listening to packages, 
         * allows TCPclients to connect to the server.
         * */ 
        static void Main(string[] args)
        {
            ServerHandleData.InitializePackageListener(); //server starts adding all the packages we created
            ServerTCP.InitializeServer(); 
            InitializeConsoleThread(); // starts the console loop
        }

        /* Starts the console thread which keeps the console running.
         */
        private static void InitializeConsoleThread()
        {
            consoleThread = new Thread(ConsoleLoop);
            consoleThread.Name = "ConsoleThread"; // if the Thread crashes, the line "ConsoleThread" is shown - therefore you know where the problem lies
            consoleThread.Start();
        }

        /* Stays always open, checks input of console, Server-Updates, etc.
         * */
        private static void ConsoleLoop()
        {
            while (true)
            {

            }
        }
    }
}
