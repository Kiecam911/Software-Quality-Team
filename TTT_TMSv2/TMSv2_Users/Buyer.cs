﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSv2_Carriers;
using TMSv2_Contracts;
using TMSv2_Order;
using TMSv2_TripPlanner;
using System.Data;

namespace TMSv2_Users
{
    /// 
    /// \class Buyer : User
    ///
    /// \brief The purpose of this class is to contain the data and methods associated with the "Buyer" actor
    /// \details <b>Details</b>
    ///
    /// This class represents the "Buyer" actor and is capable of 
    ///
    /// \var data member orderId <i>int</i> - <i>private<i> data member that holds the order's Identification number within the database
    ///
    /// \author <i>TeamTeamTeam</i>
    ///
    public class Buyer : User
    {
        ///
        /// \brief To instantiate a new Buyer
        /// \details <b>Details</b>
        ///
        /// Instantiates the Buyer class by setting the permission level
        ///
        /// \param nothing <b>void</b> - Nothing is passed into this constructor
        ///
        /// \return As this is a <i>constructor</i> for the Buyer class, nothing is returned
        ///
        public Buyer()
        {
            PermissionLevel = PERMISSION_BUYER;
        }



        ///
        /// \brief Creates a new <b>Order</b>
        /// \details <b>Details</b>
        ///
        /// Creates a new <b>Order</b>
        ///
        /// \param contract <b>Contract</b> - The contract the order is based off of
        ///
        /// \return Returns the completed order
        /// 
        /// \see Contract
        ///
        public Order CreateOrder(string clientName, int jobType, int quantity, string origin, string destination, int vanType)
        {
            Contract baseContract = new Contract();
            baseContract.ClientName = clientName;
            baseContract.JobType = jobType;
            baseContract.Quantity = quantity;
            baseContract.Origin = origin;
            baseContract.Destination = destination;
            baseContract.VanType = vanType;

            Order newOrder = new Order(baseContract);
            newOrder.IsActive = true;
            newOrder.IsCompleted = false;

            return newOrder;
        }



        ///
        /// \brief Creates an invoice based on an order
        /// \details <b>Details</b>
        ///
        /// Generates an invoice based on the details contained within the order passed
        /// as a parameter.
        ///
        /// \param Order order - The order the invoice is based off of
        ///
        /// \return None
        /// 
        /// \see Order
        ///
        public void GenerateInvoice(DataRow contractInfo, DataRow orderInfo)
        {

        }
    }
}
