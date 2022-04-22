﻿using System.Linq;
using System.Collections.Generic;

using Plus.HabboHotel.GameClients;
using Plus.HabboHotel.Achievements;

namespace Plus.Communication.Packets.Outgoing.GameCenter
{
    class GameAchievementListComposer : ServerPacket
    {
        public GameAchievementListComposer(GameClient session, ICollection<Achievement> achievements, int gameId)
            : base(ServerPacketHeader.GameAchievementListMessageComposer)
        {
            WriteInteger(gameId);
            WriteInteger(achievements.Count);
            foreach (Achievement ach in achievements.ToList())
            {
                UserAchievement userData = session.GetHabbo().GetAchievementData(ach.GroupName);
                int targetLevel = (userData != null ? userData.Level + 1 : 1);
           
                AchievementLevel targetLevelData = ach.Levels[targetLevel];

                WriteInteger(ach.Id); // ach id
                WriteInteger(targetLevel); // target level
               WriteString(ach.GroupName + targetLevel); // badge
                WriteInteger(targetLevelData.Requirement); // requirement
                WriteInteger(targetLevelData.Requirement); // requirement
                WriteInteger(targetLevelData.RewardPixels); // pixels
                WriteInteger(0); // ach score
                WriteInteger(userData != null ? userData.Progress : 0); // Current progress
                WriteBoolean(userData != null ? (userData.Level >= ach.Levels.Count) : false); // Set 100% completed(??)
               WriteString(ach.Category);
               WriteString("basejump");
                WriteInteger(0); // total levels
                WriteInteger(0);
            }
           WriteString("");
        }
    }
}
