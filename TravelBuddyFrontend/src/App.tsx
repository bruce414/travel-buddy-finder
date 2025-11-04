import React from "react";
import HomePage from "./Pages/HomePage";
import Navbar from "./Components/Navbar";
import LoginPage from "./Pages/LoginPage";
import { Routes, Route } from "react-router-dom";

const App = () => {
  return(
    <>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/login" element={<LoginPage />} />
      </Routes>
    </>
  )
}

export default App