﻿using System;
using System.Linq;
using System.Collections.Concurrent;

using Plus.Communication.Packets.Incoming;
using Plus.HabboHotel.Rooms;
using Plus.HabboHotel.Users;
using Plus.HabboHotel.Users.Badges;

namespace Plus.HabboHotel.Items.Wired.Boxes.Conditions
{
    class IsNotWearingBadgeBox : IWiredItem
    {
        public Room Instance { get; set; }
        public Item Item { get; set; }
        public WiredBoxType Type { get { return WiredBoxType.ConditionIsWearingBadge; } }
        public ConcurrentDictionary<int, Item> SetItems { get; set; }
        public string StringData { get; set; }
        public bool BoolData { get; set; }
        public string ItemsData { get; set; }

        public IsNotWearingBadgeBox(Room instance, Item item)
        {
            this.Instance = instance;
            this.Item = item;
            SetItems = new ConcurrentDictionary<int, Item>();
        }

        public void HandleSave(ClientPacket packet)
        {
            int unknown = packet.PopInt();
            string badgeCode = packet.PopString();

            StringData = badgeCode;
        }

        public bool Execute(params object[] @params)
        {
            if (@params.Length == 0)
                return false;

            if (String.IsNullOrEmpty(StringData))
                return false;

            Habbo player = (Habbo)@params[0];
            if (player == null)
                return false;

            if (!player.GetBadgeComponent().GetBadges().Contains(player.GetBadgeComponent().GetBadge(StringData)))
                return true;

            foreach (Badge badge in player.GetBadgeComponent().GetBadges().ToList())
            {
                if (badge.Slot <= 0)
                    continue;

                if (badge.Code == StringData)
                    return false;
            }

            return true;
        }
    }
}