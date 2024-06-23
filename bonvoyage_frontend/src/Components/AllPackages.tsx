import React, { useEffect, useState } from 'react';
import LocationOnSharpIcon from '@mui/icons-material/LocationOnSharp';
import FavoriteBorderSharpIcon from '@mui/icons-material/FavoriteBorderSharp';
import './AllPackages.css';
import Header from './Header';
import HeaderUser from './HeaderUser';
import { Button, TextField } from '@mui/material';
import { Label } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
// eslint-disable-next-line @typescript-eslint/no-unused-vars
//import { PlaylistAddCheckCircleRounded } from '@mui/icons-material';

function AllPackages() {

 


  
  let stringvalue:string|null=null
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const[Data,SetData]=useState<any>(null)
  
  useEffect(()=>{
      stringvalue=localStorage.getItem('UserRequiredData');
    
      if(stringvalue!==null){
          SetData(JSON.parse(stringvalue))
          console.log(Data)  
          }
  },[])
 
  interface Package {
    availableDate: string;
    maxPeople: number;
    minAge: string;
    packageCity: string;
    packageCountry: string;
    packageDesc: string;
    packageDuration: string;
    packageId: string;
    packageImage: string;
    packageIternary: string;
    packageLanguage: string;
    packageName: string;
    packagePickup: string;
    packagePrice: string;
    packageRating: number;
    packageReviews: number;
  }
  const[WishActive,SetWishActive]=useState<Boolean>(false);
  const [perPerson, setPerPerson] = useState<number>(2000);
  const [totalCosts, setTotalCosts] = useState<{ [key: string]: number }>({});
  const [packages, setPackages] = useState<Package[] | null>(null);
  const [bookButton ,SetBookButton]=useState<boolean>(true);
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const[numberofRoom,SetNumberOfRoom]=useState<any>(1);
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const[bookingId,SetBookingId]=useState<any>();
    const navigate=useNavigate();
    function navToThankYou(){
      navigate('/thankyou')
    }
  useEffect(() => {
    const fetchUserID = async () => {
        try {
            const response = await fetch('https://localhost:4000/api/Booking/GetNewBookingID');
            if (response.ok) {
                const data = await response.json();
                console.log(data);
                SetBookingId(data)
            } else {
                console.error('Failed to fetch userID:', response.statusText);
            }
        } catch (error) {
            console.error('Error fetching userID:', error);
        }
    };

    fetchUserID();
}, []);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const url = new URL('https://localhost:4000/api/Package/GetPackages');
        const response = await fetch(url, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
          },
        });

        if (response.ok) {
          const responseData = await response.json();
          setPackages(responseData.data);
        } else {
          console.error('Failed to Load packages:', response.statusText);
        }
      } catch (error) {
        console.error('Error loading packages:', error);
      }
    };

    fetchData();
  }, []);

  function handleSetPerPerson(event: React.ChangeEvent<HTMLInputElement>) {
    setPerPerson(parseInt(event.target.value));
  }
  
  useEffect(()=>{
   
   
    if(Data===null){
  console.log('bye')
      SetBookButton(true)
      
    }else{
     SetBookButton(false)
  
    }
    
  },[Data])
  function calculateTotalCost(packageId: string, packagePrice: string) {
    const totalPrice = parseInt(packagePrice) + perPerson * 2000;
    setTotalCosts((prevTotalCosts) => ({
      ...prevTotalCosts,
      [packageId]: totalPrice,
    }));
  }









  const handleSubmit = async (packageId:string,packageName:string,packageImage:string) => {
    console.log('Hello')
      console.log(totalCosts[packageId])
     try {
       console.log(Data.data.dalUser.user.userId)
       const requestBody = {
         bookingId: bookingId,
         userId: Data.data.dalUser.user.userId, // Include the userId from Data
         packageId: packageId, // Use the packageId from argument
         packageName: packageName, // Use the packageName from argument
         packageImage: packageImage,
         bookingPerson: perPerson,
         bookingRooms: numberofRoom, // Include the number of rooms state
         bookingDate: "2024-06-12",
         totalCost: totalCosts[packageId] // Use the total cost for the specific package
     };
     console.log(requestBody)
         const response = await fetch('https://localhost:4000/api/Booking/AddBooking', {
             method: 'POST',
             headers: {
                 'Content-Type': 'application/json'
             },
             body: JSON.stringify(requestBody)
         });
         if (response.ok) {
             console.log('Successfully booked');
             
             navToThankYou()
         } else {
             console.error('Failed to book:', response.statusText);
         }
     } catch (error) {
         console.error('Error booking:', error);
     }
 }


 const AddWishList = async (packageId:string) => {
  console.log('Hello')
    console.log(totalCosts[packageId])

   try {
     console.log(Data.data.dalUser.user.userId)
    

       const response = await fetch('https://localhost:4000/api/Package/AddToWishList/'+Data.data.dalUser.user.userId+'/'+packageId, {
           method: 'POST',
           headers: {
               'Content-Type': 'application/json'
           },
          
       });
       if (response.ok) {
           console.log('Added to WIshList');
           
         
       } else {
           console.error('Failed to Add To WishList:', response.statusText);
       }
   } catch (error) {
       console.error('Error Wislist:', error);
   }
}



const RemoveWishList = async (packageId:string) => {
  
    console.log(totalCosts[packageId])

   try {
     console.log(Data.data.dalUser.user.userId)
    

       const response = await fetch('https://localhost:4000/api/Package/DeleteWishList/'+Data.data.dalUser.user.userId+'/'+packageId, {
           method: 'DELETE',
           headers: {
               'Content-Type': 'application/json'
           },
          
       });
       if (response.ok) {
           console.log('Removed from WIshList');
           
         
       } else {
           console.error('Failed to Remove WishList:', response.statusText);
       }
   } catch (error) {
       console.error('Error Wislist:', error);
   }
}





  return (
    <>
      {/* Displaying header */}
      {Data ? <HeaderUser />:<Header /> }

      <ul>
        {packages?.map((item) => (
          <li key={item.packageId}>
            <div className='wholepage'>
              <div className='main'>
                <div className='container1'>
                  <div className='img' style={{ backgroundImage: `url(${item.packageImage})` }}></div>
                  <div className='details'>
                    <div>
                      <p><small>{item.packageDesc}</small></p>
                      <h4><b>{item.packageCity}</b></h4>
                      <p style={{ display: "flex", flexDirection: "row", alignItems: "center", color: "#438a56" }}>
                        <LocationOnSharpIcon />{item.packageCountry}</p>
                    </div>
                    <div>
                      <p><small>Taking safety Measures</small></p>
                      <p>Free Cancellation</p>
                    </div>
                  </div>
                </div>
                <div className='container2'>
                  <div style={{display:'flex',flexDirection:'row'}}>
                    <h2>WishList</h2>
                 <button onClick={()=>AddWishList(item.packageId)}  style={{width:'100px',backgroundColor:'white'}}>
                  Add
                  </button>
                  <button onClick={()=>RemoveWishList(item.packageId)}  style={{width:'100px',backgroundColor:'white'}}>
                 Remove
                  </button>
                  </div>
                  <div style={{ display: 'flex', flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between' }}>
                    <div style={{ display: 'flex', flexDirection: 'row', alignItems: 'center', gap: '10px' }}>
                      <label >Package Price</label>
                      <p>{item.packagePrice}</p>
                    </div>
                    <div style={{ display: 'flex', flexDirection: 'row', alignItems: 'center', gap: '10px' }}>
                      <label> Per Adult</label>
                      <p><b>2000</b></p>
                    </div>
                  </div>
                  <TextField
                    onChange={handleSetPerPerson}
                    id="outlined-basic"
                    label="Total Person"
                    variant="outlined"
                  />
                  <br></br>
                  <TextField
                    onChange={(e)=>SetNumberOfRoom(e.target.value)}
                    id="outlined-basic"
                    label="Total Rooms"
                    variant="outlined"
                  />
                  <h4>{totalCosts[item.packageId]}</h4>
                  <div style={{display:'flex',gap:'10px',flexDirection:'column'}}>
                 
                  <button style={{ color: "white" }} onClick={() => calculateTotalCost(item.packageId, item.packagePrice)}>Calculate Total Cost</button>
                  <Button disabled={bookButton}  onClick={()=>handleSubmit(item.packageId,item.packageName,item.packageImage)} >book </Button>
                  </div>
                </div>  
              </div>
            </div>
          </li>
        ))}
      </ul>
    </>
  );
}

export default AllPackages;
