﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Network.Serialization;

namespace LeagueSharp.Network.Packets
{
    class PKT_RemoveItemReq : Packet, ISerialized
    {
        public static short PacketId { get { return 0x72; } }
        private SerializedData<Byte> _slot = new SerializedData<byte>(0, 3, new List<uint>()
        {
            0x6501D62E,
            2,
            1,
            0x87CFCD92,
            0xFE0A65A2,
            0,
            unchecked ((uint)-1),
            0x21BD274B
        });

        private SerializedData<Boolean> _grantGold = new SerializedData<bool>(3, 1);

        public Byte Slot
        {
            get { return _slot.Data; }
            set { _slot.Data = value; }
        }

        public bool GrantGold
        {
            get { return _grantGold.Data; }
            set { _grantGold.Data = value; }
        }

        public bool Decode(byte[] data)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(data));

            reader.BaseStream.Position += 2;
            this.NetworkId = reader.ReadInt32();

            UInt16 bitmask = (UInt16) reader.ReadByte();

            _slot.Decode(bitmask, reader);
            _grantGold.Decode(bitmask, reader);

            return true;
        }

        public byte[] Encode()
        {
            var ms = new MemoryStream();
            var writer = new BinaryWriter(ms);

            ushort bitmask = 0;

            _slot.Encode(ref bitmask, writer);
            _grantGold.Encode(ref bitmask, writer);

            var packet = new byte[ms.Length + 8];
            BitConverter.GetBytes(PacketId).CopyTo(packet, 0);
            BitConverter.GetBytes(NetworkId).CopyTo(packet, 2);
            BitConverter.GetBytes((byte)bitmask).CopyTo(packet, 6);
            Array.Copy(ms.GetBuffer(), 0, packet, 7, ms.Length);

            return packet;
        }
    }
}
