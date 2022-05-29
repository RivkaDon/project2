import './ChatPage.css'
import './Contacts/ContactCard.css';
import { useCallback, useEffect, useRef, useState } from 'react';
import ContactCard from './Contacts/ContactCard';
import React from 'react';
import OpenChat from './Chats/ChatCard';
import { useLocation, useNavigate } from 'react-router-dom';
import usersList from '../signIn/usersList';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { NavLink } from 'react-router-dom';


function ChatPage({}) {
    const usersRef = useRef([]);
    var allUsersArr = [];
    const location = useLocation();
    var currentUser = location.state.userNameInput;
    var token = location.state.token;

    const [getS, setS] = useState(0);
    const [getAllUsers, setAllUsers] = useState([]);
    // to get all users
    const getUsers = async()=> {
        let j = new Array();    
        await fetch('https://localhost:7105/api/Users', 
        {method:'GET'}).then(response => response.json())
            .then(data => j = data);
           
            let myMap;
            let myArr = new Array;
            var i = 0;
            j.forEach(element => {
                myMap = new Array(Object.entries(element));
                myArr[i] = myMap;
                i++;
            });
            allUsersArr = myArr;
            usersRef.current = myArr;
            console.log(usersRef.current);
            return myArr;
        }
        
    
    // hook for passing and updating contact messages
    const [getMessages, setMessages] = useState();

    // hook for passing id of contact whom we clicked on their contact card
    const [getContactId, setContactId] = useState();

    // to get contacts of connected user from server
    const [list, setList] = useState([]);
    const [ myConn, setMyConn] = useState(null);
   // const [ myNewMsgs, setMyNewMsgs ] = useState([]);
    const latestMeseges = useRef(null);

    latestMeseges.current = getMessages;
  
   

    useEffect(() => {
        const signalrCon = new HubConnectionBuilder()
            .withUrl('https://localhost:7105/hubs/myChat')
            .withAutomaticReconnect()
            .build();
            setMyConn(signalrCon);
    }, []);

    useEffect(() => {
        if (myConn) {
            myConn.start()
                .then(result => {
                    
    
                    myConn.on('Receive',( message, theContactId, theUserId) => {
                       
                        
                            console.log("in if");
                            const updatedChat = [...latestMeseges.current];
                            var arr1 = [['id', '9'],['content', message],
                            ['created', '2022-05-26T23:39:09.8661885+03:00'],['sent', true]];
                            updatedChat.push(arr1);
                            setMessages(updatedChat);
                                                             
                    });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [myConn]);

    useEffect(()=>{
        var j = new Array();
        const func = async()=> {
            console.log(token + "--------------------");    
        await fetch('https://localhost:7105/api/Contacts', {method:'GET'
        , headers: {
            "Authorization" : "Bearer " + token
        }}).then(response => response.json())
        .then(data => {if (data) {j = data}});
       
        let myMap;
        let myArr = new Array;
        var i = 0;
        j.forEach(element => {
            myMap = new Array(Object.entries(element));
            myArr[i] = myMap;
            i++;
        });
        setList(myArr);
    }
        func()
    }, []);

    const getUsernameReturnContacts = function(userName){
        var temp = [];
        usersList.forEach(element => { if(element.username == userName) {temp = element.contacts; return;}}) 
        return temp;   
    }
    const getUsernameReturnImg = function(userName){
        var temp;
        usersList.forEach(element => { if(element.username == userName) {temp = element.picture; return;}}) 
        return temp;   
    }
    const [getFlag, setFlag] = useState(false);
    const [getContactImage, setContactImage] = useState();
    // hook for rerendering the page 
    const [reRender, setReRender] = useState([]);
    
    // hook for passing name of contact whom we clicked on their contact card
    const [getChat, setChat] = useState();
    // hook for reloading all the contacts after adding a contact
    const [NewContactList, addUserName] = useState(getUsernameReturnContacts(currentUser));
    // hook for storing the newly entered username 
    const newUserName = useRef();
    // function to push a new contact into the contact list
    const isUser = function (name) {
        return usersList.some(code => { return (code.username === name); });
    }

    useEffect(()=> {
        var j = new Array();
        var myArr = new Array;
        async function recieveUsers()
         {
            await fetch('https://localhost:7105/api/Users', {method:'GET'}).then(response => response.json())
        .then(data => j = data);

        let myMap;
        var i = 0;
        j.forEach(element => {
            myMap = new Array(Object.entries(element));
            myArr[i] = myMap;
            i++;
        });
        allUsersArr = myArr;} recieveUsers();}, [])

    useEffect(() => {
        getUsers();
    }, [])
    const SubmitNewContact = async function () {
        
        let userName;
        allUsersArr =  await getUsers();
        usersRef.current = allUsersArr;
        console.log("got from server:");
        console.log(usersRef.current);
        var doesExist = false;


        // to find the name of the id given for the contact to add
        allUsersArr.forEach(element => {
            if (element[0][0][1] === newUserName.current.value) {
                userName = element[0][1][1];
                doesExist = true;
            }
        });

        if (doesExist) {
        
            const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json',
                "Authorization" : "Bearer " + token
             },
            body: JSON.stringify({ id: newUserName.current.value, name: userName, server: "localhost:7105" })
        };
                await fetch('https://localhost:7105/api/Contacts/', requestOptions);
        }
        
        else {
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json'},
                // check which server needs to be in request
                body: JSON.stringify({ from: newUserName.current.value, to: currentUser, server: "localhost:7105" })
            };
                    await fetch('https://localhost:7105/api/Invitations/', requestOptions);

        }
    
        var j = new Array();
        const func = async()=> {
        await fetch('https://localhost:7105/api/Contacts', {method:'GET'
        , headers: {
            "Authorization" : "Bearer " + token
        }}).then(response => response.json())
        .then(data => j = data);
       
        let myMap;
        let myArr = new Array;
        var i = 0;
        j.forEach(element => {
            myMap = new Array(Object.entries(element));
            myArr[i] = myMap;
            i++;
        });
        setList(myArr);}
        func();
        newUserName.current.value = '';
    }
    
    const OnClickChat = function () {
    
        // to get messeges of clicked contact from server
    useEffect(()=>{
        var id = getContactId;
        var j = new Array();
        if (getFlag) {
        const func = async()=> {
        await fetch('https://localhost:7105/api/Contacts/'+id+'/messages', {method:'GET'
        , headers: {
            "Authorization" : "Bearer " + token
        }}).then(response => response.json())
        .then(data => j = data);
        let myMap;
        let myArr = new Array;
        var i = 0;
        j.forEach(element => {
            myMap = new Array(Object.entries(element));
            myArr[i] = myMap.at(0);
            i++;
        });
        latestMeseges.current = myArr;
        setMessages(myArr);
        setFlag(0);
        }
        func()
    }}, []);
        if(getFlag!= true)
        {

            return <OpenChat getter={getChat} messageGetter={getMessages} messageSetter={setMessages} contactSetter={addUserName} setReRender={setReRender} imageGetter={getContactImage} lastMessages={latestMeseges} idGetter={getContactId} contactListSetter={setList} contactID={getContactId} myConn={myConn} userID={currentUser} usersArr={usersRef.current} token={token} />

        }
    }
    // const getUsernameReturnNickName = function(userName){
        
        
       
    //     console.log( getAllUsers);
      
    //     // to find the name of the id given for the contact to add
    //     getAllUsers.forEach(element => {
    //         console.log(element[0][0][1]+","+userName);
    //         if (element[0][0][1] == userName) {
    //             return element[0][1][1];
    //         }
    //     });   
    // }
    // const currentUserName = getUsernameReturnNickName(currentUser);
    
    return (
        <div className="App container">
            <div className="container" id="topStrip">
                <div className="row">
                    <div className="col-4">
                        <div type="button" className="btn">
                            <i className="bi bi-envelope-paper-heart-fill"></i>
                        </div>{currentUser}
                        <button type="button" id='rate' className="btn" onClick={()=>NavLink("https://localhost:7136/")} >
                        <a href='https://localhost:7136/'><i className="bi bi-star"></i>Rate Us<i className="bi bi-star"></i></a>
                        </button>
                        <button type="button" id='addButton' className="btn" data-bs-toggle="modal" data-bs-target="#exampleModal">
                            <i className="bi bi-person-plus"></i>
                        </button>
                    </div>
                    <div className="col-4" id="currentChat">
                    <OnClickChat />
                    
                   
                    </div>
                </div>
            </div>
            <div>
                <div className="modal fade" id="exampleModal" tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title" id="exampleModalLabel">Add new contact</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div className="modal-body">
                                <input ref={newUserName} type="text" className="form-control" placeholder="Contact's Identifier" aria-label="Username" aria-describedby="basic-addon1"></input>
                            </div>
                            <div className="modal-footer">
                                {/*button adds the username chosen on click and modal closes*/}
                                <button onClick={SubmitNewContact} type="button" className="btn btn-primary" data-bs-dismiss="modal">Add contact</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="row">
                <div className="col-4 overflow-auto" >
                {list.map((contact, key) => <ContactCard key={key} id={contact.at(0).at(0).at(1)} name={contact.at(0).at(1).at(1)} lastMessages={contact.at(0)} setter={setChat} flagSetter={setFlag} idSetter={setContactId} />)}
                </div>

            </div>
        </div>
    );
}
export default ChatPage;