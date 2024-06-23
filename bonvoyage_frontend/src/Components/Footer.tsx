import {Grid, TextField } from "@mui/material";

function Footer(){
    return(
    <>
    <div style={{display:"flex",justifyContent:'center',backgroundColor:'black', padding:"100px 0"}}>
        <div className="Footer">
            <Grid container>
                <Grid md={2} xs={4}>
            <div className="footerone">
        <h1 > About</h1>
        <h4><a href="/about" style={{color:'white'}}>About Us</a></h4>
        </div>
                </Grid>
                <Grid md={2} xs={4}>
                    <div className="footerone">
<h1>Support</h1>
        <h4>Contact Us</h4>
        <h4>+914889239293</h4>
        </div>
                </Grid>
                <Grid md={2}xs={4}>
                    <div className="footerone">
                <h1>FAQ</h1>
        <h4><a href="/faq" style={{color:'white'}}>FAQ</a></h4>
        <h4><a href="/login" style={{color:'white'}}>Login</a></h4>
        <h4><a href="/register" style={{color:'white'}}>Register</a></h4>
        </div>
                </Grid>
                <Grid md={6}xs={12}>
                    <div  className="footersecound">
                <h1>NewsLetter</h1>
                <p>Don't miss out on the exciting world of travel -subscribe now and embark on a journey of discovery with us.</p>
                <div style={{display:'flex',justifyContent:'center',alignItems:'center',gap:'10px' ,backgroundColor:'white',padding:'5px 5px',borderRadius:'5px'}}>
                <TextField id="outlined-basic" label="Email" variant="outlined" placeholder="Email"/>
                    <button style={{background:'white',color:'black'}}>Submit</button>
                </div>
                </div>
                </Grid>
            </Grid>
            <h6>@c 2023 Bonvoyage,All Rights Reserved</h6>
        </div>
    </div>
    </>
    )

}
export default Footer;