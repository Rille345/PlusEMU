﻿namespace Plus.HabboHotel.Rooms.Chat.Commands.User
{
    class EmptyItems : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "command_empty_items"; }
        }

        public string Parameters
        {
            get { return "%yes%"; }
        }

        public string Description
        {
            get { return "Is your inventory full? You can remove all items by typing this command."; }
        }

        public void Execute(GameClients.GameClient session, Room room, string[] @params)
        {
            if (@params.Length == 1)
            {
                session.SendNotification("Are you sure you want to clear your inventory? You will lose all the furniture!\n" +
                 "To confirm, type \":emptyitems yes\". \n\nOnce you do this, there is no going back!\n(If you do not want to empty it, just ignore this message!)\n\n" +
                 "PLEASE NOTE! If you have more than 3000 items, the hidden items will also be DELETED.");
                return;
            }
            else
            {
                if (@params.Length == 2 && @params[1].ToString() == "yes")
                {
                    session.GetHabbo().GetInventoryComponent().ClearItems();
                    session.SendNotification("Your inventory has been cleared!");   
                    return;
                }
                else if (@params.Length == 2 && @params[1].ToString() != "yes")
                {
                    session.SendNotification("To confirm, you must type in :emptyitems yes");
                    return;
                }
            }
        }
    }
}
