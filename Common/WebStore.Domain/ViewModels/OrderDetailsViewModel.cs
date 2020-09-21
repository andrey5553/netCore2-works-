using WebStore.ViewModels;

namespace WebStore.Domain.ViewModels
{
    public class OrderDetailsViewModel
    {
        public CartViewModel CartViewModel { get; set; }
        public OrderViewModel OrderViewModel { get; set; }
    }
}