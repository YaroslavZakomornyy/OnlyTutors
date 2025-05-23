import './Navigation.css';
import topbar from '../files/topbar.png';
import {ProfilePicture} from '../ProfilePicture/ProfilePicture';
import {NavLink} from "react-router-dom";

export const Navigation = () => {
  
  return (
      <div className="nav">
        {(localStorage.getItem("UserType") !== "undefined") &&
        (
        <div className="nav-item">
          <div>
            <NavLink to='/home'>
                <a style={{textDecoration: 'none'}}>Classes</a>
            </NavLink>
          </div>
          
          <div>
            <NavLink to='/tutors'>
                <a style={{textDecoration: 'none'}}>Tutors</a>
            </NavLink>
          </div>
        </div>)}

        <div className="nav-item">
            <img src={topbar} alt='OnlyTutors logo'/>
        </div>

        {(localStorage.getItem("UserType") !== "undefined") &&
        (<div className="nav-item">
          <NavLink to={{ pathname: '/profile', state: { user : { type: localStorage.getItem("UserType"),
                                                                id: localStorage.getItem("UserId"), 
                                                                } }} }>
            <div className='profile'>
              <ProfilePicture />
            </div>
          </NavLink>
        </div>)}

        
      </div>
  );
}

