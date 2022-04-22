﻿using NLog;
using Plus.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace Plus.HabboHotel.Subscriptions
{
    public class SubscriptionManager
    {
        private static ILogger _log = LogManager.GetLogger("Plus.HabboHotel.Subscriptions.SubscriptionManager");

        private readonly Dictionary<int, SubscriptionData> _subscriptions = new Dictionary<int, SubscriptionData>();

        public SubscriptionManager()
        {
        }

        public void Init()
        {
            if (_subscriptions.Count > 0)
                _subscriptions.Clear();

            using (IQueryAdapter dbClient = PlusEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT * FROM `subscriptions`;");
                DataTable getSubscriptions = dbClient.GetTable();

                if (getSubscriptions != null)
                {
                    foreach (DataRow row in getSubscriptions.Rows)
                    {
                        if (!_subscriptions.ContainsKey(Convert.ToInt32(row["id"])))
                            _subscriptions.Add(Convert.ToInt32(row["id"]), new SubscriptionData(Convert.ToInt32(row["id"]), Convert.ToString(row["name"]), Convert.ToString(row["badge_code"]), Convert.ToInt32(row["credits"]), Convert.ToInt32(row["duckets"]), Convert.ToInt32(row["respects"])));
                    }
                }
            }

            _log.Info("Loaded " + _subscriptions.Count + " subscriptions.");
        }

        public bool TryGetSubscriptionData(int id, out SubscriptionData data)
        {
            return _subscriptions.TryGetValue(id, out data);
        }
    }
}
