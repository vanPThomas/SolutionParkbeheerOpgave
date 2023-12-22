using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using ParkDataLayer.Mappers;
using ParkDataLayer.Exceptions;
using System;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class HuizenRepositoryEF : IHuizenRepository
    {
        DBContext ctx;
        ParkRepository parkRepo;

        public HuizenRepositoryEF(ParkRepository repo, DBContext ctx)
        {
            this.ctx = ctx;
            parkRepo = repo;
        }

        public Huis GeefHuis(int id)
        {
            try
            {
                HuisEF h = ctx.Huizen
                    .Include(p => p.Park)
                    .ThenInclude(_h => _h._huis)
                    .ThenInclude(hc => hc.Huurcontracten)
                    .ThenInclude(hu => hu.Huurder)
                    .FirstOrDefault(h => h.Id == id);

                return h != null ? HuisMapper.MapToBusiness(h, null) : null;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefHuis", ex);
            }
        }

        public bool HeeftHuis(string straat, int nummer, Park park)
        {
            try
            {
                return ctx.Huizen.Any(
                    h => h.Straat == straat && h.Nr == nummer && h.Park.Id == park.Id
                );
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftHuis", ex);
            }
        }

        public bool HeeftHuis(int id)
        {
            try
            {
                return ctx.Huizen.Any(h => h.Id == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftHuis", ex);
            }
        }

        public void UpdateHuis(Huis huis)
        {
            try
            {
                HuisEF huisEF = ctx.Huizen.Find(huis.Id);

                huisEF.Straat = huis.Straat;
                huisEF.Nr = huis.Nr;
                huisEF.Actief = huis.Actief;

                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateHuis", ex);
            }
        }

        public Huis VoegHuisToe(Huis h)
        {
            try
            {
                HuisEF huisEF = HuisMapper.MapToData(h, parkRepo);

                ctx.Huizen.Add(huisEF);

                ctx.SaveChanges();

                return HuisMapper.MapToBusiness(huisEF, null);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegHuisToe", ex);
            }
        }
    }
}
