namespace OZRA_vaje2
{


    public class Rezultati
    {
        /*[BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }*/
        public string name { get; set; }
        public string genderRank { get; set; }

        public string divRank { get; set; }
        public string overallRank { get; set; }
        public string bib { get; set; }
        public string division { get; set; }
        public string age { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string profession { get; set; }
        public string points { get; set; }
        public string swim { get; set; }
        public string swimDistance { get; set; }
        public string t1 { get; set; }
        public string bike { get; set; }
        public string bikeDistance { get; set; }

        public string t2 { get; set; }

        public string run { get; set; }
        public string runDistance { get; set; }

        public string overall { get; set; }





        public Rezultati(string name, string genderRank, string divRank, string overallRank, string bib, string division, string age, string state, string country, string profession, string points, string swim, string swimDistance, string t1, string bike, string bikeDistance, string t2, string run, string runDistance, string overall)
        {
            this.name = name;
            this.genderRank = genderRank;
            this.divRank = divRank;
            this.overallRank = overallRank;
            this.bib = bib;
            this.division = division;
            this.age = age;
            this.state = state;
            this.country = country;
            this.profession = profession;
            this.points = points;
            this.swim = swim;
            this.swimDistance = swimDistance;
            this.t1 = t1;
            this.bike = bike;
            this.bikeDistance = bikeDistance;
            this.t2 = t2;
            this.run = run;
            this.runDistance = runDistance;
            this.overall = overall;
        }
        public Rezultati()
        {

        }

    }
    }
