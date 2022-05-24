import './ChatPage.css'
import './Contacts/ContactCard.css';
import { useCallback, useEffect, useRef, useState } from 'react';
import ContactCard from './Contacts/ContactCard';
import React from 'react';
import OpenChat from './Chats/ChatCard';
import { useLocation, useResolvedPath } from 'react-router-dom';
import usersList from '../signIn/usersList';

function ChatPage({}) {
    // hook for passing and updating contact messages
    const [getMessages, setMessages] = useState();

    // to get contacts of connected user from server
    const [list, setList] = useState([]);
    useEffect(()=>{
        var j = new Array();
        const func = async()=> {
        await fetch('https://localhost:7104/api/Contacts', {method:'GET'}).then(response => response.json())
        .then(data => j = data);
       
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

    // to get messeges of clicked contact from server
    useEffect(()=>{
        /////////- need to get id- do with getter from contactcard 
        var id = 5;
        /////////- need to get id
        var j = new Array();
        const func = async()=> {
        await fetch('https://localhost:7104/api/Contacts/'+id+'/messages', {method:'GET'}).then(response => response.json())
        .then(data => j = data);
        //console.log(j);
    }
        func()
    }, []);
    
    const location = useLocation();
    var currentUser = location.state;
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
    const [reRender, setReRender] = useState(0);
    
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
    const SubmitNewContact = function () {
        let myArray = [["", ""]];
        let name = newUserName.current.value;
        if (isUser(name)) {
            addUserName((prev) => {
                return prev.concat({ img: getUsernameReturnImg(name), name: getUsernameReturnNickName(name), lastMessage: myArray, time: "" });
            });
        }
        else {
            alert("username not found");
        }
        newUserName.current.value = '';
    }
    
    const OnClickChat = function () {
    
        if(getFlag == true)
        {
            
            return <OpenChat getter={getChat} messageGetter={getMessages} messageSetter={setMessages} contactSetter={addUserName} setReRender={setReRender} imageGetter={getContactImage} />
        }
    }
    const getUsernameReturnNickName = function(userName){
        var temp = '';
        usersList.forEach(element => { if(element.username == userName) {temp = element.nickName; return;}}) 
        return temp;   
    }
    
    return (
        <div className="App container">
            <div className="container" id="topStrip">
                <div className="row">
                    <div className="col-4">
                        <div type="button" className="btn">
                            <i className="bi bi-envelope-paper-heart-fill"></i>
                        </div>{getUsernameReturnNickName(currentUser)}
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
                {list.map((contact) => <ContactCard name={contact.at(0).at(1).at(1)} lastMessages={contact.at(0).at(3)} setter={setChat} flagSetter={setFlag} />)}
                </div>

            </div>
        </div>
    );
}
export default ChatPage;