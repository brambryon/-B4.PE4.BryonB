using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.ComponentModel;

namespace B4.PE4.BryonB.Domain.Models
{
    [Table("ShoppingDetails")]
    public class ShoppingDetail : INotifyPropertyChanged
    {
        private Guid shoppingDetailId;
        [PrimaryKey]
        public Guid ShoppingDetailId
        {
            get
            {
                return shoppingDetailId;
            }
            set
            {
                this.shoppingDetailId = value;
                OnPropertyChanged(nameof(ShoppingDetailId));
            }
        }
        [ForeignKey(typeof(Product))]
        public Guid ProductId { get; set; }
        private Product product;
        [ManyToOne(nameof(ProductId), CascadeOperations = CascadeOperation.All)]
        //[ManyToOne(nameof(ProductId))]
        public Product Product
        {
            get
            {
                return product;
            }
            set
            {
                this.product = value;
                OnPropertyChanged(nameof(Product));
            }
        }
       // private Boolean scanned;
       [Ignore]
        public Boolean Scanned
        {
            get
            {
                if (GescannedAantal >= GevraagdAantal)
                {
                    return true;
                } else
                {
                    return false;
                }               
            }
            //set
            //{
            //    this.scanned = value;
            //    OnPropertyChanged(nameof(Scanned));
            //}
        }
        private int gescannedAantal;
        public int GescannedAantal
        {
            get
            {
                return gescannedAantal;
            }
            set
            {
                this.gescannedAantal = value;
                OnPropertyChanged(nameof(GescannedAantal));
            }
        }
        private int gevraagdAantal;
        public int GevraagdAantal
        {
            get
            {
                return gevraagdAantal;
            }
            set
            {
                this.gevraagdAantal = value;
                OnPropertyChanged(nameof(GevraagdAantal));
            }
        }
        [ForeignKey(typeof(ShoppingList))]
        public Guid ShoppingListId { get; set; }
        private ShoppingList shoppingList;
        [ManyToOne(nameof(ShoppingListId), CascadeOperations = CascadeOperation.All)]
        public ShoppingList ShoppingList
        {
            get
            {
                return shoppingList;
            }
            set
            {
                this.shoppingList = value;
                OnPropertyChanged(nameof(ShoppingList));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
        }
    }
}
