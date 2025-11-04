import React from 'react'
import { useNavigate } from 'react-router-dom'
import loginCover from '../assets/login-cover.jpg'
import lantern1 from '../assets/lantern1.jpg'

const LoginPage = () => {
  const navigate = useNavigate();

  return (
    <>
        <div className='flex min-h-screen justify-center items-center'>
            <div className='flex-[0.5]'></div>

            <div className='flex-1'>
                <img src={loginCover} className='h-screen w-full object-cover'/>
            </div>
        </div>
    </>
  )
}

export default LoginPage