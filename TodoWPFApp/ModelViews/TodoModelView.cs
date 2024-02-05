using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TodoWPFApp.Commands;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using System.Text.Json;

namespace TodoWPFApp.ModelViews
{
    class TodoModelView : INotifyPropertyChanged
    {
        private ObservableCollection<Models.TodoItemModel> _todoItems; // A List to save user's todos
        private Models.AddTodoItemModel _addTodoItem;
        private Models.TodoItemModel _todoItem;
        public Commands.AddButtonCommand AddButtonCommand { get; set; }
        public Commands.ItemCheckCommand ItemCheckCommand { get; set; }

        private int _todoItemsLength = 0;

        public TodoModelView() {
            AddTodoItem = new Models.AddTodoItemModel();
            TodoItems = new ObservableCollection<Models.TodoItemModel>();
            AddButtonCommand = new Commands.AddButtonCommand(AddButtonExecuteFunc, AddButtonCanExecuteFunc);
            ItemCheckCommand = new Commands.ItemCheckCommand(ItemCheckExecuteFunc,ItemCheckCanExecuteFunc);
        }


        public Models.AddTodoItemModel AddTodoItem
        {
            get
            {
                return _addTodoItem;
            }
            set
            {
                _addTodoItem = value;
                OnPropertyChanged("AddTodoItem");
            }
        }

        public Models.TodoItemModel TodoItem
        {
            get
            {
                return _todoItem;
            }
            set
            {
                _todoItem = value;
                OnPropertyChanged("TodoItem");
            }
        }


        public ObservableCollection<Models.TodoItemModel> TodoItems
        {
            get
            {
                return _todoItems;
            }
            set
            {
                // need to append to list
                _todoItems = value;
                OnPropertyChanged("TodoItems");
            }
        }


        public void AddButtonExecuteFunc(object obj)
        {
            //Debug.WriteLine(AddTodoItem.Content);
            Models.TodoItemModel newItem = new Models.TodoItemModel();
            newItem.Content = AddTodoItem.Content;
            newItem.IsFinished = false;
            newItem.Index = _todoItemsLength;
            TodoItems.Add(newItem);
            AddTodoItem.Content = "";
            _todoItemsLength++;
            ShowItems();
        }


        private void ShowItems()
        {

            Debug.WriteLine("========[start showing Items]=========");
            for(int i = 0; i< _todoItemsLength; i++)
            {
                Debug.WriteLine(TodoItems[i].Content);
            }
            Debug.WriteLine("========[end showing Items]=========");
        }
        
        public bool AddButtonCanExecuteFunc(object obj)
        {
            if (AddTodoItem.Content != "") return true;
            else return false;
        }

        public void ItemCheckExecuteFunc(object obj) { // this has to be an array.. problem is you can't do that. Also index is just always fixed
            string obj_string = (string)obj;
            object[] obj_desirealized = JsonSerializer.Deserialize<object[]>(obj_string);
            JsonElement isCheckedJson = (JsonElement)obj_desirealized[0];
            bool isChecked = isCheckedJson.ToString() == "True" ? true : false;

            if (isChecked)
            {
                JsonElement indexJson = (JsonElement)obj_desirealized[1];
                int index = indexJson.GetInt32();

                for(int i = 0; i < _todoItemsLength; i++)
                {
                    // 어차피 queue (FIFO)형태 이기 때문에 앞에 index가 겹쳐도 된다. 단, break 필요
                    if (TodoItems[i].Index == index)
                    {
                        TodoItems.RemoveAt(i);
                        _todoItemsLength--;
                        break;
                    }
                }
            }

        }


        public bool ItemCheckCanExecuteFunc(object obj) {
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
