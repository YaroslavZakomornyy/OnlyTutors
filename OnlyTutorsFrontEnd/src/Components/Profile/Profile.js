import './Profile.css';
import {React, useState, useEffect } from 'react';
import { EditProfile } from '../EditProfile/EditProfile';

export const Profile = ({location}) => {
  const user = location.state.user;
  const [data, setData] = useState({});


  useEffect(() => {
    fetch(`http://localhost:5000/api/${user.type+"s"}/${user.id}`).then(response => {
        return response.json()
      }).then(jsonResponse => {
        if(!jsonResponse) {
          return [];
      }
      else
        setData(jsonResponse);
      
    })
  }, []);


  const displayInfo = () => {
    if(localStorage.getItem('UserType') === "tutor" || user.type === "tutor") {
      return (
      <div>
        <div className='item profile-description'>
            <h2>{data.description}</h2>
        </div>
        <div className='item profile-experience'>
            <h2>{data.experience}</h2>
        </div>
      </div>
        
      )
    }
    else {
      return (
        <div>
          <div className='item profile-experience smaller'>
            <table>
              <tr>
                <td style={{width: "30%"}}>
                  <h2>Email:</h2>
                </td>
                <td>
                  <h2>{data.email}</h2>
                </td>
              </tr>
            </table>
          </div>
          <div className='item profile-experience smaller'>
            <table>
                <tr>
                  <td style={{width: "30%"}}>
                    <h2>Phone:</h2>
                  </td>
                  <td>
                    <h2>{data.phoneNumber}</h2>
                  </td>
                </tr>
              </table>
          </div>
          <div className='item profile-experience smaller'>
            <table>
                <tr>
                  <td style={{width: "30%"}}>
                    <h2>Date of Birth:</h2>
                  </td>
                  <td>
                    <h2>{data.dateOfBirth !== undefined && data.dateOfBirth.substring(0, 10)}</h2>
                  </td>
                </tr>
              </table>
          </div>
          <div className='item profile-experience smaller'>
            <table>
                <tr>
                  <td style={{width: "30%"}}>
                    <h2>Education:</h2>
                  </td>
                  <td>
                    <h2>{data.highestLevelOfEducation}</h2>
                  </td>
                </tr>
              </table>
          </div>
        </div>
      )
    }
  }


  return (
      <div className='columns'>
        <div className='row1'>
          
          <div className='item profile-image'>
            <img src={data.imagePath} alt='profile_picture'/>
          </div>

          <div className='item profile-name'>
            <h2>{(data.name + " " + data.surname)}</h2>
          </div>

          {(localStorage.getItem('UserType') === "tutor"|| user.type === "tutor") && 
            (<div className='item profile-contact'>
              <h2>Contacts:<br/>Email: {data.email}<br/> Phone: {data.phoneNumber}<br/> Date of Birth: {data.dateOfBirth !== undefined && data.dateOfBirth.substring(0, 10)} <br/> </h2>
            </div>)
          }
        </div>
        <div className='row2'>
          {displayInfo()}
        </div>
        {
        (localStorage.getItem('UserId') == data.id) &&
            (<div className='new-project'>
            <EditProfile data={data}/>
          </div>
        )}   
      </div>
  );
}

