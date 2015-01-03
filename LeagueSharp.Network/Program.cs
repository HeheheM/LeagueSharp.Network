using System;
using LeagueSharp.Network.Packets;

namespace LeagueSharp.Network
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var upgradeSpellReq = new PKT_NPC_UpgradeSpellReq();

            upgradeSpellReq.PacketId = 0xEC;
            upgradeSpellReq.NetworkId = 0x4000000F;
            upgradeSpellReq.CheatModuleHash = 0x8888888;
            upgradeSpellReq.CheatModuleInfo1 = 0;
            upgradeSpellReq.CheatModuleInfo2 = 0;
            upgradeSpellReq.SpellSlot = 0;
            upgradeSpellReq.Evolve = 0xFE;

            byte[] data = upgradeSpellReq.Encode();

            Console.WriteLine(BitConverter.ToString(upgradeSpellReq.Encode()));

            var derp = new PKT_NPC_UpgradeSpellReq();
            derp.Decode(data);

            Console.WriteLine("{0:X}", derp.CheatModuleHash);
        }
    }
}