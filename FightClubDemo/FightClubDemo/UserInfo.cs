using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClubDemo
{
    struct UserInfo
    {
        public string Name;
        public int HP;

        public UserInfo(String initName, int initHP)
        {
            Name = initName;
            HP = initHP;
        }
    }
}
