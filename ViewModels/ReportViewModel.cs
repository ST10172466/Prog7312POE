using POEPart1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POEPart1.ViewModels
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// List to store reports
        /// </summary>
        private List<Report> reportsList;
        public List<Report> ReportsList
        {
            get { return reportsList; }
            set
            {
                reportsList = value;
                OnPropertyChanged(nameof(ReportsList));
            }
        }

        /// <summary>
        /// Selected report
        /// </summary>
        private Report selectedReport;
        public Report SelectedReport
        {
            get { return selectedReport; }
            set
            {
                selectedReport = value;
                OnPropertyChanged(nameof(SelectedReport));
            }
        }

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reports"></param>
        public ReportViewModel(List<Report> reports)
        {
            ReportsList = reports;
        }

        //-----------------------------------------------------------------------------------------------//

        // INotifyPropertyChanged implementation

        //-----------------------------------------------------------------------------------------------//
        /// <summary>
        /// INotifyPropertyChanged implementation
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //-----------------------------------------------------------------------------------------------//
    }
}
//------------------------------------------..oo00 End of File 00oo..-------------------------------------------//