
import OpenChat from '../Chats/ChatCard.js'
import './ContactCard.css';
import { useRef, useState } from "react";
import React from 'react';


function ContactCard({ name, lastMessages, setter, flagSetter}) {
  const setChat = ()=>{setter(name); flagSetter(true)};
  return (
    <div className="container" onClick={setChat}>
      <ol className="list-group">
        <li className="list-group-item d-flex justify-content-between align-items-start">  
        <img></img>
          <div className="ms-2 me-auto">
            <div className="fw-bold">{name}</div>
            <div>{lastMessages}</div>
          </div>
          <div className="position-absolute bottom-0 end-0" id='time'>{lastMessages}</div>
        </li>
      </ol>
    </div>
  );
}

export default ContactCard;
