import React from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faMapSigns, faPlane, faBriefcase } from '@fortawesome/free-solid-svg-icons'
import { faMeetup } from '@fortawesome/free-brands-svg-icons'

const FeatureCard = () => {
  return (
    <>
        <div className='flex flex-col rounded-2xl shadow-xl ring-1 ring-black/10 px-4 py-4 gap-1 text-start max-w-70'>
            <div className='flex bg-blue-300 rounded-2xl h-10 w-10 justify-center items-center'>
                <FontAwesomeIcon icon={faMeetup} className='text-blue-500' />
            </div>

            <p className='font-semibold'>Match & Chat</p>

            <p className='text-gray-400'>We provide the best matching service to match you with the best buddies possible.</p>
        </div>
    </>
  )
}

export default FeatureCard