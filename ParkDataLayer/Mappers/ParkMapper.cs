using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using ParkBusinessLayer.Exceptions;
using ParkDataLayer.Exceptions;
using System.Linq;
using ParkDataLayer.Repositories;
using System.Collections.Generic;

namespace ParkDataLayer.Mappers
{
    public class ParkMapper
    {
        public static Park MapToBusiness(ParkEF parkEF, Park park)
        {
            try
            {
                if (parkEF == null)
                    throw new MapperException("MapToBusiness - ParkEF is null");

                if (park == null)
                {
                    park = new Park(parkEF.Id, parkEF.Naam, parkEF.Locatie);
                    List<Huis> huizen = parkEF._huis
                        .Select(huisEF => HuisMapper.MapToBusiness(huisEF, park))
                        .ToList();
                    huizen.ForEach(huis => park.VoegHuisToe(huis));
                    return park;
                }
                else
                {
                    return park;
                }
            }
            catch (System.Exception ex)
            {
                throw new MapperException("MapToBusiness", ex);
            }
        }

        //public static Park MapToBusinessForHuis(ParkEF parkEF, HuisEF huisEF)
        //{
        //    try
        //    {
        //        if (parkEF == null)
        //            throw new MapperException("MapToBusiness - ParkEF is null");
        //        if (huisEF == null)
        //        {
        //            return new Park(parkEF.Id, parkEF.Naam, parkEF.Locatie);
        //        }
        //        else
        //        {
        //            if (parkEF._huis.Count == 0)
        //            {
        //                List<Huis> huizen = new List<Huis>();
        //                return new Park(parkEF.Id, parkEF.Naam, parkEF.Locatie, huizen);
        //            }
        //            else
        //            {
        //                List<Huis> huizen = new List<Huis>();

        //                foreach (HuisEF h in parkEF._huis)
        //                {
        //                    huizen.Add(HuisMapper.MapToBusiness(h, parkEF));
        //                }

        //                return new Park(parkEF.Id, parkEF.Naam, parkEF.Locatie, huizen);
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw new MapperException("MapToBusiness", ex);
        //    }
        //}

        public static ParkEF MapToData(Park park, ParkRepository parkRepository)
        {
            try
            {
                if (park == null)
                    throw new MapperException("MapToData - Park is null");

                ParkEF existingPark = parkRepository.GetOrCreatePark(park.Id);

                if (existingPark != null)
                {
                    return existingPark;
                }
                else
                {
                    return new ParkEF
                    {
                        Id = park.Id,
                        Naam = park.Naam,
                        Locatie = park.Locatie,
                        _huis = park.Huizen()
                            .Select(huis => HuisMapper.MapToData(huis, parkRepository))
                            .ToList()
                    };
                }
            }
            catch (System.Exception ex)
            {
                throw new MapperException("MapToData", ex);
            }
        }
    }
}
