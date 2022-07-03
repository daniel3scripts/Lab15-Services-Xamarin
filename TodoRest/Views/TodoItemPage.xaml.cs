using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoRest.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoRest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoItemPage : ContentPage
    {
        bool isNewItem;
        public TodoItemPage(bool isNew = false)
        {
            InitializeComponent();
            isNewItem = isNew;
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var todoItem = (TodItem)BindingContext;
            await App.TodoManager.SaveTaskAsync(todoItem, isNewItem);
            await Navigation.PopAsync();
        }
        
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var todoItem=(TodItem)BindingContext;
            await App.TodoManager.DeleteTaskAsync(todoItem);
            await Navigation.PopAsync();
        }
        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}