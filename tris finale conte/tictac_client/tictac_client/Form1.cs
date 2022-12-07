using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace tictac_client
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

        }

    
        public static string messaggio; // variabile pubblica per return messaggio socket

        private bool CheckWin()
        {
            if (b1.Text == "" && b2.Text == "" && b3.Text == "" && b4.Text == "" && b5.Text == "" && b6.Text == "" && b7.Text == "" && b8.Text == "" && b9.Text == "")
            {

                return false;

            }
            else
            {
                // x righe
                if (b1.Text == "X" && b2.Text == "X" && b3.Text == "X")
                {

                    client_message("Partita Terminata Vince X");
                    return true;
                    
                }
                if (b4.Text == "X" && b5.Text == "X" && b6.Text == "X")
                {

                    client_message("Partita Terminata Vince X");
                    return true;

                }
                if (b7.Text == "X" && b8.Text == "X" && b9.Text == "X")
                {


                    client_message("Partita Terminata Vince X");
                    return true;
                }

                // o righe
                if (b1.Text == "O" && b2.Text == "O" && b3.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;

                }
                if (b4.Text == "O" && b5.Text == "O" && b6.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;

                }
                if (b7.Text == "O" && b8.Text == "O" && b9.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;
                }

                // x colonne
                if (b1.Text == "X" && b4.Text == "X" && b7.Text == "X")
                {

                    client_message("Partita Terminata Vince X");
                    return true;

                }
                if (b2.Text == "X" && b5.Text == "X" && b8.Text == "X")
                {
                    client_message("Partita Terminata Vince X");
                    return true;
                }
                if (b3.Text == "X" && b6.Text == "X" && b9.Text == "X")
                {
                    client_message("Partita Terminata Vince X");
                    return true;
                }

                // o colonne
                if (b1.Text == "O" && b4.Text == "O" && b7.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;
                }
                if (b2.Text == "O" && b5.Text == "O" && b8.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;
                }
                if (b3.Text == "O" && b6.Text == "O" && b9.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;
                }



                // diagonali x
                if (b1.Text == "X" && b5.Text == "X" && b9.Text == "X")
                {
                    client_message("Partita Terminata Vince X");
                    return true;
                }
                if (b3.Text == "X" && b5.Text == "X" && b7.Text == "X")
                {
                    client_message("Partita Terminata Vince X");
                    return true;

                }


                // diagonali o
                if (b1.Text == "O" && b5.Text == "O" && b9.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;
                }
                if (b3.Text == "O" && b5.Text == "O" && b7.Text == "O")
                {
                    client_message("Partita Terminata Vince O");
                    return true;

                }

                if (b1.Text !="" && b2.Text != "" && b3.Text != "" && b4.Text != "" && b5.Text != "" && b6.Text != "" && b7.Text != "" && b8.Text != "" && b9.Text != "")
                {
                    client_message("Partita Terminata con parità");
                    return true;

                }


            }
            return false;
        }// funzione controllo vittoria
        public string client_message(string txt)
        {
            
            //Creo Buffer per i dati che arriveranno.  
            byte[] bytes = new byte[1024];

            // Connect to a remote device.  
            try
            {
                //Creo l'endpoint per connettermi al server usando l'ip delle textbox.  
                 
                IPAddress ipAddress = System.Net.IPAddress.Parse(textbox1.Text);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Convert.ToInt32(textbox2.Text));

                // Creo la stream socket TCP-IP.  
                Socket esender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connetto la Socket all'endpoint remoto e catcho gli errori.  
                try
                {
                    esender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        esender.RemoteEndPoint.ToString());

                    // converto il messaggio in un array di byte.  
                    byte[] msg = Encoding.ASCII.GetBytes(txt + "<EOF>");

                    // invio i dati attraverso la socket.  
                    int bytesSent = esender.Send(msg);

                    // ricevo risposta dal server.  
                    int bytesRec = esender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                      messaggio =   Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // chiudo la socket.  
                    esender.Shutdown(SocketShutdown.Both);
                    esender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    MessageBox.Show("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    MessageBox.Show("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    MessageBox.Show("Unexpected exception : {0}", e.ToString());
                }



            }
            catch (Exception e)
            {
                MessageBox.Show("Unexpected exception : {0}", e.ToString());
                Console.WriteLine(e.ToString());
            }


            return messaggio;
        }
       
        
        
        
        private void reset()
        {
            b1.Text = "";
            b2.Text = "";
            b3.Text = "";
            b4.Text = "";
            b5.Text = "";
            b6.Text = "";
            b7.Text = "";
            b8.Text = "";
            b9.Text = "";
        }


       

        private void btn_click(object sender, EventArgs e)
        {

            if(IPAddress.TryParse(textbox1.Text, out _)) //controllo indirizzo ip
            {

                Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;

                if (b.Text == "")
                {

                    if (CheckWin() == false)
                    {

                        Servermove(client_message("Mossa X: " + b.Name));
                        b.Text = "X";
                        if (CheckWin() == true) { reset(); }
                    }
                    else
                    {

                        reset();
                    }
                }
                else
                {
                    //client_message("X ha provato a muovere ma casella già impegnata");
                }
            }
            else
            {

                textbox1.Text = "Indirizzo IP non valido";
            }
            

        }

  

        private void Servermove(string move)
        {
            if (move.Contains("b1")) { b2.Text = "O"; }
            if (move.Contains("b2")) { b2.Text = "O"; }
            if (move.Contains("b3")) { b3.Text = "O"; }
            if (move.Contains("b4")) { b4.Text = "O"; }
            if (move.Contains("b5")) { b5.Text = "O"; }
            if (move.Contains("b6")) { b6.Text = "O"; }
            if (move.Contains("b7")) { b7.Text = "O"; }
            if (move.Contains("b8")) { b8.Text = "O"; }
            if (move.Contains("b9")) { b9.Text = "O"; }
        }


    }
}
