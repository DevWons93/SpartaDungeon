using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon.Models
{
    // 플레이어 정보        
    public class Player
    {
        #region Field
        private string _name;
        private int _level;
        private int _exp;
        private Class _playerClass;
        private float _attack;
        private float _defence;
        private float _atkOfGears;
        private float _defOfGears;
        private int _healthPoint;
        private int _gold;
        private List<Gear> _gearList;
        private ItemType _equipFlag;
        #endregion

        
        public Player(string name, Class playerClass)
        {
            Name = name;
            PlayerClass = playerClass;
            Level = 1;
            Exp = 0;
            ATK = 10f;
            DEF = 5f;
            ATKOfGears = 0f;
            DEFOfGears = 0f;
            HP = 100;
            Gold = 1500;
            _gearList = new List<Gear>();
            _equipFlag = 0;
        }       

        #region ProPerties
        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public int Level
        {
            get { return _level; }
            set
            {
                int inc = value - _level;
                _level = value;
                ATK += 0.5f * inc;
                DEF += inc;
            }
        }

        public int Exp
        {
            get { return _exp; }
            set
            {
                if (value > Level * 100)
                {
                    _exp = value;
                    while (_exp > Level * 100)
                    {
                        _exp -= Level * 100;
                        Level++;
                    }
                }

                else
                    _exp = value;
            }
        }

        [JsonProperty]
        public Class PlayerClass
        {
            get { return _playerClass; }
            private set { _playerClass = value; }
        }

        [JsonIgnore]
        public float ATK
        {
            get { return _attack; }
            set { _attack = value; }
        }

        [JsonIgnore]
        public float DEF
        {
            get { return _defence; }
            set { _defence = value; }
        }

        [JsonProperty]
        public float ATKOfGears
        {
            get { return _atkOfGears; }
            private set 
            {
                ATK += value - ATKOfGears;
                _atkOfGears = value;
            }
        }

        [JsonProperty]
        public float DEFOfGears
        {
            get { return _defOfGears; }
            private set
            {
                DEF += value - DEFOfGears;
                _defOfGears = value;
            }
        }
                
        public int HP
        {
            get { return _healthPoint; }
            set { _healthPoint = value; }
        }
                
        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }

        [JsonProperty]
        public List<Gear> GearList
        {
            get { return _gearList; }
            private set { _gearList = value; }
        }

        [JsonProperty]
        public ItemType EquipFlag
        {
            get { return _equipFlag; }
            private set { _equipFlag = value; }
        }

        #endregion

        #region Method        
        public List<Gear> CopyGearList()
        {
            List<Gear> list = new List<Gear>();
            list = GearList;
            return list;
        }

        public void GetItem(Item item)
        {
            Gear gear = new Gear(item);
            _gearList.Add(gear);
        }

        public void EquipItem(int num)
        {
            Gear item = GearList[num];
            List<Gear> list = GearList.FindAll(x => x.IsEquip);

            // 장비를 장착 중 일때           
            if (item.IsEquip)
            {
                item.IsEquip = false;
                EquipFlag ^= item.GearType;
                CalcEquipmentStatus(item);
            }
            // 장비를 장착 중이지 않을 때
            else
            {
                item.IsEquip = true;
                EquipFlag |= item.GearType;
                CalcEquipmentStatus(item);
                foreach (Gear g in list)
                {
                    if (item.GearType == g.GearType)
                    {
                        g.IsEquip = false;
                        CalcEquipmentStatus(g);
                    }
                }
            }
        }

        public string SellItem(int num)
        {
            Gear item = GearList[num];

            // 장비를 장착 중 일때           
            if (item.IsEquip)
            {
                item.IsEquip = false;
                EquipFlag ^= item.GearType;
                CalcEquipmentStatus(item);
            }
            Gold += (int)(item.Price * 0.85f);
            GearList.RemoveAt(num);
            return item.Name;
        }

        public void CalcEquipmentStatus(Gear gear)
        {
            if (gear.IsEquip)
            {
                ATKOfGears += gear.ATK;
                DEFOfGears += gear.DEF;
            }
            else
            {
                ATKOfGears += -gear.ATK;
                DEFOfGears += -gear.DEF;
            }
        }               
        #endregion
    }
}
