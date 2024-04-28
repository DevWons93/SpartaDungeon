using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Models
{
    // 플레이어가 장착한 장비의 클래스
    public class Gear : Item
    {
        private bool _isEquip;

        [JsonConstructor]
        public Gear(string name, float atk, float def, string info, int price, ItemType itemType) : base(name, atk, def, info, price, itemType)
        {
            IsEquip = false;
        }

        public Gear(Item item) : base(item)
        {
            IsEquip = false;
        }
        
        public bool IsEquip
        {
            get { return _isEquip; }
            set { _isEquip = value; }
        }
    }
}
