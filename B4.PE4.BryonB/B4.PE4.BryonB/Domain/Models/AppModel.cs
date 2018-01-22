using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace B4.PE4.BryonB.Domain.Models
{
    [Table("AppModel")]
    public class AppModel : INotifyPropertyChanged
    {
        private Guid appModelId;
        [PrimaryKey]
        public Guid AppModelId
        {
            get
            {
                return appModelId;
            }
            set
            {
                this.appModelId = value;
                OnPropertyChanged(nameof(AppModelId));
            }
        }

        private ObservableCollection<ShoppingList> shoppingLists;
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<ShoppingList> ShoppingLists
        {
            get
            {
                return shoppingLists;
            }
            set
            {
                this.shoppingLists = value;
                OnPropertyChanged(nameof(ShoppingLists));
            }
        }
        private ObservableCollection<Product> beschikbareProducten;
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Product> BeschikbareProducten
        {
            get
            {
                return beschikbareProducten;
            }
            set
            {
                this.beschikbareProducten = value;
                OnPropertyChanged(nameof(BeschikbareProducten));
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
