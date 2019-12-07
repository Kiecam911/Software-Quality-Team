using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_DAL;

namespace TMSv2_Contracts
{
    /// 
    /// \class OSHTRates
    ///
    /// \brief The purpose of this class is to store and manipulate the OSHT Rates table
    /// \details <b>Details</b>
    ///
    /// This class represents the Database's OSHTRates table and is meant to store said information as well
    /// as interface between the <b>DataAccess</b> class to manipulate the Table's Rates
    /// 
    /// \var data member _FTLRate <i>double</i> - <i>private<i> data member that holds the OSHT FTLRate per KM
    /// \var data member _LTLRate <i>double</i> - <i>private<i> data member that holds the OSHT LTLRate per pallet
    ///
    /// \author <i>TeamTeamTeam</i>
    /// 
    /// \sa DataAccess
    ///
    public class OSHTRates
    {
        private double _FTLRate;
        public double FTLRate
        {
            get { return _FTLRate; }
            set
            {
                if (value >= 0.0)
                {
                    _FTLRate = value;
                }
            }
        }
        private double _LTLRate;
        public double LTLRate
        {
            get { return _LTLRate; }
            set
            {
                if (value >= 0.0)
                {
                    _LTLRate = value;
                }
            }
        }


        public OSHTRates()
        {
            _FTLRate = 0.0;
            _LTLRate = 0.0;
        }


        ///
        /// \brief To retrieve Routes from Route table in the database
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the database to retrieve the data from the routes table
        ///
        /// \param <b>void</b> - None
        ///
        /// \return List Destination Returns a list of the rows of routes in the database
        ///
        public OSHTRates GetRates()
        {
            // create objects to hold information from DAL
            DataAccess tempDA = DataAccess.Instance();
            DataTable ratesTable = tempDA.GetRatesTable().Tables[0];
            OSHTRates rates = new OSHTRates();

            DataRowCollection ratesRow = ratesTable.Rows;

            // Get the one row of data from the rowCollection
            foreach (DataRow currentRow in ratesRow)
            {
                //Fill destination
                rates.FTLRate = currentRow.Field<double>(0);
                rates.LTLRate = currentRow.Field<double>(1);
            }


            return rates;
        }


        public bool UpdateRates(OSHTRates newRates)
        {
            //Variables
            DataAccess da = DataAccess.Instance();

            //If the update failed return false
            if (!da.UpdateRatesTable(newRates.FTLRate, newRates.LTLRate))
            {
                return false;
            }

            //Return success
            return true;
        }

    }
}
