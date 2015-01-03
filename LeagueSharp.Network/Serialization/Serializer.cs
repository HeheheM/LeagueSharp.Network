using System;
using System.IO;
using LeagueSharp.Network.Cryptography;

namespace LeagueSharp.Network.Serialization
{
    public static class Serializer
    {
        public static bool Encode<T>(T data, BinaryWriter writer, Operations encryptOperations)
        {
            if (typeof (T) == typeof (Int32)
                || typeof (T) == typeof (Int16))
            {
                int _data = (dynamic) data;

                while (_data > 0x7F)
                {
                    writer.Write(encryptOperations.Encrypt((byte) (_data | 0x80)));
                    _data >>= 7;
                }
                writer.Write(encryptOperations.Encrypt((byte) _data));
                return true;
            }
            if (typeof (T) == typeof (Byte))
            {
                byte _data = (dynamic) data;
                writer.Write(encryptOperations.Encrypt(_data));
                return true;
            }

            return false;
        }

        public static bool Decode<T>(out T result, BinaryReader reader, Operations encryptOperations)
        {
            result = default(T);

            if (typeof (T) == typeof (Int32)
                || typeof (T) == typeof (Int16))
            {
                var bitcounter = 0;
                var data = 0;
                do
                {
                    var encryptedByte = encryptOperations.Decrypt(reader.ReadByte());
                    data |= (encryptedByte & 0x7F) << bitcounter;
                    if ((sbyte) encryptedByte >= 0)
                    {
                        result = (dynamic) data;
                        return true;
                    }
                    bitcounter += 7;
                } while (reader.BaseStream.Position < reader.BaseStream.Length);
            }
            else if (typeof (T) == typeof (Byte))
            {
                result = (dynamic) encryptOperations.Decrypt(reader.ReadByte());
                return true;
            }

            return false;
        }
    }
}