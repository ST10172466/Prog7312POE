using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace POEPart1.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public BitmapImage Image { get; set; }

        // Override Equals and GetHashCode to ensure unique events based on Date and Title
        public override bool Equals(object obj)
        {
            if (obj is Event other)
            {
                // Compare date ignoring time portion, and compare titles case-insensitively
                return this.Date.Date == other.Date.Date && string.Equals(this.Title, other.Title, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            // Combine the hash codes of the Date and Title properties
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + this.Date.Date.GetHashCode();
                hash = hash * 23 + StringComparer.OrdinalIgnoreCase.GetHashCode(this.Title);
                return hash;
            }
        }
    }
}
