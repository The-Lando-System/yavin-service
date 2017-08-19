using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace YavinService
{
    public class TransactionRepository
    {
        private const string TABLE_NAME = "Transaction";
        
        public List<Transaction> GetTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            List<string> attrs = new List<string> { "Id", "Description", "Amount", "Time", "Type" };

            List<Dictionary<string, object>> results = DBUtils.Instance.Select(TABLE_NAME, attrs, null);

            if (results == null)
                return transactions;

            foreach (Dictionary<string, object> result in results)
            {
                transactions.Add(ConvertToTransaction(result));
            }

            return transactions;
        }

        public Transaction GetTransactionById(string id)
        {
            Transaction transaction = new Transaction();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", id);

            List<string> attrs = new List<string> { "Id", "Description", "Amount", "Time", "Type" };

            List<Dictionary<string, object>> results = DBUtils.Instance.Select(TABLE_NAME, attrs, parameters);

            return ConvertToTransaction(results[0]);
        }

        public Transaction CreateNewTransaction(Transaction transaction)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", transaction.Id);
            parameters.Add("Description", transaction.Description);
            parameters.Add("Amount", transaction.Amount);
            parameters.Add("Time", transaction.Time);
            parameters.Add("Type", transaction.Type);

            bool success = DBUtils.Instance.Insert(TABLE_NAME, parameters);

            return success ? transaction : null;
        }

        public void DeleteTransaction(string transactionId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", transactionId);

            DBUtils.Instance.Delete(TABLE_NAME, parameters);
        }

        public Transaction EditTransaction(Transaction transaction)
        {

            Transaction currentTransaction = GetTransactionById(transaction.Id);

            Dictionary<string, object> updateParameters = new Dictionary<string, object>();

            if (currentTransaction.Description != transaction.Description)
                updateParameters.Add("Description", transaction.Description);

            if (currentTransaction.Amount != transaction.Amount)
                updateParameters.Add("Amount", transaction.Amount);

            if (currentTransaction.Time != transaction.Time)
                updateParameters.Add("Time", transaction.Time);

            if(currentTransaction.Type != transaction.Type)
                updateParameters.Add("Type", transaction.Type);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", transaction.Id);

            bool success = DBUtils.Instance.Edit(TABLE_NAME, updateParameters, parameters);

            return success ? transaction : null;
        }

        private Transaction ConvertToTransaction(Dictionary<string, object> result)
        {
            Transaction t = new Transaction();
            t.Id = (string)result["Id"];
            t.Description = (string)result["Description"];
            t.Amount = (decimal)result["Amount"];
            t.Time = (DateTime)result["Time"];
            t.Type = (TransactionType) Enum.Parse(typeof(TransactionType), (string)result["Type"]);
            return t;
        }

    }
}