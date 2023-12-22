using ParkBusinessLayer.Beheerders;
using ParkBusinessLayer.Exceptions;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using ParkDataLayer.Repositories;

namespace ParksConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DBContext ctx = new DBContext();
            //create Repos
            ParkRepository parkRepo = new ParkRepository(ctx);
            IContractenRepository contractenRepo = new ContractenRepositoryEF(parkRepo, ctx);
            IHuizenRepository huizenRepo = new HuizenRepositoryEF(parkRepo, ctx);
            //create beheerders
            BeheerContracten beheerContracten = new BeheerContracten(contractenRepo);
            BeheerHuizen beheerHuizen = new BeheerHuizen(huizenRepo);

            Park park = new Park("2", "Sample Parkx", "Sample Locationx");

            try
            {
                //// Test adding a new house
                //beheerHuizen.VoegNieuwHuisToe("Sample Streetx", 123, park);

                // Test updating a house
                Huis existingHuis = beheerHuizen.GeefHuis(3);
                existingHuis.ZetStraat("Updated Streetx");
                beheerHuizen.UpdateHuis(existingHuis);

                //// Test archiving a house
                //Huis houseToArchive = beheerHuizen.GeefHuis(1);
                //beheerHuizen.ArchiveerHuis(houseToArchive);

                //// Test getting a house
                //Huis retrievedHuis = beheerHuizen.GeefHuis(1);
                //Console.WriteLine($"Retrieved House: {retrievedHuis.Id}, {retrievedHuis.Straat}");
            }
            catch (BeheerderException ex)
            {
                Console.WriteLine($"BeheerderException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            ///////////////////////////////////////////////////////////


            var huurperiode = new Huurperiode(DateTime.Now, 7);
            var huurder = new Huurder(
                "John Doex",
                new Contactgegevens("john@example.comx", "123456789", "123 Main Stx")
            );
            var huis = new Huis("TestStraatx", 123, new Park("3", "TestParkx", "TestLocationx"));

            try
            {
                // Test creating a new contract
                //beheerContracten.MaakContract("3", huurperiode, huurder, huis);

                //// Test getting a contract
                //Huurcontract retrievedContract = beheerContracten.GeefContract("1");
                //Console.WriteLine(
                //    $"Retrieved Contract: {retrievedContract.Id}, {retrievedContract.Huurperiode.StartDatum}"
                //);

                //// Test updating a contract
                //retrievedContract.ZetHuurperiode(new Huurperiode(DateTime.Now.AddDays(1), 14));
                //beheerContracten.UpdateContract(retrievedContract);

                //// Test canceling a contract
                //beheerContracten.AnnuleerContract(retrievedContract);

                //// Verify the cancellation
                //bool hasContract = contractenRepo.HeeftContract("3");
                //Console.WriteLine($"Contract exists after cancellation: {hasContract}");
            }
            catch (BeheerderException ex)
            {
                Console.WriteLine($"BeheerderException: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
