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
                if (eventArgs.PacketData[0] == PKT_InteractReq.PacketId)
                {
                    var pkt = new PKT_InteractReq();
                    pkt.Decode(eventArgs.PacketData);
                    Console.WriteLine("PKT_InteractReq: {0}", js.Serialize(pkt));
                }
                else if (eventArgs.PacketData[0] == PKT_RemoveItemReq.PacketId)
                {
                    var pkt = new PKT_RemoveItemReq();
                    pkt.Decode(eventArgs.PacketData);
                    Console.WriteLine("PKT_RemoveItemReq: {0}", js.Serialize(pkt));                    
                }
                else if (eventArgs.PacketData[0] == PKT_NPC_CastSpellReq.PacketId)
                {
                    var pkt = new PKT_NPC_CastSpellReq();
                    pkt.Decode(eventArgs.PacketData);
                    Console.WriteLine("PKT_NPC_CastSpellReq: {0}", js.Serialize(pkt));       
                    
                }
            };

            Game.OnGameProcessPacket += delegate(GamePacketEventArgs eventArgs)
            {
                if (eventArgs.PacketData[0] == PKT_RemoveItemAns.PacketId)
                {
                    var pkt = new PKT_RemoveItemAns();
                    pkt.Decode(eventArgs.PacketData);
                    Console.WriteLine("PKT_RemoveItemAns: {0}", js.Serialize(pkt));
                }
                
            };
        }
    }
}