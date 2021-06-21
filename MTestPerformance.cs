using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text;


namespace SomeNamespace {
    public class MTestPerformance {

        static int runs = 3;
        static ulong input_length = 10000000;
        static string input;

        delegate byte[] FunctionPtr(String input);

        public static void start() {

            initInput();

            Console.WriteLine("Performance Test:");
            Console.WriteLine("input length: " + input.Length);
            Console.WriteLine("runs: " + runs);
            Console.WriteLine("");

            List<(string func, Stopwatch sw)> results = new List<(string func, Stopwatch sw)>();
            Console.WriteLine("Testing Performance of V1");
            results.Add(("V1", TestPerformance(HexStringToByteArrayV1)));
            Console.WriteLine("Testing Performance of V2");
            results.Add(("V2", TestPerformance(HexStringToByteArrayV2)));
            Console.WriteLine("Testing Performance of V3");
            results.Add(("V3", TestPerformance(HexStringToByteArrayV3)));
            Console.WriteLine("Testing Performance of V4");
            results.Add(("V4", TestPerformance(HexStringToByteArrayV4)));
            Console.WriteLine("Testing Performance of V5_1");
            results.Add(("V5_1", TestPerformance(HexStringToByteArrayV5_1)));
            Console.WriteLine("Testing Performance of V5_2");
            results.Add(("V5_2", TestPerformance(HexStringToByteArrayV5_2)));
            Console.WriteLine("Testing Performance of V5_3");
            results.Add(("V5_3", TestPerformance(HexStringToByteArrayV5_3)));
            Console.WriteLine("Testing Performance of V6");
            results.Add(("V6", TestPerformance(HexStringToByteArrayV6)));
            Console.WriteLine("Testing Performance of V7");
            results.Add(("V7", TestPerformance(HexStringToByteArrayV7)));
            Console.WriteLine("Testing Performance of V8");
            results.Add(("V8", TestPerformance(HexStringToByteArrayV8)));
            Console.WriteLine("Testing Performance of V9");
            results.Add(("V9", TestPerformance(HexStringToByteArrayV9)));
            Console.WriteLine("Testing Performance of V10");
            results.Add(("V10", TestPerformance(HexStringToByteArrayV10)));
            Console.WriteLine("Testing Performance of V11");
            results.Add(("V11", TestPerformance(HexStringToByteArrayV11)));
            Console.WriteLine("Testing Performance of V12");
            results.Add(("V12", TestPerformance(HexStringToByteArrayV12)));
            Console.WriteLine("Testing Performance of V13");
            results.Add(("V13", TestPerformance(HexStringToByteArrayV13)));
            Console.WriteLine("Testing Performance of V14");
            results.Add(("V14", TestPerformance(HexStringToByteArrayV14)));
            Console.WriteLine("Testing Performance of V15");
            results.Add(("V15", TestPerformance(HexStringToByteArrayV15)));
            Console.WriteLine("Testing Performance of V16");
            results.Add(("V16", TestPerformance(HexStringToByteArrayV16)));
            Console.WriteLine("Testing Performance of V17");
            results.Add(("V17", TestPerformance(HexStringToByteArrayV17)));
            Console.WriteLine("Testing Performance of V18");
            results.Add(("V18", TestPerformance(HexStringToByteArrayV18)));
            Console.WriteLine("Testing Performance of V19");
            results.Add(("V19", TestPerformance(HexStringToByteArrayV19)));
            Console.WriteLine("Testing Performance of V20");
            results.Add(("V20", TestPerformance(HexStringToByteArrayV20)));
            Console.WriteLine("Testing Performance of V21");
            results.Add(("V21", TestPerformance(HexStringToByteArrayV21)));
            Console.WriteLine("");

            PrintPerformanceResults(results);

            Console.WriteLine("");
            Console.WriteLine("Short Correctness Test: (Upper case)");
            string hexStr = "0001090C1ADDE0FF"; // starts with 00 on purpose
            bTestCorrectnessPrintError = true;
            TestCorrectnessForAll(hexStr);

            Console.WriteLine("");
            Console.WriteLine("Short Correctness Test: (Lower case)");
            hexStr = "0001090C1ADDE0FF".ToLower();
            bTestCorrectnessPrintError = true;
            TestCorrectnessForAll(hexStr);

            Console.WriteLine("");
            Console.WriteLine("Full Correctness Test: (Upper case)");
            hexStr = "";
            for (int i = 0; i < 256; i++) {
                hexStr += i.ToString("X2");
            }
            bTestCorrectnessPrintError = false;
            TestCorrectnessForAll(hexStr);

            Console.WriteLine("");
            Console.WriteLine("Full Correctness Test: (Lower case)");
            hexStr = "";
            for (int i = 0; i < 256; i++) {
                hexStr += i.ToString("x2");
            }
            bTestCorrectnessPrintError = false;
            TestCorrectnessForAll(hexStr);

        }

        public static void initInput() {
            StringBuilder strb = new StringBuilder();
            //strb.Append((01).ToString("X2"));
            for (ulong i = 0; i < (input_length / 2); i++) {
                strb.Append((i % 256).ToString("X2"));
            }
            input = strb.ToString();
        }

        static void TestCorrectnessForAll(string hexStr) {
            Console.WriteLine("input: " + hexStr);
            TestCorrectness(HexStringToByteArrayV1, hexStr);
            TestCorrectness(HexStringToByteArrayV2, hexStr);
            TestCorrectness(HexStringToByteArrayV3, hexStr);
            TestCorrectness(HexStringToByteArrayV4, hexStr);
            TestCorrectness(HexStringToByteArrayV5_1, hexStr);
            TestCorrectness(HexStringToByteArrayV5_2, hexStr);
            TestCorrectness(HexStringToByteArrayV5_3, hexStr);
            TestCorrectness(HexStringToByteArrayV6, hexStr);
            TestCorrectness(HexStringToByteArrayV7, hexStr);
            TestCorrectness(HexStringToByteArrayV8, hexStr);
            TestCorrectness(HexStringToByteArrayV9, hexStr);
            TestCorrectness(HexStringToByteArrayV10, hexStr);
            TestCorrectness(HexStringToByteArrayV11, hexStr);
            TestCorrectness(HexStringToByteArrayV12, hexStr);
            TestCorrectness(HexStringToByteArrayV13, hexStr);
            TestCorrectness(HexStringToByteArrayV14, hexStr);
            TestCorrectness(HexStringToByteArrayV15, hexStr);
            TestCorrectness(HexStringToByteArrayV16, hexStr);
            TestCorrectness(HexStringToByteArrayV17, hexStr);
            TestCorrectness(HexStringToByteArrayV18, hexStr);
            TestCorrectness(HexStringToByteArrayV19, hexStr);
            TestCorrectness(HexStringToByteArrayV20, hexStr);
            TestCorrectness(HexStringToByteArrayV21, hexStr);
        }

        static Stopwatch TestPerformance(FunctionPtr f) {
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < runs; i++) {
                f(input);
            }
            sw.Stop();
            return sw;
        }

        static bool bTestCorrectnessPrintError = false;
        static void TestCorrectness(FunctionPtr f, string inputStr) {
            try {
                string r = ByteArrayToString(f(inputStr));
                if (r != inputStr.ToLower()) {
                    if (bTestCorrectnessPrintError) {
                        Console.WriteLine(f.Method.Name + " failed!\noutput  : " + r + "\nexpected: " + inputStr.ToLower());
                    } else {
                        Console.WriteLine(f.Method.Name + " failed!");
                    }
                } else {
                    Console.WriteLine(f.Method.Name + " successfully passed!");
                }
            } catch (Exception e) {
                Console.WriteLine(f.Method.Name + " failed! exception got caught! content: " + e.Message);
            }
        }

        static public void PrintPerformanceResults(List<(string func, Stopwatch sw)> results) {
            if(results.Count < 1) {
                return;
            }

            Stopwatch sw1 = results[0].sw;
            Console.WriteLine("average elapsed time per run:");
            for(int i = 0; i < results.Count; i++) {
                Console.WriteLine(results[i].func + " = {0}ms", (results[i].sw.Elapsed.TotalMilliseconds / runs).ToString("0.0"));
            }

            Console.WriteLine("");
            Console.WriteLine("V1 average ticks per run: " + (sw1.ElapsedTicks /(double)runs).ToString("0.0"));
            for (int i = 1; i < results.Count; i++) {
                if (sw1.ElapsedTicks > results[i].sw.ElapsedTicks) {
                    Console.WriteLine(results[i].func + " is more fast than " + results[0].func + " by: {0} times (ticks ratio)", ((sw1.ElapsedTicks / (double)results[i].sw.ElapsedTicks)).ToString("0.0"));
                } else {
                    Console.WriteLine(results[0].func + " is more fast than " + results[i].func + " by: {0} times (ticks ratio)", ((results[i].sw.ElapsedTicks / (double)sw1.ElapsedTicks)).ToString("0.0"));
                }
            }

            Console.WriteLine("");

        }

        public static string ByteArrayToString(byte[] ba) {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/311179/13817556 by Tomalak (accepted answer)
        public static byte[] HexStringToByteArrayV1(String hex) {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }


        // ***************************************************************************************************
        // https://stackoverflow.com/a/2556329/13817556 using SoapHexBinary by Mykroft
        // using System.Runtime.Remoting.Metadata.W3cXsd2001
        // SoapHexBinary is not supported in .NET Core/ .NET Standard... – juFo
        // link by Jeremy https://github.com/mono/mono/blob/main/mcs/class/corlib/System.Runtime.Remoting.Metadata.W3cXsd2001/SoapHexBinary.cs

        internal static byte[] HexStringToByteArrayV2(string value) {
            char[] chars = value.ToCharArray();
            byte[] buffer = new byte[chars.Length / 2 + chars.Length % 2];
            int charLength = chars.Length;

            if (charLength % 2 != 0) {
                //throw CreateInvalidValueException(value);
            }

            int bufIndex = 0;
            for (int i = 0; i < charLength - 1; i += 2) {
                buffer[bufIndex] = FromHex(chars[i], value);
                buffer[bufIndex] <<= 4;
                buffer[bufIndex] += FromHex(chars[i + 1], value);
                bufIndex++;
            }
            return buffer;
        }

        static byte FromHex(char hexDigit, string value) {
            //try {
                return byte.Parse(hexDigit.ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            //} catch (FormatException) {
            //    throw CreateInvalidValueException(value);
            //}           
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/6274772/13817556 look-up table by drphrozen
        private static readonly byte[] LookupTable = new byte[] {
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
        };

        private static byte Lookup(char c) {
            var b = LookupTable[c];
            if (b == 255)
                throw new IOException("Expected a hex character, got " + c);
            return b;
        }

        public static byte ToByte(char[] chars, int offset) {
            return (byte)(Lookup(chars[offset]) << 4 | Lookup(chars[offset + 1]));
        }

        public static byte[] HexStringToByteArrayV3(string str) {
            byte[] res = new byte[(str.Length % 2 != 0 ? 0 : str.Length / 2)]; //check and allocate memory
            for (int i = 0, j = 0; j < res.Length; i += 2, j++) //convert loop
                res[j] = (byte)(Lookup(str[i]) << 4 | Lookup(str[i + 1]));
            return res;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/17923942/13817556 ByteManipulation by CoperNick
        public static byte[] HexStringToByteArrayV4(string s) {
            byte[] bytes = new byte[s.Length / 2];
            for (int i = 0; i < bytes.Length; i++) {
                int hi = s[i * 2] - 65;
                hi = hi + 10 + ((hi >> 31) & 7);

                int lo = s[i * 2 + 1] - 65;
                lo = lo + 10 + ((lo >> 31) & 7) & 0x0f;

                bytes[i] = (byte)(lo | hi << 4);
            }
            return bytes;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/2050653/13817556 by Chris F
        static byte[] HexStringToByteArrayV5_1(string hexString) {
            int hexStringLength = hexString.Length;
            byte[] b = new byte[hexStringLength / 2];
            for (int i = 0; i < hexStringLength; i += 2) {
                int topChar = (hexString[i] > 0x40 ? hexString[i] - 0x37 : hexString[i] - 0x30) << 4;
                int bottomChar = hexString[i + 1] > 0x40 ? hexString[i + 1] - 0x37 : hexString[i + 1] - 0x30;
                b[i / 2] = Convert.ToByte(topChar + bottomChar);
            }
            return b;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/2050653/13817556 by Chris F
        // editted based on Amir Rezaei suggestion
        static byte[] HexStringToByteArrayV5_2(string hexString) {
            int hexStringLength = hexString.Length;
            byte[] b = new byte[hexStringLength / 2];
            for (int i = 0; i < hexStringLength; i += 2) {
                int topChar = (hexString[i] > 0x40 ? hexString[i] - 0x37 : hexString[i] - 0x30) << 4;
                int bottomChar = hexString[i + 1] > 0x40 ? hexString[i + 1] - 0x37 : hexString[i + 1] - 0x30;
                //b[i / 2] = Convert.ToByte(topChar + bottomChar);
                // Convert.ToByte(topChar + bottomChar) can be written as (byte)(topChar + bottomChar) – Amir Rezaei
                b[i / 2] = (byte)(topChar + bottomChar);
            }
            return b;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/2050653/13817556 by Chris F
        // editted based on Amir Rezaei suggestion
        // plus add support if Hex not UPPER case, based on Ben Voigt suggestion 
        static byte[] HexStringToByteArrayV5_3(string hexString) {
            int hexStringLength = hexString.Length;
            byte[] b = new byte[hexStringLength / 2];
            for (int i = 0; i < hexStringLength; i += 2) {
                int topChar = hexString[i];// & ~0x20;
                topChar = (topChar > 0x40 ? (topChar & ~0x20) - 0x37 : topChar - 0x30) << 4;
                int bottomChar = hexString[i + 1];// & ~0x20;
                bottomChar = bottomChar > 0x40 ? (bottomChar & ~0x20) - 0x37 : bottomChar - 0x30;
                //b[i / 2] = Convert.ToByte(topChar + bottomChar);
                // Convert.ToByte(topChar + bottomChar) can be written as (byte)(topChar + bottomChar) – Amir Rezaei
                b[i / 2] = (byte)(topChar + bottomChar);
            }
            return b;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/10706477/13817556 by Ben Mosher
        public static byte[] HexStringToByteArrayV6(string hexString) {
            if (hexString.Length % 2 != 0) throw new ArgumentException("String must have an even length");
            var array = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2) {
                array[i / 2] = ByteFromTwoChars(hexString[i], hexString[i + 1]);
            }
            return array;
        }

        private static byte ByteFromTwoChars(char p, char p_2) {
            byte ret;
            if (p <= '9' && p >= '0') {
                ret = (byte)((p - '0') << 4);
            } else if (p <= 'f' && p >= 'a') {
                ret = (byte)((p - 'a' + 10) << 4);
            } else if (p <= 'F' && p >= 'A') {
                ret = (byte)((p - 'A' + 10) << 4);
            } else throw new ArgumentException("Char is not a hex digit: " + p, "p");

            if (p_2 <= '9' && p_2 >= '0') {
                ret |= (byte)((p_2 - '0'));
            } else if (p_2 <= 'f' && p_2 >= 'a') {
                ret |= (byte)((p_2 - 'a' + 10));
            } else if (p_2 <= 'F' && p_2 >= 'A') {
                ret |= (byte)((p_2 - 'A' + 10));
            } else throw new ArgumentException("Char is not a hex digit: " + p_2, "p_2");

            return ret;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/20695932/13817556 by Maratius
        [System.Diagnostics.Contracts.Pure]
        public static byte[] HexStringToByteArrayV7(string value) {
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length % 2 != 0)
                throw new ArgumentException("Hexadecimal value length must be even.", "value");

            unchecked {
                byte[] result = new byte[value.Length / 2];
                for (int i = 0; i < result.Length; i++) {
                    // 0(48) - 9(57) -> 0 - 9
                    // A(65) - F(70) -> 10 - 15
                    int b = value[i * 2]; // High 4 bits.
                    int val = ((b - '0') + ((('9' - b) >> 31) & -7)) << 4;
                    b = value[i * 2 + 1]; // Low 4 bits.
                    val += (b - '0') + ((('9' - b) >> 31) & -7);
                    result[i] = checked((byte)val);
                }
                return result;
            }
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/20695932/13817556 by Maratius (unsafe version)
        [System.Diagnostics.Contracts.Pure]
        public static unsafe byte[] HexStringToByteArrayV8(string value) {
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length % 2 != 0)
                throw new ArgumentException("Hexadecimal value length must be even.", "value");

            unchecked {
                byte[] result = new byte[value.Length / 2];
                fixed (char* valuePtr = value) {
                    char* valPtr = valuePtr;
                    for (int i = 0; i < result.Length; i++) {
                        // 0(48) - 9(57) -> 0 - 9
                        // A(65) - F(70) -> 10 - 15
                        int b = *valPtr++; // High 4 bits.
                        int val = ((b - '0') + ((('9' - b) >> 31) & -7)) << 4;
                        b = *valPtr++; // Low 4 bits.
                        val += (b - '0') + ((('9' - b) >> 31) & -7);
                        result[i] = checked((byte)val);
                    }
                }
                return result;
            }
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/34333212/13817556 by Geograph
        public static byte[] HexStringToByteArrayV9(string hexString) {
            byte[] b = new byte[hexString.Length / 2];
            char c;
            for (int i = 0; i < hexString.Length / 2; i++) {
                c = hexString[i * 2];
                b[i] = (byte)((c < 0x40 ? c - 0x30 : (c < 0x47 ? c - 0x37 : c - 0x57)) << 4);
                c = hexString[i * 2 + 1];
                b[i] += (byte)(c < 0x40 ? c - 0x30 : (c < 0x47 ? c - 0x37 : c - 0x57));
            }

            return b;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/64498722/13817556 by AlejandroAlis 
        static public byte[] HexStringToByteArrayV10(string str) {
            byte[] res = new byte[(str.Length % 2 != 0 ? 0 : str.Length / 2)]; //check and allocate memory
            for (int i = 0, j = 0; j < res.Length; i += 2, j++) //convert loop
                res[j] = (byte)((str[i] % 32 + 9) % 25 * 16 + (str[i + 1] % 32 + 9) % 25);
            return res;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/2889978/13817556 by Fredrik Hu
        public static byte[] HexStringToByteArrayV11(string hex) {
            byte[] bytes = new byte[hex.Length / 2];
            int bl = bytes.Length;
            for (int i = 0; i < bl; ++i) {
                bytes[i] = (byte)((hex[2 * i] > 'F' ? hex[2 * i] - 0x57 : hex[2 * i] > '9' ? hex[2 * i] - 0x37 : hex[2 * i] - 0x30) << 4);
                bytes[i] |= (byte)(hex[2 * i + 1] > 'F' ? hex[2 * i + 1] - 0x57 : hex[2 * i + 1] > '9' ? hex[2 * i + 1] - 0x37 : hex[2 * i + 1] - 0x30);
            }
            return bytes;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/21246369/13817556 by Maarten Bodewes
        public static byte[] HexStringToByteArrayV12(String hex) {

            // pre-create the array
            int resultLength = hex.Length / 2;
            byte[] result = new byte[resultLength];
            // set validity = 0 (0 = valid, anything else is not valid)
            int validity = 0;
            int c, isLetter, value, validDigitStruct, validDigit, validLetterStruct, validLetter;
            for (int i = 0, hexOffset = 0; i < resultLength; i++, hexOffset += 2) {
                c = hex[hexOffset];

                // check using calculation over bits to see if first char is a letter
                // isLetter is zero if it is a digit, 1 if it is a letter (upper & lowercase)
                isLetter = (c >> 6) & 1;

                // calculate the tuple value using a multiplication to make up the difference between
                // a digit character and an alphanumerical character
                // minus 1 for the fact that the letters are not zero based
                value = ((c & 0xF) + isLetter * (-1 + 10)) << 4;

                // check validity of all the other bits
                validity |= c >> 7; // changed to >>, maybe not OK, use UInt?

                validDigitStruct = (c & 0x30) ^ 0x30;
                validDigit = ((c & 0x8) >> 3) * (c & 0x6);
                validity |= (isLetter ^ 1) * (validDigitStruct | validDigit);

                validLetterStruct = c & 0x18;
                validLetter = (((c - 1) & 0x4) >> 2) * ((c - 1) & 0x2);
                validity |= isLetter * (validLetterStruct | validLetter);

                // do the same with the lower (less significant) tuple
                c = hex[hexOffset + 1];
                isLetter = (c >> 6) & 1;
                value ^= (c & 0xF) + isLetter * (-1 + 10);
                result[i] = (byte)value;

                // check validity of all the other bits
                validity |= c >> 7; // changed to >>, maybe not OK, use UInt?

                validDigitStruct = (c & 0x30) ^ 0x30;
                validDigit = ((c & 0x8) >> 3) * (c & 0x6);
                validity |= (isLetter ^ 1) * (validDigitStruct | validDigit);

                validLetterStruct = c & 0x18;
                validLetter = (((c - 1) & 0x4) >> 2) * ((c - 1) & 0x2);
                validity |= isLetter * (validLetterStruct | validLetter);
            }

            if (validity != 0) {
                throw new ArgumentException("Hexadecimal encoding incorrect for input " + hex);
            }

            return result;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/6275329/13817556 by ClausAndersen
        private static readonly byte[] LookupTableLow = new byte[] {
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
        };

        private static readonly byte[] LookupTableHigh = new byte[] {
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0x00, 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80, 0x90, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xA0, 0xB0, 0xC0, 0xD0, 0xE0, 0xF0, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xA0, 0xB0, 0xC0, 0xD0, 0xE0, 0xF0, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
          0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
        };

        private static byte LookupLow(char c) {
            var b = LookupTableLow[c];
            if (b == 255)
                throw new IOException("Expected a hex character, got " + c);
            return b;
        }

        private static byte LookupHigh(char c) {
            var b = LookupTableHigh[c];
            if (b == 255)
                throw new IOException("Expected a hex character, got " + c);
            return b;
        }

        public static byte ToByteV2(char[] chars, int offset) {
            return (byte)(LookupHigh(chars[offset++]) | LookupLow(chars[offset]));
        }

        public static byte[] HexStringToByteArrayV13(string str) {
            byte[] res = new byte[(str.Length % 2 != 0 ? 0 : str.Length / 2)]; //check and allocate memory
            for (int i = 0, j = 0; j < res.Length; i += 2, j++) //convert loop
                res[j] = (byte)(LookupHigh(str[i]) | LookupLow(str[i + 1]));
            return res;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/6378247/13817556 by Stas Makutin 
        public static byte[] HexStringToByteArrayV14(string src) {
            if (String.IsNullOrEmpty(src))
                return null;

            int index = src.Length;
            int sz = index / 2;
            if (sz <= 0)
                return null;

            byte[] rc = new byte[sz];

            while (--sz >= 0) {
                char lo = src[--index];
                char hi = src[--index];

                rc[sz] = (byte)(
                    (
                        (hi >= '0' && hi <= '9') ? hi - '0' :
                        (hi >= 'a' && hi <= 'f') ? hi - 'a' + 10 :
                        (hi >= 'A' && hi <= 'F') ? hi - 'A' + 10 :
                        0
                    )
                    << 4 |
                    (
                        (lo >= '0' && lo <= '9') ? lo - '0' :
                        (lo >= 'a' && lo <= 'f') ? lo - 'a' + 10 :
                        (lo >= 'A' && lo <= 'F') ? lo - 'A' + 10 :
                        0
                    )
                );
            }

            return rc;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/16565896/13817556 by JJJ
        public static byte[] HexStringToByteArrayV15(string s) {
            byte[] ab = new byte[s.Length >> 1];
            for (int i = 0; i < s.Length; i++) {
                int b = s[i];
                b = (b - '0') + ((('9' - b) >> 31) & -7);
                ab[i >> 1] |= (byte)(b << 4 * ((i & 1) ^ 1));
            }
            return ab;
        }


        // ***************************************************************************************************
        // https://stackoverflow.com/a/16907438/13817556 by JamieSee
        public static byte[] HexStringToByteArrayV16(string hexString) {
            if (!string.IsNullOrEmpty(hexString) && hexString.Length % 2 != 0) {
                throw new FormatException("Hexadecimal string must not be empty and must contain an even number of digits to be valid.");
            }

            hexString = hexString.ToUpperInvariant();
            byte[] data = new byte[hexString.Length / 2];

            for (int index = 0; index < hexString.Length; index += 2) {
                int highDigitValue = hexString[index] <= '9' ? hexString[index] - '0' : hexString[index] - 'A' + 10;
                int lowDigitValue = hexString[index + 1] <= '9' ? hexString[index + 1] - '0' : hexString[index + 1] - 'A' + 10;

                if (highDigitValue < 0 || lowDigitValue < 0 || highDigitValue > 15 || lowDigitValue > 15) {
                    throw new FormatException("An invalid digit was encountered. Valid hexadecimal digits are 0-9 and A-F.");
                } else {
                    byte value = (byte)((highDigitValue << 4) | (lowDigitValue & 0x0F));
                    data[index / 2] = value;
                }
            }

            return data;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/18939148/13817556 by spacepille
        private static readonly byte[] HexNibble = new byte[] {
            0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7,
            0x8, 0x9, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
            0x0, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF, 0x0,
            0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
            0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
            0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
            0x0, 0xA, 0xB, 0xC, 0xD, 0xE, 0xF
        };

        public static byte[] HexStringToByteArrayV17(string str) {
            int byteCount = str.Length >> 1;
            byte[] result = new byte[byteCount + (str.Length & 1)];
            for (int i = 0; i < byteCount; i++)
                result[i] = (byte)(HexNibble[str[i << 1] - 48] << 4 | HexNibble[str[(i << 1) + 1] - 48]);
            if ((str.Length & 1) != 0)
                result[byteCount] = (byte)HexNibble[str[str.Length - 1] - 48];
            return result;
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/60626134/13817556 by Gregory Morse
        public static byte[] HexStringToByteArrayV18(string str) {
            return BigInteger.Parse(str, System.Globalization.NumberStyles.HexNumber).ToByteArray(isBigEndian: true);//.Reverse().ToArray();
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/10758999/13817556 by Rick
        // this doesn't support FF, because dictionary loop, to fix it add line hd.Add("FF", 255);
        static Dictionary<string, byte> hexindex = initDictionary();
        static Dictionary<string, byte> initDictionary() {
            Dictionary<string, byte>  hd = new Dictionary<string, byte>();
            //for (byte i = 0; i < 255; i++) {
            for (byte i = 0; i < 255; i++) {
                hd.Add(i.ToString("X2"), i);
            }
            hd.Add("FF", 255);
            return hd;
        }

        public static byte[] HexStringToByteArrayV19(string str) {
            List<byte> hexres = new List<byte>();
            for (int i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return hexres.ToArray();
        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/56378760/13817556 by SandRock 
        /// <summary>Reads a hex string into bytes</summary>
        public static IEnumerable<byte> HexadecimalStringToBytes(string hex) {
            if (hex == null) {
                throw new ArgumentNullException(nameof(hex));
            }

            char c, c1 = default(char);
            bool hasc1 = false;
            unchecked {
                for (int i = 0; i < hex.Length; i++) {
                    c = hex[i];

                    bool isValid = 'A' <= c && c <= 'f' || 'a' <= c && c <= 'f' || '0' <= c && c <= '9';
                    if (!hasc1) {
                        if (isValid) {
                            hasc1 = true;
                        }
                    } else {
                        hasc1 = false;
                        if (isValid) {
                            yield return (byte)((GetHexVal(c1) << 4) + GetHexVal(c));
                        } else {
                        }
                    }

                    c1 = c;
                }
            }
        }

        /// <summary>Reads a hex string into a byte array</summary>
        public static byte[] HexStringToByteArrayV20(string hex) {
            if (hex == null) {
                throw new ArgumentNullException(nameof(hex));
            }

            var bytes = new List<byte>(hex.Length / 2);
            foreach (var item in HexadecimalStringToBytes(hex)) {
                bytes.Add(item);
            }

            return bytes.ToArray();
        }

        private static byte GetHexVal(char val) {
            return (byte)(val - (val < 0x3A ? 0x30 : val < 0x5B ? 0x37 : 0x57));
            //                   ^^^^^^^^^^^^^^^^^   ^^^^^^^^^^^^^^^^^   ^^^^
            //                       digits 0-9       upper char A-Z     a-z

        }

        // ***************************************************************************************************
        // https://stackoverflow.com/a/63864709/13817556 by Paul
        private static readonly ushort[] HexValues =
        {
            0x0000, 0x0001, 0x0002, 0x0003, 0x0004, 0x0005, 0x0006, 0x0007, 0x0008, 0x0009, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100,
            0x000A, 0x000B, 0x000C, 0x000D, 0x000E, 0x000F, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100,
            0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x0100, 0x000A, 0x000B,
            0x000C, 0x000D, 0x000E, 0x000F
        };

        private static readonly byte[] Empty = new byte[0];

        public static byte[] HexStringToByteArrayV21(string hexadecimalString) {
            if (!TryParse(hexadecimalString, out var value)) {
                throw new ArgumentException("Invalid hexadecimal string", nameof(hexadecimalString));
            }

            return value;
        }

        public static bool TryParse(string hexadecimalString, out byte[] value) {
            if (hexadecimalString.Length == 0) {
                value = Empty;
                return true;
            }

            if (hexadecimalString.Length % 2 != 0) {
                value = Empty;
                return false;
            }

            try {

                value = new byte[hexadecimalString.Length / 2];
                for (int i = 0, j = 0; j < hexadecimalString.Length; i++) {
                    value[i] = (byte)((HexValues[hexadecimalString[j++] - '0'] << 4)
                                      | HexValues[hexadecimalString[j++] - '0']);
                }

                return true;
            } catch (OverflowException) {
                value = Empty;
                return false;
            }
        }

        // ***************************************************************************************************
        //
        // ***************************************************************************************************
        //

    }
}
