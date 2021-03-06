﻿using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Renci.SshNet.Async;
using System.IO;
using System.Threading;

namespace demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize client and connect like you normally would
            //var client = new SshClient("127.0.0.1", 55555, "admin", " ");
            //var client = new SshClient("127.0.0.1", 55555, "admin", " ");
            //client.Connect();

            // quick way to use ist, but not best practice - SshCommand is not Disposed, ExitStatus not checked...
            //Console.WriteLine(client.CreateCommand("ls -l").Execute());

            //Console.WriteLine(client.CreateCommand("gcc -v").Execute());

            //// await a directory listing
            //var listing = await client.ListDirectoryAsync(".");

            //// await a file upload
            //using (var localStream = File.OpenRead("path_to_local_file"))
            //{
            //    await client.UploadAsync(localStream, "upload_path");
            //}

            // disconnect like you normally would
            //client.Disconnect();

            ExecuteCommandOnThreadPool();

            Console.ReadLine();
        }
         

        public static void ExecuteCommandOnThreadPool()
        {
            string host = "127.0.0.1";
            string user = "admin";
            string pass = " ";

            Action runCommand = () =>
            {
                SshClient client = new SshClient(host, 55555, user, pass);
                try
                {
                    client.Connect();
                    //var terminal = client.RunCommand("/bin/run.sh");

                    Thread.Sleep(3000);

                    var terminal = client.RunCommand("gcc -v");

                    Console.WriteLine(terminal.Result);
                }
                finally
                {
                    client.Disconnect();
                    client.Dispose();
                }
            };

            ThreadPool.QueueUserWorkItem(x => runCommand());
        }
    }
} 
