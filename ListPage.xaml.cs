using FilepRebecaLab7.Models;
using System.Collections.ObjectModel;
namespace FilepRebecaLab7
{
public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	public ObservableCollection<ShopList> ShoppingList { get; set; } = new ObservableCollection<ShopList>();

	private async void OnDeleteItemClicked(object sender, EventArgs e)
		{
			var selectedItem = shoppingListView.SelectedItem as ShopList;
            if (selectedItem != null)
			{
				await App.Database.DeleteShopListAsync(selectedItem);
				ShoppingList.Remove(selectedItem);	
			}
        }
	async void OnSaveButtonClicked(object sender, EventArgs e)
    {
		var slist = (ShopList)BindingContext;
		slist.Date = DateTime.UtcNow;
		await App.Database.SaveShopListAsync(slist);
		await Navigation.PopAsync();
    }

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		var slist = (ShopList)BindingContext;
		await App.Database.DeleteShopListAsync(slist);
		await Navigation.PopAsync();
	}
		async void OnChooseButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
			{
				BindingContext = new Product()
			});
		}

	protected override async void OnAppearing()
		{
			base.OnAppearing();
			var shop1 = (ShopList)BindingContext;
			listView.ItemsSource = await App.Database.GetListProductsAsync(shop1.ID);
		}
}
}