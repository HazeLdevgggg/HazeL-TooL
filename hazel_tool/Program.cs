using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static void Main()
    {
        Console.WriteLine("  HazeL Tool by HazeL#2876");
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine("   type 'help' to see command");
        Console.WriteLine("-----------------------------------------------------------");

        while (true)
        {
            string input = Console.ReadLine();

            if (input.ToLower() == "help")
            {
                AfficherAide();
            }
            else if (input.ToLower() == "checkping")
            {
                Console.WriteLine("Entrez l'adresse IP : ");
                string ipAddress = Console.ReadLine();
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(ipAddress);

                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine($"Latence : {reply.RoundtripTime} ms");
                    Console.WriteLine("-----------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("La requête a échoué, IP peut-être invalide");
                    Console.WriteLine("-----------------------------------------------------------");
                }
            }
            else if (input.ToLower() == "checkwifi")
            {
                List<string> connectedDevices = new List<string>();
                NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface networkInterface in networkInterfaces)
                {
                    if (networkInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                        foreach (UnicastIPAddressInformation ip in ipProperties.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                connectedDevices.Add(ip.Address.ToString());
                            }
                        }
                    }
                }
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine("Appareils connectés au réseau :");
                foreach (string ipAddress in connectedDevices)
                {
                    Console.WriteLine(ipAddress);
                }
                Console.WriteLine("-----------------------------------------------------------");
            }
            else if (input.ToLower() == "checkwifipassword")
            {
                Console.WriteLine("Indiquez le nom de votre wifi :");
                string profileName = Console.ReadLine();
                string arguments = $"wlan show profile name={profileName} key=clear";
                Process process = new Process();
                process.StartInfo.FileName = "netsh";
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine(output);
            }
            else if (input.ToLower() == "checkipconfig")
            {
                Console.WriteLine("-----------------------------------------------------------");
                ProcessStartInfo psi = new ProcessStartInfo("ipconfig");
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = psi;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine(output);
                Console.WriteLine("-----------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("Unknow command. type 'help' to see all the command");
                Console.WriteLine("-----------------------------------------------------------");
            }
        }
    }

    static void AfficherAide()
    {
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine("  help - show all command");
        Console.WriteLine("  checkwifi - see the ip of all the user on your wifi");
        Console.WriteLine("  checkping - see the ping of your wifi");
        Console.WriteLine("  checkwifipassword - get your wifi password and other information");
        Console.WriteLine("  checkipconfig - get some usefull information about your wifi");
        Console.WriteLine("-----------------------------------------------------------------------");
    }


}


