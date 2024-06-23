import { json, useLocation, useNavigate } from 'react-router-dom';
import Header from './Header';
import Footer from './Footer';
import { useEffect, useState } from 'react';
import { Flare } from '@mui/icons-material';
import HeaderUser from './HeaderUser';
import axios from 'axios';
import { Button, Card, CardContent, CardMedia, Typography } from '@mui/material';

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
interface packageData{
  Pacakage:Package[]
}
function WishList(){

  useEffect(()=>{
    axios.get('https://localhost:4000/api/Package/WishList/'+Data.data.dalUser.user.userId).then(response=>{
    
      setPackages(response.data)
      console.log(packages)
      console.log("yooo")
    }).catch(error => {

      console.error('Error fetching data: ', error);
    });
  },[])
   const[packages,setPackages]=useState<Package[]>();
   //const[WishActive,SetWishActive]=useState<Boolean>(false);
    const[Data,SetData]=useState(()=>{
        const savedValue=  localStorage.getItem('UserRequiredData');
        return savedValue!==null?JSON.parse(savedValue):null;
         });
function DeleteFromWish(packageId:any) {

   axios.delete('https://localhost:4000/api/Package/DeleteWishList/'+Data.data.dalUser.user.userId+'/'+packageId).
  then(response=>{
    console.log(response.data)
    if(response.data===true){
      window.location.reload()
    }
  }).catch(error => {
   
    console.error('Error Deleting WishList: ', error);
  });
} 

const navigate=useNavigate();
         
   
    return (
        <>
     {Data ? <HeaderUser />:<Header /> }


      
     <ul style={{display:'flex',gap:'20px'}}>
    {packages?.map((item :any, index:any) => (
      <li style={{width:'300px'}} key={index}>
        <Card sx={{ maxWidth: 500 }} style={{margin:'20px 0'}}>
      <CardMedia
        sx={{ height: 140 }}
        image={item.packageImage
            }
        title={item.packageName}
      />
      <CardContent>
        <Typography gutterBottom variant="h3" component="div">
          {item.packageName}
        </Typography>
        <Typography gutterBottom variant="h5" component="div">
          {item.packageCountry}
        </Typography>
        <Typography gutterBottom variant="body2" component="div">
          {item.packageDesc}
        </Typography>
        
        <Typography gutterBottom variant="body2" component="div">
        PackageDuration:  {item.packageDuration}
        </Typography>
        <Typography gutterBottom variant="body2" component="div">
          {item.packageIternary}
        </Typography>
        <Typography gutterBottom variant="body2" component="div">
          PackagePrice: {item.packagePrice}
        </Typography>
       
      </CardContent>
      <div style={{display:'flex',justifyContent:'space-between',padding:'0 20px'}}>
            <Button onClick={()=>DeleteFromWish(item.packageId)}>Delete</Button>
            <Button onClick={()=>navigate('/allPackages')} >Book</Button>
            </div>
    </Card>
      </li>
    ))}
  </ul>
    
     <Footer></Footer>
        </>
    )
}
export default WishList;