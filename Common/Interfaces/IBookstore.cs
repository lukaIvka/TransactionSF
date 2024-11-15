﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IBookstore: ITransaction
    {
        [OperationContract]
        void ListAvailableItems();
        [OperationContract]
        void EnlistPurchase(string bookId, uint count);
        [OperationContract]
        double GetItemPrice(string bookId);
    }
}
