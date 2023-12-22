using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Mappers
{
    public class HuisMapper
    {
        public static Huis MapToBusiness(HuisEF huisEF)
        {
            try
            {
                if (huisEF == null)
                    return null;

                Dictionary<Huurder, List<Huurcontract>> _huurcontracten =
                    new Dictionary<Huurder, List<Huurcontract>>();

                foreach (HuurcontractEF huurcontractEF in huisEF.Huurcontracten)
                {
                    Huurder huurder = HuurderMapper.MapToBusiness(huurcontractEF.Huurder);
                    Huurcontract huurcontract = HuurcontractMapper.MapToBusiness(huurcontractEF);
                    bool isContained = false;
                    foreach (Huurder h in _huurcontracten.Keys)
                    {
                        if (h.Id == huurder.Id)
                        {
                            isContained = true;
                            break;
                        }
                    }
                    if (!isContained)
                    {
                        List<Huurcontract> huurcontracten = huisEF.Huurcontracten
                            .Select(
                                huurcontractEF => HuurcontractMapper.MapToBusiness(huurcontractEF)
                            )
                            .ToList();
                        _huurcontracten.Add(huurder, huurcontracten);
                    }
                }

                return new Huis(
                    huisEF.Id,
                    huisEF.Straat,
                    huisEF.Nr,
                    huisEF.Actief,
                    ParkMapper.MapToBusiness(huisEF.Park),
                    _huurcontracten
                );
            }
            catch (Exception ex)
            {
                throw new MapperException("Error mapping HuisEF to Huis.", ex);
            }
        }

        public static HuisEF MapToData(Huis huis)
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
                    Park = ParkMapper.MapToData(huis.Park),
                    Huurcontracten = MapHuurcontracten(huurcontracten)
                };
            }
            catch (Exception ex)
            {
                throw new MapperException("Error mapping Huis to HuisEF.", ex);
            }
        }

        private static ICollection<HuurcontractEF> MapHuurcontracten(
            List<Huurcontract> huurcontracten
        )
        {
            var result = new List<HuurcontractEF>();

            if (huurcontracten != null)
            {
                foreach (var huurcontract in huurcontracten)
                {
                    result.Add(HuurcontractMapper.MapToData(huurcontract));
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
