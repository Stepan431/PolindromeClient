using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PolindromeClient
{
     internal static class PalindromeClient
    {
        internal static string Folder;

        internal static List<string> Names = new List<string>{};

         public static void Communication()
        {
            int serverPort = 9595;
            string serverIP = "127.0.0.1";
            StringBuilder sb = new StringBuilder();
            InputFolder(out Folder);
            List<string> ToRepeat = new List<string> { };

            void InputFolder(out string Folder)
            {
                do
                {
                    Console.WriteLine("Введите путь");
                    Folder = Console.ReadLine();
                } while (!Directory.Exists(Folder));
            }
            Names = MyFileWork.ReadFolder(Folder);
            do
            {
                ToRepeat = Names;
                try
                {

                     foreach (string name in ToRepeat.ToArray())
                    {
                        TcpClient client = new TcpClient(serverIP, serverPort);         

                    NetworkStream stream = client.GetStream();

                        sb.Clear();
                        sb.Append(MyFileWork.FileToString(name));
                        byte[] data = Encoding.Unicode.GetBytes(sb.ToString());

                        stream.Write(data, 0, data.Length);

                        Console.WriteLine($"Отправлено {sb.ToString()}");

                        string responceData = String.Empty;

                        int bytes = stream.Read(data, 0, data.Length);

                        responceData = System.Text.Encoding.Unicode.GetString(data, 0, bytes);

                        Console.WriteLine($"Получено: {responceData}");

                        switch (responceData)
                        {
                            case "NotPalindrome":
                                MyFileWork.NotPalindroms.Add(name);
                                Names.Remove(name);
                                break;

                            case "Palindrome":
                                MyFileWork.Palindroms.Add(name);
                                Names.Remove(name);
                                break;

                            default:
                                ToRepeat.Add(name);
                                break;
                        }
                        
                        stream.Close();
                        client.Close();
                    }
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"Argumentnullexception {e}");
                }
                catch (SocketException e)
                {
                    Console.WriteLine($"SocketException {e}");
                }
                Console.WriteLine(ToRepeat.Count);

            } while (ToRepeat.Count > 0);
           // MyFileWork.WriteFiles();
            Console.WriteLine("Обработка завершена.");
            Console.WriteLine("Палиндромов {0}.", MyFileWork.Palindroms.Count);
            Console.WriteLine("Не палиндромов{0}.", MyFileWork.NotPalindroms.Count);
            Console.WriteLine("Не обработано {0}", ToRepeat.Count);
        }

        
    }
}
