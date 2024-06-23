namespace UserService.DAL
{
    public class SingleUser
    {
        public string Status { get; set; }
        public int Result { get; set; }
        public SingleUserData user { get; set; } = new SingleUserData();
    }
}
