using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace YavinService
{
    public class TransactionService : ITransactionService
    {

        TransactionRepository transactionRepo = new TransactionRepository();

        #region Public Methods

        public Transaction CreateTransaction(Transaction transaction)
        {
            return transactionRepo.CreateNewTransaction(transaction);
        }

        public void DeleteTransaction(string transactionId)
        {
            transactionRepo.DeleteTransaction(transactionId);
        }

        public Transaction EditTransaction(Transaction transcation)
        {
            return transactionRepo.EditTransaction(transcation);
        }

        public Transaction GetTransactionById(string transactionId)
        {
            return transactionRepo.GetTransactionById(transactionId);
        }

        public List<Transaction> GetTransactions()
        {
            return transactionRepo.GetTransactions();
        }

        #endregion

        #region Private Methods

        private List<Transaction> createTestTransactions()
        {
            List<Transaction> txs = new List<Transaction>();
            Random rnd = new Random();

            for (int i = 0; i < 5; i++)
            {
                Transaction tx = new Transaction();
                tx.Id = i + "";
                tx.Description = "Test transaction " + i;
                tx.Time = DateTime.Now;
                tx.Amount = rnd.Next(0, 1001);
                txs.Add(tx);
            }

            return txs;
        }

        #endregion
    }
}
