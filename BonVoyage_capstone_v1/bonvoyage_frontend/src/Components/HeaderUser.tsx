import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';

function HeaderUser(){
    

const naviagate=useNavigate();
function nav(){
        naviagate("/register")
}
function navigateToLogin(){
    localStorage.removeItem('UserRequiredData');
    naviagate("/login")
}
function navigateToFaq(){
    naviagate("/faq")
}
function navigateToMyBooking(){
    naviagate("/mybookings")
}
function navigateToWishList(){
    naviagate("/WishList")
}
function navigateToHome(){
    naviagate("/")
}
function navigateToAbout(){
    naviagate("/about")
}
function navigateToPackages(){
    naviagate("/allPackages")
}
    return(
        <>
        <div style={{boxShadow:'1px 0.5px white',display:'flex',justifyContent:'center',backgroundColor:'black'}}>
<div className="Header">
<h1>BonVoyage</h1>
<div className="menu">
    <h6 className='buttonx' onClick={navigateToHome}>Home</h6>
    <h6 className='buttonx' onClick={navigateToAbout}>About</h6>
    
    <h6 className='buttonx' onClick={navigateToMyBooking} >My Bookings</h6>
    <h6 className='buttonx' onClick={navigateToPackages}> All Packages</h6>
    <h6 className='buttonx' onClick={navigateToWishList} >whisList</h6>
</div>
<div className="LoginSign">
    <div className="Login"><Button onClick={navigateToLogin}  variant='outlined' style={{color:'white',borderColor:'white'}}>LogOut</Button></div>

</div>
</div>
</div>
</>
)
}

export default HeaderUser;