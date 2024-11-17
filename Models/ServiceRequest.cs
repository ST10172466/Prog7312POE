using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEPart1.Models
{
    public class ServiceRequest
    {
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Int that holds the ID of the service request
        /// </summary>
        public int ServiceRequestID { get; set; }
        /// <summary>
        /// String that holds the Title of the service request
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// String that holds the Status of the service request
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// DateTime that holds the Date of the service request
        /// </summary>
        public DateTime DateSubmitted { get; set; }
        /// <summary>
        /// String that holds the Description of the service request
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Int that holds the Priority of the service request
        /// </summary>
        public int Priority { get; set; }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//