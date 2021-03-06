﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Project
{
    class Sword : Weapon
    {
        //Default constructor
        public Sword() { }

        public Sword(string weapName)
        {
            this.Name = weapName;
            this.cost = 10;
        }

        /***Methods***/
        //Establish bonus stats for the weapon
        public override int CreateWeapForShop(string material)
        {
            base.CreateWeapForShop(material);

            //Update bonus points for swords (balanced)
            AtkBonus += 5;
            SpdBonus += 5;
            DefBonus += 5;

            return cost;
        }
    }
}
