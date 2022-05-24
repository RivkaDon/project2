import React, { useEffect } from 'react';
import AttachImage from './ImageAttachment';
import { useRef, useState } from "react";
import './ChatCard.css'
import AttachVideo from './VideoAttachment';
import AttachSound from './SoundAttachment';
import AttachRecording from './RecordAttachment';

function OpenChat({ getter, messageGetter, messageSetter, contactSetter, setReRender, imageGetter}) {
    // in order to go to bottom of scrollbar after opening each chat so we can see the last messeges in convo
    const MyComponent = () => {
        const divRef = useRef(null);
      
        useEffect(() => {
          divRef.current.scrollIntoView({ behavior: 'smooth' });
        });
      
        return <div ref={divRef} />;
      }
    const [getMessage, setMessage] = useState(0);
    const newMessage = useRef();
    const showNewMessage = () => {
        messageGetter.push([1, newMessage.current.value, (new Date()).toLocaleString(), 'text']);
        setMessage(getMessage+1);
        newMessage.current.value = "";
        setReRender(messageGetter[messageGetter.length-1][2]);
    };
    const messageArr = messageGetter;
    const loadMessages = function () {
        if (messageArr) {
            return (messageArr.map((element,key) => {
                switch (element[0]) {
                    case 1:
                        if (element[3] == 'text') {
                            return <div key={element} className="sent"><span className="badge bg-secondary">{element[1]}<br></br><div className="dateAndTime">{element[2]}</div></span>
                            </div>;
                        }
                        else if (element[3] == 'vid') {
                            return <div key={element} className="sent"><span className="badge bg-secondary"><video width="320" height="240" muted controls playsInline>
                                <source src={element[1]} type="video/mp4"></source>
                            </video><br></br><div className="dateAndTime">{element[2]}</div></span></div>;
                        }
                        else if (element[3] == 'sound') {
                            return <div key={element} className="sent"><span className="badge bg-secondary"><audio controls>
                                <source src={element[1]} type="audio/mpeg"></source>
                            </audio><br></br><div className="dateAndTime">{element[2]}</div></span></div>;
                        }
                        else if (element[3] == 'recording') {
                            return <div key={element} className="sent"><span className="badge bg-secondary"><audio controls>
                                <source src={element[1]} type="audio/mpeg"></source>
                            </audio><br></br><div className="dateAndTime">{element[2]}</div></span></div>;
                        }
                        return <div key={element} className="sent"><div className="badge bg-secondary "><img id="picSent" src={element[1]} alt="" />
                        <div className="dateAndTime">{element[2]}</div></div></div>;
                    case 0:
                        if (element[3] == 'text') {
                            return <div key={element} className="recieved"><span className="badge bg-secondary">{element[1]}<br></br><div className="dateAndTime">{element[2]}</div></span>
                            </div>;
                        }
                        else if (element[3] == 'vid') {
                            return <div key={element} className="recieved"><span className="badge bg-secondary"><video width="320" height="240" muted controls playsInline>
                                <source src={element[1]} type="video/mp4"></source>
                            </video><br></br><div className="dateAndTime">{element[2]}</div></span></div>;
                        }
                        else if (element[3] == 'sound') {
                            return <div key={element} className="recieved"><span className="badge bg-secondary"><audio controls>
                                <source src={element[1]} type="audio/mpeg"></source>
                            </audio><br></br><div className="dateAndTime">{element[2]}</div></span></div>;
                        }
                        else if (element[3] == 'recording') {
                            return <div key={element} className="recieved"><span className="badge bg-secondary"><audio controls>
                                <source src={element[1]} type="audio/mpeg"></source>
                            </audio><br></br><div className="dateAndTime">{element[2]}</div></span></div>;
                        }
                        return <div key={element} className="recieved"><div className="badge bg-secondary "><img id="picSent" src={element[1]} alt="" />
                        <div className="dateAndTime">{element[2]}</div></div></div>;
                    default:
                        return;
                }
            }));
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