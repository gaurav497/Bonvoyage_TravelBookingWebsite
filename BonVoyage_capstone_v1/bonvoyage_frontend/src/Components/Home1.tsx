import './Home1.css'

function Home1() {
  return (
    <div style={{display:'flex',flexDirection:'column'}} className="container">
      <div className="main-content">
        <div className="background-overlay"></div>
        <div className="content-overlay" style={{backgroundImage:"url('homebg.jpg')",display:'flex',flexDirection:'column',justifyContent:'center',alignItems:'center'}}>
          <h2 className="heading">Unlock Your Next Adventure With Seamless Travel Planning</h2>
          <a href="/allPackages" className="button">View Packages</a>
        </div>
      </div>
      
      <h3 className="sub-heading">Your Gateway to seamless journey and unforgettable adventures</h3>
      <div className="table-box">
        <div className="table-element" style={{borderLeft: '1px dotted #000', height: '200px'}}>
          <h3>Tell us what you want to do</h3>
          <p>Let us know your preferences and interests</p>
        </div>
        <div className="table-element" style={{borderLeft: '1px dotted #000', height: '200px'}}>
          <h3>Share your Travel Locations</h3>
          <p>Share your desired travel destinations</p>
        </div>
        <div className="table-element" style={{borderLeft: '1px dotted #000', height: '200px'}}>
          <h3>We are trusted agency</h3>
          <p>Count on us for reliable and hassle-free travel experiences</p>
          
        </div>
      </div>
    </div>
  );
}

export default Home1;
