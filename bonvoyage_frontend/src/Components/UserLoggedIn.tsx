import { Button } from "@mui/material";
import React, { useEffect, useState } from "react";
import { useLocation } from 'react-router-dom';
import Header from "./Header";
import HeaderUser from "./HeaderUser";

function UserLoggedIn(){
    let stringvalue:string|null=null
    const[Data,SetData]=useState<any>(null)
    
    useEffect(()=>{
        stringvalue=localStorage.getItem('UserRequiredData');
        if(stringvalue!==null){
            SetData(JSON.parse(stringvalue))
            console.log(Data)  
            }
    },[])
   
    return(
        <>
      
        <div>
        {Data ? <HeaderUser /> :<Header /> }
        <div style={{display:'flex',alignItems:'center',justifyContent:'right'}} >
        <div style={{width:"100px" ,height:'20px',backgroundColor:'blue'}}></div>
        <Button>LogOut</Button>

        </div>


        </div>
       

</>
    )
}

export default UserLoggedIn;