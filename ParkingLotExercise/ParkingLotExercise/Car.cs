using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotExercise
{
    public class Car
    {
        protected uint uiCarId;

        public Car (uint _uiCarId)
        {
            uiCarId = _uiCarId;
        }

        public uint GetCarId()
        {
            return uiCarId;
        }
    }

    public class NormalCar : Car
    {
        public NormalCar(uint _uiCarId) : base(_uiCarId)
        {

        }
    }

    public class DisabledCar : Car
    {
        public DisabledCar(uint _uiCarId) : base(_uiCarId)
        {

        }
    }
}
