using BookingWebService.Models;

namespace BookingWebService.Repository
{
    public class BookingRepo:IBookingRepo
    {
        private BonvoyageBookingDbContext _bonvoyageBookingDbContext;
        public BookingRepo(BonvoyageBookingDbContext bonvoyageBookingDbContext)
        {
            _bonvoyageBookingDbContext = bonvoyageBookingDbContext;
        }
        public BookingRepo()
        {
            _bonvoyageBookingDbContext=new BonvoyageBookingDbContext();
        }

        public List<Booking> GetAllBooking() { 
            List<Booking>bookings = new List<Booking>();
            try
            {
                bookings = (from b in _bonvoyageBookingDbContext.Bookings
                            select b
                          ).ToList();
            }
            catch(Exception ex) { 
                Console.WriteLine("Not able to fetch from db");
            }
            return bookings;
        }
        public List<Booking> GetBooking(string userId)
        {
            List<Booking> bookings = new List<Booking>();
            try
            {
                bookings = (from b in _bonvoyageBookingDbContext.Bookings
                            where b.UserId == userId
                            select b).ToList();
            }
            catch (Exception ex)
            {

                Console.WriteLine("not able to fetch booking");
            }
            
            return bookings;
        }

        public Booking UpdateBooking(Booking booking)
        {
            Booking book = new Booking();
            try
            {
                var existingBooking = _bonvoyageBookingDbContext.Bookings.FirstOrDefault(b => b.UserId == booking.UserId);
                if (existingBooking != null)
                {
                    existingBooking.PackageId = booking.PackageId;
                    existingBooking.PackageName = booking.PackageName;
                    existingBooking.PackageImage = booking.PackageImage;
                    existingBooking.BookingPerson = booking.BookingPerson;
                    existingBooking.BookingRooms = booking.BookingRooms;
                    existingBooking.Bookingdate = booking.Bookingdate;
                    _bonvoyageBookingDbContext.SaveChanges();
                    book= existingBooking;
                }
            }catch (Exception ex) { Console.WriteLine("Value not Updated"); }
            return book;
        }


        public string GetNewUserId()
        {
          
            string q = _bonvoyageBookingDbContext.Bookings
                            .OrderByDescending(item => item.BookingId)
                            .Select(item => item.BookingId)
                            .FirstOrDefault();
            int number = int.Parse(q.Substring(1)); 
            number++;

            string newUserId = $"B{number:D3}";
            return newUserId;

        }
        public bool AddBooking(Booking booking)
        {
            bool ans = false;
            try
            {
                _bonvoyageBookingDbContext.Bookings.Add(booking);
                _bonvoyageBookingDbContext.SaveChanges();
                ans = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("not able to book");
            }
            return ans;
        }
    }
}
