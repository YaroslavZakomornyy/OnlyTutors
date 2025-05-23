import './Home.css';
import {SearchBarClasses} from '../SearchBarClasses/SearchBarClasses';
import {ClassTile} from '../ClassTile/ClassTile';
import {NewProject} from '../NewClass/NewClass';
import {React, useState, useEffect} from 'react';

export const Home = () => {

  localStorage.setItem("ProfileId", localStorage.getItem("UserId"));
  const [lesson, setLessons] = useState([]);
  
    useEffect(() => {
      fetch("http://localhost:5000/api/lessons",{
      method: "GET",
      mode: "cors"
    }).then(response => {
      return response.json()
    }).then(jsonResponse => {
      if(!jsonResponse) {
        return [];
      }
      setLessons(jsonResponse);
    })
    }, [])
  
    const updateParentState = (obj) => {
      var check = 0;
      if (obj.open)
        check = 1;
      fetch(`http://localhost:5000/api/lessons/search/${obj.str}/${check}`,{
      method: "GET"
    }).then(response => {
      return response.json()
    }).then(jsonResponse => {
      if(!jsonResponse) {
        return [];
      }
      setLessons(jsonResponse);
    })
    };

  
  return (
    <div className="body">

      <div className="content">
        <SearchBarClasses updateParentState={updateParentState}/>
        <div className="projects">
            { 
              (lesson.length !== 0) &&
                lesson.map((item) => {
                  if (item.students !==0 && item.students.some(user => user.id == localStorage.getItem("UserId"))) {
                    return (
                      <div class = "new-color item">
                        <ClassTile lesson={item}/>
                      </div>
                    )
                  } else {
                    return (
                      <div class = "item">
                        <ClassTile lesson={item}/>
                      </div>
                    )
                  }
              })
            }
            
          
          {/* <div className = "item">
            <ClassTile lesson = {{students: [], name: 'Database Design', description: 'This course covers the fundamentals and applications of database management systems, including data models, relational database design, query languages, and web-based database applications.'}}/>
          </div> 
          <div className = "item">
            <ClassTile lesson = {{students: [], name: 'Database Design', description: 'This course covers the fundamentals and applications of database management systems, including data models, relational database design, query languages, and web-based database applications.'}}/>
          </div> 
          <div className = "item">
            <ClassTile lesson = {{students: [], name: 'Database Design', description: 'This course covers the fundamentals and applications of database management systems, including data models, relational database design, query languages, and web-based database applications.'}}/>
          </div>  */}
          
        </div>
      </div>
      { //localStorage.setItem('UserType', "tutor/student"); 
      localStorage.getItem('UserType') === "tutor" && (
        <div className='new-project'>
              <NewProject />
        </div>
      )}
    </div>
  );
}

