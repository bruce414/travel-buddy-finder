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
      <div className='bg-white min-h-screen'>
        <nav className='flex items-center justify-between px-6 py-4 border-b shadow-sm'>
          <div className='flex items-center space-x-4'>
            {/* logo img src */}
          </div>
          <div className='hidden md:flex items-center space-x-8'>
            <div className='flex flex-col items-center text-black'>
              <div className='flex flex-row justify-center'>
                <img src='...' className='h-6'></img>
                <span className='font-semibold'>Home</span>
              </div>
              <div className='h-1 w-6 bg-black mt-1 rounded-full'></div>
            </div>
            <div className='flex flex-col items-center text-black'>
              <div className='flex flex-row justify-center'>
                <img src='...' className='h-6'></img>
                <span>Explore</span>
              </div>
              <div className='h-1 w-6 bg-black mt-1 rounded-full'></div>
            </div>
            <div className='flex flex-col items-center text-black'>
              <div className='flex flex-row justify-center'>
                <img src='...' className='h-6'></img>
                <span>Connect</span>
              </div>
              <div className='h-1 w-6 bg-black mt-1 rounded-full'></div>
            </div>
          </div>
        </nav>
      </div>
    </>
  )
}

export default Navbar