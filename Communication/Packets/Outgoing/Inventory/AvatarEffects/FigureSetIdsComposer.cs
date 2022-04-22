﻿using System.Linq;
using System.Collections.Generic;
using Plus.HabboHotel.Users.Clothing.Parts;

namespace Plus.Communication.Packets.Outgoing.Inventory.AvatarEffects
{
    class FigureSetIdsComposer : ServerPacket
    {
        public FigureSetIdsComposer(ICollection<ClothingParts> clothingParts)
            : base(ServerPacketHeader.FigureSetIdsMessageComposer)
        {
            WriteInteger(clothingParts.Count);
            foreach (ClothingParts part in clothingParts.ToList())
            {
                WriteInteger(part.PartId);
            }

            WriteInteger(clothingParts.Count);
            foreach (ClothingParts part in clothingParts.ToList())
            {
               WriteString(part.Part);
            }
        }
    }
}
