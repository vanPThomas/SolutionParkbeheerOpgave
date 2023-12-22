using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;

namespace ParkDataLayer.Mappers
{
    public class HuurderMapper
    {
        public static Huurder MapToBusiness(HuurderEF huurderEF)
        {
            try
            {
                if (huurderEF == null)
                    return null;

                return new Huurder(
                    huurderEF.Id,
                    huurderEF.Naam,
                    new Contactgegevens(huurderEF.Email, huurderEF.Tel, huurderEF.Adres)
                );
            }
            catch (Exception ex)
            {
                throw new MapperException("Error mapping HuurderEF to Huurder.", ex);
            }
        }

        public static HuurderEF MapToData(Huurder h)
        {
            try
            {
                if (h == null)
                    return null;

                return new HuurderEF
                {
                    Id = h.Id,
                    Naam = h.Naam,
                    Email = h.Contactgegevens.Email,
                    Tel = h.Contactgegevens.Tel,
                    Adres = h.Contactgegevens.Adres
                };
            }
            catch (Exception ex)
            {
                throw new MapperException("Error mapping Huurder to HuurderEF.", ex);
            }
        }
    }
}
