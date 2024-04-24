using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SpartaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager("Wons", Class.ROGUE);
            gameManager.StartGame();
        }
    }

    public class GameManager
    {
        private Player player;
        private ViewController vc;
        private List<Goods> goodsList;

        public GameManager(string name, Class playerClass)
        {
            this.player = new Player(name, playerClass);
            this.vc = new ViewController();
            this.goodsList = new List<Goods>();
            this.SettingShop();
        }

        public void SettingShop()
        {
            goodsList.Add(new Goods("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000, ItemType.Armor));
            goodsList.Add(new Goods("무쇠갑옷", 0, 9, "수련에 도움을 주는 갑옷입니다.", 1800, ItemType.Armor));
            goodsList.Add(new Goods("스파르타의 갑옷", 0, 15, "수련에 도움을 주는 갑옷입니다.", 3500, ItemType.Armor));
            goodsList.Add(new Goods("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600, ItemType.Weapon));
            goodsList.Add(new Goods("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500, ItemType.Weapon));
            goodsList.Add(new Goods("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2700, ItemType.Weapon));
        }
        
        public void StartGame()
        {
            int? keyInput = 0;            
            do
            {
                vc.ViewStartMenu();
                if (keyInput == null)
                    keyInput = vc.ViewSelectMenu(true);
                else
                    keyInput = vc.ViewSelectMenu();

                switch (keyInput)
                {
                    case 1:
                        this.DisplayStatus();
                        break;
                    case 2:
                        this.DisplayInventoryMenu();
                        break;
                    case 3:
                        this.DisplayShopMenu();
                        break;
                    default:
                        keyInput = null;
                        break;
                }
            }
            while (true);                   
        }

        public void DisplayStatus()
        {
            int? keyInput = 0;
            do
            {
                vc.ViewyStatus(this.player);
                if (keyInput == null)
                    keyInput = vc.ViewSelectMenu(true);
                else
                    keyInput = vc.ViewSelectMenu();

                if (keyInput == 0) return;
                keyInput = null;
            }
            while (true);
                        
        }

        public void DisplayInventoryMenu()
        {
            int? keyInput = 0;
            do
            {
                vc.ViewInventoryMenu();
                if (keyInput == null)
                    keyInput = vc.ViewSelectMenu(true);
                else
                    keyInput = vc.ViewSelectMenu();

                switch (keyInput)
                {
                    case 0:
                        return;
                    case 1:
                        this.ManagementGear();
                        break;                    
                    default:
                        keyInput = null;
                        break;
                }
            }
            while (true);
        }

        public void ManagementGear()
        {
            int? keyInput = 0;
            do
            {
                vc.ViewInventory(player);
                if (keyInput == null)
                    keyInput = vc.ViewSelectMenu(true);
                else
                    keyInput = vc.ViewSelectMenu();

                if (keyInput == 0) return;
                for(int i = 0; i < player.GearCount(); i++)
                {
                    if(keyInput == i + 1)
                    {
                        player.EquipItem(i);
                        keyInput = i + 1;
                        break;
                    }                    
                }
                if (keyInput < 0) keyInput = null;
            }
            while (true);
        }

        public void DisplayShopMenu()
        {
            int? keyInput = 0;
            do
            {
                vc.ViewShopMenu(player, goodsList);
                if (keyInput == null)
                    keyInput = vc.ViewSelectMenu(true);
                else
                    keyInput = vc.ViewSelectMenu();

                switch (keyInput)
                {
                    case 0:
                        return;
                    case 1:
                        this.BuyItem();
                        break;
                    default:
                        keyInput = null;
                        break;
                }
            }
            while (true);
        }
        public void BuyItem()
        {
            int? keyInput = 0;
            do
            {
                vc.ViewBuyItem(player, goodsList);
                if (keyInput == null)
                    keyInput = vc.ViewSelectMenu(true);
                else
                    keyInput = vc.ViewSelectMenu();

                if (keyInput == 0) return;
                for (int i = 0; i < goodsList.Count(); i++)
                {
                    Goods goods = goodsList[i];
                    if (keyInput == i + 1)
                    {
                        if (player.Gold < goods.Price) 
                        {
                            break;
                        }

                        // 아이템 구매
                        else if (!goods.IsSoldOut)
                        {
                            player.GetItem((Item)goods);
                            player.Gold -= goods.Price;
                            goods.IsSoldOut = true;
                        }
                        break;
                    }                    
                }
                if (keyInput < 0) keyInput = null;
            }
            while (true);
        }
    }

    public class ViewController
    {
        private int cursurPoint;        

        public ViewController()
        {
            this.cursurPoint = 0;
        }

        public int? ViewSelectMenu()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            if (!(int.TryParse(Console.ReadLine(), out int result)))
                return null;
            return result;
        }

        public int? ViewSelectMenu(bool isWrong)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">> ");            
            if (isWrong) Console.WriteLine("잘못된 입력입니다");
            Console.SetCursorPosition(3, this.cursurPoint + 1);
            if (!(int.TryParse(Console.ReadLine(), out int result)))
                return null;
            return result;
        }

        public void ViewStartMenu()
        {   
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기\r\n2. 인벤토리\r\n3. 상점\r\n");
            this.cursurPoint = 7;            
        }

        public void ViewyStatus(Player player)
        {
            string str;
            Console.Clear();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"LV. {player.Level}");
            Console.WriteLine($"{player.Name} ( {player.PlayerClass} )");
            str = (player.ATKOfGears > 0) ? "(+" + player.ATKOfGears.ToString() + ")" : " ";            
            Console.WriteLine($"공격력 : {player.ATK + player.ATKOfGears} " + str);
            str = (player.DEFOfGears > 0) ? "(+" + player.DEFOfGears.ToString() + ")" : " ";
            Console.WriteLine($"방어력 : {player.DEF + player.DEFOfGears} " + str);
            Console.WriteLine($"체  력 : {player.HP}");
            Console.WriteLine($"Gold   : {player.Gold}");
            Console.WriteLine("\r\n0. 나가기\n");
            this.cursurPoint = 11;                       
        }

        public void ViewInventoryMenu()
        {
            Console.Clear();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기\n");
            this.cursurPoint = 7;
        }

        public void ViewInventory(Player player)
        {
            Console.Clear();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");
            List<Gear> gear = player.CopyGearList();
            for (int i = 0; i < gear.Count; i++)
            {
                Gear item = gear[i];
                Console.Write($"- {i + 1} ");
                if (item.IsEquip) Console.Write("[E]");
                Console.Write($"{item.Name}\t|");
                if (item.ATK > 0) Console.Write($" 공격력 +{item.ATK}");
                if (item.DEF > 0) Console.Write($" 방어력 +{item.DEF}");
                Console.Write($" \t| {item.Info}\t");                
                Console.WriteLine();
            }
            if (gear.Count > 0) Console.WriteLine();
            Console.WriteLine("0. 나가기\n");
            this.cursurPoint = 6 + player.GearCount();
        }        

        public void ViewShopMenu(Player player, List<Goods> goods)
        {
            Console.Clear();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");

            Console.WriteLine("[아이템 목록]");            
            for (int i = 0; i < goods.Count; i++)
            {
                Goods item = goods[i];
                Console.Write("- ");                
                Console.Write($"{item.Name}\t|");
                if (item.ATK > 0) Console.Write($" 공격력 +{item.ATK}");
                if (item.DEF > 0) Console.Write($" 방어력 +{item.DEF}");
                Console.Write($" \t| {item.Info}\t| ");                 
                if (!item.IsSoldOut) Console.Write($" {item.Price} G");
                else Console.Write($" 구매완료");
                Console.WriteLine();
            }
            if (goods.Count > 0) Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기\n");
            this.cursurPoint = 11 + goods.Count();
        }
        
        public void ViewBuyItem(Player player, List<Goods> goods)
        {
            Console.Clear();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < goods.Count; i++)
            {
                Goods item = goods[i];
                Console.Write($"- {i + 1} ");
                Console.Write($"{item.Name}\t|");
                if (item.ATK > 0) Console.Write($" 공격력 +{item.ATK}");
                if (item.DEF > 0) Console.Write($" 방어력 +{item.DEF}");
                Console.Write($" \t| {item.Info}\t| ");
                if (!item.IsSoldOut) Console.Write($" {item.Price} G");
                else Console.Write($" 구매완료");
                Console.WriteLine();
            }
            if (goods.Count > 0) Console.WriteLine();            
            Console.WriteLine("0. 나가기\n");
            this.cursurPoint = 9 + goods.Count();
        }
    }

    // Player 정보
    public class Player
    {
        #region Field
        private string _name;
        private int _level;
        private Class _playerClass;
        private float _attack;        
        private float _defence;
        private float _atkOfGears;
        private float _defenseOfGears;
        private int _healthPoint;
        private int _gold;
        private List<Gear> _gearList;
        private int _equipFlag;
        #endregion

        public Player(string name, Class playerClass)
        {
            this.Name = name;
            this.PlayerClass = playerClass;
            this.Level = 1;
            this.ATK = 10f;
            this.DEF = 5f;
            this.ATKOfGears = 0f;
            this.DEFOfGears = 0f;
            this.HP = 100;
            this.Gold = 15000;                        
            this._gearList = new List<Gear>();
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
            set { _level = value; }
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
            return this._gearList.Count;
        }
        
        public List<Gear> CopyGearList()
        {
            List<Gear> list = new List<Gear>();
            list = this._gearList;
            return list;
        }

        public void GetItem(Item item)
        {
            Gear gear = new Gear(item.Name, item.ATK, item.DEF, item.Info, item.GearType); // Value?Ref
            this._gearList.Add(gear);
        }

        public void EquipItem(int num)
        {
            Gear item = this._gearList[num];
            List<Gear> list = this._gearList.FindAll(x => x.IsEquip);

            // 장비를 장착 중 일때           
            if (item.IsEquip)
            {
                CalcEquipmentStatus(item);
                item.IsEquip = false;
                this._equipFlag ^= (int)item.GearType;
            }
            // 장비를 장착 중이지 않을 때
            else
            {
                CalcEquipmentStatus(item);
                item.IsEquip = true;
                this._equipFlag |= (int)item.GearType;
                foreach (Gear g in list)
                {
                    if (item.GearType == g.GearType)
                    {
                        CalcEquipmentStatus(g);
                        g.IsEquip = false;
                    }
                }
            }
        }

        public void CalcEquipmentStatus(Gear gear)
        {
            if (gear.IsEquip)
            {
                this.ATKOfGears -= gear.ATK;
                this.DEFOfGears -= gear.DEF;
            }
            else
            {
                this.ATKOfGears += gear.ATK;
                this.DEFOfGears += gear.DEF;
            }
        }
        #endregion
    }

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
            private set { _name = value; }
        }

        public float ATK
        {
            get { return _attack; }
            private set { _attack = value; }
        }

        public float DEF
        {
            get { return _defence; }
            private set { _defence = value; }
        }

        public string Info
        {
            get { return _info; }
            private set { _info = value; }
        }

        public int Price
        {
            get { return  _price; }
            private set { _price = value; }
        }       

        public ItemType GearType
        {
            get { return _gearType; }
            private set { _gearType = value; }
        }

        #endregion
    }

    public class Gear : Item
    {
        private bool _isEquip;

        public Gear(string name, float atk, float def, string info, ItemType itemType) : base (name, atk, def, info, itemType)
        {
            this.IsEquip = false;            
        }        

        public bool IsEquip
        {
            get { return _isEquip; }
            set { _isEquip = value; }
        }
    }

    public class Goods : Item
    {
         private bool _isSoldOut;

        public Goods(string name, float atk, float def, string info, int price, ItemType itemType) : base (name, atk, def, info, price, itemType)
        {
            this.IsSoldOut = false;            
        }        

        public bool IsSoldOut
        {
            get { return _isSoldOut; }
            set { _isSoldOut = value; }
        }

        public bool CheckSold(string name)
        {
            if(this.Name == name) return true;
            return false;
        }
    }

    // 직업 종류 열거형
    public enum Class
    {
        WARRIOR, // 전사
        MAGE,    // 마법사
        ROGUE,   // 도적
        PRIEST,  // 사제
        PALADIN, // 성기사
        WARLOCK, // 흑마법사
        HUNTER   // 사냥꾼
    }            

    // 장비 유형 열거형
    public enum ItemType
    {
         Weapon = 1,
         Armor = 2
    }
}
