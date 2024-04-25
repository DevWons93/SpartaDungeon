using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
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
        private float _defenseOfGears;
        private int _healthPoint;
        private int _gold;
        private List<Gear> gearList;
        private int _equipFlag;
        #endregion

        public Player(string name, Class playerClass)
        {
            this.Name = name;
            this.PlayerClass = playerClass;
            this.Level = 1;
            this.Exp = 0;
            this.ATK = 10f;
            this.DEF = 5f;
            this.ATKOfGears = 0f;
            this.DEFOfGears = 0f;
            this.HP = 100;
            this.Gold = 1500;
            this.gearList = new List<Gear>();
            this._equipFlag = 0;
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
                this.ATK += 0.5f * inc;
                this.DEF += inc;
            }
        }

        public int Exp
        {
            get { return _exp; }
            set 
            {
                if (value > this.Level * 100)
                {
                    _exp = value;
                    while (_exp > this.Level * 100)
                    {
                        _exp -= this.Level * 100;
                        this.Level++;                        
                    }                    
                }

                else
                    _exp = value;
            }
        }

        public Class PlayerClass
        {
            get { return _playerClass; }
            private set { _playerClass = value; }
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

        public float ATKOfGears
        {
            get { return _atkOfGears; }
            private set { _atkOfGears = value; }
        }

        public float DEFOfGears
        {
            get { return _defenseOfGears; }
            private set { _defenseOfGears = value; }
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

        #endregion

        #region Method
        public int GearCount()
        {
            return this.gearList.Count;
        }

        public List<Gear> CopyGearList()
        {
            List<Gear> list = new List<Gear>();
            list = this.gearList;
            return list;
        }

        public void GetItem(Item item)
        {
            Gear gear = new Gear(item.Name, item.ATK, item.DEF, item.Info, item.Price,item.GearType);
            this.gearList.Add(gear);
        }

        public void EquipItem(int num)
        {
            Gear item = this.gearList[num];
            List<Gear> list = this.gearList.FindAll(x => x.IsEquip);

            // 장비를 장착 중 일때           
            if (item.IsEquip)
            {
                item.IsEquip = false;
                this._equipFlag ^= (int)item.GearType;
                CalcEquipmentStatus(item);
            }
            // 장비를 장착 중이지 않을 때
            else
            {
                item.IsEquip = true;
                this._equipFlag |= (int)item.GearType;
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
            Gear item = this.gearList[num];            

            // 장비를 장착 중 일때           
            if (item.IsEquip)
            {
                item.IsEquip = false;
                this._equipFlag ^= (int)item.GearType;                
                CalcEquipmentStatus(item);                
            }
            this.Gold += (int)(item.Price * 0.85f);
            this.gearList.RemoveAt(num);
            return item.Name;
        }

        public void CalcEquipmentStatus(Gear gear)
        {
            if (gear.IsEquip)
            {
                this.ATKOfGears += gear.ATK;
                this.DEFOfGears += gear.DEF;
            }
            else
            {
                this.ATKOfGears -= gear.ATK;
                this.DEFOfGears -= gear.DEF;
            }
        }
        #endregion
    }
}
