using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Attributes;

namespace OZRA_vaje2
{
    public class Tekmovanje_1
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string ime_tekmovanja { get; set; }

        public string drzava { get; set; }
        public string leto_izvedbe { get; set; }
        public string averageSwimTime { get; set; }
        public List<Rezultati_1> results { get; set; }
        public List<Rezultati_1> rezultati { get; set; }
        public Tekmovanje_1(string ime_tekmovanja, string drzava, string leto_izvedbe, List<Rezultati_1> rezultati, List<Rezultati_1> results, string averageSwimTime)
        {
            this.ime_tekmovanja = ime_tekmovanja;
            this.drzava = drzava;
            this.leto_izvedbe = leto_izvedbe;
            this.rezultati = rezultati;
            this.results = results;

            this.averageSwimTime = averageSwimTime;
            this.averageSwimTime = averageSwimTime; 
        }
        public Tekmovanje_1()
        {

        }
    }


}
