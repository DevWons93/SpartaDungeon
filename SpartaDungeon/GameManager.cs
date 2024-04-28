using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SpartaDungeon.Models;

namespace SpartaDungeon
{
    public class GameManager
    {
        private Player player;
        private ViewController vc;
        private List<Goods> goodsList;
        private List<Dungeon> dungeonList;

        public GameManager(string name, Class playerClass)
        {
            this.player = new Player(name, playerClass);
            this.vc = new ViewController();
            this.goodsList = new List<Goods>();
            this.dungeonList = new List<Dungeon>();
            this.Setting();
        }

        public void Setting()
        {
            #region GoodsInitialize
            goodsList.Add(new Goods("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000, ItemType.Armor));
            goodsList.Add(new Goods("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1800, ItemType.Armor));
            goodsList.Add(new Goods("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, ItemType.Armor));
            goodsList.Add(new Goods("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600, ItemType.Weapon));
            goodsList.Add(new Goods("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500, ItemType.Weapon));
            goodsList.Add(new Goods("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2700, ItemType.Weapon));
            goodsList.Add(new Goods("닳은 부츠", 1, 1, "수련에 도움을 주는 신발입니다.", 700, ItemType.Boots));
            goodsList.Add(new Goods("강철 부츠", 3, 2, "강철로 만들어진 신발입니다.", 1600, ItemType.Boots));
            goodsList.Add(new Goods("스파르타의 부츠", 5, 3, "스파르타의 전사들이 사용했다는 전설의 신발입니다.", 3000, ItemType.Boots));
            #endregion

            #region DungeonInitialize
            dungeonList.Add(new Dungeon("쉬운", 1, 5, 1000, 40));
            dungeonList.Add(new Dungeon("보통", 1, 11, 1700, 70));
            dungeonList.Add(new Dungeon("어려운", 1, 17, 2500, 110));
            #endregion
        }

        public void SaveData()
        {
            JsonFormat json = new JsonFormat();
            json.Player = player;            
            json.Goods = goodsList; 
            json.Gears = player.CopyGearList();
            string fileName = "../../../Resource/SaveFile.json";
            string jsonString = JsonConvert.SerializeObject(json);
            File.WriteAllText(fileName, jsonString);
        }

        public void LoadData()
        {            
            string fileName = "../../../Resource/SaveFile.json";
            string jsonString = File.ReadAllText(fileName);
            JObject jobj = JObject.Parse(jsonString);
            //JsonFormat json = JsonSerializer.Deserialize<JsonFormat>(jsonString);

            player = JsonConvert.DeserializeObject<Player>(jobj["Player"].ToString());
            goodsList = JsonConvert.DeserializeObject<List<Goods>>(jobj["Goods"].ToString());            
            List<Gear> gears = JsonConvert.DeserializeObject<List<Gear>>(jobj["Gears"].ToString());
            //player.LoadGear(gears);
        }        

        public void StartGame()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            do
            {
                vc.ViewStartMenu();
                keyInput = vc.ViewSelectMenu(message);

                switch (keyInput)
                {
                    case 1:
                        this.DisplayStatus();
                        message = MessageType.Normal;
                        break;
                    case 2:
                        this.DisplayInventoryMenu();
                        message = MessageType.Normal;
                        break;
                    case 3:
                        this.DisplayShopMenu();
                        message = MessageType.Normal;
                        break;
                    case 4:
                        this.SelectDungeon();
                        message = MessageType.Normal;
                        break;
                    case 5:
                        this.GetRest();
                        message = MessageType.Normal;
                        break;
                    case 6:
                        this.DisplaySuccesSave();
                        message = MessageType.Normal;
                        break;
                    case 7:
                        this.DisplaySuccesLoad();
                        message = MessageType.Normal;
                        break;
                    default:
                        message = MessageType.Error;
                        break;
                }
            }
            while (true);
        }

        public void DisplayStatus()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            do
            {
                vc.ViewyStatus(player);
                keyInput = vc.ViewSelectMenu(message);

                if (keyInput == 0) return;
                message = MessageType.Error;
            }
            while (true);

        }

        public void DisplayInventoryMenu()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            do
            {
                vc.ViewInventoryMenu();
                keyInput = vc.ViewSelectMenu(message);

                switch (keyInput)
                {
                    case 0:
                        message = MessageType.Normal;
                        return;
                    case 1:
                        this.ManagementGear();
                        message = MessageType.Normal;
                        break;
                    default:
                        message = MessageType.Error;
                        break;
                }
            }
            while (true);
        }

        public void ManagementGear()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            do
            {
                vc.ViewInventory(player);
                keyInput = vc.ViewSelectMenu(message);

                if (keyInput == 0) return;
                for (int i = 0; i < player.GearList.Count; i++)
                {
                    if (keyInput == i + 1)
                    {
                        player.EquipItem(i);                        
                        message = MessageType.Normal;
                        break;
                    }
                    message = MessageType.Error;
                }

                if (keyInput == null) message = MessageType.Error;
            }
            while (true);
        }

        public void DisplayShopMenu()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            do
            {
                vc.ViewShopMenu(player, goodsList);
                keyInput = vc.ViewSelectMenu(message);

                switch (keyInput)
                {
                    case 0:
                        message = MessageType.Normal;
                        return;
                    case 1:
                        this.BuyItem();
                        message = MessageType.Normal;
                        break;
                    case 2:
                        this.SellItem();
                        message = MessageType.Normal;
                        break;
                    default:
                        message = MessageType.Error;
                        break;
                }
            }
            while (true);
        }

        public void BuyItem()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            do
            {
                vc.ViewBuyItem(player, goodsList);
                keyInput = vc.ViewSelectMenu(message);

                if (keyInput == 0) return;
                for (int i = 0; i < goodsList.Count(); i++)
                {
                    Goods goods = goodsList[i];
                    if (keyInput == i + 1)
                    {
                        // 구매완료된 상품
                        if (goods.IsSoldOut)
                        {   
                            message = MessageType.SoldOut;
                        }

                        // 골드 부족
                        else if (player.Gold < goods.Price)
                        {   
                            message = MessageType.NotEnoughGold;                            
                        }

                        // 아이템 구매
                        else 
                        {
                            message = MessageType.SuccesBuyGoods;
                            player.GetItem((Item)goods);
                            player.Gold -= goods.Price;
                            goods.IsSoldOut = true;                            
                        }                        
                        break;
                    }
                    message = MessageType.Error;
                }

                if (keyInput == null) message = MessageType.Error;

            } while (true);
        }

        public void SellItem()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            do
            {
                vc.ViewSellItem(player);
                keyInput = vc.ViewSelectMenu(message);                

                if (keyInput == 0) return;
                
                for (int i = 0; i < player.GearList.Count; i++)
                {   
                    if (keyInput == i + 1)
                    {
                        string name = player.SellItem(i);
                        Goods goods = this.goodsList.Find(g => g.Name == name);
                        goods.IsSoldOut = false;
                        message = MessageType.Normal;
                        break;                          
                    }
                    message = MessageType.Error;
                }
                
                if (keyInput == null) message = MessageType.Error;

            } while (true);
        }

        public void SelectDungeon()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            do
            {
                vc.ViewDungeonMenu(dungeonList);
                keyInput = vc.ViewSelectMenu(message);

                if (keyInput == 0) return;

                for (int i = 0; i < dungeonList.Count(); i++)
                {
                    if (keyInput == i + 1)
                    {
                        if (player.HP == 0)
                        {
                            message = MessageType.NotEnoughHP;
                            break;
                        }
                        //던전입장
                        this.DisplayDungeonClear(i);
                        message = MessageType.Normal;
                        break;
                    }
                    message = MessageType.Error;
                }

                if (keyInput == null) message = MessageType.Error;

            } while (true);
        }

        public void DisplayDungeonClear(int num)
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            Dungeon dg = dungeonList[num];
            Random random = new Random();
            int fail = random.Next(0, 100);
            string name = dg.Name;
            int prevHP = player.HP;
            int damage;
            int reward;
            int exp;

            if (dg.DEFSpec > player.DEF && fail < 40)
            {
                damage = 50;
                reward = 0;
                exp = 0;
            }
            else
            {
                damage = dg.GetDamage(player);
                reward = dg.GetClearReward(player);
                exp = dg.Exp;
            }

            int hpTakenDamage = player.HP > damage ? player.HP - damage : 0;

            player.HP = hpTakenDamage;
            player.Gold += reward;
            player.Exp += exp;

            do
            {
                vc.ViewDungeonClear(name, prevHP, reward, player);
                keyInput = vc.ViewSelectMenu(message);

                if (keyInput == 0) break;
                message = MessageType.Error;

            } while (true);
        }

        public void GetRest()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            do
            {
                vc.ViewRestRoom(player);
                keyInput = vc.ViewSelectMenu(message);

                if (keyInput == 0) return;

                switch (keyInput)
                {
                    case 1:
                        if(player.Gold < 500)
                        {
                            message = MessageType.NotEnoughGold;
                            break;
                        }                        
                        else if(player.HP == 100)
                        {
                            message = MessageType.FullHP;
                            break;
                        }
                        player.Gold -= 500;
                        player.HP = 100;
                        message = MessageType.GetRest;
                        break;                    
                    default:
                        message = MessageType.Error;
                        break;
                }
            }
            while (true);
        }

        public void DisplaySuccesSave()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;

            this.SaveData();

            do
            {
                vc.ViewSave();
                keyInput = vc.ViewSelectMenu(message);

                if (keyInput == 0) return;
                message = MessageType.Error;
            }
            while (true);
        }

        public void DisplaySuccesLoad()
        {
            int? keyInput = 0;
            MessageType message = MessageType.Normal;
            
            this.LoadData();

            do
            {
                vc.ViewLoad();
                keyInput = vc.ViewSelectMenu(message);

                if (keyInput == 0) return;
                message = MessageType.Error;
            }
            while (true);
        }
    }
}
