﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plus.HabboHotel.Groups
{
    public class GroupMember
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Look { get; set; }

        public GroupMember(int id, string username, string look)
        {
            this.Id = id;
            this.Username = username;
            this.Look = look;
        }
    }
}
