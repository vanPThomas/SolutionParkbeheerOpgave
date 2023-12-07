using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using System;
using ParkDataLayer.Mappers;
using System.Collections.Generic;
using ParkDataLayer.Exceptions;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class HuizenRepositoryEF : IHuizenRepository
    {
        DBContext ctx;

        public HuizenRepositoryEF()
        {
            ctx = new DBContext();
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

                return h != null ? HuisMapper.MapToBusiness(h) : null;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefHuis", ex);
            }
        }

        public bool HeeftHuis(string straat, int nummer, Park park)
        {
            throw new NotImplementedException();
        }

        public bool HeeftHuis(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateHuis(Huis huis)
        {
            throw new NotImplementedException();
        }

        public Huis VoegHuisToe(Huis h)
        {
            throw new NotImplementedException();
        }
    }
}
