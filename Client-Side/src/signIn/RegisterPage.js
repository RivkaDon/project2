import { useRef, useState } from 'react';
import usersList from './usersList';
import './RegisterPage.css'


import {
    BrowserRouter as Router,
    Link,
    Navigate,
    Route
} from "react-router-dom";

var userNameInput = "";

function BasePage() {

    const [isCorrect, setIsCorrect] = useState(false);
    const [imageSource, setImageSource] = useState();

    function CheckInput(event) {
        // get values from the inputs.
        var flag = true;
        const form = document.getElementById('form');
        const userName = document.getElementById('userName');
        const nickName = document.getElementById('nickName');
        const password = document.getElementById('password');
        const confirmPassword = document.getElementById('confirmPassword');



        userNameInput = userName.value.trim();
        const nickNameInput = nickName.value.trim();
        const passwordInput = password.value.trim();
        const confirmPasswordInput = confirmPassword.value.trim();


        if (userNameInput === '') {
            setErrorFor(userName, 'UserName cannot be blank');
            flag = false;
        } else if (!isUserName(userNameInput)) {
            setErrorFor(userName, 'UserName must contain only numbers and letters')
            flag = false;
        }
        else if (isUser(userNameInput)) {
            setErrorFor(userName, 'UserName already exist')
            flag = false;
        }
        else {
            setSuccessFor(userName);
        }

        if (nickNameInput === '') {
            setErrorFor(nickName, 'NickName cannot be blank');
            flag = false;
        }
        else {
            setSuccessFor(nickName);
        }
        if (password === '') {
            setErrorFor(password, 'Password cannot be blank');
            flag = false;
        }
        else if (!isPassword(passwordInput)) {
            setErrorFor(password, 'Password must include numbers and letters.')
            flag = false;
        }
        else {
            setSuccessFor(password);
        }
        if (confirmPasswordInput === '') {
            setErrorFor(confirmPassword, 'Confirm password cannot be blank');
            flag = false;
        }
        else if (passwordInput !== confirmPasswordInput) {
            setErrorFor(confirmPassword, 'Your password and confirmation password do not match.')
            flag = false;
        }
        else {
            setSuccessFor(confirmPassword);
        }
        if (flag === true) {
            if(!document.getElementById('image_input')) {
                imageSource = "/profilePic.jpg";
            }
            usersList.push({ username: userNameInput, password: passwordInput, nickName: nickNameInput, picture: imageSource, contacts: [] });
            setIsCorrect(true);
        }
    }

    function setImageInput() {
        var imageInput = document.getElementById('image_input');
        var reader = new FileReader();
        if (!imageInput) {
            return;
        }
        reader.readAsDataURL(imageInput.files[0]);
        reader.onload = function () {
            setImageSource(reader.result);
        };
    }

    function setErrorFor(input, message) {
        const formControl = input.parentElement;
        const small = formControl.querySelector('small');
        small.innerText = message;
        formControl.className = 'form-control design error';
    }

    function setSuccessFor(input) {
        const formControl = input.parentElement;
        formControl.className = 'form-control design succes';
    }

    function isUserName(userName) {
        return /^[a-zA-Z0-9]*$/.test(userName);
    }
    function isPassword(password) {
        return /^(?=.*[a-zA-Z])(?=.*[0-9])/.test(password);
    }
    const isUser = function (name) {
        return usersList.some(code => { return (code.username === name); });
    }


    return (
        <div id='containerAll'>
            {(isCorrect) ?
                (<Navigate to="/chat" state={userNameInput} />) :
                (
                    <div className="container" id='container'>
                        <div className="header">
                            <h2>Register</h2>
                        </div>
                        <div className="form" id="form">
                            <div className='form-control design'>
                                <label>Username</label>
                                <input type="text" id='userName'></input>
                                <small>Error message</small>
                            </div>

                            <div className='form-control design'>
                                <label>Nickname</label>
                                <input type="text" id="nickName" ></input>
                                <small>Error message</small>
                            </div>

                            <div className='form-control design'>
                                <label>Password</label>
                                <input type="password" id="password"></input>
                                <small>Error message</small>
                            </div>

                            <div className='form-control design'>
                                <label >Confirm password</label>
                                <input type="password" id="confirmPassword"></input>
                                <small>Error message</small>
                            </div>


                            <div className='form-control design' >
                                <label >Image</label>
                                <input type="file" id="image_input" onChange={setImageInput} ></input>
                                <div id='display_image'>
                                    <img src={imageSource}></img>
                                </div>
                            </div>

                            <button onClick={CheckInput}>Submit</button>
                            <div className="signup">
                                Already registered? <a href="/">Click here</a> to login
                            </div>
                        </div>
                    </div>
                )
            }
        </div>
    )
}

export default BasePage;
