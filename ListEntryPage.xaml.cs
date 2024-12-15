using MotMariusLab7.Models;

namespace MotMariusLab7;

    public partial class ListEntryPage : ContentPage
    {
        public ListEntryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetShopListsAsync();
        }

        async void OnShopListAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListPage
            {
                BindingContext = new ShopList()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ListPage
                {
                    BindingContext = e.SelectedItem as ShopList
                });
            }
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var shopList = button.BindingContext as ShopList;
            
            if (shopList != null)
            {
                var answer = await DisplayAlert("Delete List", 
                    "Are you sure you want to delete this shopping list?", 
                    "Yes", "No");
                
                if (answer)
                {
                    await App.Database.DeleteShopListAsync(shopList);
                    await RefreshListView();
                }
            }
        }

        private async Task RefreshListView()
        {
            listView.ItemsSource = await App.Database.GetShopListsAsync();
        }
    }
