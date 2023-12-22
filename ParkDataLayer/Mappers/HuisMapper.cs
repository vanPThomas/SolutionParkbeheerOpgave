using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using ParkDataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Mappers
{
    public class HuisMapper
    {
        public static Huis MapToBusiness(HuisEF huisEF, Park park)
        {
            try
            {
                if (huisEF == null)
                    return null;

                Dictionary<Huurder, List<Huurcontract>> _huurcontracten =
                    new Dictionary<Huurder, List<Huurcontract>>();

                if (huisEF.Huurcontracten.Count > 0)
                {
                    _huurcontracten = huisEF.Huurcontracten
                        .GroupBy(hcEF => HuurderMapper.MapToBusiness(hcEF.Huurder))
                        .ToDictionary(
                            group => group.Key,
                            group => group.Select(HuurcontractMapper.MapToBusiness).ToList()
                        );
                }

                return new Huis(
                    huisEF.Id,
                    huisEF.Straat,
                    huisEF.Nr,
                    huisEF.Actief,
                    park != null ? park : ParkMapper.MapToBusiness(huisEF.Park, park),
                    _huurcontracten
                );
            }
            catch (Exception ex)
            {
                throw new MapperException("Error mapping HuisEF to Huis.", ex);
            }
        }

        public static Huis MapToBusinessForHuurcontract(HuisEF huisEF)
        {
            try
            {
                if (huisEF == null)
                    return null;

                if (huisEF.Park != null)
                {
                    return new Huis(
                        huisEF.Id,
                        huisEF.Straat,
                        huisEF.Nr,
                        huisEF.Actief,
                        ParkMapper.MapToBusiness(huisEF.Park, null)
                    );
                }
                else
                {
                    return new Huis(huisEF.Id, huisEF.Straat, huisEF.Nr, huisEF.Actief, null);
                }
            }
            catch (Exception ex)
            {
                throw new MapperException("Error mapping HuisEF to Huis.", ex);
            }
        }

        public static HuisEF MapToData(Huis huis, ParkRepository parkRepo)
        {
            try
            {
                if (huis == null)
                    return null;
                List<Huurcontract> huurcontracten = huis.Huurcontracten().ToList();

                return new HuisEF
                {
                    Id = huis.Id,
                    Straat = huis.Straat,
                    Nr = huis.Nr,
                    Actief = huis.Actief,
                    Park = ParkMapper.MapToData(huis.Park, parkRepo),
                    Huurcontracten = MapHuurcontracten(huurcontracten, parkRepo)
                };
            }
            catch (Exception ex)
            {
                throw new MapperException("Error mapping Huis to HuisEF.", ex);
            }
        }

        private static ICollection<HuurcontractEF> MapHuurcontracten(
            List<Huurcontract> huurcontracten,
            ParkRepository parkRepo
        )
        {
            var result = new List<HuurcontractEF>();

            if (huurcontracten != null)
            {
                foreach (var huurcontract in huurcontracten)
                {
                    result.Add(HuurcontractMapper.MapToData(huurcontract, parkRepo));
                }
            }

            return result;
        }

        private static IReadOnlyList<Huurcontract> MapHuurcontracten(
            ICollection<HuurcontractEF> huurcontractenEF
        )
        {
            var result = new List<Huurcontract>();

            if (huurcontractenEF != null)
            {
                foreach (var huurcontractEF in huurcontractenEF)
                {
                    result.Add(HuurcontractMapper.MapToBusiness(huurcontractEF));
                }
            }

            return result.AsReadOnly();
        }
    }
}
