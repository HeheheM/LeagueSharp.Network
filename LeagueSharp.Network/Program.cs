using System;
using LeagueSharp.Network.Packets;
using SharpDX;

namespace LeagueSharp.Network
{
    internal class Program
    {
        private static void Main(string[] args)
        {/*
            Game.OnGameSendPacket += delegate(GamePacketEventArgs eventArgs)
            {
                if (eventArgs.PacketData[0] == 0x03 && eventArgs.PacketData[1] == 0x01)
                {
                    Console.WriteLine("Len {0}", eventArgs.PacketData.Length);
                    foreach (var b in eventArgs.PacketData)
                    {
                        Console.Write("0x{0:X}, ", b);
                    }
                    Console.WriteLine();
                }
            };
            */
                        var pktData = new byte[]
                        {
                          0x03, 0x01, 0x05, 0x00, 0x00, 0x40, 0x05, 0xC2, 0x5A, 0xED, 0xCD, 0x42, 0xE3, 0x57, 0x51, 0x85, 0xAD, 0xE8, 0xD4
                         //   0x03, 0x01, 0x05, 0x00, 0x00, 0x40, 0x05, 0x45, 0xBD, 0x1A, 0x37, 0x42, 0xE1, 0x49, 0xF5, 0x85, 0x2D, 0x98, 0x94,
                        }; 
                           
                        var derpSpell = new PKT_ChargedSpell();
                        derpSpell.Decode(pktData);

                        Console.WriteLine("derka {0}", derpSpell.TargetPosition.ToString());
        }
    }
}