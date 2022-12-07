using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace tictac_serverconsole
{
    class Program
    {

    
        // Stabilisco il local endpoint per la socket.  
        public static IPAddress ipAddress; // = System.Net.IPAddress.Parse("127.0.0.1");
        public static IPEndPoint localEndPoint; //= new IPEndPoint(ipAddress, 5000);
        public static string data = null;






        // Lista Mosse Possibili Che il Server Ha a disposizione
        static List<int> mossePossibili = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        // Incoming data from the client.  
        // public static string data = null;


        // in base alle mosse che il server ha a disposizione, viene generata una mossa casuale
        public static string move_generator()
        {
            if (mossePossibili.Count == 0)
            {
                return "";
            }
            Random rnd = new Random();
            int mossa = rnd.Next(0, mossePossibili.Count);
            int risultato = mossePossibili[mossa];
            mossePossibili.RemoveAt(mossa);
            Console.WriteLine("Mossa Ai: b" + risultato.ToString() + '\n');
            return "b" + risultato.ToString();
        }


                                            // funzione principale della socket, che permette al server di ricevere e interpretare pacchetti
        public static void StartListening()
        {
                                                // Buffer Per i dati che stanno arrivando 
            byte[] bytes = new Byte[1024];
            Console.WriteLine("Inserire Indirizzo ip su cui ospitare il gioco: ");
            IPAddress ipAddress = System.Net.IPAddress.Parse(Console.ReadLine());
            Console.WriteLine("Inserire Porta Server: ");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Convert.ToInt32(Console.ReadLine()));


            // Creo la socket TCP/IP inserendo l'ip a cui voglio connettermi e il protocollo di trasmissione.  
            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            // Associo la socket alla porta designata   
            // Avvio l'ascolto alle connessioni in arrivo.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // inzio del cisclo di ascolto.  
                while (true)
                {
                    Console.WriteLine("listening avviato Aspetto Mossa\n");
                    // finche non arrivano connessioni il programma è sospeso  
                    Socket handler = listener.Accept();
                    data = null;

                    // Processo la connessione in arrivo ricevendo i byte e trasformandoli in una stringa.  
                    while (true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }


                    //una volta convertita la stringa creo un protocollo basato sulle stringhe per elaborare i dati e rispedirli al client

                    //Nella stringa del client ottengo chi muove e le cordinate ex Mossa X: B1
                    if (data.Contains("Mossa"))
                    {

                        int b = Convert.ToInt32(data[10].ToString());

                        //Rimuovo dalla lista delle mosse possibili La mossa effetuata dal client
                        mossePossibili.Remove(b);
                        // Scrivo a console le istruzioni ricevute dal client  
                        Console.WriteLine(data.ToString());

                    }
                    if (data.Contains("Partita Terminata"))
                    {
                        // se il client si accorge di aver terminato la partita pulisco le mosse e comunico l'esito al server
                        Console.WriteLine("OK " + data + "\nRipristino mosse in Corse");
                        mossePossibili.Clear();
                        mossePossibili.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                    }





                    // converto in byte il messaggio e e spedisco al client la mossa che il server ha processato
                    byte[] msg = Encoding.ASCII.GetBytes(move_generator());
                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();







                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }



             
        






    public static int Main(String[] args)
            {
            // starto il programma
            
                StartListening();
                return 0;
            }




    }
}
