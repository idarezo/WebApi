using MongoDB.Bson.Serialization.Attributes;

namespace OZRA_vaje2
{
    public class Tekmovanje
    {



        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string ime_tekmovanja { get; set; }
        public string sumOfAges { get; set; } // New field added to the class definition
        public string drzava { get; set; }
        public string leto_izvedbe { get; set; }
        public string averageSwimTime { get; set; }

        public List<Rezultati> rezultati { get; set; }
        public List<Rezultati> results { get; set; }
        public Tekmovanje(string ime_tekmovanja, string drzava, string leto_izvedbe, List<Rezultati> rezultati, List<Rezultati> results, string sumOfAges,string averageSwimTime)
        {
            this.ime_tekmovanja = ime_tekmovanja;
            this.drzava = drzava;
            this.leto_izvedbe = leto_izvedbe;
            this.rezultati = rezultati;
            this.results = rezultati;

            this.sumOfAges = sumOfAges;
            this.averageSwimTime = averageSwimTime;

        }
        public Tekmovanje()
        {

        }

    }
}
