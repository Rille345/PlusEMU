﻿using Plus.HabboHotel.GameClients;

namespace Plus.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class DisconnectCommand :IChatCommand
    {
        public string PermissionRequired
        {
            get { return "command_disconnect"; }
        }

        public string Parameters
        {
            get { return "%username%"; }
        }

        public string Description
        {
            get { return "Disconnects another user from the hotel."; }
        }

        public void Execute(GameClient session, Room room, string[] @params)
        {
            if (@params.Length == 1)
            {
                session.SendWhisper("Please enter the username of the user you wish to Disconnect.");
                return;
            }

            GameClient targetClient = PlusEnvironment.GetGame().GetClientManager().GetClientByUsername(@params[1]);
            if (targetClient == null)
            {
                session.SendWhisper("An error occoured whilst finding that user, maybe they're not online.");
                return;
            }

            if (targetClient.GetHabbo().GetPermissions().HasRight("mod_tool") && !session.GetHabbo().GetPermissions().HasRight("mod_disconnect_any"))
            {
                session.SendWhisper("You are not allowed to Disconnect that user.");
                return;
            }

            targetClient.GetConnection().Dispose();
        }
    }
}
