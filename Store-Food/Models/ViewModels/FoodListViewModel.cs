namespace Store_Food.Models.ViewModels
{
    public class FoodListViewModel
    {
        public IEnumerable<Food> Foods { get; set; } = Enumerable.Empty<Food>();
       public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
