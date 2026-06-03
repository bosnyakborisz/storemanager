using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Models
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string GamerTag { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int LoyaltyPoints { get; set; }

        public string PreferredPlatform { get; set; }

        public DateTime RegisteredAt { get; set; }
    }
}
