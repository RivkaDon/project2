
import { useRef, useState, useEffect } from 'react';
import usersList from './usersList';
import './RegisterPage.css'

import {
    BrowserRouter as Router,
    Link,
    Navigate,
    Route
} from "react-router-dom";

var flag;
var userNameInput = "";
var passwordInput = "";


function SignPage2() {
    
    var token = useRef("");
    const [isCorrect, setIsCorrect] = useState(false);
    // to get users list from server
    const [getUsers, setUsers] = useState([]);
    var j = new Array();
        let myArr = new Array;
        useEffect(()=> {
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
        setUsers(myArr);} recieveUsers();}, [])
        

let componentDidMount;    
function CheckInput(event) {
        // get values from the inputs.
        flag = true;
        const form = document.getElementById('form');
        const userName = document.getElementById('userName');
        const password = document.getElementById('password');

        userNameInput = userName.value.trim();
        passwordInput = password.value.trim();

        if (userNameInput === '') {
            setErrorFor(userName, 'UserName cannot be blank');
            flag = false;
        }
        else {
            const formControl = userName.parentElement;
            formControl.className = 'form-control design';
        }
        if (passwordInput === '') {
            setErrorFor(password, 'Password cannot be blank');
            flag = false;
        }
        else {
            const formControl = password.parentElement;
            formControl.className = 'form-control design';
        }
        
        if (userNameInput !== '' && passwordInput !== '' && !isUser(userNameInput, passwordInput)) {
            setErrorFor(userName, 'User does not exist')
            setErrorFor(password, 'User does not exist');
            flag = false;
        }
        
        
        
        // Simple POST request with a JSON body using fetch
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
            };
            fetch('https://localhost:7105/api/Users/?id='+userNameInput+'&password='+passwordInput, requestOptions)
            .then(res=>{ var t = res.text();
            console.log(t+","+res);
        return t}).then(tok=>{token.current = tok;
                console.log(token.current);
                if (flag === true) {
                    setIsCorrect(true);
                }});
}

    

    function setErrorFor(input, message) {
        const formControl = input.parentElement;
        const small = formControl.querySelector('small');
        small.innerText = message;
        formControl.className = 'form-control design error';
    }

    
    const isUser = function (name, password) {
        
        return getUsers.some(code => { return (code.at(0).at(0).at(1) === name && code.at(0).at(2).at(1) === password); });
    }


    return (
        <div id='containerAll'>
            {(isCorrect) ?
                (<Navigate to="/chat" state={{"userNameInput": userNameInput, "token": token.current}} />) :
                (
                    <div className="container" id='container'>
                        <div className="header">
                            <h3>Sign in</h3>
                        </div>
                        <div className="form" id="form">
                            <div className='form-control design'>
                                <label>Username</label>
                                <input type="text" id='userName'></input>
                                <small>Error message</small>
                            </div>

                            <div className='form-control design'>
                                <label>Password</label>
                                <input type="password" id="password"></input>
                                <small>Error message</small>
                            </div>
                            <button onClick={CheckInput}>Submit</button>
                            <div className="signup">
                                Not registered? <a href="/Register">Click here</a> to sign up
                            </div>
                        </div>
                    </div>
                )
            }
        </div>
    )
}

export default SignPage2;
