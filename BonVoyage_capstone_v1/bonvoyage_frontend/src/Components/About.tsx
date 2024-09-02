import './style.css'
import { Typography } from '@mui/material';
import FlightIcon from '@mui/icons-material/Flight';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import EventIcon from '@mui/icons-material/Event';
import { useState } from 'react';
import Footer from './Footer';
import HeaderUser from './HeaderUser';
import Header from './Header';
const About = () => {
    const siteName = " The Bon Voyage Traveling Website";
    const aboutBV = "Welcome to Bon Voyage Traveling Website! We are dedicated to utilizing state-of-the-art technology to deliver exceptional travel experiences to our clients. Whether you seek a serene beach getaway, an adrenaline-pumping trekking adventure, or an enriching cultural exploration, we have everything you need for an unforgettable journey";
    const[Data,SetData]=useState(()=>{
        const savedValue=  localStorage.getItem('UserRequiredData');
        return savedValue!==null?JSON.parse(savedValue):null;
         });
    return (
        <>
        {Data ? <HeaderUser />:<Header /> }
        <div className="about-container">
            <h1 className="title">About {siteName}</h1>
            <h4 style={{color:"gray"}}>Want to Have Fun?We are number one...</h4>
            <div className="about-content">
            <Typography variant="body1">{aboutBV}</Typography>
                
                <div className="icon-container">
                    <FlightIcon className="icon" />
                    <LocationOnIcon className="icon" />
                    <EventIcon className="icon" />
                </div>
                <img src={"beachimage.jpg"} alt="Beach" className="about-image" /> {/* Use the imported image */}
                <p className="cta">Explore our website to discover amazing destinations, find travel tips, and book your next adventure with us!</p>
            </div>
        </div>
        <Footer></Footer>
        </>
    );
}

export default About;
