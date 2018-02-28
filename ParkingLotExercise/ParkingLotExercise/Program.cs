using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotExercise
{
    class Program
    {
        public static MyMap PopulateMap()
        {
            MyMap myMap = new MyMap();
            const uint MAX_LEVEL = 2;
            const uint MAX_SPACE = 3;

            for (uint ilevel = 1; ilevel <= MAX_LEVEL; ilevel++)
            {
                // insert normalLot
                //
                for (uint iSpace = 2; iSpace <= MAX_SPACE; iSpace++)
                {
                    myMap.InsertSpace(new NormalParkingLot(ilevel * 100 + iSpace));
                }
                
                // insert disabledLot
                //
                for (uint iSpace = 1; iSpace <= 1; iSpace++)
                {
                    myMap.InsertSpace(new DisabledParkingLot(ilevel * 100 + iSpace));
                }
            }

            return myMap;
        }

        protected static void ScenarioValidOne()
        {
            // Step 1 - Populate Parking Map
            //
            ParkingMap myParkingMap = new ParkingMap(PopulateMap());
            Console.WriteLine("***************************************");
            Console.WriteLine("BEFORE ADDING ANYTHING");
            Console.WriteLine("***************************************");
            myParkingMap.PrintMap();

            List<int> lstParkedSpotIds = new List<int>();

            // Step 2 - Insert Few Cars
            //
            Console.WriteLine("\n\n");
            Console.WriteLine("***************************************");
            Console.WriteLine("6 NORMAL CARS + 2 DISABLED CAR ADDED");
            Console.WriteLine("***************************************");
            lstParkedSpotIds.Add(myParkingMap.RequestParking(new NormalCar(12300)));
            lstParkedSpotIds.Add(myParkingMap.RequestParking(new DisabledCar(12301)));
            lstParkedSpotIds.Add(myParkingMap.RequestParking(new NormalCar(12302)));
            lstParkedSpotIds.Add(myParkingMap.RequestParking(new NormalCar(12303)));
            lstParkedSpotIds.Add(myParkingMap.RequestParking(new DisabledCar(12304)));
            lstParkedSpotIds.Add(myParkingMap.RequestParking(new NormalCar(12305)));
            lstParkedSpotIds.Add(myParkingMap.RequestParking(new NormalCar(12306)));
            lstParkedSpotIds.Add(myParkingMap.RequestParking(new NormalCar(12307)));
            myParkingMap.PrintMap();

            // Step 3 - Insert More Cars
            //
            Console.WriteLine("\n\n");
            Console.WriteLine("***************************************");
            Console.WriteLine("1 NORMAL CARS + 1 DISABLED CAR ADDED [OVERFLOW]");
            Console.WriteLine("***************************************");
            myParkingMap.RequestParking(new NormalCar(99900));
            myParkingMap.RequestParking(new DisabledCar(99901));
            myParkingMap.PrintMap();

            // Step 4 - Release Parking
            //
            Console.WriteLine("\n\n");
            Console.WriteLine("***************************************");
            Console.WriteLine("RELEASED ALL PARKING SPOTS");
            Console.WriteLine("***************************************");
            foreach (uint iParkingSpotId in lstParkedSpotIds)
            {
                myParkingMap.ReleaseParkingByParkingLotId(iParkingSpotId);
            }
            myParkingMap.PrintMap();

            // Step 5 - Insert More Cars
            //
            Console.WriteLine("\n\n");
            Console.WriteLine("***************************************");
            Console.WriteLine("2 NORMAL CARS + 2 DISABLED CAR ADDED");
            Console.WriteLine("***************************************");
            myParkingMap.RequestParking(new NormalCar(99902));
            myParkingMap.RequestParking(new NormalCar(99903));
            myParkingMap.RequestParking(new DisabledCar(99904));
            myParkingMap.RequestParking(new DisabledCar(99905));
            myParkingMap.PrintMap();

            // Step 6 - Insert More Cars
            //
            Console.WriteLine("\n\n");
            Console.WriteLine("***************************************");
            Console.WriteLine("2 NORMAL CARS + 2 DISABLED CAR ADDED [DUPLICATE]");
            Console.WriteLine("***************************************");
            myParkingMap.RequestParking(new NormalCar(99902));
            myParkingMap.RequestParking(new NormalCar(99903));
            myParkingMap.RequestParking(new DisabledCar(99904));
            myParkingMap.RequestParking(new DisabledCar(99905));
            myParkingMap.PrintMap();

            // Step 7 - Release Using Car ID
            //
            Console.WriteLine("\n\n");
            Console.WriteLine("***************************************");
            Console.WriteLine("RELEASED CAR ID");
            Console.WriteLine("***************************************");
            myParkingMap.ReleaseParkingByCarId(12306); 
            myParkingMap.ReleaseParkingByCarId(99903); 
            myParkingMap.PrintMap();

            // Step 8 - Release INVALID Car ID
            //
            Console.WriteLine("\n\n");
            Console.WriteLine("***************************************");
            Console.WriteLine("RELEASED INVALID CAR ID");
            Console.WriteLine("***************************************");
            myParkingMap.ReleaseParkingByCarId(12306); // already released
            myParkingMap.ReleaseParkingByCarId(99903); // already released
            myParkingMap.PrintMap();

            // Step 9 - Release Invalid Parking Spot
            //
            Console.WriteLine("\n\n");
            Console.WriteLine("***************************************");
            Console.WriteLine("RELEASED INVALID PARKING SPOT");
            Console.WriteLine("***************************************");
            myParkingMap.ReleaseParkingByParkingLotId(102); // unused
            myParkingMap.PrintMap();

        }

        public static void Main(string[] args)
        {
            ScenarioValidOne();

            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
        }
    }
}
