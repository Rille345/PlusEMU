﻿namespace Plus.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class TeleportCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "command_teleport"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "The ability to teleport anywhere within the room."; }
        }

        public void Execute(GameClients.GameClient session, Room room, string[] @params)
        {
            RoomUser user = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            if (user == null)
                return;

            user.TeleportEnabled = !user.TeleportEnabled;
            room.GetGameMap().GenerateMaps();
        }
    }
}
