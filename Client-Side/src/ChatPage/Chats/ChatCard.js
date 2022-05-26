import React, { useEffect } from 'react';
import AttachImage from './ImageAttachment';
import { useRef, useState } from "react";
import './ChatCard.css'
import AttachVideo from './VideoAttachment';
import AttachSound from './SoundAttachment';
import AttachRecording from './RecordAttachment';

function OpenChat({ getter, messageGetter, messageSetter, contactSetter, setReRender, imageGetter, lastMessages, contactId, myConn}) {
    // in order to go to bottom of scrollbar after opening each chat so we can see the last messeges in convo
    const MyComponent = () => {
        // lastMessages.current = messageGetter לפני הset
        const divRef = useRef(null);
      
        useEffect(() => {
          divRef.current.scrollIntoView({ behavior: 'smooth' });
        });
      
        return <div ref={divRef} />;
      }
    const [getMessage, setMessage] = useState(0);
    const newMessage = useRef();
    const showNewMessage = async() => {
        messageGetter.push([1, newMessage.current.value, (new Date()).toLocaleString(), 'text']);
        try {
            await myConn.invoke('Send', newMessage.current.value, contactId);
        }
        catch(e) {
            console.log(e);
        }

        setMessage(getMessage+1);
        newMessage.current.value = "";
        setReRender(messageGetter[messageGetter.length-1][2]);
        
    };
    const messageArr = messageGetter;
    
    const loadMessages = function () {
        if (messageArr) {
            return (messageArr.map((element,key) => {
                
                if (element[3][1])
                {
                return <div key={element[0][1]} className="sent"><div className="badge bg-secondary ">{element[1][1]}
                <div className="dateAndTime">{element[2][1]}</div></div></div>;
                }
                else {
                return <div key={element[0][1]} className="recieved"><div className="badge bg-secondary ">{element[1][1]}
                        <div className="dateAndTime">{element[2][1]}</div></div></div>;
                    
                }
            }
            ));
        }
    };
    return (
        <div><span id = "chatNickName"><img id="contactImage" src={imageGetter}></img>{getter}</span><div className="chatElement" id="background">
            <AttachImage messageGetter={messageGetter} setReRender={setReRender} loadMessages={loadMessages} setMessage={setMessage} getMessage={getMessage}/>
            <AttachVideo messageGetter={messageGetter} setReRender={setReRender} loadMessages={loadMessages} setMessage={setMessage} getMessage={getMessage}/>
            <AttachSound messageGetter={messageGetter} setReRender={setReRender} loadMessages={loadMessages} setMessage={setMessage} getMessage={getMessage}/>
            <AttachRecording messageGetter={messageGetter} setReRender={setReRender} loadMessages={loadMessages} setMessage={setMessage} getMessage={getMessage}/>
            <div className="overflow-auto chatElement" id="chatBox">{loadMessages()}{MyComponent()}</div>
            <div className="dropdown">
                <button className="btn btn-secondary dropdown-toggle btn-sm" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                    <i className="bi bi-paperclip"></i>
                </button>
                <ul className="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                    <li><a className="dropdown-item" data-bs-toggle="modal" data-bs-target="#picModal"><i className="bi bi-camera attach"></i> Image
                    </a></li>
                    <li><a className="dropdown-item" data-bs-toggle="modal" data-bs-target="#vidModal"><i className="bi bi-camera-reels attach"></i> Video</a></li>
                    <li><a className="dropdown-item" data-bs-toggle="modal" data-bs-target="#recordModal"><i className="bi bi-mic attach"></i> Record</a></li>
                    <li><a className="dropdown-item"  data-bs-toggle="modal" data-bs-target="#soundModal"><i className="bi bi-file-earmark-music attach"></i> Sound</a></li>
                </ul>
            </div><input type="text" ref={newMessage} className="form-control bottomChat" placeholder="New Message" aria-label="Username" ></input>
            <button type="button" className="btn btn-secondary myButton" id="sendButton" onClick={showNewMessage}>Send</button>
        </div>
        </div>
    )
}
export default OpenChat;