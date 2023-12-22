using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using ParkDataLayer.Mappers;
using ParkDataLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class HuurderRepositoryEF : IHuurderRepository
    {
        DBContext ctx;

        public HuurderRepositoryEF()
        {
            ctx = new DBContext();
        }

        public Huurder GeefHuurder(int id)
        {
            try
            {
                HuurderEF huurderEF = ctx.Huurders.FirstOrDefault(h => h.Id == id);

                return huurderEF != null ? HuurderMapper.MapToBusiness(huurderEF) : null;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefHuurder", ex);
            }
        }

        public List<Huurder> GeefHuurders(string naam)
        {
            try
            {
                List<HuurderEF> huurdersEF = ctx.Huurders
                    .Where(h => h.Naam.Contains(naam))
                    .ToList();

                return huurdersEF.Select(HuurderMapper.MapToBusiness).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefHuurders", ex);
            }
        }

        public bool HeeftHuurder(string naam, Contactgegevens contact)
        {
            try
            {
                return ctx.Huurders.Any(
                    h =>
                        h.Naam == naam
                        && h.Email == contact.Email
                        && h.Tel == contact.Tel
                        && h.Adres == contact.Adres
                );
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftHuurder", ex);
            }
        }

        public bool HeeftHuurder(int id)
        {
            try
            {
                return ctx.Huurders.Any(h => h.Id == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftHuurder", ex);
            }
        }

        public void UpdateHuurder(Huurder huurder)
        {
            try
            {
                HuurderEF huurderEF = ctx.Huurders.FirstOrDefault(h => h.Id == huurder.Id);

                if (huurderEF != null)
                {
                    huurderEF.Naam = huurder.Naam;
                    huurderEF.Email = huurder.Contactgegevens.Email;
                    huurderEF.Tel = huurder.Contactgegevens.Tel;
                    huurderEF.Adres = huurder.Contactgegevens.Adres;

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateHuurder", ex);
            }
        }

        public Huurder VoegHuurderToe(Huurder h)
        {
            try
            {
                HuurderEF huurderEF = HuurderMapper.MapToData(h);

                ctx.Huurders.Add(huurderEF);

                ctx.SaveChanges();

                return HuurderMapper.MapToBusiness(huurderEF);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegHuurderToe", ex);
            }
        }
    }
}
