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
        Console.WriteLine("  Bienvenue sur le HazeL Tool, conçu pour surveiller votre WiFi.");
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine("  Tapez 'help' pour obtenir plus d'informations.");
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
            else if (input.ToLower() == "checksafety")
            {
                
            }
            else
            {
                // Traitez les autres commandes ou actions ici
                // ...
                Console.WriteLine("Commande inconnue. Tapez 'help' pour obtenir de l'aide.");
                Console.WriteLine("-----------------------------------------------------------");
            }
        }
    }

    static void AfficherAide()
    {
        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine("  help - Affiche cette aide.");
        Console.WriteLine("  checkwifi - Affiche les appareils connectés au réseau.");
        Console.WriteLine("  checkping - Affiche la latence du réseau.");
        Console.WriteLine("  checkwifipassword - Obtenez le mot de passe du wifi");
        Console.WriteLine("  checkipconfig - quelques informations utiles sur ton réseau");
        Console.WriteLine("  checksafety - voir les réseaux autours et leurs caractéristiques");
        Console.WriteLine("-----------------------------------------------------------------------");
    }


}


