using System;
using System.Collections.Generic;
using System.IO;
using LeagueSharp.Network.Serialization;
using SharpDX;

namespace LeagueSharp.Network.Packets
{
    class PKT_NPC_CastSpellReq : Packet, ISerialized
    {
        public static short PacketId { get { return 0xDE; } }

        private SerializedData<Vector2> _from = new SerializedData<Vector2>(6, 1, new List<uint>() { 0x842A6B66 });
        private SerializedData<Vector2> _to = new SerializedData<Vector2>(5, 1, new List<uint>() { 0xF14F0ADF });
        private SerializedData<Int32> _targetNetworkId = new SerializedData<Int32>(7, 1, new List<uint>() { 0x1BF4047 });
        private SerializedData<Byte> _spellSlot = new SerializedData<byte>(2, 3, new List<uint>()
        {
            0x41659787,
            0,
            1,
            0xF68409C9,
            0xCB51772A,
            0xCAA8873F,
            2,
            unchecked ((uint)-1)
        });
        private SerializedData<Boolean> _unknown1 = new SerializedData<bool>(0, 1);
        private SerializedData<Boolean> _unknown2 = new SerializedData<bool>(1, 1);

        public Vector2 From
        {
            get { return _from.Data; }
            set { _from.Data = value; }
        }

        public Vector2 To
        {
            get { return _to.Data; }
            set { _to.Data = value; }
        }

        public Int32 TargetNetworkId
        {
            get { return _targetNetworkId.Data; }
            set { _targetNetworkId.Data = value; }
        }

        public Byte SpellSlot
        {
            get { return _spellSlot.Data; }
            set { _spellSlot.Data = value; }
        }

        public bool Unknown1
        {
            get { return _unknown1.Data; }
            set { _unknown1.Data = value; }
        }

        public bool Unknown2
        {
            get { return _unknown2.Data; }
            set { _unknown2.Data = value; }
        }

        public bool Decode(byte[] data)
        {
            var reader = new BinaryReader(new MemoryStream(data));

            reader.ReadInt16(); // skip packet id
            NetworkId = reader.ReadInt32();

            var bitmask = reader.ReadUInt16();
            _from.Decode(bitmask, reader);
            _to.Decode(bitmask, reader);
            _targetNetworkId.Decode(bitmask, reader);
            _spellSlot.Decode(bitmask, reader);
            _unknown1.Decode(bitmask, reader);
            _unknown2.Decode(bitmask, reader);

            return true;
        }

        public byte[] Encode()
        {
            var ms = new MemoryStream();
            var writer = new BinaryWriter(ms);

            ushort bitmask = 0;

            _from.Encode(ref bitmask, writer);
            _to.Encode(ref bitmask, writer);
            _targetNetworkId.Encode(ref bitmask, writer);
            _spellSlot.Encode(ref bitmask, writer);
            _unknown1.Encode(ref bitmask, writer);
            _unknown2.Encode(ref bitmask, writer);

            var packet = new byte[ms.Length + 8];
            BitConverter.GetBytes(PacketId).CopyTo(packet, 0);
            BitConverter.GetBytes(NetworkId).CopyTo(packet, 2);
            BitConverter.GetBytes(bitmask).CopyTo(packet, 6);
            Array.Copy(ms.GetBuffer(), 0, packet, 8, ms.Length);

            return packet;
        }
    }
}
