using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotExercise
{
    public class MyMap
    {
        protected SortedDictionary<uint, NormalParkingLot> dctNormalParkingLots;
        protected SortedDictionary<uint, DisabledParkingLot> dctDisabledParkingLots;

        #region Constructors/Destructors
        public MyMap()
        {
            dctNormalParkingLots = new SortedDictionary<uint, NormalParkingLot>();
            dctDisabledParkingLots = new SortedDictionary<uint, DisabledParkingLot>();
        }
        #endregion

        #region InsertToList
        public bool InsertSpace (ParkingLot plAvailableParkingLot)
        {
            if (plAvailableParkingLot != null)
            {
                if (plAvailableParkingLot is NormalParkingLot)
                {
                    dctNormalParkingLots.Add(plAvailableParkingLot.GetId(),
                        (NormalParkingLot)plAvailableParkingLot);
                    return true;
                }
                else if (plAvailableParkingLot is DisabledParkingLot)
                {
                    dctDisabledParkingLots.Add(plAvailableParkingLot.GetId(),
                        (DisabledParkingLot)plAvailableParkingLot);
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region DequeueFromList
        public ParkingLot GetSpace (Car carWaitingToPark)
        {
            // check the type of car
            if (carWaitingToPark is NormalCar && dctNormalParkingLots.Count() > 0 
                && !dctNormalParkingLots.ContainsKey(carWaitingToPark.GetCarId()))
            {
                ParkingLot plReturnedLot = dctNormalParkingLots.Values.First();
                dctNormalParkingLots.Remove(plReturnedLot.GetId());
                return plReturnedLot;
            }
            else if (carWaitingToPark is DisabledCar && dctDisabledParkingLots.Count() > 0
                && !dctDisabledParkingLots.ContainsKey(carWaitingToPark.GetCarId()))
            {
                ParkingLot plReturnedLot = dctDisabledParkingLots.Values.First();
                dctDisabledParkingLots.Remove(plReturnedLot.GetId());
                return plReturnedLot;
            }
            return null;
        }

        public ParkingLot GetSpace(uint uiParkingLotId)
        {
            if (dctNormalParkingLots.ContainsKey(uiParkingLotId))
            {
                ParkingLot plReturnedLot = dctNormalParkingLots[uiParkingLotId];
                dctNormalParkingLots.Remove(uiParkingLotId);
                return plReturnedLot;
            }
            else if (dctDisabledParkingLots.ContainsKey(uiParkingLotId))
            {
                ParkingLot plReturnedLot = dctDisabledParkingLots[uiParkingLotId];
                dctDisabledParkingLots.Remove(uiParkingLotId);
                return plReturnedLot;
            }
            return null;
        }
        #endregion

        #region Utilities
        public void PrintMap()
        {
            Console.WriteLine("  " + "Normal Parking Lots");
            foreach (KeyValuePair<uint, NormalParkingLot> entry in dctNormalParkingLots)
            {
                string message = entry.Key.ToString();
                if (entry.Value.GetCar() != null)
                {
                    message = message + "[" + entry.Value.GetCar().GetCarId() + "]";
                }
                Console.WriteLine("    "+ message);

            }
            Console.WriteLine("  " + "Disabled Parking Lots");
            foreach (KeyValuePair<uint, DisabledParkingLot> entry in dctDisabledParkingLots)
            {
                string message = entry.Key.ToString();
                if (entry.Value.GetCar() != null)
                {
                    message = message + "[" + entry.Value.GetCar().GetCarId() + "]";
                }
                Console.WriteLine("    " + message);

            }
        }
        #endregion
    }

    public class ParkingMap
    {
        protected MyMap availableParkingSpots;
        protected MyMap unavailableParkingSpots = new MyMap();
        protected SortedDictionary<uint, uint> mapCarAgainstParkingSpot = new SortedDictionary<uint, uint>();
        protected Dictionary<uint, Car> mapCarQueue = new Dictionary<uint, Car>();

        #region Constructors/Destructors
        public ParkingMap(MyMap _availableParkingSpots)
        {
            availableParkingSpots = _availableParkingSpots;
        }
        #endregion

        #region RequestParking
        // Request Parking
        //   Move from Available to Unavailable Spots
        //
        public int RequestParking(Car carToPark)
        {
            if (carToPark is null)
            {
                return -1;
            }

            if (!mapCarAgainstParkingSpot.ContainsKey(carToPark.GetCarId()))
            {
                ParkingLot availableParkingLot = availableParkingSpots.GetSpace(carToPark);
                if (availableParkingLot != null)
                {
                    // set car to park
                    //
                    availableParkingLot.SetCar(carToPark);
                    if (unavailableParkingSpots.InsertSpace(availableParkingLot))
                    {
                        mapCarAgainstParkingSpot.Add(carToPark.GetCarId(), availableParkingLot.GetId());
                        return (int)availableParkingLot.GetId();
                    }
                }

                // if unable to add to the parking spots
                //  - add to queue
                //
                if (!mapCarQueue.ContainsKey(carToPark.GetCarId()))
                {
                    mapCarQueue.Add(carToPark.GetCarId(), carToPark);
                }
            }

            return -1;
        }
        protected void ConsumeWaitingList(ParkingLot availableParkingLot)
        {
            if (availableParkingLot is NormalParkingLot)
            {
                RequestParking(DequeueFirstCar(typeof(NormalCar)));                
            }
            else if (availableParkingLot is DisabledParkingLot)
            {
                RequestParking(DequeueFirstCar(typeof(DisabledCar)));
            }
        }
        protected Car DequeueFirstCar(Type carType)
        {
            foreach (KeyValuePair<uint, Car> carIteration in mapCarQueue)
            {
                if (carIteration.Value.GetType() == carType)
                {
                    // remove from the queue and return result
                    //
                    mapCarQueue.Remove(carIteration.Key);
                    return carIteration.Value;
                }
            }
            return null;
        }
        #endregion

        #region ReleaseParking
        // Release Parking
        //   Move from Available to Unavailable Spots
        //
        public bool ReleaseParkingByParkingLotId(uint uidParkingLotId)
        {
            if (mapCarAgainstParkingSpot.ContainsValue(uidParkingLotId))
            {
                return ReleaseParkingCommonLogic(uidParkingLotId);
            }
            return false;
        }
        public bool ReleaseParkingByCarId (uint uidCarId)
        {
            if (mapCarAgainstParkingSpot.ContainsKey(uidCarId))
            {
                return ReleaseParkingCommonLogic(mapCarAgainstParkingSpot[uidCarId]);
            }
            return false;
        }
        protected bool ReleaseParkingCommonLogic (uint uidParkingLotId)
        {
            ParkingLot unavailableParkingLot = unavailableParkingSpots.GetSpace(uidParkingLotId);
            if (unavailableParkingLot != null)
            {
                // reset car to park
                //
                mapCarAgainstParkingSpot.Remove(unavailableParkingLot.GetCar().GetCarId());
                unavailableParkingLot.SetCar(null);
                if (availableParkingSpots.InsertSpace(unavailableParkingLot))
                {
                    // if successfully released, check the waitingline queue
                    //
                    ConsumeWaitingList(unavailableParkingLot);
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Utilities
        public void PrintMap()
        {
            Console.WriteLine("=======================");
            Console.WriteLine("Available Parking Spots");
            Console.WriteLine("=======================");
            availableParkingSpots.PrintMap();

            Console.WriteLine("===========================");
            Console.WriteLine("Not Available Parking Spots");
            Console.WriteLine("===========================");
            unavailableParkingSpots.PrintMap();

            Console.WriteLine("===========================");
            Console.WriteLine("Car::ParkingSpot Map");
            Console.WriteLine("===========================");
            foreach(KeyValuePair<uint, uint> entry in mapCarAgainstParkingSpot)
            {
                Console.WriteLine("  CarID = " + entry.Key + " - Parking Spot ID = " + entry.Value);
            }

            Console.WriteLine("===========================");
            Console.WriteLine("Waiting List Cars");
            Console.WriteLine("===========================");
            foreach (KeyValuePair<uint, Car> entry in mapCarQueue)
            {
                Console.WriteLine("  CarID = " + entry.Value.GetCarId() + " [" + entry.Value.GetType() + "]");
            }
        }
        #endregion
    }
}
