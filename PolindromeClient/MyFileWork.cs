
using System.Collections.Generic;
using System.IO;


namespace PolindromeClient
{
    public static class MyFileWork
    {
        internal static List<string> Palindroms = new List<string>();
        internal static List<string> NotPalindroms = new List<string>();

        internal static List<string> ReadFolder(string Folder)
        {

            string[] temp = Directory.GetFiles(Folder, "*.txt");
            foreach(string name in temp)
            {
                //Console.WriteLine(name);
                  PalindromeClient.Names.Add(name);
            }
            return PalindromeClient.Names;
        }

        internal static string FileToString(string nameOfFile)
        {
            StreamReader sr = new StreamReader(nameOfFile);
            string fileText = sr.ReadToEnd();
            return fileText;
        }

        internal static void WriteFiles()
        {
            File.WriteAllLines(PalindromeClient.Folder/* + "/Palindroms"*/, Palindroms);
            File.WriteAllLines(PalindromeClient.Folder/* + "/NotPalindroms"*/, NotPalindroms);
        }
    }
}
