/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/no-explicit-any */
// SignUp.js
import FilledInput from '@mui/material/FilledInput/FilledInput';
import FormControl from '@mui/material/FormControl';
import IconButton from '@mui/material/IconButton/IconButton';
import InputAdornment from '@mui/material/InputAdornment/InputAdornment';
import InputLabel from '@mui/material/InputLabel/InputLabel';
import TextField from '@mui/material/TextField';
import React, { ChangeEvent, FormEvent, useEffect } from 'react';
import { useState } from 'react';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import Header from './Header';
import Footer from './Footer';
import { Grid } from '@mui/material';
import { useNavigate } from 'react-router-dom';

function SignUp() {
    const navigate=useNavigate();
    const [formData, setFormData] = useState({
        userID:'',
        userName: '',
        userEmail: '',
        userPhone: '',
        userPassword: '',
        userAddress: '',
        userRole: 'customer'

    });
    useEffect(() => {
        const fetchUserID = async () => {
            try {
                const response = await fetch('https://localhost:4000/api/UserRegistration/GetNewUserId');
                if (response.ok) {
                    const data = await response.json();
                    console.log(data);
                    setFormData(prevState => ({
                        ...prevState,
                        userID: data
                    }));
                } else {
                    console.error('Failed to fetch userID:', response.statusText);
                }
            } catch (error) {
                console.error('Error fetching userID:', error);
            }
        };

        fetchUserID();
    }, []);
    const [showPassword, setShowPassword] = useState(false);

    const handleClickShowPassword = () => setShowPassword((show) => !show);

    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    }

    const handleChange = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = event.target;
        setFormData({
            ...formData,
            [name]: value,
        });
    }

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        try {
            console.log(formData)
            const requestBody = {
                userID: formData.userID,
                userName: formData.userName,
                userEmail: formData.userEmail,
                userPhone: formData.userPhone,
                userPassword: formData.userPassword,
                userAddress: formData.userAddress,
                userRole: formData.userRole
            };
            const response = await fetch('https://localhost:4000/api/UserRegistration/RegisterUser', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(requestBody)
            });
            if (response.ok) {
                console.log('User registered successfully');
                navigate('/login')
              
            } else {
                console.error('Failed to register user:', response.statusText);
            }
        } catch (error) {
            console.error('Error registering user:', error);
        }
    }
    return (
        <>
        <Header></Header>


            <div className="container">
                <Grid container style={{width:'80vw' ,margin:'0 auto'}}>
                    <Grid lg={6}>
                <img style={{height:'500px',width:'500px'}} src="3903465_2062689.jpg" alt="" />
                </Grid>
                <Grid lg={6}>
                <h2>Sign Up</h2>
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <TextField id="outlined-basic" className="form-control" name="userName" required label="Name"  variant="outlined"  onChange={handleChange} />

                    </div>
                    <br></br>
                    <div className="form-group">
                        <TextField id="outlined-basic" className="form-control" name="userEmail" required label="Email" variant="outlined"  inputProps={{
        maxLength: 100, 
        pattern: "[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", 
      }} onChange={handleChange} />

                    </div>
                    <br></br>
                    <div className="form-group">
                        <TextField id="outlined-basic" className="form-control" name="userPhone" required label="Phone Number" variant="outlined"  inputProps={{
        maxLength: 15, 
        pattern: "[0-9]{10}", 
      }} onChange={handleChange} />

                    </div>
                    <br></br>
                    <div className="form-group">
                 
                        <FormControl sx={{ m: 1, width: '25ch' }} variant="filled">
                            <InputLabel htmlFor="filled-adornment-password">Password</InputLabel>
                            <FilledInput
                                id="filled-adornment-password" 
                                type={showPassword ? 'text' : 'password'}
                                name='userPassword'
                                onChange={handleChange}
                            
                                endAdornment={
                                    <InputAdornment  position="end">
                                        <IconButton
                                            aria-label="toggle password visibility"
                                            onClick={handleClickShowPassword}
                                            onMouseDown={handleMouseDownPassword}
                                            edge="end"
                                        >
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                }
                            />
                        </FormControl>
                    </div>
                    <br></br>
                    <div className="form-group">
                        <TextField id="outlined-basic" className="form-control" name="userAddress" label="Address" required variant="outlined" onChange={handleChange} />

                    </div>
                    <br></br>
                    <button style={{color:"white"}} type="submit" className="btn btn-primary">Sign Up</button>
                </form>
                </Grid>
                </Grid>
            </div>
            <Footer></Footer>
        </>

    )
}

export default SignUp;