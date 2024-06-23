import "./PageNotFound.css";
import { Button } from "@mui/material";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
const PageNotFound = () => {
  const navigate=useNavigate()
   
  return (
    <>
      <div className="topPageNotFound">
        <div className="pagenotfound"></div>
      </div>

      <div className="gotohome">
        <Link to="/">
          <Button>GO TO HOMEPAGE</Button>
        </Link>
      </div>
    </>
  );
};

export default PageNotFound;
