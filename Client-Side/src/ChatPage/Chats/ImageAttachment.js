import { useRef, useState } from "react";

function AttachImage(messageGetter, setReRender, loadMessages, setMessage, getMessage) {
    const newImage = useRef();
    const [img, setImg] = useState();
    const onImageChange = () => {
        const [file] = newImage.current.files;
        if (file) {
            messageGetter.messageGetter.push([1, file.name, (new Date()).toLocaleString(), 'pic']);
            setImg(URL.createObjectURL(file));
            messageGetter.setReRender(messageGetter.messageGetter[messageGetter.messageGetter.length - 1][2]);
            newImage.current.value = null;
            // in order to rerender the chat page and show duplicate messeges
            messageGetter.setMessage(messageGetter.getMessage+1);
        }
    };
    return (
        <div className="modal fade" id="picModal" tabIndex="-1" aria-labelledby="picModalLabel" aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title" id="exampleModalLabel">Add Image:</h5>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <input type="file" ref={newImage} />
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <button type="button" className="btn btn-primary" data-bs-dismiss="modal" onClick={onImageChange}>Send Image</button>
                    </div>
                </div>
            </div>
        </div>
    );
}
export default AttachImage;