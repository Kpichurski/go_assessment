using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using WebExperience.Test.Models;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// CSV processing test
    /// </summary>
    public class CsvProcessingTest : ITest
    {
        public void Run()
        {
            // TODO: 
            // Create a domain model via POCO classes to store the data available in the CSV file below
            // Objects to be present in the domain model: Asset, Country and Mime type
            // Process the file in the most robust way possible
            // The use of 3rd party plugins is permitted
            try
            {
                using (DBEntities entites = new DBEntities())
                {
                    var csvFile = Resources.AssetImport.Split(new string[] { "\n" }, StringSplitOptions.None).Skip(1).ToList();
                    List<Asset> asset = new List<Asset>();
                    foreach (var line in csvFile)
                    {
                        var records = line.Split(new char[] { ',' }, 7);
                        if (records[0] == "")
                            continue;

                        asset.Add(new Asset
                        {
                            AssetId = records[0],
                            FileName = records[1],
                            MimeType = records[2],
                            Email = records[4],
                            Country = records[5],
                            Description = records[6]
                        });
                    }
                    
                    entites.Asset.AddRange(asset);
                    entites.SaveChanges();
                }
            }
            catch (EntityException)
            {
                 Console.WriteLine("Failed connect to database!");
            }



        }
    }

}
