import { useEffect, useState } from "react";
import Header from "./Header";
import HeaderUser from "./HeaderUser";
import Footer from "./Footer";
import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material";
import axios from "axios";
import { redirect, useNavigate } from "react-router-dom";
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
   const[accessToken,SetAcessToken]=useState(()=>{
    const savedValue=localStorage.getItem('accessToken');
    
    return savedValue;
   });
   
useEffect(()=>{
 
  if(accessToken!=null){
  axios.get('https://localhost:4000/api/Booking/GetBooking/'+Data.data.dalUser.user.userId,
    {
      headers:{"Authorization" : `bearer ${accessToken}`}
    }
  )
  .then(response => {
  
    SetBooking(response.data)
   
  })
  .catch(error => {
   
    console.error('Error fetching data: ', error);
  });
  }

},[])

const[booking,SetBooking]=useState<Booking[]>()
useEffect(()=>{
   
},[booking])

const navigate=useNavigate();
const deleteBooking = async (BookingId:string) => {
 
axios.delete('https://localhost:4000/api/Booking/DeleteBooking/'+BookingId).then(
response=>{
 if(response.status){

  window.location.reload();
 }
}).catch(error => {
   
  console.error('Error deleting data: ', error);
});

}
    return(

        <>
        {Data ? <HeaderUser />:<Header /> }
     
       <ul style={{display:'flex',gap:'20px',flexWrap:'wrap',listStyle:'none',marginLeft:'50px'}}>
    {booking?.map((item :any, index:any) => (
      <li style={{width:'200px'}} key={item.bookingId}>
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
        <Button onClick={()=>deleteBooking(item.bookingId)}>Cancel Booking</Button>
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