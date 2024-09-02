import React, {useState}from 'react'
import WhyUS from './WhyUS';
import Header from './Header';
import Footer from './Footer';

// import  Box from '@mui/material/Box';
// import Paper from '@mui/material/Paper';
// import { styled } from '@mui/material/styles';
// import Grid from '@mui/material/Grid';

interface FAQarray{
    Q:string;
    A:string;
}
const faqitems: FAQarray[]=[
    {
        Q:'Who We are ?',
        A:"Bonvoyage is India's first online Holiday Marketplace launched in 2011 as a platform to help travelers customize, plan and have hassle-free holidays. Bonvoyage connects travelers with verified genuine and reviewed local/expert travel agents who are already trusted by other travelers like them."
    },
    {
        Q:'How to contact us ?',
        A:'You can write to us at customercare@bonvoyage.com or give us a call at 1800 123 5555 between 10 am to 8 pm. Our office locations are based out of Gurugram, Mumbai and USA.'
    },
    {
        Q:'How do I book a trip through Bonvoyage ? Check Booking Status ?',
        A:'You will have to sign up to create an account on our website to book a trip. The best travel agents, for your specific destination will prepare an itinerary for your trip & provide you the best quotes in response to your request. You can choose the one that suits you the best and book your trip by making the payment. Please Login and View at your Status'
    },
    {
        Q:'What are the different modes of payment for booking a trip on Bonvoyage?',
        A:'We have various payment options to give you a delightful booking experience: Debit/Credit Cards,Net Banking, NEFT Transfer'
    },
    {
        Q:'What is your Cancellation Policy?',
        A:'Bonvoyage ensures that it delivers great customer experience. However, there may arise cases where dissatisfaction is expressed for our offerings in terms of products and services.  We have a very liberal cancellation policy as following:'
    }
];

function FAQ() {
    const[Digit,setDigit] = useState<number | null>(null);
    const ontoggle =(index: number) =>{
        setDigit(Digit === index ? null : index);
    }
  return (
    <>
    <Header></Header>
        <WhyUS/>
        <div style={{padding:'20px'}}>
        <h2 style={{color:'#153243',padding:'1px', textAlign:'center',fontWeight:'bold'}}>FAQ's</h2>
        <h1 style={{textAlign:'center',fontWeight:'bold'}}>Frequent Asked Question</h1>
        <p style={{textAlign:'center'}}>Got Questions ? Check our FAQ's For Quick Answer</p>
        {faqitems.map((item, index) =>(
            <div key={index}>
                <div onClick={() => ontoggle(index)}
                style={{cursor: 'pointer',padding:'10px',backgroundColor:'#284B63',color:'#B4B8AB',
                        marginBottom:'5px',display:'flex',justifyContent:'space-between',alignItems:'center',fontWeight:'bold',borderRadius:'10px'}}>
                    {item.Q}
                    <span style={{transform: Digit===index ? 'rotate(180deg)':'rotate(0deg)',transition:'transform 0.3'}}>▼</span>
                </div>
                {Digit === index && <div style={{padding:'25px',backgroundColor:'#B3B5BD',borderRadius:'10px'}}>{item.A}</div>}
            </div>
        ))}
        </div>
        <Footer></Footer>
    </>

  );
}

export default FAQ