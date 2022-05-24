
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
    const [isCorrect, setIsCorrect] = useState(false);
    // to send username and password to server
//     useEffect(()=>{
//     console.log(userNameInput+','+passwordInput);
    
//     const func = async()=> {
//     await fetch('https://localhost:7104/api/Users',{
//     method: 'POST',
//             headers:{'Content-type':'application/json'},
//             body: {userNameInput, passwordInput}
        //   }).then(r=>r.json()).then(res=>{
        //     if(res){
        //       console.log("yayyyy");
        //     }
        //     else {
        //         console.log("noooo");
        //     }
//           });
//     func()
// }}, []);
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
        if (flag === true) {
            setIsCorrect(true);
        }
        
         
        var z = {
            id: userNameInput,
            password: passwordInput,
          };
        // Simple POST request with a JSON body using fetch
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: z
            };
            
            fetch('https://localhost:7104/api/Users', requestOptions)
                .then(response => response.json()).
                then(res=>{
                    if(res){
                      console.log(res);
                    }
                    else {
                        console.log("noooo");
                    };
        })
}

    

    function setErrorFor(input, message) {
        const formControl = input.parentElement;
        const small = formControl.querySelector('small');
        small.innerText = message;
        formControl.className = 'form-control design error';
    }

    const isUser = function (name, password) {
        return usersList.some(code => { return (code.username === name && code.password === password); });
    }


    return (
        <div id='containerAll'>
            {(isCorrect) ?
                (<Navigate to="/chat" state={userNameInput} />) :
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
