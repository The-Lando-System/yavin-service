﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YavinWindowsClient.YavinServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Transaction", Namespace="http://schemas.datacontract.org/2004/07/YavinService")]
    [System.SerializableAttribute()]
    public partial class Transaction : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private YavinWindowsClient.YavinServiceReference.TransactionType TypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Time {
            get {
                return this.TimeField;
            }
            set {
                if ((this.TimeField.Equals(value) != true)) {
                    this.TimeField = value;
                    this.RaisePropertyChanged("Time");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public YavinWindowsClient.YavinServiceReference.TransactionType Type {
            get {
                return this.TypeField;
            }
            set {
                if ((this.TypeField.Equals(value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TransactionType", Namespace="http://schemas.datacontract.org/2004/07/YavinService")]
    public enum TransactionType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PURCHASE = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INCOME = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="YavinServiceReference.ITransactionService")]
    public interface ITransactionService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionService/GetTransactions", ReplyAction="http://tempuri.org/ITransactionService/GetTransactionsResponse")]
        YavinWindowsClient.YavinServiceReference.Transaction[] GetTransactions();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionService/GetTransactions", ReplyAction="http://tempuri.org/ITransactionService/GetTransactionsResponse")]
        System.Threading.Tasks.Task<YavinWindowsClient.YavinServiceReference.Transaction[]> GetTransactionsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionService/GetTransactionById", ReplyAction="http://tempuri.org/ITransactionService/GetTransactionByIdResponse")]
        YavinWindowsClient.YavinServiceReference.Transaction GetTransactionById(string transactionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionService/GetTransactionById", ReplyAction="http://tempuri.org/ITransactionService/GetTransactionByIdResponse")]
        System.Threading.Tasks.Task<YavinWindowsClient.YavinServiceReference.Transaction> GetTransactionByIdAsync(string transactionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionService/CreateTransaction", ReplyAction="http://tempuri.org/ITransactionService/CreateTransactionResponse")]
        YavinWindowsClient.YavinServiceReference.Transaction CreateTransaction(YavinWindowsClient.YavinServiceReference.Transaction transaction);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionService/CreateTransaction", ReplyAction="http://tempuri.org/ITransactionService/CreateTransactionResponse")]
        System.Threading.Tasks.Task<YavinWindowsClient.YavinServiceReference.Transaction> CreateTransactionAsync(YavinWindowsClient.YavinServiceReference.Transaction transaction);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionService/DeleteTransaction", ReplyAction="http://tempuri.org/ITransactionService/DeleteTransactionResponse")]
        void DeleteTransaction(string transactionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionService/DeleteTransaction", ReplyAction="http://tempuri.org/ITransactionService/DeleteTransactionResponse")]
        System.Threading.Tasks.Task DeleteTransactionAsync(string transactionId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionService/EditTransaction", ReplyAction="http://tempuri.org/ITransactionService/EditTransactionResponse")]
        YavinWindowsClient.YavinServiceReference.Transaction EditTransaction(YavinWindowsClient.YavinServiceReference.Transaction transcation);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITransactionService/EditTransaction", ReplyAction="http://tempuri.org/ITransactionService/EditTransactionResponse")]
        System.Threading.Tasks.Task<YavinWindowsClient.YavinServiceReference.Transaction> EditTransactionAsync(YavinWindowsClient.YavinServiceReference.Transaction transcation);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITransactionServiceChannel : YavinWindowsClient.YavinServiceReference.ITransactionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TransactionServiceClient : System.ServiceModel.ClientBase<YavinWindowsClient.YavinServiceReference.ITransactionService>, YavinWindowsClient.YavinServiceReference.ITransactionService {
        
        public TransactionServiceClient() {
        }
        
        public TransactionServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TransactionServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransactionServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TransactionServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public YavinWindowsClient.YavinServiceReference.Transaction[] GetTransactions() {
            return base.Channel.GetTransactions();
        }
        
        public System.Threading.Tasks.Task<YavinWindowsClient.YavinServiceReference.Transaction[]> GetTransactionsAsync() {
            return base.Channel.GetTransactionsAsync();
        }
        
        public YavinWindowsClient.YavinServiceReference.Transaction GetTransactionById(string transactionId) {
            return base.Channel.GetTransactionById(transactionId);
        }
        
        public System.Threading.Tasks.Task<YavinWindowsClient.YavinServiceReference.Transaction> GetTransactionByIdAsync(string transactionId) {
            return base.Channel.GetTransactionByIdAsync(transactionId);
        }
        
        public YavinWindowsClient.YavinServiceReference.Transaction CreateTransaction(YavinWindowsClient.YavinServiceReference.Transaction transaction) {
            return base.Channel.CreateTransaction(transaction);
        }
        
        public System.Threading.Tasks.Task<YavinWindowsClient.YavinServiceReference.Transaction> CreateTransactionAsync(YavinWindowsClient.YavinServiceReference.Transaction transaction) {
            return base.Channel.CreateTransactionAsync(transaction);
        }
        
        public void DeleteTransaction(string transactionId) {
            base.Channel.DeleteTransaction(transactionId);
        }
        
        public System.Threading.Tasks.Task DeleteTransactionAsync(string transactionId) {
            return base.Channel.DeleteTransactionAsync(transactionId);
        }
        
        public YavinWindowsClient.YavinServiceReference.Transaction EditTransaction(YavinWindowsClient.YavinServiceReference.Transaction transcation) {
            return base.Channel.EditTransaction(transcation);
        }
        
        public System.Threading.Tasks.Task<YavinWindowsClient.YavinServiceReference.Transaction> EditTransactionAsync(YavinWindowsClient.YavinServiceReference.Transaction transcation) {
            return base.Channel.EditTransactionAsync(transcation);
        }
    }
}
