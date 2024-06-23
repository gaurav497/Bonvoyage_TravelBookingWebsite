import { LockOutlined } from "@mui/icons-material";
import {
  Container,
  CssBaseline,
  Box,
  Avatar,
  Typography,
  TextField,
  Button,
  Grid,
} from "@mui/material";
import { useState } from "react";
import { Link, json, useNavigate } from "react-router-dom";
import "./Login.css";
import Header from "./Header";
import Footer from "./Footer";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [Data,setDate]=useState([]);
const navigate=useNavigate();
  const handleLogin = async (event: any) => {
    event.preventDefault();
    try {
      
      const url = new URL('https://localhost:4000/api/UserRegistration/Login');
      url.searchParams.append('useremail', email);
      url.searchParams.append('password', password);

      const response = await fetch(url, {
          method: 'POST',
          headers: {
              'Content-Type': 'application/json'
          },
          
      });
      if (response.ok) {
          console.log('Loged In');
          const data = await response.json()
          setDate(data)
          const stringvalue=JSON.stringify(data)
          console.log(stringvalue+"login")
          localStorage.setItem('UserRequiredData',stringvalue)
          localStorage.setItem('userName',email)
          localStorage.setItem('password',password)
          
          navigate('/allPackages');
        
      } else {
          console.error('Failed to Login:', response.statusText);
      }
  } catch (error) {
      console.error('Error Logging  user:', error);
  }
  };

  return (
    <>
    <Header/>
     
        
        <Grid container style={{padding:'50px 0'}}>
          <Grid lg={6}>
            <img style={{height:'500px',width:'500px'}} src="4957136_4957136.jpg" alt="" />
          </Grid>
            <Grid lg={6} style={{padding:'0 100px'}}><Box 
          sx={{
            textAlign: "center",
            backgroundColor: "white",
            opacity:'90%',
            p: 3,
            borderRadius: 4,
            boxShadow: 1,
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: "primary.light" }}>
            <LockOutlined />
          </Avatar>
          <Typography variant="h5">Login</Typography>
          <Box sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              autoFocus
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />

            <TextField
              margin="normal"
              required
              fullWidth
              id="password"
              name="password"
              label="Password"
              type="password"
              value={password}
              onChange={(e) => {
                setPassword(e.target.value);
              }}
            />

            <Button
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              onClick={handleLogin}
            >
              Login
            </Button>
            <Grid container justifyContent={"flex-end"}>
              <Grid item>
                <Link to="/register">Don't have an account? Register</Link>
              </Grid>
            </Grid>
          </Box>
        </Box></Grid>
          </Grid> 
        

        
      
      
      <Footer></Footer>
    </>
  );
};
export default Login;



