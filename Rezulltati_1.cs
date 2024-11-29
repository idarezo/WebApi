namespace OZRA_vaje2
{
    public class Rezultati_1
    {

        public string Rank { get; set; }
        public string Overall { get; set; }

        public string Competitor { get; set; }
        public string Country { get; set; }
        public string Age_Category { get; set; }
        public string Swim { get; set; }
        public string Trans1 { get; set; }
        public string Bike { get; set; }
        public string Trans2 { get; set; }
        public string Run { get; set; }
        public string Finish { get; set; }
        public string Comment { get; set; }

        public Rezultati_1(string Rank, string Overall, string Competitor, string Country, string Age_Category, string Swim, string Trans1, string Bike, string Trans2, string Run, string Finish, string Comment)
        {
            this.Rank = Rank;
            this.Overall = Overall;
            this.Competitor = Competitor;
            this.Country = Country;
            this.Age_Category = Age_Category;
            this.Swim = Swim;
            this.Trans1 = Trans1;
            this.Bike = Bike;
            this.Trans2 = Trans2;
            this.Run = Run;
            this.Comment = Comment;
            this.Finish = Finish;
        }
    }
}
