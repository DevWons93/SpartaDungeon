﻿using System.Collections.Generic;
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
}
