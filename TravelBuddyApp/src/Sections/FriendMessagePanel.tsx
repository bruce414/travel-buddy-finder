import React from 'react'
import type { Message } from '../Models/Message';

interface FriendMessagePanelProps {
    messages: Message[];
}

const FriendMessagePanel = ({messages}: FriendMessagePanelProps) => {
    if (!messages.length) {
        return <p className='text-black'>No Recent Contacts</p>
    }

    return (
       <div>
            <h2 className='text-xl font-bold mb-4'>Recent conversations</h2>
            <ul className='space-y-2'>
                {messages.map((msg) => (
                    <li key={msg.messageId} className='p-3 border rounded-2xl'>
                        <strong>{msg.senderId}</strong>
                    </li>
                ))}
            </ul>
       </div>
    )
}

export default FriendMessagePanel