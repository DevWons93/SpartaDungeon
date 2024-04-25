using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    // 상점에서 파는 상품의 클래스
    public class Goods : Item
    {
        private bool _isSoldOut;

        public Goods(string name, float atk, float def, string info, int price, ItemType itemType) : base(name, atk, def, info, price, itemType)
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
            if (this.Name == name) return true;
            return false;
        }
    }
}
