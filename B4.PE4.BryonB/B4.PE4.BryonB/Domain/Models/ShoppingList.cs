using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace B4.PE4.BryonB.Domain.Models
{
    [Table("ShoppingLists")]
    public class ShoppingList : INotifyPropertyChanged
    {                
        private Guid shoppingListId;
        [PrimaryKey]
        public Guid ShoppingListId
        {
            get
            {
                return shoppingListId;
            }
            set
            {
                this.shoppingListId = value;
                OnPropertyChanged(nameof(ShoppingListId));
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
