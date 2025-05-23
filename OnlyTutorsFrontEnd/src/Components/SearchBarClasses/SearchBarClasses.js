import './SearchBarClasses.css';
import {React} from 'react';


export const SearchBarClasses = ({ updateParentState }) => {

  const handleSubmit = (event) => {
    event.preventDefault();
    let form = event.target;
    updateParentState({str: form.searchstr.value,
                      open: form.checkbox.checked,                  
    });
  };


    return (
      <form onSubmit={handleSubmit}>
          <div className="SearchBar">
            <label className="switch">
              <input type="checkbox" name="checkbox"/>
              <span className="slider round"></span>
            </label>
            <span class="switch-label-text">Open Sections</span>
            <input name="searchstr" placeholder="Find your group!"/>
            <button className="SearchButton">Search</button>
          </div>
      </form>
    )
} 