import './ProfilePicture.css';
import {React, useState, useEffect} from 'react';
import profile_pic from '../files/Default_pfp.png';

export const ProfilePicture = () => {
    const [data, setData] = useState({});
    useEffect(() => {
        
        fetch(`http://localhost:5000/api/${localStorage.getItem("UserType")+"s"}/${localStorage.getItem("UserId")}`).then(response => {
            return response.json()
          }).then(jsonResponse => {
            if(!jsonResponse) {
              return [];
          }
          else
            setData(jsonResponse);
          
        })
      }, []);

    return (
        <div className = 'profile'>
            <img src={data.imagePath} alt='Profile'/>
        </div>
    )
}