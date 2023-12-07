using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers
{
    public class HuisMapper
    {
        public static Huis MapToBusiness(HuisEF hef)
        {
            return new Huis();
        }
    }
}
