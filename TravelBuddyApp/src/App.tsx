import { useState, useEffect } from 'react'
import './App.css'
import HomePage from './Pages/HomePage';

function App() {
  const [data, setData] = useState<String>("");

  useEffect(() => {
    fetch("http://localhost:5229/user/getusers")
    .then((res) => res.json())
    .then((d) => setData(JSON.stringify(d)))
    .catch((err) => console.error("Error: ", err));
  }, []);

  return (
    <>
      <main>
        <HomePage />
      </main>
    </>
  )
}

export default App
