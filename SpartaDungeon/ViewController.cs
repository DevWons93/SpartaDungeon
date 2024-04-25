using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class ViewController
    {
        private int cursurPoint;

        public ViewController()
        {
            this.cursurPoint = 0;
        }        

        public int? ViewSelectMenu(MessageType message)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">> ");
            switch (message)
            {
                case MessageType.Error: 
                    Console.WriteLine("잘못된 입력입니다");
                    break;
                case MessageType.SoldOut:
                    Console.WriteLine("이미 구매한 아이템입니다");
                    break;
                case MessageType.SuccesBuyGoods:
                    Console.WriteLine("구매를 완료했습니다.");
                    break;
                case MessageType.NotEnoughGold:
                    Console.WriteLine("Gold 가 부족합니다.");
                    break;
                case MessageType.NotEnoughHP:
                    Console.WriteLine("체력이 부족합니다.");
                    break;
                case MessageType.FullHP:
                    Console.WriteLine("체력이 이미 가득 찼습니다.");
                    break;
                case MessageType.GetRest:
                    Console.WriteLine("휴식을 완료했습니다.");
                    break;
            }            
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
            Console.WriteLine("1. 상태 보기\r\n2. 인벤토리\r\n3. 상점\r\n4. 던전 입장\r\n5. 휴식하기\r\n6. 저장하기\r\n7. 불러오기\r\n");
            this.cursurPoint = 11;
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
            Console.WriteLine();
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
            Console.WriteLine();
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
            Console.WriteLine();
            Console.WriteLine("0. 나가기\n");
            this.cursurPoint = 9 + goods.Count();
        }

        public void ViewSellItem(Player player)
        {
            Console.Clear();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");

            Console.WriteLine("[아이템 목록]");
            List<Gear> gear = player.CopyGearList();
            for (int i = 0; i < gear.Count; i++)
            {
                Gear item = gear[i];
                Console.Write($"- {i + 1} ");                
                Console.Write($"{item.Name}\t|");
                if (item.ATK > 0) Console.Write($" 공격력 +{item.ATK}");
                if (item.DEF > 0) Console.Write($" 방어력 +{item.DEF}");
                Console.Write($" \t| {item.Info}\t| {item.Price} G");                
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기\n");
            this.cursurPoint = 9 + player.GearCount();
        }

        public void ViewDungeonMenu(List<Dungeon> dungeons)
        {
            Console.Clear();
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");            

            for (int i = 0; i < dungeons.Count; i++)
            {
                Dungeon dg = dungeons[i];
                Console.Write($"{i + 1} ");
                Console.Write($"{dg.Name} 던전\t|");
                Console.Write($" 방어력 {dg.DEFSpec} 이상 권장");                
                Console.WriteLine();
            }            
            Console.WriteLine("0. 나가기\n");
            this.cursurPoint = 4 + dungeons.Count;
        }

        public void ViewDungeonClear(string dungeonName,int damage, int reward, Player player)
        {
            Console.Clear();
            if (reward > 0)  
                Console.WriteLine($"축하합니다!!\r\n{dungeonName} 던전을 클리어 하였습니다.\n");
            else
                Console.WriteLine($"{dungeonName} 던전 클리어에 실패 하셨습니다!!\r\n다시 도전해 보세요\n");

            Console.WriteLine("[탐험 결과]");            
            Console.WriteLine($"체력 {player.HP} -> {damage}");
            Console.WriteLine($"Gold {player.Gold} G -> {player.Gold + reward} G \n");
            Console.WriteLine("0. 나가기\n");
            this.cursurPoint = 9;
        }

        public void ViewRestRoom(Player player)
        {
            Console.Clear();
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold} G)\n");            
            Console.WriteLine("1. 휴식하기\r\n0. 나가기\n");
            this.cursurPoint = 5;
        }

        public void ViewSave()
        {
            Console.Clear();
            Console.WriteLine("게임을 저장하였습니다.\n");
            Console.WriteLine("0. 나가기\n");
            this.cursurPoint = 4;
        }
        public void ViewLoad()
        {
            Console.Clear();
            Console.WriteLine("게임을 로드하였습니다.\n");
            Console.WriteLine("0. 나가기\n");
            this.cursurPoint = 4;
        }
    }
}
