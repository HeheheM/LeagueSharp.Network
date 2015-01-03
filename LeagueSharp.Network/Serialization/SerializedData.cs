using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LeagueSharp.Network.Cryptography;
using LeagueSharp.Network.Util;

namespace LeagueSharp.Network.Serialization
{
    public class SerializedData<T>
    {
        private static readonly Random random = new Random();
        private readonly int bitIndex;
        private readonly int bits;
        private readonly List<uint> dict;

        public SerializedData(int bitIndex, int bits, List<uint> dict)
        {
            this.bitIndex = bitIndex;
            this.bits = bits;
            this.dict = dict;
        }

        public T Data { get; set; }

        public T Decode(ushort bitmask, BinaryReader reader)
        {
            var result = default(T);

            var entry = (int) dict[bitmask.GetBits(bitIndex, bits)];

            if (typeof (T) == typeof (Int32)
                || typeof (T) == typeof (Int16)
                || typeof (T) == typeof (Byte))
            {
                if (entry < -1 || entry > 7)
                {
                    Serializer.Decode(out result, reader, Operations.GetOperations((uint) entry));
                }
                else
                {
                    result = (T) (dynamic) entry;
                }

                return (Data = result);
            }

            return result;
        }

        public bool Encode(ref ushort bitmask, BinaryWriter writer)
        {
            if (typeof (T) == typeof (Int32)
                || typeof (T) == typeof (Int16)
                || typeof (T) == typeof (Byte))
            {
                var _data = (Int32) (dynamic) Data;

                switch (_data)
                {
                    case -1:
                    case 0:
                    case 1:
                    case 2:
                        bitmask = bitmask.SetRange(bitIndex, bits, (ushort) dict.IndexOf((uint) _data));
                        return true;
                    default:
                        break;
                }

                var cryptOperationHashes = dict.Where(x => x > 7 && x != unchecked ((uint) -1)).ToList();
                var cryptOperation = cryptOperationHashes[random.Next()%cryptOperationHashes.Count];
                bitmask = bitmask.SetRange(bitIndex, bits, (ushort) dict.IndexOf(cryptOperation));
                return Serializer.Encode(Data, writer, Operations.GetOperations(cryptOperation));
            }

            return false;
        }
    }
}