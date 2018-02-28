using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotExercise
{
    public class ParkingLot
    {
        protected uint uiId;           // spot id
        protected Car carParkedCar;    // parked car

        public ParkingLot (uint _uiId)
        {
            uiId = _uiId;
            carParkedCar = null;
        }

        public uint GetId()
        {
            return uiId;
        }

        public Car GetCar()
        {
            return carParkedCar;
        }
        public void SetCar(Car _carParkedCar)
        {
            carParkedCar = _carParkedCar;
        }
    }

    public class NormalParkingLot : ParkingLot
    {
        public NormalParkingLot (uint _uiId) : base (_uiId)
        {

        }
    }

    public class DisabledParkingLot : ParkingLot
    {
        public DisabledParkingLot(uint _uiId) : base(_uiId)
        {

        }
    }
}
