import React from 'react';
import tailwindcss from '@tailwindcss/vite';
import { useState, useEffect } from 'react';
import { FaComment } from "react-icons/fa";
import { FaUser } from 'react-icons/fa';
import { FaUserPlus } from 'react-icons/fa';
import { FaPlaneDeparture } from 'react-icons/fa';
import axios from 'axios';

interface SideBarProps {
    setActivePanel: (panel: "friends" | "groups" | "explore-friends" | "explore-trips") => void;
}

const SideBar = ({setActivePanel}: SideBarProps) => {
    const sidebarMenuItems = [
        {icon: <FaComment className='h-6 w-6 bg-red-400' />, hoverColor: "hover:bg-red-300", onclick: () => setActivePanel("friends")},
        {icon: <FaUser className='h-6 w-6 bg-blue-400' />, hoverColor: "hover:bg-blue-300", onclick: () => setActivePanel("groups")},
        {icon: <FaUserPlus className='h-6 w-6 bg-green-400' />, hoverColor: "hover:bg-green-300", onclick: () => setActivePanel("explore-friends")},
        {icon: <FaPlaneDeparture className='h-6 w-6 bg-yellow-400'/>, hoverColor: "hover:bg-yellow-300", onclick: () => setActivePanel("explore-trips")}
    ]

    return (
        <div className='h-screen w-16 mt-0 ml-0 mb-0 fixed bg-amber-50 flex flex-col items-center py-4 space-y-4'>
            {sidebarMenuItems.map((item, index) => (
                <button key={index} className={`flex items-center justify-center w-12 h-12 rounded-md transition ${item.hoverColor}`}
                    onClick={item.onclick}>
                    {item.icon}
                </button>   
            ))}
        </div>
    )
}

export default SideBar