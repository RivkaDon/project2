import './App.css';
import ChatPage from './ChatPage/ChatPage.js';
import BasePage from './signIn/RegisterPage';
import SignPage2 from './signIn/SignInPage';
// import RegisterPage from './RegisterPage/RegisterPage2'
// import SignInPage from './signIn/SignInPage/SignInPage.js';
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import React from 'react';


function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<SignPage2 />} />
        <Route path="/chat" element={<ChatPage />} />
        <Route path="/register" element={<BasePage />} />
      </Routes>
    </Router>
  )
}

export default App;
