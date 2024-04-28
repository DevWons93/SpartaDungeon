using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpartaDungeon.Models;

namespace SpartaDungeon
{
    public class Dungeon
    {
        #region Field
        private string _name;
        private int _level;
        private float _defSpec;
        private int _reward;
        private int _exp;
        #endregion

        public Dungeon(string name, int level, float def, int reward, int exp)
        {
            this.Name = name;
            this.Level = level;
            this.DEFSpec = def;
            this.Reward = reward;
            this.Exp = exp;
        }

        #region Properties
        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public int Level
        {
            get { return _level; }
            private set { _level = value; }
        }

        public float DEFSpec
        {
            get { return _defSpec; }
            private set { _defSpec = value; }
        }

        public int Reward
        {
            get { return _reward; }
            private set { _reward = value; }
        }           

        public int Exp
        {
            get { return _exp; }
            private set { _exp = value; }
        }
        #endregion

        public int GetDamage(Player player)
        {
            Random random = new Random();
            int damage = random.Next(20, 36);
            float reduceByDEF = this.DEFSpec - player.DEF;
            int result = damage + (int)reduceByDEF;
            return result;
        }

        public int GetClearReward(Player player)
        {
            Random random = new Random();
            int bonusPointByATK = random.Next((int)player.ATK * 10, (int)(2 * player.ATK * 10) + 1);
            int result = (int)(this.Reward * (1 + 0.001f * bonusPointByATK));

            return result;
        }
    }
}
