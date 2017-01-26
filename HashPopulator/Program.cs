using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashPopulator
{
    class Program
    {
        const int saltsize = 16;
        const int hashsize = 32;
        const int pbkdf2iter = 12000;


        /// <summary>
        /// Main console application for input
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string p;
            string hex;
            string cont;
            do
            {
                p = getPass();
                byte[] b = new byte[20];
                b = hash(p, hashsize, pbkdf2iter);
                hex = BitConverter.ToString(b).Replace("-", string.Empty);
                Console.WriteLine(hex + Environment.NewLine);
                Console.WriteLine("Continue? y/n");
                cont = Console.ReadLine();
            }
            while (cont == "y");
            Console.ReadLine();
        }

        /// <summary>
        /// Input funtion to grab the password
        /// </summary>
        /// <returns></returns>
        private static string getPass()
        {
            string p;
            Console.WriteLine("Enter the password to be hashed.");
            p = Console.ReadLine();
            return p;
        }

        /// <summary>
        /// Function to generate a salt, write it, then use the generated salt in order to generate a hash
        /// </summary>
        /// <param name="p">Password to be hashed</param>
        /// <param name="outputBytes">The byte-level output of the hashing algorithim</param>
        /// <param name="iter">The number of iterations for the hashing algorithm to pass though</param>
        /// <returns></returns>
        private static byte[] hash(string p, int outputBytes, int iter)
        {
            string hex;
            byte[] salt = new byte[saltsize];
            System.Security.Cryptography.RNGCryptoServiceProvider salter = new System.Security.Cryptography.RNGCryptoServiceProvider();
            salter.GetBytes(salt);
            hex = BitConverter.ToString(salt).Replace("-", string.Empty);
            Console.WriteLine(hex);
            System.Security.Cryptography.Rfc2898DeriveBytes hasher = new System.Security.Cryptography.Rfc2898DeriveBytes(p, salt, iter);
            return hasher.GetBytes(outputBytes);
        }
    }
}
