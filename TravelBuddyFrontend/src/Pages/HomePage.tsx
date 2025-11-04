import React from 'react'
import Navbar from '../Components/Navbar'
import travelCover from '../assets/travel-cover.jpg'
import BookingBox from '../Components/BookingBox'
import FeatureCard from '../Components/FeatureCard'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faArrowRight } from '@fortawesome/free-solid-svg-icons'

const HomePage = () => {
 
  return (
    <>
        <Navbar />
        
        <div className='relative flex'>
            <div className='px-20 flex flex-[0.5] flex-col gap-8 text-start justify-center'>
                <div className='gap-4'>
                    <h3 className='text-5xl font-bold'>Find you next</h3>
                    <h3 className='text-5xl font-bold'>travel buddy</h3>
                </div>

                <p className='text-sm'>Connect with explores, share trips, and explore together</p>

                <BookingBox />
            </div>

            <div className='flex-1'>
                <img src={travelCover} />
            </div>
        </div>

        <div className='px-20 pt-10 flex flex-col text-start gap-7'>
            <div className='flex items-center gap-5'>
                <p className='text-blue-500 font-semibold text-[18px]'>What We Give</p>
                <FontAwesomeIcon icon={faArrowRight} className='text-blue-500 font-semibold' />
            </div>

            <div className='flex justify-between'>
                <div className='flex flex-col gap-8'>
                    <div className=''>
                        <p className='font-extrabold text-4xl'>Best Features</p>
                        <p className='font-semibold text-3xl'>For You</p>
                    </div>
                    <p className='text-sm text-gray-400'>
                        We will provide the best features for <br />
                        those of you who want to travel <br />
                        comfortably with your family.
                    </p>
                </div>

                <div className='flex gap-3'>
                <FeatureCard />
                <FeatureCard />
                <FeatureCard />
                </div>
            </div>
        </div>
    </>
  )
}

export default HomePage