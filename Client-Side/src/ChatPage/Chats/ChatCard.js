import React, { useEffect } from 'react';
import AttachImage from './ImageAttachment';
import { useRef, useState } from "react";
import './ChatCard.css'
import AttachVideo from './VideoAttachment';
import AttachSound from './SoundAttachment';
import AttachRecording from './RecordAttachment';
import { ConsoleLogger } from '@microsoft/signalr/dist/esm/Utils';


function OpenChat({ getter, messageGetter, messageSetter, contactSetter, setReRender, imageGetter, lastMessages, idGetter, contactListSetter, contactId, myConn, userID}) {
    
    const showAllContactMesseges = async(id)=> {
        console.log(id);
        let j = new Array();
        await fetch('https://localhost:7105/api/Contacts/'+id+'/messages', {method:'GET'}).then(response => response.json())
        .then(data => j = data);
        let myMap;
        let myArr = new Array;
        var i = 0;
        j.forEach(element => {
            myMap = new Array(Object.entries(element));
            myArr[i] = myMap.at(0);
            i++;
        });
        messageSetter(myArr);
        }

    const showContacts = async()=> {
        let j = new Array();    
        await fetch('https://localhost:7105/api/Contacts', {method:'GET'}).then(response => response.json())
            .then(data => j = data);
           
            let myMap;
            let myArr = new Array;
            var i = 0;
            j.forEach(element => {
                myMap = new Array(Object.entries(element));
                myArr[i] = myMap;
                i++;
            });
            contactListSetter(myArr);
        }          
    
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

    
    const showNewMessage = async () => {
    
        // Simple POST request with a JSON body using fetch
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ content: newMessage.current.value })
        };
        console.log(idGetter + "contact");
        await fetch('https://localhost:7105/api/Contacts/'+idGetter+'/messages', requestOptions)
        //await fetch('https://localhost:7105/api/Contacts/'+userID+'/messages', requestOptions)
        console.log(userID + "user");
        try {
            await myConn.invoke('Send', newMessage.current.value, contactId);
        }
        catch(e) {
            console.log(e);
        }
            showAllContactMesseges(idGetter);
            //showAllContactMesseges(userID);
            showContacts();
         newMessage.current.value = "";
    };
    
    
    const formatTime = function(time) {
        const myArray = time.split("T");
        const finalArray = myArray[1].split(".");
        return myArray[0]+" "+finalArray[0];  
    }
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