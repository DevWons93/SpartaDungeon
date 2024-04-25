using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
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
        Armor = 2,
        Boots = 4
    }

    public enum MessageType
    {
        Normal,
        Error,
        SoldOut,
        SuccesBuyGoods,
        NotEnoughGold,
        NotEnoughHP,
        FullHP,
        GetRest
    }
}
