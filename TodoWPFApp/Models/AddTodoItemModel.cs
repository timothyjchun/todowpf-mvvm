using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace TodoWPFApp.Models 
{
    class AddTodoItemModel : INotifyPropertyChanged
    {


        private String _content;

        public String Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
