import React from 'react'
import { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faLocationDot, faCalendar } from '@fortawesome/free-solid-svg-icons'
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import LocationPicker from './LocationPicker';

type Country = {
    name: string,
    flag: string,
}

const BookingBox = () => {
  const [startDate, setStartDate] = useState(new Date());
  const [selectedCountry, setSelectedCountry] = useState<Country | null>(null);
  const [selectedDate, setSelectedDate] = useState(null);
  const [open, setOpen] = useState(false);

  return (
    <>
        <div className='absolute left-1/18 top-[70%] flex rounded-2xl shadow-xl ring-1 ring-black/10 px-8 py-4 gap-5 items-center'>
            {/* <div className='relative'>
                <div onClick={() => setOpen(!open)}
                className='flex gap-3.5 items-center cursor-pointer'>
                    <div className='rounded-full bg-gray-200 p-2'>
                        <FontAwesomeIcon icon={faLocationDot} />
                    </div>
                    <div className='flex flex-col'>
                        <p className='font-semibold'>Location</p>
                        <p className='text-sm text-gray-400'>Where are you going?</p>
                    </div>
                </div>
            </div> */}
            <LocationPicker selected={selectedCountry} onSelect={setSelectedCountry} />

            <div className="h-8 w-px bg-gray-300 mx-2"></div>

            <div className='flex gap-3.5 items-center cursor-pointer'>
                <div className='rounded-full bg-gray-200 p-2'>
                    <FontAwesomeIcon icon={faCalendar} />
                </div>
                <div className='flex flex-col'>
                    <p className='font-semibold'>Select Date</p>
                    {/* <p className='text-sm text-gray-400'>27th Oct 2025</p> */}
                    <DatePicker
                        selected={startDate}
                        onChange={(date) => setStartDate(date!)}
                        placeholderText="27th Oct 2025"
                        className="border-none focus:ring-0 text-gray-400 text-sm"
                    />
                </div>
            </div>

            <div className='flex rounded-2xl bg-blue-500 text-white font-semibold justify-center items-center px-3 py-2 cursor-pointer'>
                Find Buddies
            </div>
        </div>
    </>
  )
}

export default BookingBox