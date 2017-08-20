using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace YavinService
{

    [ServiceContract]
    public interface ITransactionService
    {

        [OperationContract]
        List<Transaction> GetTransactions();

        [OperationContract]
        Transaction GetTransactionById(string transactionId);

        [OperationContract]
        Transaction CreateTransaction(Transaction transaction);

        [OperationContract]
        void DeleteTransaction(string transactionId);

        [OperationContract]
        Transaction EditTransaction(Transaction transcation);

    }

    [DataContract(Name = "TransactionType")]
    public enum TransactionType
    {
        [EnumMember]
        PURCHASE,

        [EnumMember]
        INCOME
    }

    [DataContract]
    public class Transaction
    {
        string id;
        string description;
        DateTime time;
        decimal amount;
        TransactionType type;

        [DataMember]
        public string Id
        {
            get
            {
                if (id == null || id.Trim() == "")
                {
                    id = System.Guid.NewGuid().ToString();
                }
                return id;
            }
            set {  id = value; }
        }

        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [DataMember]
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        [DataMember]
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        [DataMember]
        public TransactionType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
