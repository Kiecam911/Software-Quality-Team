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
                tempContract.ClientName = currentRow.Field<string>(kClientNameField);
                tempContract.JobType = currentRow.Field<int>(kJobTypeField);
                tempContract.Quantity = currentRow.Field<int>(kQuantityField);
                tempContract.Origin = currentRow.Field<string>(kOriginField);
                tempContract.Destination = currentRow.Field<string>(kDestinationField);
                tempContract.VanType = currentRow.Field<int>(kVanTypeField);
                newContracts.Add(tempContract);
            }
            return newContracts;
        }
    }
}
