using System;
using System.Collections.Generic;

namespace beadando
{
    public partial class Production
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Association { get; set; } = null!;
        public int NoOfCompetitors { get; set; }
        public int AgeGroup { get; set; }
        public string Category { get; set; } = null!;

        public Production(int id, string name, string association, int noOfCompetitors, int ageGroup, string category)
        {
            Id = id;
            Name = name;
            Association = association;
            NoOfCompetitors = noOfCompetitors;
            AgeGroup = ageGroup;
            Category = category;
        }

        public override string ToString()
        {
            return $"({Id}) - {Name} - {Association} - {getProductionType(NoOfCompetitors)} - {getAgeGroupType(AgeGroup)} - {Category}";
        }

        public string getProductionType(int competitors)
        {
            if (competitors == 1)
            {
                return "egyéni";
            }
            else if (competitors == 2)
            {
                return "duó";
            }
            else if (competitors == 3)
            {
                return "trió";
            }
            else if (competitors > 3 && competitors < 11)
            {
                return "csapat";
            }
            else
            {
                return "formáció";
            }
        }

        public string getAgeGroupType(int ageGroup)
        {
            if (ageGroup <= 5)
            {
                return "totyogó";
            }
            else if (ageGroup >= 6 && ageGroup <= 10)
            {
                return "gyerek";
            }
            else if (ageGroup >= 11 && ageGroup <= 17)
            {
                return "junior";
            }
            else
            {
                return "felnőtt";
            }
        }
    }
}
