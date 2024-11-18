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
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// String that holds the Title of the event
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// String that holds the Description of the event
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// DateTime that holds the Date of the event
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// String that holds the Category of the event
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// BitmapImage that holds the Image of the event
        /// </summary>
        public BitmapImage Image { get; set; }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to override Equals to ensure unique events based on Date and Title
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Event other)
            {
                // Compare date ignoring time portion, and compare titles case-insensitively
                return this.Date.Date == other.Date.Date && string.Equals(this.Title, other.Title, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Method to override GetHashCode to ensure unique events based on Date and Title
        /// </summary>
        /// <returns></returns>
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

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//