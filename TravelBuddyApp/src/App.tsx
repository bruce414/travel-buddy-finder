import { useState, useEffect, act } from 'react'
import './App.css'
import './index.css'
import Navbar from './Components/Navbar'
// import SideBar from './Sections/SideBar';
// import FriendMessagePanel from './Sections/FriendMessagePanel';
// import GroupMessagePanel from './Sections/GroupMessagePanel';
// import ExploreFriendsPanel from './Sections/ExploreFriendsPanel';
// import ExploreTripsPanel from './Sections/ExploreTripsPanel';
// import axios from 'axios';
// import type { Message } from './Models/Message';
// import type { User } from './Models/User';
// import { SiD } from 'react-icons/si';

const App = () => {
  return (
    <Navbar />
  )
}

export default App

// function App() {
//   type PanelType = "FriendMessage" | "GroupMessage" | "ExploreFriends" | "ExploreTrips";

//   const [activePanel, setActivePanel] = useState<PanelType>("FriendMessage");
//   const [currentUser, setCurrentUser] = useState<User | null>(null);
//   const [RecentContacts, setRecentContacts] = useState<Message[]>([]);

//   useEffect(() => {
//     const fetchUser = async() => {
//       try {
//         const res = await axios.get(`http://localhost:5223/api/user/${1}`)
//         setCurrentUser(res.data);
//       } catch (err) {
//         console.log("Failed to fetch user");
//       }
//     };
//     fetchUser();
//   }, [])

//   useEffect(() => {
//     const fetchRecentContacts = async() => {
//       if (activePanel === "FriendMessage" && currentUser){
//         try{
//           const res = await axios.get(`http://localhost:5223/api/message/${1}`);
//           setRecentContacts(res.data);
//         } catch (err) {
//           console.log("Failed to fetch recent contacts of the user");
//         }
//       }
//     };
//     fetchRecentContacts();
//   }, [activePanel, currentUser])

//   // useEffect(() => {
//   //   fetch("http://localhost:5229/user/getusers")
//   //   .then((res) => res.json())
//   //   .then((d) => setData(JSON.stringify(d)))
//   //   .catch((err) => console.error("Error: ", err));
//   // }, []);

//   const renderPanel = () => {
//     switch (activePanel) {
//       case "FriendMessage":
//         return <FriendMessagePanel messages = {RecentContacts}/>;
//       case "GroupMessage":
//         return <GroupMessagePanel />;
//       case "ExploreFriends":
//         return <ExploreFriendsPanel />;
//       case "ExploreTrips":
//         return <ExploreTripsPanel />;
//       default:
//         return null;
//     }
//   }

//   return (
//     <div>
//       {/* <SideBar setActivePanel = {setActivePanel} /> */}
//       <div className='flex-1 p-6'>{renderPanel()}</div>
//     </div>
//   )
// }

// export default App
