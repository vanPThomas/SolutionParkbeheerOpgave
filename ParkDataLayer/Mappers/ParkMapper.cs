using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using ParkBusinessLayer.Exceptions;
using ParkDataLayer.Exceptions;
using System.Linq;

namespace ParkDataLayer.Mappers
{
    public class ParkMapper
    {
        public static Park MapToBusiness(ParkEF parkEF)
        {
            try
            {
                if (parkEF == null)
                    throw new MapperException("MapToBusiness - ParkEF is null");

                return new Park(
                    parkEF.Id,
                    parkEF.Naam,
                    parkEF.Locatie,
                    parkEF._huis.Select(huisEF => HuisMapper.MapToBusiness(huisEF)).ToList()
                );
            }
            catch (System.Exception ex)
            {
                throw new MapperException("MapToBusiness", ex);
            }
        }

        public static ParkEF MapToData(Park park)
        {
            try
            {
                if (park == null)
                    throw new MapperException("MapToData - Park is null");

                return new ParkEF
                {
                    Id = park.Id,
                    Naam = park.Naam,
                    Locatie = park.Locatie,
                    _huis = park.Huizen().Select(huis => HuisMapper.MapToData(huis)).ToList()
                };
            }
            catch (System.Exception ex)
            {
                throw new MapperException("MapToData", ex);
            }
        }
    }
}
