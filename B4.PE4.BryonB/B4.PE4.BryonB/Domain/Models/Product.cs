using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace B4.PE4.BryonB.Domain.Models
{
    [Table("Products")]
    public class Product : INotifyPropertyChanged
    {
        private Guid productId;
        [PrimaryKey]
        public Guid ProductId
        {
            get
            {
                return productId;
            }
            set
            {
                this.productId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }
        private String naam;
        public String Naam
        {
            get
            {
                return naam;
            }
            set
            {
                this.naam = value;
                OnPropertyChanged(nameof(Naam));
            }
        }
        private String result;
        public String Result
        {
            get
            {
                return result;
            }
            set
            {
                this.result = value;
                OnPropertyChanged(nameof(Result));
            }
        }        
        private ObservableCollection<ShoppingDetail> shoppingDetails;
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<ShoppingDetail> ShoppingDetails
        {
            get
            {
                return shoppingDetails;
            }
            set
            {
                this.shoppingDetails = value;
                OnPropertyChanged(nameof(ShoppingDetails));
            }
        }
        [ForeignKey(typeof(AppModel))]
        public Guid AppModelId { get; set; }
        [ManyToOne(nameof(AppModelId))]
        public AppModel AppModel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
        }
    }
}
