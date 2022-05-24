import { useRef, useState } from "react";

function AttachSound(messageGetter, setReRender, loadMessages, setMessage, getMessage) {
    const newSound = useRef();
    const [sound, setSound] = useState();
    const onSoundChange = () => {
        const [file] = newSound.current.files;
        if (file) {
            messageGetter.messageGetter.push([1, file.name, (new Date()).toLocaleString(), 'sound']);
            setSound(URL.createObjectURL(file));
            messageGetter.setReRender(messageGetter.messageGetter[messageGetter.messageGetter.length - 1][2]);
            newSound.current.value = null;
            // in order to rerender the chat page and show duplicate messeges
            messageGetter.setMessage(messageGetter.getMessage+1);
        }
    };
    return (
        <div className="modal fade" id="soundModal" tabIndex="-1" aria-labelledby="soundModalLabel" aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title" id="exampleModalLabel">Add Sound File:</h5>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <input type="file" ref={newSound} />
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <button type="button" className="btn btn-primary" data-bs-dismiss="modal" onClick={onSoundChange}>Send File</button>
                    </div>
                </div>
            </div>
        </div>
    );
}
export default AttachSound;