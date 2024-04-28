using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Models
{
    // 아이템 정보
    public class Item
    {
        #region Field
        private string _name;
        private float _attack;
        private float _defence;
        private string _info;
        private int _price;
        private ItemType _gearType;
        #endregion

        [JsonConstructor]
        public Item(string name, float atk, float def, string info, int price, ItemType itemType)
        {
            Name = name;
            ATK = atk;
            DEF = def;
            Info = info;
            Price = price;
            GearType = itemType;
        }
                
        public Item(Item item)
        {
            this.Name = item.Name;
            this.ATK = item.ATK;
            this.DEF = item.DEF;
            this.Info = item.Info;
            this.Price = item.Price;
            this.GearType = item.GearType;
        }

        #region Properties        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
                
        public float ATK
        {
            get { return _attack; }
            set { _attack = value; }
        }

        public float DEF
        {
            get { return _defence; }
            set { _defence = value; }
        }

        public string Info
        {
            get { return _info; }
            set { _info = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        [JsonProperty]
        public ItemType GearType
        {
            get { return _gearType; }
            private set { _gearType = value; }
        }

        #endregion
    }
}
