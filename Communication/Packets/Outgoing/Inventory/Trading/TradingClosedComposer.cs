﻿namespace Plus.Communication.Packets.Outgoing.Inventory.Trading
{
    class TradingClosedComposer : ServerPacket
    {
        public TradingClosedComposer(int userId)
            : base(ServerPacketHeader.TradingClosedMessageComposer)
        {
            WriteInteger(userId);
            WriteInteger(0);
        }
    }
}
