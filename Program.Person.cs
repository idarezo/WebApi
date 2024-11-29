using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
namespace OZRA_vaje2
{
    public class Program
    {
        public static void Users(WebApplication app)
        {
            // Console.WriteLine("test");

            string connectionString = "mongodb://127.0.0.1:27017";
            string databaseName = "ORZ_db";
            string collectionName = "tekmovanja";
            string collectionName1 = "tekmovanja_2";


            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<Tekmovanje>(collectionName);
            var collection1 = db.GetCollection<Tekmovanje_1>(collectionName1);

            //Vsa tekmovanja ene vrste

            app.MapGet("/tekmovanja", () =>
            {
               
                var projection = Builders<Tekmovanje>.Projection
                    .Exclude(t => t.rezultati); // Exclude the rezultati array

                List<Tekmovanje> tekmovanja = db.GetCollection<Tekmovanje>(collectionName)
                    .Find(Builders<Tekmovanje>.Filter.Empty)
                    .Project<Tekmovanje>(projection)
   
                    .ToList();

                return tekmovanja;
            });

            //Vsa tekmovanje druge vrste
            app.MapGet("/tekmovanjeDrugeVrste", () =>
            {
                var projection = Builders<Tekmovanje_1>.Projection
                    .Exclude(t => t.rezultati); // Exclude the rezultati array

                List<Tekmovanje_1> tekmovanja = db.GetCollection<Tekmovanje_1>(collectionName1)
                    .Find(Builders<Tekmovanje_1>.Filter.Empty)
                    .Project<Tekmovanje_1>(projection)
                   
                    .ToList();

                return tekmovanja;
            });

        
            //Tekmovanje na podlagi specificnega ID-ja

            app.MapGet("/tekmovanjeID/{id}", (string id) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);


                var objectId = ObjectId.Parse(id);

                var filter = Builders<Tekmovanje>.Filter.Eq("_id", objectId);



                var projection = Builders<Tekmovanje>.Projection
                .Exclude(t => t.rezultati); // Exclude the rezultati array

                var tekmovanje = collection.Find(filter)
                    .Project<Tekmovanje>(projection)
                    .FirstOrDefault();

                if (tekmovanje != null)
                {

                    return tekmovanje;
                }
                else
                {

                    throw new Exception("Tekmovanje not found.");
                }
            });


            app.MapGet("/tekmovanjeIDDrugo/{id}", (string id) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);


                var objectId = ObjectId.Parse(id);

                var filter = Builders<Tekmovanje_1>.Filter.Eq("_id", objectId);



                var projection = Builders<Tekmovanje_1>.Projection
                .Exclude(t => t.rezultati); // Exclude the rezultati array

                var tekmovanje = collection.Find(filter)
                    .Project<Tekmovanje_1>(projection)
                    .FirstOrDefault();

                if (tekmovanje != null)
                {

                    return tekmovanje;
                }
                else
                {

                    throw new Exception("Tekmovanje not found.");
                }
            });



            //Drzava kjer se je dogajalo tekmovanje z določenim ID-jom
            app.MapGet("/tekmovanjeDrugoIDdrzava/{id}", (string id) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);


                var objectId = ObjectId.Parse(id);

                var filter = Builders<Tekmovanje_1>.Filter.Eq("_id", objectId);

                var projection = Builders<Tekmovanje_1>.Projection
                .Exclude(t => t.rezultati); // Exclude the rezultati array

                var tekmovanje = collection.Find(filter)
                    .Project<Tekmovanje_1>(projection)
                    .FirstOrDefault();


                if (tekmovanje != null)
                {

                    return tekmovanje.drzava;
                }
                else
                {

                    throw new Exception("Tekmovanje not found.");
                }
            });

            //Tekmovanja, ki so se dogajala v specificnem casovnem obdobju(po dolocenem letu)

            app.MapGet("/tekmovanjeCas/{datumVnos}", async (string datumVnos) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);
                int pogoj = int.TryParse(datumVnos, out int result) ? result : 0;
                var filter = Builders<Tekmovanje>.Filter.Empty;
                var projection = Builders<Tekmovanje>.Projection
                .Exclude(t => t.rezultati); // Exclude the rezultati array

                var cursor = await collection.Find(filter)
                    .Project<Tekmovanje>(projection)
                    .ToListAsync();

                //var cursor = await collection.Find(filter).ToListAsync();
                List<Tekmovanje> tekmovanjePravo = new List<Tekmovanje>();

                foreach (var document in cursor)
                {
                    var datum_izvedba = document.leto_izvedbe;
                    int datum = 0;
                    if (int.TryParse(datum_izvedba, out datum))
                    {
                        if (datum > pogoj)
                        {
                            tekmovanjePravo.Add(document);
                        }

                    }

                }

                return tekmovanjePravo;
            });


            app.MapGet("/tekmovanjeCasDrugo/{datumVnos}", async (string datumVnos) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);
                int pogoj = int.TryParse(datumVnos, out int result) ? result : 0;
                var filter = Builders<Tekmovanje_1>.Filter.Empty;
                var projection = Builders<Tekmovanje_1>.Projection
                .Exclude(t => t.rezultati); // Exclude the rezultati array

                var cursor = await collection.Find(filter)
                    .Project<Tekmovanje_1>(projection)
                    .ToListAsync();

                //var cursor = await collection.Find(filter).ToListAsync();
                List<Tekmovanje_1> tekmovanjePravo = new List<Tekmovanje_1>();

                foreach (var document in cursor)
                {
                    var datum_izvedba = document.leto_izvedbe;
                    int datum = 0;
                    if (int.TryParse(datum_izvedba, out datum))
                    {
                        if (datum > pogoj)
                        {
                            tekmovanjePravo.Add(document);
                        }

                    }

                }

                return tekmovanjePravo;
            });

            //Vsa tekmovanja, ki so dogajala v specificni drzavi.
            app.MapGet("/tekmovanjaDrzava/{drzava}", async (string drzava) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);

                var filter = Builders<Tekmovanje>.Filter.Empty;

                var projection = Builders<Tekmovanje>.Projection.Exclude(t => t.rezultati);

                var cursor = await collection.Find(filter)
                    .Project<Tekmovanje>(projection)
                    .ToListAsync();
                List<Tekmovanje> tekmovanjePravo = new List<Tekmovanje>();

                foreach (var document in cursor)
                {

                    if (document.drzava == drzava)
                    {
                        tekmovanjePravo.Add(document);

                    }

                }

                return tekmovanjePravo;
            });

            app.MapGet("/tekmovanjaDrzavaDrugo/{drzava}", async (string drzava) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);

                var filter = Builders<Tekmovanje_1>.Filter.Empty;

                var projection = Builders<Tekmovanje_1>.Projection.Exclude(t => t.rezultati);

                var cursor = await collection.Find(filter)
                    .Project<Tekmovanje_1>(projection)
                    .ToListAsync();
                List<Tekmovanje_1> tekmovanjePravo = new List<Tekmovanje_1>();

                foreach (var document in cursor)
                {

                    if (document.drzava == drzava)
                    {
                        tekmovanjePravo.Add(document);

                    }

                }

                return tekmovanjePravo;
            });

            //Iskanje specificnega igralca v dolocenem tekmovanju na podlagi ID-ja
            //ni primerno za tekmovanje_1
            app.MapGet("/rezultati/{idTekmovanja}/{idtekmovalec}", async (string id, int idtekmovalec) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);

                var filter = Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(id));


                var cursor = await collection.Find(filter).ToListAsync();

                List<Rezultati> rezultati = new List<Rezultati>();


                foreach (var document in cursor)
                {


                    rezultati = document.rezultati;




                }
                if (idtekmovalec >= 0 && idtekmovalec < rezultati.Count)
                {
                    return rezultati[idtekmovalec];
                }

                return rezultati.First();
            });


            //Spremenimo lokacijo dolocenega tekmovanja
            app.MapPut("/spremeniLokacijo/{idTekmovanja}", async (string idTekmovanja, [FromBody] string newLocation) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);

                var filter = Builders<Tekmovanje>.Filter.And(
                    Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(idTekmovanja))
                );

                var update = Builders<Tekmovanje>.Update.Set("drzava", newLocation);
                var result = await collection.UpdateOneAsync(filter, update);


            });

            app.MapPut("/spremeniLokacijoDrugo/{idTekmovanja}", async (string idTekmovanja, [FromBody] string newLocation) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);

                var filter = Builders<Tekmovanje_1>.Filter.And(
                    Builders<Tekmovanje_1>.Filter.Eq("_id", ObjectId.Parse(idTekmovanja))
                );

                var update = Builders<Tekmovanje_1>.Update.Set("drzava", newLocation);
                var result = await collection.UpdateOneAsync(filter, update);


            });

            //Spremenimo ime igralca znotraj specificnega tekmovanja
            //Ni primerna za tekmovanje_1
            app.MapPut("/spremeniImeIgralca/{idTekmovanja}/{tekmovalec}", async (string idTekmovanja, int tekmovalec, [FromBody] string novoIme) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);

                var filter = Builders<Tekmovanje>.Filter.And(
                    Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(idTekmovanja))
                );

                var cursor = await collection.Find(filter).ToListAsync();


                List<Rezultati> rezultati = new List<Rezultati>();
                foreach (var document in cursor)
                {
                    // Assuming "rezultati" is a list property of "Tekmovanje"
                    for (int i = 0; i < document.rezultati.Count; i++)
                    {
                        if (i == tekmovalec)
                        {
                            document.rezultati[i].name = novoIme;
                            await collection.ReplaceOneAsync(filter, document);


                        }
                    }
                }

            });

            //Vsem tekmovanjem, ki so se zgodile po dolocenem letu spremenimo ime drzave kjer se je dogajalo
            app.MapPut("/tekmovanjaDrzavaSpremeni/{datumVnos}", async (int datumVnos, [FromBody] string novaDrzava) =>
            {


                List<Tekmovanje> tekmovanja = await db.GetCollection<Tekmovanje>(collectionName)
                .Find(Builders<Tekmovanje>.Filter.Empty)
                .ToListAsync();

                foreach (var document in tekmovanja)
                {

                    if (int.TryParse(document.leto_izvedbe, out int datum))
                    {
                        if (datum > datumVnos)
                        {


                            Console.WriteLine(document.leto_izvedbe);
                            var update = Builders<Tekmovanje>.Update.Set("drzava", novaDrzava);
                            var filter = Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(document.Id));
                            await collection.UpdateOneAsync(filter, update);
                        }
                    }
                    else
                    {

                        Console.WriteLine($"Invalid leto_izvedbe value: {document.drzava}");
                    }
                }
            });

            app.MapPut("/tekmovanjaDrzavaSpremeniDrugo/{datumVnos}", async (int datumVnos, [FromBody] string novaDrzava) =>
            {


                List<Tekmovanje_1> tekmovanja = await db.GetCollection<Tekmovanje_1>(collectionName1)
                .Find(Builders<Tekmovanje_1>.Filter.Empty)
                .ToListAsync();

                foreach (var document in tekmovanja)
                {

                    if (int.TryParse(document.leto_izvedbe, out int datum))
                    {
                        if (datum > datumVnos)
                        {


                            Console.WriteLine(document.leto_izvedbe);
                            var update = Builders<Tekmovanje_1>.Update.Set("drzava", novaDrzava);
                            var filter = Builders<Tekmovanje_1>.Filter.Eq("_id", ObjectId.Parse(document.Id));
                            await collection1.UpdateOneAsync(filter, update);
                        }
                    }
                    else
                    {

                        Console.WriteLine($"Invalid leto_izvedbe value: {document.drzava}");
                    }
                }
            });


            //Zaokrozimo navzgor koliko so prekolesarili znotraj tekmovanja
            //Ni primerna za Tekmovanje_1
            app.MapPut("/zaokroziNavzgor/{idTekmovanja}", async (string idTekmovanja) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);

                var filter = Builders<Tekmovanje>.Filter.And(
                    Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(idTekmovanja))
                );

                var cursor = await collection.Find(filter).ToListAsync();


                foreach (var document in cursor)
                {
                    for (int i = 0; i < document.rezultati.Count(); i++)
                    {
                        string swimDistanceValue = document.rezultati[i].swimDistance;
                        string numericniDel = swimDistanceValue.Split(' ')[0];
                        double cas = double.Parse(numericniDel);
                        double zaokrozeniCas = Math.Ceiling(cas);
                        string roundedSwimDistance = zaokrozeniCas.ToString() + " km";
                        document.rezultati[i].swimDistance = roundedSwimDistance;
                    }

                    var update = Builders<Tekmovanje>.Update.Set("rezultati", document.rezultati);
                    await collection.UpdateOneAsync(filter, update);

                }

                //  await collection.ReplaceManyAsync(filter, result);


            });

            //Izbrisemo znotraj tekmovanje rezultate iz dolocene drzave
            //Ni primerno za Tekmovanje_1
            app.MapPut("/diskfalikacija/{idTekmovanja}/{drzava}", async (string idTekmovanja,string drzava) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);
                var filter = Builders<Tekmovanje>.Filter.And(
                    Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(idTekmovanja))
                );
                var cursor = await collection.Find(filter).ToListAsync();

                foreach (var document in cursor)
                {
                    for (int i = 0; i < document.rezultati.Count(); i++)
                    {
                        if (document.rezultati[i].country == drzava)
                        {
                            document.rezultati[i].t1 = "DISQUALIFIED";
                            var update = Builders<Tekmovanje>.Update.Set($"rezultati.{i}.t1", "DISQUALIFIED");
                            await collection.UpdateOneAsync(filter, update);
                        }
                    }

                   

                }



            });

            //Izracun povprecnega casa
            //Ni povprevno za Tekmovanje_1

            app.MapPost("/izracunajPovprecje/{idTekmovanja}", async (string idTekmovanja) =>
            {
                
                    var collection = db.GetCollection<Tekmovanje>(collectionName);
                    var filter = Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(idTekmovanja));

                    var competition = await collection.Find(filter).FirstOrDefaultAsync();




                TimeSpan sum = TimeSpan.Zero;
                    foreach (var result in competition.rezultati)
                    {
                        
                        TimeSpan swimTime;
                        if (TimeSpan.TryParse(result.t1, out swimTime))
                        {
                            sum += swimTime;
                        }
                        else
                        {
                            
                            Console.WriteLine($"Invalid swim time format: {result.t1}");
                        }
                    }



                TimeSpan averageTime = TimeSpan.FromTicks(sum.Ticks / competition.rezultati.Count);

                var update = Builders<Tekmovanje>.Update.Set("averageSwimTime", averageTime.ToString());
                await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });


            });


            //Izracun povprecne starost tekmovalcev v tekmovanjih, ki so se zgodile po določenem letu
            //Ni primerno za tekmovanje_1
            app.MapPost("/calculateAverage/{datumVnos}", async (string datumVnos) =>
            {

                var collection = db.GetCollection<Tekmovanje>(collectionName);
                int pogoj = int.TryParse(datumVnos, out int result) ? result : 0;
                var documents = await collection.Find(Builders<Tekmovanje>.Filter.Empty).ToListAsync();

                var filteredDocuments = documents.Where(document => {
                    if (int.TryParse(document.leto_izvedbe, out int datum))
                    {
                        return datum > pogoj;
                    }
                    return false;
                });

               
                foreach (var document in filteredDocuments)
                {
                    double povprecjeStarosti = 0;
                    foreach (var rezultat in document.rezultati)
                    {
                        if (int.TryParse(rezultat.age, out int age))
                        {
                            povprecjeStarosti += age;
                        }
                        else
                        {
                            // Handle invalid age format
                          //  Console.WriteLine($"Invalid age format: {rezultat.age}");
                        }
                    }
                    povprecjeStarosti = povprecjeStarosti / document.rezultati.Count();

                    Console.WriteLine($"Calculated sum of ages: {povprecjeStarosti}");
                    Console.WriteLine(document.Id);


                    var update = Builders<Tekmovanje>.Update.Set("sumOfAges", povprecjeStarosti);

                    var filter = Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(document.Id));
                    await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });


                }


        
            });

            //Dodajanje k obstojecim rezultatom
            app.MapPost("/RezultatiDodaj/{id}/Rezultat", async (string id, [FromBody] Rezultati noviRezultat) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);
                var filter = Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(id));

                var update = Builders<Tekmovanje>.Update.Push("rezultati", noviRezultat);
                await collection.UpdateOneAsync(filter, update);
            });

            //Dodajanje k obstojecim rezultatom
            app.MapPost("/RezultatiDodajDrugo/{id}/Rezultat", async (string id, [FromBody] Rezultati_1 noviRezultat) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);
                var filter = Builders<Tekmovanje_1>.Filter.Eq("_id", ObjectId.Parse(id));

                var update = Builders<Tekmovanje_1>.Update.Push("rezultati", noviRezultat);
                await collection.UpdateOneAsync(filter, update);
            });


            //Dodajanje nove lastnosti "novi_rezultati"
            app.MapPost("/NovirezultatiDodaj/{id}", async (string id, [FromBody] Rezultati noviRezultat) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);
                var filter = Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(id));

                var update = Builders<Tekmovanje>.Update.Push("results", noviRezultat);
                await collection.UpdateOneAsync(filter, update);
            });

            app.MapPost("/NovirezultatiDodajDrugo/{id}", async (string id, [FromBody] Rezultati_1 noviRezultat) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);
                var filter = Builders<Tekmovanje_1>.Filter.Eq("_id", ObjectId.Parse(id));

                var update = Builders<Tekmovanje_1>.Update.Push("results", noviRezultat);
                await collection.UpdateOneAsync(filter, update);
            });



            //Dodajanje novega tekmovanja
            app.MapPost("/tekmovanjeDodaj", async ([FromBody] Tekmovanje novoTekmovanje) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);              
                //ObjectId newObjectId = ObjectId.GenerateNewId(); 
                //novoTekmovanje.Id = newObjectId.ToString();                
                await collection.InsertOneAsync(novoTekmovanje);
            });


            app.MapPost("/tekmovanjeDodajrugi", async ([FromBody] Tekmovanje_1 novoTekmovanje) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);
                //ObjectId newObjectId = ObjectId.GenerateNewId(); 
                //novoTekmovanje.Id = newObjectId.ToString();                
                await collection.InsertOneAsync(novoTekmovanje);
            });


            //Brisanje tekmovanj
            app.MapDelete("/deleteTekmovanje/{id}", async (string id) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);
                var filter = Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(id));

                await collection.DeleteOneAsync(filter);
            });

            app.MapDelete("/deleteTekmovanjeDrugo/{id}", async (string id) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);
                var filter = Builders<Tekmovanje_1>.Filter.Eq("_id", ObjectId.Parse(id));

                await collection.DeleteOneAsync(filter);
            });

            //Brisanje tekmovanje, ki so starejsa od dolocenih let
            app.MapDelete("/deleteTekmovanjePoStarosti/{letoStarost}", async (string letoStarost) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);
                var filter = Builders<Tekmovanje>.Filter.Lt("leto_izvedbe", letoStarost);

                await collection.DeleteManyAsync(filter);
            });

            app.MapDelete("/deleteTekmovanjePoStarostiDrugo/{letoStarost}", async (string letoStarost) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);
                var filter = Builders<Tekmovanje_1>.Filter.Lt("leto_izvedbe", letoStarost);

                await collection.DeleteManyAsync(filter);
            });

            //Zbrisemo rezultate ki so pod dolocenim rangom
            //Ni pprimerno z atekmovanje_1
            app.MapDelete("/deleteRezultat/{idTekmovanje}/{stMest}", async (string idTekmovanje, int stMest) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);
                var filter = Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(idTekmovanje));
               
                var iskanaTekma = await collection.Find(filter).FirstOrDefaultAsync();

                //iskanaTekma.rezultati = iskanaTekma.rezultati.Where(result => int.TryParse(result.le, out int age) && age <= int.Parse(letoStarost)).ToList();

                for (int i = iskanaTekma.rezultati.Count - 1; i >= stMest; i--)
                {
                    iskanaTekma.rezultati.RemoveAt(i);
                }

                var update = Builders<Tekmovanje>.Update.Set("rezultati", iskanaTekma.rezultati);
                await collection.UpdateOneAsync(filter, update);
            
            });

            app.MapDelete("/deleteTekmovanjePoDrzavi/{drzava}", async (string drzava) =>
            {
                var collection = db.GetCollection<Tekmovanje>(collectionName);
                var filter = Builders<Tekmovanje>.Filter.Eq("drzava", drzava);

                await collection.DeleteManyAsync(filter);
            });

            app.MapDelete("/deleteTekmovanjePoDrzaviDrugo/{drzava}", async (string drzava) =>
            {
                var collection = db.GetCollection<Tekmovanje_1>(collectionName1);
                var filter = Builders<Tekmovanje_1>.Filter.Eq("drzava", drzava);

                await collection.DeleteManyAsync(filter);
            });

            /*app.MapPut("/spremeniImeIgralca/{idTekmovanja}/{tekmovalec}", async (string idTekmovanja, int tekmovalec, [FromBody] string novoIme) =>
{
    var collection = db.GetCollection<Tekmovanje>(collectionName);

    var filter = Builders<Tekmovanje>.Filter.Eq("_id", ObjectId.Parse(idTekmovanja));

    // Update the specific element in the "rezultati" array
    var update = Builders<Tekmovanje>.Update.Set($"rezultati.{tekmovalec}.name", novoIme);

    // Apply the update
    await collection.UpdateOneAsync(filter, update);
});*/
        }
    }

    }

