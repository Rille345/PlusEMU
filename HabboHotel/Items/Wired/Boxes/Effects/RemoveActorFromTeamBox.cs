﻿using System.Collections.Concurrent;

using Plus.HabboHotel.Rooms;
using Plus.HabboHotel.Users;
using Plus.HabboHotel.Rooms.Games.Teams;
using Plus.Communication.Packets.Incoming;

namespace Plus.HabboHotel.Items.Wired.Boxes.Effects
{
    class RemoveActorFromTeamBox : IWiredItem
    {
        public Room Instance { get; set; }
        public Item Item { get; set; }
        public WiredBoxType Type { get { return WiredBoxType.EffectRemoveActorFromTeam; } }
        public ConcurrentDictionary<int, Item> SetItems { get; set; }
        public string StringData { get; set; }
        public bool BoolData { get; set; }
        public string ItemsData { get; set; }

        public RemoveActorFromTeamBox(Room instance, Item item)
        {
            this.Instance = instance;
            this.Item = item;

            SetItems = new ConcurrentDictionary<int, Item>();
        }

        public void HandleSave(ClientPacket packet)
        {
            int unknown = packet.PopInt();
        }

        public bool Execute(params object[] @params)
        {
            if (@params.Length == 0 || Instance == null)
                return false;

            Habbo player = (Habbo)@params[0];
            if (player == null)
                return false;

            RoomUser user = Instance.GetRoomUserManager().GetRoomUserByHabbo(player.Id);
            if (user == null)
                return false;

            if (user.Team != Team.None)
            {
                TeamManager team = Instance.GetTeamManagerForFreeze();
                if (team != null)
                {
                    team.OnUserLeave(user);

                    user.Team = Rooms.Games.Teams.Team.None;

                    if (user.GetClient().GetHabbo().Effects().CurrentEffect != 0)
                        user.GetClient().GetHabbo().Effects().ApplyEffect(0);
                }
            }
            return true;
        }
    }
}