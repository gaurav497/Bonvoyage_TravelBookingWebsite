import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';

function Header(){
    

const naviagate=useNavigate();
function nav(){
        naviagate("/register")
}
function navigateToLogin(){
    naviagate("/login")
}
function navigateToFaq(){
    naviagate("/faq")
}
function navigateToBook(){
    naviagate("/book")
}
function navigateToWishList(){
    naviagate('/WishList')
}
function navigateToHome(){
    naviagate("/")
}
function navigateToAbout(){
    naviagate("/about")
}
function navigateToPackages(){

    naviagate("/allpackages")
}
    return(
        <>
        <div style={{boxShadow:'1px 0.5px white',display:'flex',justifyContent:'center',backgroundColor:'black'}}>
<div className="Header">
<h1>BonVoyage</h1>
<div className="menu">
    <h6 className='buttonx' onClick={navigateToHome}>Home</h6>
    <h6 className='buttonx' onClick={navigateToAbout}>About</h6>
    <h6 className='buttonx' onClick={navigateToFaq} >FAQ</h6>
    <h6 className='buttonx' onClick={navigateToPackages}>Packages</h6>
    <h6 className='buttonx' onClick={navigateToWishList} >whisList</h6>
</div>
<div className="LoginSign">
    <div className="Login"><Button onClick={navigateToLogin}  variant='outlined' style={{color:'white',borderColor:'white'}}>LOGIN</Button></div>
    <div className="Register"><Button onClick={nav} variant='outlined'style={{color:'white',borderColor:'white'}}>Register</Button></div>
</div>
</div>
</div>
</>
)
}

export default Header;