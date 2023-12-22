using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;

namespace ParkDataLayer.Mappers
{
    public class HuurcontractMapper
    {
        public static Huurcontract MapToBusiness(HuurcontractEF contractEF)
        {
            try
            {
                if (contractEF == null)
                    return null;

                return new Huurcontract(
                    contractEF.Id,
                    new Huurperiode(contractEF.StartDatum, contractEF.Aantaldagen),
                    HuurderMapper.MapToBusiness(contractEF.Huurder),
                    HuisMapper.MapToBusiness(contractEF.Huis)
                );
            }
            catch (Exception ex)
            {
                throw new MapperException("Error mapping HuurcontractEF to Huurcontract.", ex);
            }
        }

        public static HuurcontractEF MapToData(Huurcontract contract)
        {
            try
            {
                if (contract == null)
                    return null;

                return new HuurcontractEF
                {
                    Id = contract.Id,
                    Huurder = HuurderMapper.MapToData(contract.Huurder),
                    Huis = HuisMapper.MapToData(contract.Huis),
                    StartDatum = contract.Huurperiode.StartDatum,
                    EindDatum = contract.Huurperiode.EindDatum,
                    Aantaldagen = contract.Huurperiode.Aantaldagen
                };
            }
            catch (Exception ex)
            {
                throw new MapperException("Error mapping Huurcontract to HuurcontractEF.", ex);
            }
        }
    }
}
