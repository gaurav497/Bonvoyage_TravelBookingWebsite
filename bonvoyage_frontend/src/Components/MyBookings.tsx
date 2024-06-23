import { useEffect, useState } from "react";
import Header from "./Header";
import HeaderUser from "./HeaderUser";
import Footer from "./Footer";
import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material";
import axios from "axios";
interface Booking {
    bookingId: string;
    bookingPerson: number;
    bookingRooms: number;
    bookingDate: Date | null;
    packageId: string;
    packageImage: string;
    packageName: string;
    totalCost: number;
    userId: string;
  }



  interface BookingData {
    bookings: Booking[];
  }
function MyBooking(){

   const[Data,SetData]=useState(()=>{
  const savedValue=  localStorage.getItem('UserRequiredData');
  return savedValue!==null?JSON.parse(savedValue):null;
   });
   
useEffect(()=>{
 
  
  axios.get('https://localhost:4000/api/Booking/GetBooking/'+Data.data.dalUser.user.userId)
  .then(response => {
    // Handle success
    console.log('Data: ', response.data);
   
    SetBooking(response.data)
    console.log(booking)
  })
  .catch(error => {
   
    console.error('Error fetching data: ', error);
  });


},[])

const[booking,SetBooking]=useState<Booking[]>()
useEffect(()=>{
    console.log(booking)
},[booking])
    return(

        <>
        {Data ? <HeaderUser />:<Header /> }
     
       <ul style={{display:'flex',gap:'20px'}}>
    {booking?.map((item :any, index:any) => (
      <li style={{width:'300px'}} key={index}>
        <Card sx={{ maxWidth: 500 }} style={{margin:'20px 0'}}>
      <CardMedia
        sx={{ height: 140 }}
        image={item.packageImage
            }
        title={item.packageName}
      />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {item.packageName}
        </Typography>
        <Typography variant="body2" color="text.secondary">
            <div style={{display:'flex',flexDirection:"column"}}>
            <h4>Total Cost</h4>
            {item.totalCost}
            </div>
            <div style={{display:'flex',flexDirection:"column"}}>
                <h4>Booking Person</h4>
            {item.bookingPerson}
            </div>
            <div style={{display:'flex',flexDirection:"column"}}>
                <h4>Booking Rooms</h4>
            {item.bookingRooms}
            </div>
        </Typography>
      </CardContent>
    
    </Card>
      </li>
    ))}
  </ul>

       <Footer></Footer>
        </>
    )
}

export default MyBooking;