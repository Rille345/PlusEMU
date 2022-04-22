﻿namespace Plus.HabboHotel.Rooms.Chat.Commands.Moderator.Fun
{
    class ForceSitCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "command_forcesit"; }
        }

        public string Parameters
        {
            get { return "%username%"; }
        }

        public string Description
        {
            get { return "Force another to user sit."; }
        }

        public void Execute(GameClients.GameClient session, Room room, string[] @params)
        {
            if (@params.Length == 1)
            {
                session.SendWhisper("Oops, you forgot to choose a target user!");
                return;
            }

            RoomUser user = room.GetRoomUserManager().GetRoomUserByHabbo(@params[1]);
            if (user == null)
                return;

            if (user.Statusses.ContainsKey("lie") || user.IsLying || user.RidingHorse || user.IsWalking)
                return;

            if (!user.Statusses.ContainsKey("sit"))
            {
                if ((user.RotBody % 2) == 0)
                {
                    if (user == null)
                        return;

                    try
                    {
                        user.Statusses.Add("sit", "1.0");
                        user.Z -= 0.35;
                        user.IsSitting = true;
                        user.UpdateNeeded = true;
                    }
                    catch { }
                }
                else
                {
                    user.RotBody--;
                    user.Statusses.Add("sit", "1.0");
                    user.Z -= 0.35;
                    user.IsSitting = true;
                    user.UpdateNeeded = true;
                }
            }
            else if (user.IsSitting == true)
            {
                user.Z += 0.35;
                user.Statusses.Remove("sit");
                user.Statusses.Remove("1.0");
                user.IsSitting = false;
                user.UpdateNeeded = true;
            }
        }
    }
}
