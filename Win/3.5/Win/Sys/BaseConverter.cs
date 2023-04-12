using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace MControl.Sys
{
    public class BaseConverter
    {
 
        #region method ToHex

        /// <summary>
        /// Converts specified string to HEX string.
        /// </summary>
        /// <param name="text">String to convert.</param>
        /// <returns>Returns hex string.</returns> 
        public static string Hex(string text)
        {
            return BitConverter.ToString(Encoding.UTF8.GetBytes(text)).ToLower().Replace("-", "");
        }

        /// <summary>
        /// Converts string to hex string.
        /// </summary>
        /// <param name="data">String to convert.</param>
        /// <returns>Returns data as hex string.</returns>
        public static string ToHexString(string data)
        {
            return Encoding.UTF8.GetString(ToHex(Encoding.UTF8.GetBytes(data)));
        }

        /// <summary>
        /// Converts string to hex string.
        /// </summary>
        /// <param name="data">Data to convert.</param>
        /// <returns>Returns data as hex string.</returns>
        public static string ToHexString(byte[] data)
        {
            return Encoding.UTF8.GetString(ToHex(data));
        }

        /// <summary>
        /// Convert byte to hex data.
        /// </summary>
        /// <param name="byteValue">Byte to convert.</param>
        /// <returns></returns>
        public static byte[] ToHex(byte byteValue)
        {
            return ToHex(new byte[] { byteValue });
        }

        /// <summary>
        /// Converts data to hex data.
        /// </summary>
        /// <param name="data">Data to convert.</param>
        /// <returns></returns>
        public static byte[] ToHex(byte[] data)
        {
            byte[] val = null;
            char[] hexChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

            using (MemoryStream retVal = new MemoryStream(data.Length * 2))
            {
                foreach (byte b in data)
                {
                    byte[] hexByte = new byte[2];

                    // left 4 bit of byte
                    hexByte[0] = (byte)hexChars[(b & 0xF0) >> 4];

                    // right 4 bit of byte
                    hexByte[1] = (byte)hexChars[b & 0x0F];

                    retVal.Write(hexByte, 0, 2);
                }
                val = retVal.ToArray();
            }
            return val;
        }


        /// Converts string from hex string.
        /// </summary>
        /// <param name="data">String to convert.</param>
        /// <returns>Returns data as hex string.</returns>
        public static string FromHexString(string data)
        {

            byte[] bytes = FromHex(Encoding.UTF8.GetBytes(data));
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Converts hex byte data to normal byte data. Hex data must be in two bytes pairs, for example: 0F,FF,A3,... .
        /// </summary>
        /// <param name="hexData">Hex data.</param>
        /// <returns></returns>
        public static byte[] FromHex(byte[] hexData)
        {
            if (hexData.Length < 2 || (hexData.Length / (double)2 != Math.Floor(hexData.Length / (double)2)))
            {
                throw new Exception("Illegal hex data, hex data must be in two bytes pairs, for example: 0F,FF,A3,... .");
            }
            byte[] val = null;
            using (MemoryStream retVal = new MemoryStream(hexData.Length / 2))
            {
                // Loop hex value pairs
                for (int i = 0; i < hexData.Length; i += 2)
                {
                    byte[] hexPairInDecimal = new byte[2];
                    // We need to convert hex char to decimal number, for example F = 15
                    for (int h = 0; h < 2; h++)
                    {
                        if (((char)hexData[i + h]) == '0')
                        {
                            hexPairInDecimal[h] = 0;
                        }
                        else if (((char)hexData[i + h]) == '1')
                        {
                            hexPairInDecimal[h] = 1;
                        }
                        else if (((char)hexData[i + h]) == '2')
                        {
                            hexPairInDecimal[h] = 2;
                        }
                        else if (((char)hexData[i + h]) == '3')
                        {
                            hexPairInDecimal[h] = 3;
                        }
                        else if (((char)hexData[i + h]) == '4')
                        {
                            hexPairInDecimal[h] = 4;
                        }
                        else if (((char)hexData[i + h]) == '5')
                        {
                            hexPairInDecimal[h] = 5;
                        }
                        else if (((char)hexData[i + h]) == '6')
                        {
                            hexPairInDecimal[h] = 6;
                        }
                        else if (((char)hexData[i + h]) == '7')
                        {
                            hexPairInDecimal[h] = 7;
                        }
                        else if (((char)hexData[i + h]) == '8')
                        {
                            hexPairInDecimal[h] = 8;
                        }
                        else if (((char)hexData[i + h]) == '9')
                        {
                            hexPairInDecimal[h] = 9;
                        }
                        else if (((char)hexData[i + h]) == 'A' || ((char)hexData[i + h]) == 'a')
                        {
                            hexPairInDecimal[h] = 10;
                        }
                        else if (((char)hexData[i + h]) == 'B' || ((char)hexData[i + h]) == 'b')
                        {
                            hexPairInDecimal[h] = 11;
                        }
                        else if (((char)hexData[i + h]) == 'C' || ((char)hexData[i + h]) == 'c')
                        {
                            hexPairInDecimal[h] = 12;
                        }
                        else if (((char)hexData[i + h]) == 'D' || ((char)hexData[i + h]) == 'd')
                        {
                            hexPairInDecimal[h] = 13;
                        }
                        else if (((char)hexData[i + h]) == 'E' || ((char)hexData[i + h]) == 'e')
                        {
                            hexPairInDecimal[h] = 14;
                        }
                        else if (((char)hexData[i + h]) == 'F' || ((char)hexData[i + h]) == 'f')
                        {
                            hexPairInDecimal[h] = 15;
                        }
                    }

                    // Join hex 4 bit(left hex cahr) + 4bit(right hex char) in bytes 8 it
                    retVal.WriteByte((byte)((hexPairInDecimal[0] << 4) | hexPairInDecimal[1]));
                }
                val = retVal.ToArray();
            }

            return val;
        }

       
        #endregion

        #region base 32

        // the valid chars for the encoding
            //private static string Base32Chars = "QAZ2WSX3" + "EDC4RFV5" + "TGB6YHN7" + "UJM8K9LP";

            private static string Base32Chars = "ABCDEFGH" + "IJKLMNOP" + "QRSTUVWX" + "YZ456789";

            /// <summary>
            /// Convert string to Base32 string
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public static string ToBase32(string text)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                return ToBase32String(bytes);
            }

            /// <summary>
            /// Convert Base32String to string
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public static string FromBase32(string text)
            {
                byte[] bytes = FromBase32String(text);
                string result = Encoding.UTF8.GetString(bytes);
                return result;
            }

            /// <summary>
            /// Converts an array of bytes to a Base32 string.
            /// </summary>
            public static string ToBase32String(byte[] bytes)
            {
                StringBuilder sb = new StringBuilder();         // holds the base32 chars
                byte index;
                int hi = 5;
                int currentByte = 0;

                while (currentByte < bytes.Length)
                {
                    // do we need to use the next byte?
                    if (hi > 8)
                    {
                        // get the last piece from the current byte, shift it to the right
                        // and increment the byte counter
                        index = (byte)(bytes[currentByte++] >> (hi - 5));
                        if (currentByte != bytes.Length)
                        {
                            // if we are not at the end, get the first piece from
                            // the next byte, clear it and shift it to the left
                            index = (byte)(((byte)(bytes[currentByte] << (16 - hi)) >> 3) | index);
                        }

                        hi -= 3;
                    }
                    else if (hi == 8)
                    {
                        index = (byte)(bytes[currentByte++] >> 3);
                        hi -= 3;
                    }
                    else
                    {

                        // simply get the stuff from the current byte
                        index = (byte)((byte)(bytes[currentByte] << (8 - hi)) >> 3);
                        hi += 5;
                    }

                    sb.Append(Base32Chars[index]);
                }

                return sb.ToString();
            }


            /// <summary>
            /// Converts a Base32-k string into an array of bytes.
            /// </summary>
            /// <exception cref="System.ArgumentException">
            /// Input string <paramref name="s">s</paramref> contains invalid Base32 characters.
            /// </exception>
            public static byte[] FromBase32String(string str)
            {
                int numBytes = str.Length * 5 / 8;
                byte[] bytes = new Byte[numBytes];

                // all UPPERCASE chars
                str = str.ToUpper();

                int bit_buffer;
                int currentCharIndex;
                int bits_in_buffer;

                if (str.Length < 3)
                {
                    bytes[0] = (byte)(Base32Chars.IndexOf(str[0]) | Base32Chars.IndexOf(str[1]) << 5);
                    return bytes;
                }

                bit_buffer = (Base32Chars.IndexOf(str[0]) | Base32Chars.IndexOf(str[1]) << 5);
                bits_in_buffer = 10;
                currentCharIndex = 2;
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = (byte)bit_buffer;
                    bit_buffer >>= 8;
                    bits_in_buffer -= 8;
                    while (bits_in_buffer < 8 && currentCharIndex < str.Length)
                    {
                        bit_buffer |= Base32Chars.IndexOf(str[currentCharIndex++]) << bits_in_buffer;
                        bits_in_buffer += 5;
                    }
                }

                return bytes;
            }
 

        /*
        private const int IN_BYTE_SIZE = 8;
        private const int OUT_BYTE_SIZE = 5;
        private static char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();

        public static string Base32Encode(byte[] data)
        {
            int i = 0, index = 0, digit = 0;
            int current_byte, next_byte;
            StringBuilder result = new StringBuilder((data.Length + 7) * IN_BYTE_SIZE / OUT_BYTE_SIZE);

            while (i < data.Length)
            {
                current_byte = (data[i] >= 0) ? data[i] : (data[i] + 256); // Unsign

                // Is the current digit going to span a byte boundary? 
                if (index > (IN_BYTE_SIZE - OUT_BYTE_SIZE))
                {
                    if ((i + 1) < data.Length)
                        next_byte = (data[i + 1] >= 0) ? data[i + 1] : (data[i + 1] + 256);
                    else
                        next_byte = 0;

                    digit = current_byte & (0xFF >> index);
                    index = (index + OUT_BYTE_SIZE) % IN_BYTE_SIZE;
                    digit <<= index;
                    digit |= next_byte >> (IN_BYTE_SIZE - index);
                    i++;
                }
                else
                {
                    digit = (current_byte >> (IN_BYTE_SIZE - (index + OUT_BYTE_SIZE))) & 0x1F;
                    index = (index + OUT_BYTE_SIZE) % IN_BYTE_SIZE;
                    if (index == 0)
                        i++;
                }
                result.Append(alphabet[digit]);
            }

            return result.ToString();
        }
        */
        //public static void testBase32(string[] args)
        //{
        //    if (args.Length < 1)
        //    {
        //        Console.WriteLine("Must give name of file(s) to be sha1 hashed.");
        //        return;
        //    }

        //    SHA1Managed hasher = new SHA1Managed();
        //    foreach (string file_path in args)
        //    {
        //        string file_hash = Transcoder.Base32Encode(hasher.ComputeHash(File.OpenRead(file_path)));
        //        Console.WriteLine("File: \"{0}\" Hash: \"{1}\"", file_path, file_hash);
        //    }
        //}
        /*
        public static string ToBase32(string text)
        {
            BitArray ba = new BitArray(Encoding.UTF8.GetBytes(text));
            return GenerateBase32FromString(ba);
        }

         public static string GenerateBase32FromString(BitArray ba)
         {

             try
             {
                 // user will modify the order as per requirement.         
                 char[] cProdKeyPoss = "ABGCD1EF2HI3KL4MN5PQ6RS7TV8WX9YZ".ToCharArray();
                 StringBuilder finalstring = new StringBuilder();
                 // add zero value bits to round off to multiple of 5         
                 //ba.Length = ba.Length + (5-(ba.Length % 5));         
                 // remove bits to round off to multiple of 5         
                 ba.Length = ba.Length - (ba.Length % 5);
                 //Console.WriteLine("ba.length = " + ba.Length.ToString());
                 for (int i = 0; i < ba.Length; i = i + 5)
                 {
                     int[] bitvalue = { 0, 0, 0, 0, 0 };
                     if (ba.Get(i) == true) bitvalue[0] = 1;
                     if (ba.Get(i + 1) == true) bitvalue[1] = 1;
                     if (ba.Get(i + 2) == true) bitvalue[2] = 1;
                     if (ba.Get(i + 3) == true) bitvalue[3] = 1;
                     if (ba.Get(i + 4) == true) bitvalue[4] = 1;
                     int temp = (16 * bitvalue[0]) + (8 * bitvalue[1]) + (4 * bitvalue[2]) + (2 * bitvalue[3]) + (bitvalue[4]);
                     finalstring.Append(cProdKeyPoss[temp].ToString());
                 }
                 Console.WriteLine(finalstring.ToString());
                 return finalstring.ToString();
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message); return null;
             }
         } 
        */
        #endregion

        #region  base converter

        static char[] map = new char[] { 
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 
        'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 
        'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 
        'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 
        'u', 'v', 'x', 'y', 'z', '2', '3', '4', };

        static char[] base62Map = new char[] { '0','1','2','3','4','5','6','7','8','9',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 
        'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 
        'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 
        'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 
        'u', 'v', 'x', 'y', 'z', '2', '3', '4', };


        
        public static string ToBase62(long inp)
        {
            return Encode(inp, base62Map);
        }

        public static long FromBase62(string encoded)
        {
            return Decode(encoded, base62Map);
        }

        // This does not "pad" values 
        public static string Encode(long inp, IEnumerable<char> map)
        {
            var b = map.Count();
            // value -> character 
            var toChar = map.Select((v, i) => new { Value = v, Index = i }).ToDictionary(i => i.Index, i => i.Value);
            var res = "";
            if (inp == 0)
            {
                return "" + toChar[0];
            }
            while (inp > 0)
            {
                // encoded least-to-most significant 
                var val = (int)(inp % b);
                inp = inp / b;
                res += toChar[val];
            }
            return res;
        }

        public static long Decode(string encoded, IEnumerable<char> map)
        {
            var b = map.Count();
            // character -> value 
            var toVal = map.Select((v, i) => new { Value = v, Index = i }).ToDictionary(i => i.Value, i => i.Index);
            long res = 0;
            // go in reverse to mirror encoding 
            for (var i = encoded.Length - 1; i >= 0; i--)
            {
                var ch = encoded[i];
                var val = toVal[ch];
                res = (res * b) + val;
            }
            return res;
        }
        #endregion

        #region base converter
        /*
        private static string ALPHANUMERIC =
"0123456789" +
"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
"abcdefghijklmnopqrstuvwxyz";

        public static string ToBase(long input, string baseChars)
        {
            string r = string.Empty;
            int targetBase = baseChars.Length;
            do
            {
                r = string.Format("{0}{1}",
                    baseChars[(int)(input % targetBase)],
                    r);
                input /= targetBase;
            } while (input > 0);

            return r;
        }

        public static long FromBase(string input, string baseChars)
        {
            int srcBase = baseChars.Length;
            long id = 0;
            string r = MControl.Strings.StrReverse(input);
            //string r = input.Reverse();

            for (int i = 0; i < r.Length; i++)
            {
                int charIndex = baseChars.IndexOf(r[i]);
                id += charIndex * (long)Math.Pow(srcBase, i);
            }

            return id;
        }

        public static string ToBase(byte[] input, string baseChars)
        {
            long l = BitConverter.ToInt64(input, 0);
            return ToBase(l, baseChars);
        }
         */ 
        #endregion


    //    void Main()
    //    {
    //        // for a 48-bit base, omits l/L, 1, i/I, o/O, 0 
    //        var map = new char[] { 
    //    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 
    //    'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 
    //    'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 
    //    'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 
    //    'u', 'v', 'x', 'y', 'z', '2', '3', '4', 
    //};
    //        var test = new long[] { 0, 1, 9999999999, 4294965286, 2292964213, 1000000000 };
    //        foreach (var t in test)
    //        {
    //            var encoded = Encode(t, map);
    //            var decoded = Decode(encoded, map);
    //            Console.WriteLine(string.Format("value: {0} encoded: {1}", t, encoded));
    //            if (t != decoded)
    //            {
    //                throw new Exception("failed for " + t);
    //            }
    //        }
    //    } 



/*
byte[] data = BitConverter.GetBytes(value); 
// make data big-endian if needed 
if (BitConverter.IsLittleEndian) { 
   Array.Reverse(data); 
} 
// first 5 base-64 character always "A" (as first 30 bits always zero) 
// only need to keep the 6 characters (36 bits) at the end  
string base64 = Convert.ToBase64String(data, 0, 8).Substring(5,6); 
 
byte[] data2 = new byte[8]; 
// add back in all the characters removed during encoding 
Convert.FromBase64String("AAAAA" + base64 + "=").CopyTo(data2, 0); 
// reverse again from big to little-endian 
if (BitConverter.IsLittleEndian) { 
   Array.Reverse(data2); 
} 
long decoded = BitConverter.ToInt64(data2, 0);
*/

    }
}
