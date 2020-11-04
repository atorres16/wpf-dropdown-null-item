using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Combobox_Null_Item
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Items = new ObservableCollection<Item>()
            {
                new Item {Id=1,Name="A" },
                new Item {Id=2,Name="B" },
                new Item {Id=3,Name="C" }
            };
        }


        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }


        private Item selectedItem;
        public Item SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                this.OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Item : NotifyPropertyChanged
    {

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                this.OnPropertyChanged(nameof(Id));
            }
        }


        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                this.OnPropertyChanged(nameof(Name));
            }
        }
    }

}
