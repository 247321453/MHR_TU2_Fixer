using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace MHR_TU2_Fixer
{
    public static class PrefabFixer
    {
        [STAThreadAttribute]
        public static void GeneratePrefabs(DirectoryInfo baseFolder, DirectoryInfo conversionFolder)
        {
            var prefabs = Directory.GetFiles(conversionFolder.FullName, "*.pfb.17", SearchOption.AllDirectories);

            //TU1 Conversion
            var oldPrefabHex = "66 e1 a6 8f 06 6d d5 ed d1 07 28 e8 bb dd 1d 11";
            var oldPrefabBytes = HexStringToByte(oldPrefabHex);

            var newPrefabHex = "66 e1 a6 8f 46 5f 73 52 d1 07 28 e8 bb dd 1d 11";
            var newPrefabBytes = HexStringToByte(newPrefabHex);

            ConvertPrefabs(prefabs, oldPrefabBytes, newPrefabBytes);

            /*
              sRules.Add(new Tree<string, byte[], byte[]>("B_Bow", new byte[] { 0x6E, 0x86, 0x8F, 0x9D, 0xFD, 0xD8, 0xB3, 0x42 }, new byte[] { 0x6E, 0x86, 0x8F, 0x9D, 0x94, 0x67, 0x61, 0xCE }));
            sRules.Add(new Tree<string, byte[], byte[]>("B_Ydt", new byte[] { 0x09, 0x19, 0x1E, 0x71, 0xE2, 0x38, 0x6D, 0xA7 }, new byte[] { 0x09, 0x19, 0x1E, 0x71, 0x44, 0x72, 0x7A, 0xEF }));
            sRules.Add(new Tree<string, byte[], byte[]>("CA_sld", new byte[] { 0x0F, 0x33, 0xA4, 0xF6, 0xCE, 0x9B, 0xD2, 0x51 }, new byte[] { 0x0F, 0x33, 0xA4, 0xF6, 0xAF, 0xA3, 0x68, 0x89 }));
            sRules.Add(new Tree<string, byte[], byte[]>("CA_Swd", new byte[] { 0x08, 0x9C, 0x21, 0x4B, 0x5C, 0xA3, 0xAF, 0x3A }, new byte[] { 0x08, 0x9C, 0x21, 0x4B, 0x41, 0x79, 0x2C, 0xD6 }));
            sRules.Add(new Tree<string, byte[], byte[]>("DB_L", new byte[] { 0xC8, 0x48, 0x61, 0xC1, 0x2D, 0x68, 0x39, 0xE3 }, new byte[] { 0xC8, 0x48, 0x61, 0xC1, 0x11, 0xC1, 0x6F, 0xB8 }));
            sRules.Add(new Tree<string, byte[], byte[]>("DB_R", new byte[] { 0xFC, 0xC3, 0x52, 0x4C, 0x10, 0xC0, 0xE7, 0xDC }, new byte[] { 0xFC, 0xC3, 0x52, 0x4C, 0x94, 0xD4, 0xC1, 0x21 }));
            sRules.Add(new Tree<string, byte[], byte[]>("G_Swd", new byte[] { 0xED, 0xE8, 0x4E, 0x0E, 0xA7, 0x76, 0xAB, 0xE0 }, new byte[] { 0xED, 0xE8, 0x4E, 0x0E, 0xCF, 0xFD, 0x7E, 0x09 }));
            sRules.Add(new Tree<string, byte[], byte[]>("GL_Lan", new byte[] { 0x81, 0xC4, 0x56, 0x20, 0x36, 0x40, 0x12, 0x22 }, new byte[] { 0x81, 0xC4, 0x56, 0x20, 0x58, 0xDD, 0xAF, 0x30 }));
            sRules.Add(new Tree<string, byte[], byte[]>("GL_Sld", new byte[] { 0x30, 0x8D, 0x5F, 0x99, 0xB3, 0xCD, 0xA5, 0xB1 }, new byte[] { 0x30, 0x8D, 0x5F, 0x99, 0x33, 0xFA, 0x76, 0x49 }));
            sRules.Add(new Tree<string, byte[], byte[]>("Hammer", new byte[] { 0xBB, 0x19, 0xAE, 0xA0, 0xC9, 0xA8, 0x75, 0x96 }, new byte[] { 0xBB, 0x19, 0xAE, 0xA0, 0x14, 0x09, 0x04, 0x21 }));
            sRules.Add(new Tree<string, byte[], byte[]>("H_Bg", new byte[] { 0x08, 0x1E, 0x6F, 0x92, 0x7F, 0xB8, 0xE2, 0xC1 }, new byte[] { 0x08, 0x1E, 0x6F, 0x92, 0x00, 0xFD, 0x4F, 0x1C }));
            sRules.Add(new Tree<string, byte[], byte[]>("Hrn", new byte[] { 0x6D, 0xFC, 0x61, 0x11, 0xAF, 0x53, 0xF3, 0xA2 }, new byte[] { 0x6D, 0xFC, 0x61, 0x11, 0x34, 0xA6, 0xDE, 0x83 }));
            sRules.Add(new Tree<string, byte[], byte[]>("IG_Ins", new byte[] { 0xFF, 0x2F, 0xC4, 0x2A, 0xAC, 0x29, 0x47, 0x32 }, new byte[] { 0xFF, 0x2F, 0xC4, 0x2A, 0x5F, 0x4D, 0x31, 0x16 }));
            sRules.Add(new Tree<string, byte[], byte[]>("I_Gla", new byte[] { 0x71, 0x50, 0x5A, 0x40, 0x35, 0x6B, 0xC3, 0xA0 }, new byte[] { 0x71, 0x50, 0x5A, 0x40, 0x2F, 0xC9, 0xE8, 0xCF }));
            sRules.Add(new Tree<string, byte[], byte[]>("L_Lan", new byte[] { 0x4B, 0x02, 0x16, 0x8E, 0xBD, 0x5D, 0xC7, 0xBC }, new byte[] { 0x4B, 0x02, 0x16, 0x8E, 0xC7, 0xA4, 0xA5, 0xC1 }));
            sRules.Add(new Tree<string, byte[], byte[]>("L_Sld", new byte[] { 0xD1, 0xB2, 0x54, 0xCC, 0xEE, 0xDC, 0x39, 0x1A }, new byte[] { 0xD1, 0xB2, 0x54, 0xCC, 0x29, 0xD5, 0xC8, 0xCE }));
            sRules.Add(new Tree<string, byte[], byte[]>("L_Bg", new byte[] { 0xFE, 0x0A, 0xAC, 0x5C, 0x99, 0xE2, 0xBA, 0x58 }, new byte[] { 0xFE, 0x0A, 0xAC, 0x5C, 0x5E, 0xC0, 0x45, 0xBD }));
            sRules.Add(new Tree<string, byte[], byte[]>("LS_Saya", new byte[] { 0x3F, 0xED, 0xEF, 0xCC, 0x62, 0x8B, 0x2E, 0x40 }, new byte[] { 0x3F, 0xED, 0xEF, 0xCC, 0x6D, 0x42, 0xDC, 0x31 }));
            sRules.Add(new Tree<string, byte[], byte[]>("LS_Swd", new byte[] { 0xF5, 0xD8, 0x0E, 0x31, 0x25, 0x38, 0xB9, 0x5E }, new byte[] { 0xF5, 0xD8, 0x0E, 0x31, 0x06, 0x72, 0x21, 0xB6 }));
            sRules.Add(new Tree<string, byte[], byte[]>("SS_Sld", new byte[] { 0x2F, 0x95, 0xBE, 0xF2, 0x3E, 0xB4, 0x79, 0x1E }, new byte[] { 0x2F, 0x95, 0xBE, 0xF2, 0x47, 0x5E, 0x3B, 0x18 }));
            sRules.Add(new Tree<string, byte[], byte[]>("SS_Swd", new byte[] { 0x40, 0x00, 0x7F, 0x8B, 0xB7, 0x77, 0xF9, 0x14 }, new byte[] { 0x40, 0x00, 0x7F, 0x8B, 0x96, 0x8A, 0x16, 0x6B }));
   
             */
            //TU2 Conversion
            oldPrefabHex = "46 5F 73 52 D1 07 28 E8 BB DD 1D 11";
            oldPrefabBytes = HexStringToByte(oldPrefabHex);

            newPrefabHex = "7F D7 47 7F D1 07 28 E8 68 20 A6 CB";
            newPrefabBytes = HexStringToByte(newPrefabHex);

            ConvertPrefabs(prefabs, oldPrefabBytes, newPrefabBytes);
        }

        private static  string ReplaceHexFromString(string hex, string source, string replacement)
        {
            var hexSource = AddBlankHex(AsciiToHexString(source));
            var hexReplacement = AddBlankHex(AsciiToHexString(replacement));
            
            return hex.Replace(hexSource, hexReplacement);
        }

        private static void ConvertPrefabs(string[] prefabs, byte[] oldPrefabBytes, byte[] newPrefabBytes)
        {
            string exPath = Path.Combine(Program.CurrentDirectory, "Prefab");
            foreach (var prefab in prefabs)
            {
                if (prefab.StartsWith(exPath))
                {
                    continue;
                }
                Console.WriteLine("fix pfb:" + prefab);
                if (prefab.Contains("helm"))
                {
                    //Need to recreate the helm file, so let's use a premade one and replace the file names inside with the armor id!
                    var filename = Path.GetFileName(prefab);
                    var fileBytes = File.ReadAllBytes(prefab);
                    var prefabBytes = File.ReadAllBytes($@"{Program.CurrentDirectory}\Prefab\example\TU2\f_helm001.pfb.17");

                    var armorId = int.Parse(new string(filename.Replace(".pfb.17", "").Where(z => char.IsDigit(z)).ToArray())).ToString("000");
                    var isMale = filename.Contains("m_helm");

                    var fileHex = ByteToHexString(fileBytes);
                    var prefabHex = ByteToHexString(prefabBytes);

                    var newHex = prefabHex + string.Empty;

                    newHex = ReplaceHexFromString(newHex, "f_helm001", (isMale ? "m" : "f") + "_helm" + armorId);
                    newHex = ReplaceHexFromString(newHex, "pl001", "pl" + armorId);

                    if (isMale)
                    {
                        newHex = ReplaceHexFromString(newHex, "mod/f/", "mod/m/");
                    }
                    
                    var bytes = HexStringToByte(newHex);
                    File.WriteAllBytes(prefab, bytes);
                }
                else
                {
                    var prefabBytes = File.ReadAllBytes(prefab);

                    if (ContainsBytes(prefabBytes, oldPrefabBytes))
                    {
                        //Attempt conversion, and copy over
                        Console.WriteLine($"{prefab} contains old prefab bytes, will attempt to convert");

                        var newPrefab = ReplaceBytes(prefabBytes, oldPrefabBytes, newPrefabBytes);

                        File.WriteAllBytes(prefab, newPrefab);

                        //Add check to make sure that the bytes got written properly and are the correct length
                        var tmpNewFileBytes = File.ReadAllBytes(prefab);

                        if (newPrefab.Length != tmpNewFileBytes.Length || !BytesSimilar(newPrefab, tmpNewFileBytes))
                        {
                            throw new Exception($"{prefab} has encountered an issue where the written bytes do not match the actual bytes, this may be caused due to the folder being on a different drive or folder permissions. Please try moving the prefab fixer to the same drive as the folder, or run this program as administrator.");
                        }
                    }
                    else if (ContainsBytes(prefabBytes, newPrefabBytes))
                    {
                        //Don't convert, just output message
                        Console.WriteLine($"{prefab} contains new prefab bytes, no need to convert.");
                    }
                    else
                    {
                        //Throw exception and warn of issue
                        //throw new Exception($"{prefab} does not contain any sequence for new and old prefab bytes, this has been thrown to avoid converting it.");
                    }
                }

              
            }
        }

        private static bool BytesSimilar(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1.Length != bytes2.Length)
            {
                return false;
            }

            for (var i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] != bytes2[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static string AddBlankHex(string hexString)
        {
            var list = Enumerable
                .Range(0, hexString.Length / 2)
                .Select(i => hexString.Substring(i * 2, 2));

            return string.Join("00", list);
        }

        private static string AsciiToHexString(string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.ASCII.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        private static byte[] ReplaceBytes(byte[] bytes, byte[] search, byte[] replace)
        {
            var byteString = ByteToHexString(bytes);
            var searchString = ByteToHexString(search);
            var replaceString = ByteToHexString(replace);

            var newString = byteString.Replace(searchString, replaceString);

            return HexStringToByte(newString);
        }

        private static bool ContainsBytes(byte[] haystack, byte[] needle)
        {
            return SearchBytes(haystack, needle) >= 0;
        }

        private static int SearchBytes(byte[] haystack, byte[] needle)
        {
            var len = needle.Length;
            var limit = haystack.Length - len;
            for (var i = 0; i <= limit; i++)
            {
                var k = 0;
                for (; k < len; k++)
                {
                    if (needle[k] != haystack[i + k]) break;
                }
                if (k == len) return i;
            }
            return -1;
        }

        private static string ByteToHexString(byte[] bytes)
        {
            var hexString = new StringBuilder();

            foreach (int bytePart in bytes)
            {
                hexString.Append(string.Format("{0:X2}", bytePart));
            }

            return hexString.ToString();
        }

        private static byte[] HexStringToByte(string hexString)
        {
            if (hexString.Contains(" "))
            {
                hexString = hexString.Replace(" ", "");
            }

            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            var data = new byte[hexString.Length / 2];
            for (var index = 0; index < data.Length; index++)
            {
                var byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }
    }
}