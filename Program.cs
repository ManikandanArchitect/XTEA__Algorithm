using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTEA__Algorithm
{
    class Program
    {
        public static int num_rounds = 32;
        public static UInt32[] key = { 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF };
        static void Main(string[] args)
        {
            byte[] _firstInput = { 0x02, 0x03, 0x2F, 0x3F };
            byte[] _secondInput = { 0x05, 0x01, 0x4F, 0xc6 };

         

            UInt32 v0 = BitConverter.ToUInt32(_firstInput,0);
            UInt32 v1 = BitConverter.ToUInt32(_secondInput, 0);

            Encode(v0,v1);

            Console.ReadKey();
        }

        public static void Encode(UInt32 v0,UInt32 v1)
        {
            uint i;
            UInt32 sum = 0, delta = 0x9E3779B9;
            for (i = 0; i < num_rounds; i++)
            {
                v0 += (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]);
                sum += delta;
                v1 += (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]);
            }
            UInt32 testFirstEncode = v0; 
            UInt32 testsecondEncode = v1;

            var firstOutput = BitConverter.GetBytes(testFirstEncode);
            var SecondOutput = BitConverter.GetBytes(testsecondEncode);

            Console.WriteLine(BitConverter.ToString(firstOutput));
            Console.WriteLine(BitConverter.ToString(SecondOutput));

            Decode(v0,v1);

        }

        public static void Decode(UInt32 v0,UInt32 v1)
        {
            uint i;
            UInt32 delta = 0x9E3779B9;
            UInt32 sum = (UInt32)(delta * num_rounds);
            for (i = 0; i < num_rounds; i++)
            {
                v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum >> 11) & 3]);
                sum -= delta;
                v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]);
            }
          
            UInt32 testFirstDecode = v0;
            UInt32 testsecondDecode = v1;

            var firstOutput = BitConverter.GetBytes(testFirstDecode);
            var SecondOutput = BitConverter.GetBytes(testsecondDecode);

            Console.WriteLine(BitConverter.ToString(firstOutput));
            Console.WriteLine(BitConverter.ToString(SecondOutput));
        }
    }
}
