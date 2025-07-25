import React from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBell } from '@fortawesome/free-solid-svg-icons';

const Navbar = () => {
  const handleNotifcation = (Event: React.MouseEvent<HTMLButtonElement>) => {
    Event.preventDefault();
    console.log("Notification button clicked");
  }

  return (
    <>
      <header>
        <nav className='top-nav'>
          <a href="/" className='logo'>TravelBuddyFinder</a>
          <ul className='topnav-links'>
            <li><a href='/'>Home</a></li>
            <li><a href='/trips'>Trips</a></li>
            <li><a href='/connect'>Connect</a></li>
          </ul>
          <div className='admin-section'>
            <button onClick={handleNotifcation} className='notification-button'>
              <FontAwesomeIcon icon={faBell} size='lg' />
            </button>
          </div>
        </nav>
      </header>
    </>
  )
}

export default Navbar