using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightClubDemo
{

    delegate void UserDelegate(UserInfo ui);

    class UserFighter
    {
        public String Name { private set; get; }
        public BodyPart BlockPart { set; get; }
        public int Health { private set; get; }

        public event UserDelegate Block;
        public event UserDelegate Wound;
        public event UserDelegate Death;

        public UserFighter(string initName)
        {
            Name = initName;
            Health = 100;
        }

        public void SetBlock(BodyPart part)
        {
            BlockPart = part;
        }

        public void GetHit(BodyPart part)
        {
            if(part != BlockPart)
            {
                Block(new UserInfo(Name, Health));
            }
            else
            {
                Health -= 25;
                Wound(new UserInfo(Name, Health));
                if(Health <= 0)
                {
                    Death(new UserInfo(Name, Health));
                }
            }
        }
    }
}
