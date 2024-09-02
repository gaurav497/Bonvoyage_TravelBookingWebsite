
import './App.css'
import SignUp from './Components/SignUp'
import PageNotFound from "./Components/PageNotFound"
import { BrowserRouter, Routes,Route } from "react-router-dom";
import Login from './Components/Login';
import UserLoggedIn from './Components/UserLoggedIn';
import FAQ from './Components/FAQ';
import Home from './Components/Home';
import Book from './Components/Book';
import WishList from './Components/WishList';
import AllPackages from './Components/AllPackages';
import MyBooking from './Components/MyBookings';
import About from './Components/About';
import ThankYou from './Components/ThankYou';
function App() {

    return (
        <>
            <BrowserRouter>
                <Routes>
                    <Route path='/' element={<Home></Home>}></Route>
                    <Route path='/login' element={<Login></Login>}></Route>
                    <Route path="/register" element={<SignUp/>} />
                    <Route path='/about' element={<About></About>}></Route>
                    <Route path='/book' element={<Book></Book>}></Route>
                    <Route path='/mybookings' element={<MyBooking></MyBooking>}></Route>
                    <Route path='/allPackages' element={<AllPackages></AllPackages>}></Route>
                    <Route path='/WishList' element={<WishList></WishList>}></Route>
                    <Route path='/faq' element={<FAQ></FAQ>}></Route>
                    <Route path='/UserLoggedIn' element={<UserLoggedIn></UserLoggedIn>} ></Route>
                    <Route path='/thankyou' element={<ThankYou></ThankYou>}></Route>
                    <Route path="*" element={<PageNotFound></PageNotFound>} />
                   
                </Routes>
            </BrowserRouter>
        </>
    );

}


export default App
