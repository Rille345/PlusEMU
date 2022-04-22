﻿using System;
using System.Collections.Concurrent;
using Plus.HabboHotel.Rooms;
using Plus.Communication.Packets.Incoming;

namespace Plus.HabboHotel.Items.Wired.Boxes.Effects
{
    internal class RegenerateMapsBox : IWiredItem
    {
        public Room Instance { get; set; }
        public Item Item { get; set; }

        public WiredBoxType Type
        {
            get { return WiredBoxType.EffectRegenerateMaps; }
        }

        public ConcurrentDictionary<int, Item> SetItems { get; set; }
        public string StringData { get; set; }
        public bool BoolData { get; set; }
        public string ItemsData { get; set; }

        public RegenerateMapsBox(Room instance, Item item)
        {
            this.Instance = instance;
            this.Item = item;
            StringData = "";
            SetItems = new ConcurrentDictionary<int, Item>();
        }

        public void HandleSave(ClientPacket packet)
        {
            int unknown = packet.PopInt();
            string unknown2 = packet.PopString();
        }

        public bool Execute(params object[] @params)
        {
            if (Instance == null)
                return false;

            TimeSpan timeSinceRegen = DateTime.Now - Instance.LastRegeneration;

            if (timeSinceRegen.TotalMinutes > 1)
            {
                Instance.GetGameMap().GenerateMaps();
                Instance.LastRegeneration = DateTime.Now;
                return true;
            }
            
            return false;
        }
    }
}
