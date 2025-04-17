using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Test.MVVM.Model
{
    public class Item : INotifyPropertyChanged
    {
        private string _task;
        public string Task
        {
            get => _task;
            set
            {
                if (_task != value)
                {
                    _task = value;
                    OnPropertyChanged(nameof(Task));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
