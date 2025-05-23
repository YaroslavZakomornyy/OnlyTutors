import './SearchBarTutors.css';
import {React} from 'react';


export const SearchBarTutors = ({ updateParentState }) => {
  const handleSubmit = (event) => {
    event.preventDefault();
    let form = event.target;
    updateParentState(form.searchstr.value);
  };

    return (
      <form onSubmit={handleSubmit}>
          <div className="SearchBar">
            <input name="searchstr" placeholder="Find your group!"/>
            <button className="SearchButton">Search</button>
          </div>
      </form>
    )
} 