using PADataToDatabase.DBContext;
using PADataToDatabase.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PADataToDatabase
{
    class Program
    {
        const string DIRECTORY = @"C:\PA DATA";

        static void Main(string[] args)
        {
            Console.WriteLine("Program started.");

            CultureInfo culture = new CultureInfo("en-us");

            DirectoryInfo di = new DirectoryInfo(DIRECTORY);
            FileInfo[] files = di.GetFiles("*.txt");

            /// split files into their types: 
            /// DISTRICT election map
            /// DISTRICT zone codes
            /// DISTRICT zone types
            /// DISTRICT FVE
            var electionMapsTexts = files.Where(f => f.Name.Contains("Election Map"));
            var zoneCodesTexts = files.Where(f => f.Name.Contains("Zone Codes"));
            var zoneTypesTexts = files.Where(f => f.Name.Contains("Zone Types"));
            var FVEsTexts = files.Where(f => f.Name.Contains("FVE"));

            List<ElectionMap> ElectionMapsData = new List<ElectionMap>();
            List<ZoneCode> ZoneCodesData = new List<ZoneCode>();
            List<ZoneType> ZoneTypesData = new List<ZoneType>();
            List<Fve> FveData = new List<Fve>();

            foreach(FileInfo electionMapText in electionMapsTexts)
            {
                using (StreamReader fs = new StreamReader(electionMapText.FullName))
                {
                    string line;
                    while (!String.IsNullOrWhiteSpace(line = fs.ReadLine()))
                    {
                        string[] items = line.Split('\t');

                        ElectionMapsData.Add(new ElectionMap()
                        {
                            County = items[0].Trim('"'),
                            ElectionIndex = Int32.Parse(items[1]),
                            ElectionName = items[2].Trim('"'),
                            When = DateTime.ParseExact(items[3].Trim('"'), "MM/dd/yyyy", culture)
                        });
                    }
                }
            }

            foreach (FileInfo zoneCodeText in zoneCodesTexts)
            {
                using (StreamReader fs = new StreamReader(zoneCodeText.FullName))
                {
                    string line;
                    while (!String.IsNullOrWhiteSpace(line = fs.ReadLine()))
                    {
                        string[] items = line.Split('\t');

                        ZoneCodesData.Add(new ZoneCode()
                        {
                            County = items[0].Trim('"'),
                            TypeIndex = Int32.Parse(items[1]),
                            Code = items[2].Trim('"'),
                            Index = items[3].Trim('"')
                        });
                    }
                }
            }

            foreach (FileInfo zoneTypeText in zoneTypesTexts)
            {
                using (StreamReader fs = new StreamReader(zoneTypeText.FullName))
                {
                    string line;
                    while (!String.IsNullOrWhiteSpace(line = fs.ReadLine()))
                    {
                        string[] items = line.Split('\t');

                        ZoneTypesData.Add(new ZoneType()
                        {
                            County = items[0].Trim('"'),
                            TypeIndex = Int32.Parse(items[1]),
                            Code = items[2].Trim('"'),
                            Name = items[3].Trim('"')
                        });
                    }
                }
            }

            using (PADataContext databaseContext = new PADataContext())
            {
                databaseContext.ElectionMaps.AddRange(ElectionMapsData);
                databaseContext.ZoneCodes.AddRange(ZoneCodesData);
                databaseContext.ZoneTypes.AddRange(ZoneTypesData);

                databaseContext.SaveChanges();
            }

            foreach (FileInfo FVEsText in FVEsTexts)
            {
                using (StreamReader fs = new StreamReader(FVEsText.FullName))
                {
                    string line;
                    while (!String.IsNullOrWhiteSpace(line = fs.ReadLine()))
                    {
                        string[] items = line.Split('\t');

                        FveData.Add(new Fve()
                        {
                            RegistrationCode = items[0].Trim('"'),
                            Title = items[1].Trim('"'),
                            LastName = items[2].Trim('"'),
                            FirstName = items[3].Trim('"'),
                            MiddleName = items[4].Trim('"'),
                            Suffix = items[5].Trim('"'),
                            Gender = items[6].Trim('"').FirstOrDefault(),
                            DateOfBirth = String.IsNullOrWhiteSpace(items[7]) ? null : (DateTime?)DateTime.ParseExact(items[7], "MM/dd/yyyy", culture),
                            RegistrationDate = String.IsNullOrWhiteSpace(items[8]) ? null : (DateTime?)DateTime.ParseExact(items[8], "MM/dd/yyyy", culture),
                            VoterStatus = items[9].Trim('"').FirstOrDefault() == 'A',
                            StatusChangeDate = String.IsNullOrWhiteSpace(items[10]) ? null : (DateTime?)DateTime.ParseExact(items[10], "MM/dd/yyyy", culture),
                            Affiliation = items[11].Trim('"').FirstOrDefault(),
                            HouseNumber = items[12].Trim('"'),
                            HouseNumberSuffix = items[13].Trim('"'),
                            Street = items[14].Trim('"'),
                            ApartmentNumber = items[15].Trim('"'),
                            AddressLineTwo = items[16].Trim('"'),
                            City = items[17].Trim('"'),
                            State = items[18].Trim('"'),
                            PostalCode =  Int32.TryParse(items[19].Trim('"'), out int postalCode) ? postalCode : 0,
                            MailAddress1 = items[20].Trim('"'),
                            MailAddress2 = items[21].Trim('"'),
                            MailCity = items[22].Trim('"'),
                            MailState = items[23].Trim('"'),
                            MailZip = items[24].Trim('"'),
                            LastVoteDate = String.IsNullOrWhiteSpace(items[25]) ? null : (DateTime?)DateTime.ParseExact(items[25], "MM/dd/yyyy", culture),
                            PrecintCode = items[26].Trim('"'),
                            PrecintSplitId = items[27].Trim('"'),
                            DateLastChanged = String.IsNullOrWhiteSpace(items[28]) ? null : (DateTime?)DateTime.ParseExact(items[28], "MM/dd/yyyy", culture),
                            LegacySystemIdNumber = items[29].Trim('"'),

                            VotingMethod = items[70].Trim('"'),
                            VotingParty = items[71].Trim('"')
                        });
                    }
                }

                using (PADataContext databaseContext = new PADataContext())
                {
                    databaseContext.Fves.AddRange(FveData);
                    databaseContext.SaveChanges();

                    FveData.Clear();
                }
            }
        }
    }
}
