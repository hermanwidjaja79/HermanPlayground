using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingLotExercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotExercise.Tests
{
    [TestClass()]
    public class ParkingMapTests
    {
        protected static MyMap PopulateMap()
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

        [TestMethod()]
        public void RequestParkingTest()
        {
        }

        [TestMethod()]
        public void ReleaseParkingByParkingLotIdTest()
        {
            ParkingMap parkingMap = new ParkingMap(PopulateMap());
            Assert.IsFalse(parkingMap.ReleaseParkingByParkingLotId(999999));
        }

        [TestMethod()]
        public void ReleaseParkingByCarIdTest()
        {
            ParkingMap parkingMap = new ParkingMap(PopulateMap());
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(999999));
        }

        [TestMethod()]
        public void UserScenario_1()
        {
            ParkingMap parkingMap = new ParkingMap(PopulateMap());

            // Space is 4 Normal and 2 Disabled Cars
            //
            Assert.AreNotEqual(-1, parkingMap.RequestParking(new NormalCar(12300)));
            Assert.AreNotEqual(-1, parkingMap.RequestParking(new DisabledCar(12301)));
            Assert.AreNotEqual(-1, parkingMap.RequestParking(new NormalCar(12302)));
            Assert.AreNotEqual(-1, parkingMap.RequestParking(new NormalCar(12303)));
            Assert.AreNotEqual(-1, parkingMap.RequestParking(new DisabledCar(12304)));
            Assert.AreNotEqual(-1, parkingMap.RequestParking(new NormalCar(12305)));

            // These will fail but they will be pushed to queue
            //
            Assert.AreEqual(-1, parkingMap.RequestParking(new DisabledCar(12306)));
            Assert.AreEqual(-1, parkingMap.RequestParking(new NormalCar(12307)));

            // Release 1 Normal and 1 Disabled
            //
            Assert.IsTrue(parkingMap.ReleaseParkingByCarId(12300));
            Assert.IsTrue(parkingMap.ReleaseParkingByCarId(12301));

            // Release unknowns
            //
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(12300));
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(12301));

            // Release all
            //
            Assert.IsTrue(parkingMap.ReleaseParkingByCarId(12302));
            Assert.IsTrue(parkingMap.ReleaseParkingByCarId(12303));
            Assert.IsTrue(parkingMap.ReleaseParkingByCarId(12304));
            Assert.IsTrue(parkingMap.ReleaseParkingByCarId(12305));
            Assert.IsTrue(parkingMap.ReleaseParkingByCarId(12306));
            Assert.IsTrue(parkingMap.ReleaseParkingByCarId(12307));

            // Release unknowns
            //
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(12302));
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(12303));
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(12304));
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(12305));
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(12306));
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(12307));


            // Request more
            //
            Assert.AreNotEqual(-1, parkingMap.RequestParking(new DisabledCar(12310)));
            Assert.AreNotEqual(-1, parkingMap.RequestParking(new NormalCar(12311)));
            Assert.AreEqual(-1, parkingMap.RequestParking(new DisabledCar(12310)));
            Assert.AreEqual(-1, parkingMap.RequestParking(new NormalCar(12311)));
            Assert.IsTrue(parkingMap.ReleaseParkingByCarId(12310));
            Assert.IsTrue(parkingMap.ReleaseParkingByCarId(12311));
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(12310));
            Assert.IsFalse(parkingMap.ReleaseParkingByCarId(12311));
        }
    }
}