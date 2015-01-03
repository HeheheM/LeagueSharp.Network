using System;
using LeagueSharp.Network.Packets;
using SharpDX;

namespace LeagueSharp.Network
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var chargedSpell = new PKT_ChargedSpell();

            chargedSpell.PacketId = 0x103;
            chargedSpell.NetworkId = 0x4000000F;
            chargedSpell.SpellSlot = 3;
            chargedSpell.TargetPosition = new Vector3(10.0f, 20.0f, 30.0f);
            chargedSpell.Unknown1 = true;
            chargedSpell.Unknown2 = true;

            var pktData = chargedSpell.Encode();
            Console.WriteLine(BitConverter.ToString(pktData));

            var derpSpell = new PKT_ChargedSpell();
            derpSpell.Decode(pktData);

            Console.WriteLine("derka {0}", derpSpell.TargetPosition.X);
        }
    }
}