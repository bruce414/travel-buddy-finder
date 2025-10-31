import React from 'react'

const Navbar = () => {
  return (
    <>
        <nav className='flex items-center justify-between px-20'>
            <div className='cursor-pointer text-2xl font-bold'>
                TravelBuddy<strong className='text-blue-500'>Finder</strong>
            </div>
            
            <div className='pl-30 flex justify-center items-center gap-25'>
                <ul className='flex list-none gap-5'>
                    <li className='cursor-pointer text-[19px]'><a>Home</a></li>
                    <li className='cursor-pointer text-[19px]'><a>Trips</a></li>
                    <li className='cursor-pointer text-[19px]'><a>Buddies</a></li>
                    <li className='cursor-pointer text-[19px]'><a>Messages</a></li>
                    <li className='cursor-pointer text-[19px]'><a>Guides</a></li>
                </ul>

                <div className='bg-blue-500 text-white py-3 px-4 rounded-2xl cursor-pointer text-[18px]'>Login / Sign up</div>
            </div>
        </nav>
    </>
  )
}

export default Navbar