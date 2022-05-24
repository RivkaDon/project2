import { useRef, useState } from "react";
import ContactCard from "./ContactCard";
import ContactList from "./ContactList";

function AddContact() {
  const [newContactList, setContactList] = useState(ContactList);
  // hook for storing the newly entered username 
  const newUserName = useRef(null);
  // function to push a new contact into the contact list
  const addUserName = function(){
    ContactList.push(<ContactCard key={ContactList.length} name={newUserName.current.value} lastMessage="" time=""/>)
  };
  return (
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
          <button onClick={addUserName} type="button" className="btn btn-primary" data-bs-dismiss="modal">Add contact</button>
          </div>
        </div>
      </div>
    </div>
);}
export default AddContact;