
import ShieldSharpIcon from '@mui/icons-material/ShieldSharp';
import SupportAgentTwoToneIcon from '@mui/icons-material/SupportAgentTwoTone';
import ThumbUpAltRoundedIcon from '@mui/icons-material/ThumbUpAltRounded';
import HandshakeTwoToneIcon from '@mui/icons-material/HandshakeTwoTone';
import HolidayVillageTwoToneIcon from '@mui/icons-material/HolidayVillageTwoTone';

const Whyarray=[
    {
        icon : <ShieldSharpIcon style={{fontSize:'large',height:"80px",width:"80px"}}/>,
        title:'Your Advaisor',
        Description:' We will answer your every question. Our experienced travel experts and real-time on field know-how gives us this advantage. '
    },
    {
        icon: <SupportAgentTwoToneIcon style={{fontSize:'large',height:"80px",width:"80px"}}/>,
        title:'We Love Listing',
        Description:' Your holiday, your terms. We’ll fill in the blanks to plan the perfect trip in the blink of an eye.'
    },
    {
        icon:<ThumbUpAltRoundedIcon style={{fontSize:'large',height:"80px",width:"80px"}}/>,
        title:'Memorable Experiance',
        Description:' Do everything or do nothing. Either way, your holiday will be nothing less than extraordinary.  '
    },
    {
        icon:<HandshakeTwoToneIcon style={{fontSize:'large',height:"80px",width:"80px"}}/>,
        title:'Easy as ABC',
        Description:'Travel smooth and stress-free. That’s how easy we make it because that’s how your holiday should be.'
    },
    {
        icon:<HolidayVillageTwoToneIcon style={{fontSize:'large',height:"80px",width:"80px"}}/>,
        title:'Handcrafted Holidays',
        Description:' We interact with our loyal customers to co-create unique experiences that will take your holiday to the next level.  '
    }
];

function WhyUS() {
  return (
    <div className='WhyusContainer' style={{textAlign:"center",padding:"20px"}}>
    <h2>Why BonVoyage ?</h2>
    <div className="WhyusItem" style={{display:'flex',textAlign:'center'}}>
        {Whyarray.map((item, index)=>(
            
            <div key={index}>
            <div>{item.icon}</div>
            <h3 style={{marginBottom:'10px'}}>{item.title}</h3>
            <p style={{paddingLeft:'20px',paddingRight:'20px'}}>{item.Description}</p>
            </div>
        ))}
        </div>
    
    </div>
  );
}

export default WhyUS