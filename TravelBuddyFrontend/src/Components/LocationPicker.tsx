import React, { type JSX } from 'react'
import { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faLocationDot } from '@fortawesome/free-solid-svg-icons';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

type Country = {
    name: string,
    flag: string,
}

type LocationPickerProps = {
    selected: Country | null,
    onSelect: (country: Country) => void,
}

const countries = [
  { name: "Australia", flag: "🇦🇺" },
  { name: "France", flag: "🇫🇷" },
  { name: "Germany", flag: "🇩🇪" },
  { name: "Japan", flag: "🇯🇵" },
  { name: "New Zealand", flag: "🇳🇿" },
  { name: "United States", flag: "🇺🇸" },
  { name: "Fiji", flag: "🇫🇯" },
];

const LocationPicker = ({selected, onSelect} : LocationPickerProps): JSX.Element => {
  const [open, setOpen] = useState(false);
  const [search, setSearch] = useState("");

  const filteredCountries = countries.filter((c) => c.name.toLowerCase().includes(search.toLowerCase()));

  return (
    <>
        <div className='relative'>
            <div onClick={() => setOpen(!open)}
            className='flex gap-3.5 items-center cursor-pointer'>
                <div className='rounded-full bg-gray-200 p-2'>
                    <FontAwesomeIcon icon={faLocationDot} />
                </div>
                <div className='flex flex-col'>
                    <p className='font-semibold'>Location</p>
                    <p className='text-sm text-gray-400'>
                        {selected ? 
                        <div className='flex items-center gap-2.5'>
                            <div className=''>{selected.flag}</div>
                            <div className=''>{selected.name}</div>
                        </div> :"Where are you going?"}
                    </p>
                </div>
            </div>

            {open && (
                <div className='absolute top-18 left-0 z-20 bg-white rounded-2xl shadow-lg w-64 p-3'>
                    <div className='flex items-center border-b pb-2 mb-2'>
                        <FontAwesomeIcon icon={faSearch} className='text-gray-400' />
                        <input type='text' placeholder='search' className='flex-1 text-sm outline-none' value={search}
                            onChange={(e) => setSearch(e.target.value)} />
                    </div>

                    <div className='max-h-52 overflow-y-auto'>
                        {filteredCountries.map((country) => (
                            <div key={country.name} 
                            onClick={() => {
                                onSelect(country);
                                setOpen(false);
                            }}
                            className='flex items-center gap-3 p-2 rounded-lg hover:bg-blue-50 cursor-pointer'>
                                <span className='text-lg'>{country.flag}</span>
                                <span className='text-sm'>{country.name}</span>
                            </div>
                        ))}
                    </div>

                    {/* <button
                        className="w-full mt-3 py-2 rounded-lg bg-gray-200 text-gray-600 font-medium hover:bg-gray-300"
                        onClick={() => setOpen(false)}>
                        Apply
                    </button> */}
                </div>
            )}
        </div>
    </>
  )
}

export default LocationPicker