using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    static class SpawnManager
    {
        static float shipSpawnCounter;
        static float powerUpSpawnCounter;
        static Queue<EnemyShip>[] shipsPool;
        static List<PowerUp> powerUpList;

        static SpawnManager()
        {
            shipSpawnCounter = 5;
            powerUpSpawnCounter = 8;

            shipsPool = new Queue<EnemyShip>[2];

            int poolSize = 10;
            
            for(int i = 0; i < shipsPool.Length; i++)
            {
                shipsPool[i] = new Queue<EnemyShip>(poolSize);
            }

            for(int i = 0; i < poolSize; i++)
            {
                shipsPool[0].Enqueue(new EnemyShip());
                shipsPool[1].Enqueue(new RedEnemyShip());
            }

            powerUpList = new List<PowerUp>();
            powerUpList.Add(new NrgPowerUp(Vector2.Zero));
            powerUpList.Add(new FastFirePowerUp(Vector2.Zero));
            powerUpList.Add(new FastFirePowerUp(Vector2.Zero));
        }

        public static void Update()
        {
            //ships spawn
            shipSpawnCounter -= Game.DeltaTime;
            if (shipSpawnCounter <= 0)
            {
                shipSpawnCounter = RandomGenerator.GetRandom(3, 5);

                int choice = RandomGenerator.GetRandom(0, 100);
                EnemyShip newShip = null;
                if (choice < 60)
                {
                    choice = 0;
                }
                else
                {
                     choice = 1;
                }

                newShip = shipsPool[choice].Dequeue();

                SetRandomPosition(newShip);                
                newShip.IsActive = true;
            }

            //power up spawn
            powerUpSpawnCounter -= Game.DeltaTime;

            if (powerUpSpawnCounter <= 0)
            {
                powerUpSpawnCounter = RandomGenerator.GetRandom(8, 15);

                PowerUp p = GetRandomPowerUp();
                if (p != null)
                {
                    SetRandomPosition(p);
                    p.IsActive = true;
                }
            }
        }

        private static void SetRandomPosition(GameObject item)
        {
            int minY = item.Height / 2 +50;
            int maxY = Game.window.Height - (item.Height / 2) -50 ;
            item.Position= new Vector2(Game.window.Width + item.Width, RandomGenerator.GetRandom(minY, maxY));
        }

        public static void Restore(EnemyShip item)
        {
            if(item.GetType() == typeof(RedEnemyShip))
            {
                shipsPool[1].Enqueue(item);
            }
            else
            {
                shipsPool[0].Enqueue(item);
            }
        }

        public static void Restore(PowerUp item)
        {
            item.IsActive = false;
            powerUpList.Add(item);
        }

        private static PowerUp GetRandomPowerUp()
        {
            if (powerUpList.Count > 0)
            {
                int randomItemIndex = RandomGenerator.GetRandom(0, powerUpList.Count);
                PowerUp p = powerUpList[randomItemIndex];
                powerUpList.RemoveAt(randomItemIndex);
                return p;
            }
            return null;
        }
    }
}
