using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RandomManager
    {
        private static RandomManager instance;
        private static Random pRandom;

        private RandomManager() {
            RandomManager.pRandom = new Random();
        }

        public static void Create() {
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new RandomManager();
            }
            Debug.Assert(instance != null);
        }

        public static void Destroy()
        {
            
        }

        public static int RandomInt(int start, int end) {
            int RandInt = RandomManager.pRandom.Next(start, end);
            return RandInt;
        }

    }
}
