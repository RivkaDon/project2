
import OpenChat from '../Chats/ChatCard.js'
import './ContactCard.css';
import { useRef, useState } from "react";
import React from 'react';


const formatTime = function(time) {
  if (time != null) {      
        const myArray = time.split("T");
        const finalArray = myArray[1].split(".");
        return myArray[0]+" "+finalArray[0];  
      }
  return "";
    }
function ContactCard({ id, name, lastMessages, setter, flagSetter, idSetter}) {
  const setChat = ()=>{setter(name); flagSetter(true); idSetter(id);};
  return (
    <div className="container" onClick={setChat}>
      <ol className="list-group">
        <li className="list-group-item d-flex justify-content-between align-items-start">  
        <img></img>
          <div className="ms-2 me-auto">
            <div className="fw-bold">{name}</div>
            <div>{lastMessages.at(3).at(1)}</div>
          </div>
          <div className="position-absolute bottom-0 end-0" id='time'>{formatTime(lastMessages.at(4).at(1))}</div>
        </li>
      </ol>
    </div>
  );
}

export default ContactCard;
