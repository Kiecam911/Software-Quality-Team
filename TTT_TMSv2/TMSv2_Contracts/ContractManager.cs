using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_DAL;

namespace TMSv2_Contracts
{
    public static class ContractManager
    {
        private const string kClientNameField = "Client_Name";
        private const string kJobTypeField = "Job_Type";
        private const string kQuantityField = "Quantity";
        private const string kOriginField = "Origin";
        private const string kDestinationField = "Destination";
        private const string kVanTypeField = "Van_Type";


        ///
        /// \brief To retrieve details for the new contract from the contract marketplace
        /// \details <b>Details</b>
        ///
        /// This method interfaces with the contract marketplace database to populate the variables
        /// related to the contract marketplace. 
        ///
        /// \param <b>void</b> - None
        ///
        /// \return Nothing
        ///
        public static List<Contract> CreateNewContracts()
        {
            // create objects to hold information from DAL
            DataTable contractTable = DataAccess.GetContracts().Tables[0];
            List<Contract> newContracts = new List<Contract>();

            DataRowCollection contractRows = contractTable.Rows;
            DataColumnCollection contractCols = contractTable.Columns;

            // loop through each row of data, creating contract and assigning values to it
            foreach (DataRow currentRow in contractRows)
            {
                Contract tempContract = new Contract();
                tempContract.clientName = currentRow.Field<string>(kClientNameField);
                tempContract.jobType = currentRow.Field<int>(kJobTypeField);
                tempContract.quantity = currentRow.Field<int>(kQuantityField);
                tempContract.origin = currentRow.Field<string>(kOriginField);
                tempContract.destination = currentRow.Field<string>(kDestinationField);
                tempContract.vanType = currentRow.Field<int>(kVanTypeField);
                newContracts.Add(tempContract);
            }
            return newContracts;
        }
    }
}
