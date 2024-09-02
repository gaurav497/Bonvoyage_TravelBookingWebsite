import { useState } from "react";
import HeaderUser from "./HeaderUser";
import Header from "./Header";
import Footer from "./Footer";
import { Button } from "@mui/material";
import { useNavigate } from "react-router-dom";
function ThankYou(){
    const navigate=useNavigate();
    function navToPackages(){
navigate('/allPackages')
    }
    const[Data,SetData]=useState(()=>{
        const savedValue=  localStorage.getItem('UserRequiredData');
        return savedValue!==null?JSON.parse(savedValue):null;
         });

         return(
            <>
                 {Data ? <HeaderUser />:<Header /> }
                    <div style={{display:'flex',justifyContent:'center',alignItems:'center',flexDirection:'column'}}>
                        <h1>CONFIRMATION</h1>
                        <h3>ThankYou For your Booking</h3>
                        <Button onClick={navToPackages} >Return To Packages</Button>
                    </div>
                 <Footer></Footer>
            </>
         )

}


export default ThankYou;