using System;
using System.Web.Script.Serialization;
using LeagueSharp.Network.Packets;
using SharpDX;

namespace LeagueSharp.Network
{
    internal class Program
    {
        static JavaScriptSerializer js = new JavaScriptSerializer();

        private static void Main(string[] args)
        {
            Game.OnGameSendPacket += delegate(GamePacketEventArgs eventArgs)
            {
                var packet = Packet.CreatePacket(BitConverter.ToInt16(eventArgs.PacketData, 0));

                if (packet != null)
                {
                    packet.Decode(eventArgs.PacketData);
                    Console.WriteLine("C->S {0}: {1}", packet.GetType().Name, js.Serialize(packet));
                    Console.WriteLine("{0}", BitConverter.ToString(eventArgs.PacketData));
                }
            };

            Game.OnGameProcessPacket += delegate(GamePacketEventArgs eventArgs)
            {
                var packet = Packet.CreatePacket(BitConverter.ToInt16(eventArgs.PacketData, 0));

                if (packet != null)
                {
                    packet.Decode(eventArgs.PacketData);
                    Console.WriteLine("S->C {0}: {1}", packet.GetType().Name, js.Serialize(packet));
                    Console.WriteLine("{0}", BitConverter.ToString(eventArgs.PacketData));
                }        
            };
        }
    }
}