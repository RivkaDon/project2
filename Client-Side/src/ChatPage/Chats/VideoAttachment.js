import { useRef, useState } from "react";

function AttachVideo(messageGetter, setReRender, loadMessages, setMessage, getMessage) {
    const newVideo = useRef();
    const [vid, setVid] = useState();
    const onVidChange = () => {
        const [file] = newVideo.current.files;
        if (file) {
            console.log(file.name);
            messageGetter.messageGetter.push([1, file.name, (new Date()).toLocaleString(), 'vid']);
            setVid(URL.createObjectURL(file));
            messageGetter.setReRender(messageGetter.messageGetter[messageGetter.messageGetter.length - 1][2]);
            newVideo.current.value = null;
            // in order to rerender the chat page and show duplicate messeges
            messageGetter.setMessage(messageGetter.getMessage+1);
        }
    };
    return (
        <div className="modal fade" id="vidModal" tabIndex="-1" aria-labelledby="vidModalLabel" aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title" id="exampleModalLabel">Add Video:</h5>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <input type="file" ref={newVideo} />
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <button type="button" className="btn btn-primary" data-bs-dismiss="modal" onClick={onVidChange}>Send Video</button>
                    </div>
                </div>
            </div>
        </div>
    );
}
export default AttachVideo;