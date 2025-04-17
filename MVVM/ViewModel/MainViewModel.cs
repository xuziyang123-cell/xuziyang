using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Test.MVVM.Model;

namespace Test.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        private string? _newItemTask;
        public string NewItemTask
        {
            get => _newItemTask;
            set
            {
                if (_newItemTask != value)
                {
                    _newItemTask = value;
                    OnPropertyChanged(nameof(NewItemTask));
                    (AddCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private Item? _selectedItem;
        public Item? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            Items = new ObservableCollection<Item>();

            AddCommand = new RelayCommand(AddItem, CanAddItem);
            DeleteCommand = new RelayCommand(DeleteItem, CanDeleteItem);
        }

        private void AddItem(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(NewItemTask))
            {
                Items.Add(new Item { Task = NewItemTask });
                NewItemTask = string.Empty;
            }
        }

        private bool CanAddItem(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewItemTask);
        }

        private void DeleteItem(object parameter)
        {
            if (SelectedItem != null)
            {
                Items.Remove(SelectedItem);
            }
        }

        private bool CanDeleteItem(object parameter)
        {
            return SelectedItem != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
