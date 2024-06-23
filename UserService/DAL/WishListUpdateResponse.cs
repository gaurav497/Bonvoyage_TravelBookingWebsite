namespace UserService.DAL
{
    public class WishListUpdateResponse
    {
        public string Status { get; set; }
        public WishListResponseData data { get; set; } = new WishListResponseData();
    }
}
