using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
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

        public Item(string name, float atk, float def, string info, ItemType itemType)
        {
            this.Name = name;
            this.ATK = atk;
            this.DEF = def;
            this.Info = info;
            this.GearType = itemType;
        }
        public Item(string name, float atk, float def, string info, int price, ItemType itemType)
        {
            this.Name = name;
            this.ATK = atk;
            this.DEF = def;
            this.Info = info;
            this.Price = price;
            this.GearType = itemType;
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

        public ItemType GearType
        {
            get { return _gearType; }
            set { _gearType = value; }
        }

        #endregion
    }
}
