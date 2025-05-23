import './SignUpPage.css';
import logo from '../files/logo_main.png';
import {React} from 'react';
import {useHistory} from "react-router-dom";
import { NavLink } from 'react-router-dom/cjs/react-router-dom.min';

export const SignUpPage = () => {
    const history = useHistory();
    var type = "";
    localStorage.setItem("UserType", "undefined");

    function setStudent() {
      type = "student";
    }

    function setTutor() {
      type = "tutor";
    }

    function handleSubmit(event) {
      if (type === "student") {
        handleSubmitStudent(event);
      } else {
        handleSubmitTutor(event);
      }
    }

    function hash_password(password) {
      let hash = 0;

      if (password.length === 0) {
        return hash;
      }
    
      for (let i = 0; i < password.length; i++) {
        const char = password.charCodeAt(i);
        hash = (hash << 5) - hash + char;
      }
    
      const hashedString = hash.toString(36);
      return hashedString;
    }

    function handleSubmitStudent (event) {
        event.preventDefault();

        fetch('http://localhost:5000/api/students', {
          method: 'POST',
          headers: {"Content-Type":'application/json'},
          body: JSON.stringify({id:0,
            name:event.target.fname.value,
            surname: event.target.lname.value,
            email: event.target.email.value,
            phoneNumber: event.target.phone.value,
            password: hash_password(event.target.password.value),
            dateOfBirth:event.target.dateOfBirth.value,
            rating:0.0,
            imagePath:"https://onlytutors/default/student.png",
            highestLevelOfEducation: "",
          }),
        }).then(responseJson =>  {    
            history.push("/");
            window.location.reload();
        });
    }
    
    function handleSubmitTutor (event) {
        event.preventDefault();

        fetch('http://localhost:5000/api/tutors', {
          method: 'POST',
          headers: {"Content-Type":'application/json'},
          body: JSON.stringify({id:0,
            name:event.target.fname.value,
            surname: event.target.lname.value,
            email: event.target.email.value,
            phoneNumber: event.target.phone.value,
            password: hash_password(event.target.password.value),
            dateOfBirth:event.target.dateOfBirth.value,
            rating:0.0,
            imagePath:"https://onlytutors/default/tutor.png",
            description: "",
            experience: "",
          }),
        }).then(responseJson =>  {  
            console.log("1");  
            history.push("/");
        });
    }

  return (
    <div className='columns'>
        <div className='row1'>
          
          <form onSubmit={handleSubmit}>

            <div className='item signup username'>
                <h2>Name:</h2>
                <input placeholder='your first name' name="fname" required></input>
            </div>

            <div className='item signup username'>
                <h2>Surname:</h2>
                <input placeholder='your last name' name="lname" required></input>
            </div>

            <div className='item signup username'>
                <h2>Date of Birth:</h2>
                <input name="dateOfBirth" type="date" required></input>
            </div>

            <div className='item signup username'>
                <h2>Email:</h2>
                <input placeholder='your email adress' name="email" type="email" required></input>
            </div>

            <div className='item signup username'>
                <h2>Phone:</h2>
                <input placeholder='your phone number' name="phone" type="tel" required></input>
            </div>

            <div className='item signup username'>
                <h2>Password:</h2>
                <input placeholder='your password' name="password" type='password' required></input>
            </div>
            <div className='login'>
                <button type='submit' className="submit-button" onClick={setStudent}>Sign Up as Student</button>
            </div>
            <div className='login sign'>
                <button type='submit' className="submit-button" onClick={setTutor}>Sign Up as Tutor</button>
            </div>
            <NavLink to='/'>
              <div className='login sign'>
                  Already have an account? Sign in!
              </div>
            </NavLink>
          </form>
        </div>
    </div>
  );
}

