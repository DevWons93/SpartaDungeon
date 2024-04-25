using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    // 플레이어가 장착한 장비의 클래스
    public class Gear : Item
    {
        private bool _isEquip;

        public Gear(string name, float atk, float def, string info, int price,ItemType itemType) : base(name, atk, def, info, price,itemType)
        {
            this.IsEquip = false;
        }

        public bool IsEquip
        {
            get { return _isEquip; }
            set { _isEquip = value; }
        }
    }
}
