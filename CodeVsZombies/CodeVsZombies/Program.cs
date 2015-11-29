using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;


/**
 * Save humans, destroy zombies!
 **/

internal class Player
{
    public class Human
    {
        public int HumanId { get; set; }
        public int HumanX { get; set; }
        public int HumanY { get; set; }
        public ICollection<Human> Humans { get; set; }
    }

    public class MySelf
    {
        public int MyCoordinateX { get; set; }
        public int MyCoordinateY { get; set; }
    }

    public class Zombie
    {
        public int ZombieId { get; set; }
        public int ZombieX { get; set; }
        public int ZombieY { get; set; }
        public int ZombieNextX { get; set; }
        public int ZombieNextY { get; set; }
        public ICollection<Zombie> Zombies { get; set; }
    }

    public static double CalculateDistance(Zombie zombie, Human human)
    {
        return Math.Sqrt(Math.Pow(Math.Abs(zombie.ZombieNextX - human.HumanX), 2)
                         + Math.Pow(Math.Abs(zombie.ZombieNextY - human.HumanY), 2));
    }

    public static double CalculateDistanceMe(Zombie zombie, MySelf mySelf)
    {
        return
            Math.Sqrt(Math.Pow(Math.Abs(zombie.ZombieNextX - mySelf.MyCoordinateX), 2) +
                      Math.Pow(Math.Abs(zombie.ZombieNextY - mySelf.MyCoordinateY), 2));
    }

    public static double CalculateDistanceHuman(MySelf mySelf, Human human)
    {
        return Math.Sqrt(Math.Pow(Math.Abs(mySelf.MyCoordinateX - human.HumanX), 2)
                         + Math.Pow(Math.Abs(mySelf.MyCoordinateY - human.HumanY), 2));
    }

    private static void Main(string[] args)
    {
        string[] inputs;

        // game loop
        while (true)
        {
            var mySelf = new MySelf();
            var human = new Human();
            var humans = new Collection<Human>();
            var zombie = new Zombie();
            var zombies = new Collection<Zombie>();

            inputs = Console.ReadLine().Split(' ');
            mySelf.MyCoordinateX = int.Parse(inputs[0]);
            mySelf.MyCoordinateY = int.Parse(inputs[1]);
            Console.Error.WriteLine("My position: " + mySelf.MyCoordinateX + " " + mySelf.MyCoordinateY);

            int humanCount = int.Parse(Console.ReadLine());
            Console.Error.WriteLine("Humans Alive: " + humanCount);

            for (int i = 0; i < humanCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');

                human.HumanId = int.Parse(inputs[0]);

                human.HumanX = int.Parse(inputs[1]);
                human.HumanY = int.Parse(inputs[2]);

                human.Humans.Add(human);

                Console.Error.WriteLine("Human " + human.HumanId + " is located at: " + human.HumanX + " " +
                                        human.HumanY);
            }

            int zombieCount = int.Parse(Console.ReadLine());
            Console.Error.WriteLine("I have to kill " + zombieCount + " zombies!");

            for (int i = 0; i < zombieCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');

                zombie.ZombieId = int.Parse(inputs[0]);

                zombie.ZombieX = int.Parse(inputs[1]);
                zombie.ZombieY = int.Parse(inputs[2]);

                zombie.ZombieNextX = int.Parse(inputs[3]);
                zombie.ZombieNextY = int.Parse(inputs[4]);

                zombie.Zombies.Add(zombie);

                Console.Error.WriteLine("Zombie " + zombie.ZombieId + " is located at: " + zombie.ZombieX + " " +
                                        zombie.ZombieY);
                Console.Error.WriteLine("Zombie " + zombie.ZombieId + " is going to: " + zombie.ZombieNextX + " " +
                                        zombie.ZombieNextY);
            }
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine();

            if (zombies.Count > 0)
            {
                int coordX = 0;
                int coordY = 0;

                for (int i = 0; i < zombies.Count; i++)
                {
                    double dist1 = 0;
                    double dist3 = 0;

                    var dist2 = CalculateDistanceMe(zombies.ElementAt(i), mySelf);
                    Console.Error.WriteLine("distance between zombie " + zombies.ElementAt(i).ZombieId + " and me is " + dist2);

                    for (int j = 0; j < humans.Count; j++)
                    {
                        dist1 = CalculateDistance(zombies.ElementAt(i), humans.ElementAt(j));
                        dist3 = CalculateDistanceHuman(mySelf, humans.ElementAt(j));

                        Console.Error.WriteLine("Zombie " + zombies.ElementAt(i).ZombieId + " are " + dist1 +
                                                "away from human" + humans.ElementAt(j).HumanId);
                        Console.Error.WriteLine("Distance between human " + humans.ElementAt(j).HumanId + " and me is " +
                                                dist3);

                        if (dist1 > dist3 || dist2 < dist1)
                        {
                            coordX = humans.ElementAt(j).HumanX;
                            coordY = humans.ElementAt(j).HumanY;
                        }
                        else if (dist2 < dist3 || dist2 < 2000)
                        {
                            coordX = zombies.ElementAt(i).ZombieX;
                            coordY = zombies.ElementAt(i).ZombieY;
                        }
                        else
                        {
                            coordX = zombies.ElementAt(i).ZombieNextX;
                            coordY = zombies.ElementAt(i).ZombieNextY;
                        }
                    }
                    
                }
                Console.WriteLine(coordX + " " + coordY);
            }
        }
    }
}

//using System;
//using System.Linq;
//using System.IO;
//using System.Text;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;

///**
// * Save humans, destroy zombies!
// **/
//class Player
//{
//    public class Human
//    {
//        public int HumanId { get; set; }
//        public int HumanX { get; set; }
//        public int HumanY { get; set; }
//        public ICollection<Human> Humans { get; set; }
//    }

//    public class MySelf
//    {
//        public int MyCoordinateX { get; set; }
//        public int MyCoordinateY { get; set; }
//    }

//    public class Zombie
//    {
//        public int ZombieId { get; set; }
//        public int ZombieX { get; set; }
//        public int ZombieY { get; set; }
//        public int ZombieNextX { get; set; }
//        public int ZombieNextY { get; set; }
//        public ICollection<Zombie> Zombies { get; set; }
//    }

//    public static double CalculateDistance(Zombie zombie, Human human)
//    {
//        return Math.Sqrt(Math.Pow(Math.Abs(zombie.ZombieNextX - human.HumanX), 2)
//            + Math.Pow(Math.Abs(zombie.ZombieNextY - human.HumanY), 2));
//    }
//    public static double CalculateDistanceMe(Zombie zombie, MySelf mySelf)
//    {
//        return
//               Math.Sqrt(Math.Pow(Math.Abs(zombie.ZombieNextX - mySelf.MyCoordinateX), 2) +
//                         Math.Pow(Math.Abs(zombie.ZombieNextY - mySelf.MyCoordinateY), 2));
//    }
//    public static double CalculateDistanceHuman(MySelf mySelf, Human human)
//    {
//        return Math.Sqrt(Math.Pow(Math.Abs(mySelf.MyCoordinateX - human.HumanX), 2)
//            + Math.Pow(Math.Abs(mySelf.MyCoordinateY - human.HumanY), 2));
//    }
//    static void Main(string[] args)
//    {
//        string[] inputs;

//        // game loop
//        while (true)
//        {
//            var mySelf = new MySelf();
//            var human = new Human();
//            var humans = new Collection<Human>();
//            var zombie = new Zombie();
//            var zombies = new Collection<Zombie>();

//            inputs = Console.ReadLine().Split(' ');
//            mySelf.MyCoordinateX = int.Parse(inputs[0]);
//            mySelf.MyCoordinateY = int.Parse(inputs[1]);
//            Console.Error.WriteLine("My position: " + mySelf.MyCoordinateX + " " + mySelf.MyCoordinateY);

//            int humanCount = int.Parse(Console.ReadLine());
//            Console.Error.WriteLine("Humans Alive: " + humanCount);

//            for (int i = 0; i < humanCount; i++)
//            {
//                inputs = Console.ReadLine().Split(' ');

//                human.HumanId = int.Parse(inputs[0]);

//                human.HumanX = int.Parse(inputs[1]);
//                human.HumanY = int.Parse(inputs[2]);

//                humans.Add(human);

//                Console.Error.WriteLine("Human " + human.HumanId + " is located at: " + human.HumanX + " " + human.HumanY);
//            }

//            int zombieCount = int.Parse(Console.ReadLine());
//            Console.Error.WriteLine("I have to kill " + zombieCount + " zombies!");

//            for (int i = 0; i < zombieCount; i++)
//            {
//                inputs = Console.ReadLine().Split(' ');

//                zombie.ZombieId = int.Parse(inputs[0]);

//                zombie.ZombieX = int.Parse(inputs[1]);
//                zombie.ZombieY = int.Parse(inputs[2]);

//                zombie.ZombieNextX = int.Parse(inputs[3]);
//                zombie.ZombieNextY = int.Parse(inputs[4]);

//                zombies.Add(zombie);

//                Console.Error.WriteLine("Zombie " + zombie.ZombieId + " is located at: " + zombie.ZombieX + " " + zombie.ZombieY);
//                Console.Error.WriteLine("Zombie " + zombie.ZombieId + " is going to: " + zombie.ZombieNextX + " " + zombie.ZombieNextY);
//            }
//            // Write an action using Console.WriteLine()
//            // To debug: Console.Error.WriteLine("Debug messages...");

//            if (zombieCount > 0)
//            {
//                int coordX = 0;
//                int coordY = 0;
//                for (int i = 0; i < zombieCount; i++)
//                {
//                    for (int j = 0; j < humanCount; j++)
//                    {

//                        var dist1 = CalculateDistance(zombies.ElementAt(i), humans.ElementAt(j));
//                        var dist2 = CalculateDistanceMe(zombies.ElementAt(i), mySelf);
//                        var dist3 = CalculateDistanceHuman(mySelf, humans.ElementAt(j));

//                        Console.Error.WriteLine("Distance 1: " + dist1);
//                        Console.Error.WriteLine("Distance 2: " + dist2);
//                        Console.Error.WriteLine("Distance 3: " + dist3);

//                        if (dist1 > dist3 || dist2 < dist1)
//                        {
//                            coordX = humans.ElementAt(j).HumanX;
//                            coordY = humans.ElementAt(j).HumanY;
//                        }
//                        else if (dist2 < dist3 || dist2 < 2000)
//                        {
//                            coordX = zombies.ElementAt(i).ZombieX;
//                            coordY = zombies.ElementAt(i).ZombieY;
//                        }
//                        else
//                        {
//                            coordX = zombies.ElementAt(i).ZombieNextX;
//                            coordY = zombies.ElementAt(i).ZombieNextY;
//                        }
//                    }

//                }
//                Console.WriteLine(coordX + " " + coordY);

//            }

//        }
//    }
//}