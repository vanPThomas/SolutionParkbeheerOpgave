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
    public class ContractenRepositoryEF : IContractenRepository
    {
        DBContext ctx;

        public ContractenRepositoryEF()
        {
            ctx = new DBContext();
        }

        public void AnnuleerContract(Huurcontract contract)
        {
            try
            {
                HuurcontractEF contractEF = ctx.Huurcontracten.FirstOrDefault(
                    c => c.Id == contract.Id
                );

                if (contractEF != null)
                {
                    ctx.Huurcontracten.Remove(contractEF);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("AnnuleerContract", ex);
            }
        }

        public Huurcontract GeefContract(string id)
        {
            try
            {
                HuurcontractEF contractEF = ctx.Huurcontracten
                    .Include(h => h.Huurder)
                    .Include(h => h.Huis)
                    .FirstOrDefault(c => c.Id == id);

                return contractEF != null ? HuurcontractMapper.MapToBusiness(contractEF) : null;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefContract", ex);
            }
        }

        public List<Huurcontract> GeefContracten(DateTime dtBegin, DateTime? dtEinde)
        {
            try
            {
                List<HuurcontractEF> contractenEF = ctx.Huurcontracten
                    .Include(h => h.Huurder)
                    .Include(h => h.Huis)
                    .Where(
                        c =>
                            c.StartDatum >= dtBegin
                            && (!dtEinde.HasValue || c.EindDatum <= dtEinde.Value)
                    )
                    .ToList();

                return contractenEF.Select(HuurcontractMapper.MapToBusiness).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GeefContracten", ex);
            }
        }

        public bool HeeftContract(DateTime startDatum, int huurderid, int huisid)
        {
            try
            {
                return ctx.Huurcontracten.Any(
                    c =>
                        c.StartDatum == startDatum
                        && c.Huurder.Id == huurderid
                        && c.Huis.Id == huisid
                );
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftContract", ex);
            }
        }

        public bool HeeftContract(string id)
        {
            try
            {
                return ctx.Huurcontracten.Any(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("HeeftContract", ex);
            }
        }

        public void UpdateContract(Huurcontract contract)
        {
            try
            {
                HuurcontractEF contractEF = ctx.Huurcontracten.FirstOrDefault(
                    c => c.Id == contract.Id
                );

                if (contractEF != null)
                {
                    contractEF.StartDatum = contract.Huurperiode.StartDatum;
                    contractEF.EindDatum = contract.Huurperiode.EindDatum;
                    contractEF.Aantaldagen = contract.Huurperiode.Aantaldagen;
                    // Save changes
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateContract", ex);
            }
        }

        public void VoegContractToe(Huurcontract contract)
        {
            try
            {
                HuurcontractEF contractEF = HuurcontractMapper.MapToData(contract);

                ctx.Huurcontracten.Add(contractEF);

                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegContractToe", ex);
            }
        }
    }
}
