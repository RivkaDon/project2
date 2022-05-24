import { useEffect, useRef, useState } from "react";


function AttachRecording(messageGetter, setReRender, loadMessages, setMessage, getMessage) {
    const useRecorder = () => {
        const [audioURL, setAudioURL] = useState("");
        const [isRecording, setIsRecording] = useState(false);
        const [recorder, setRecorder] = useState(null);
      
        useEffect(() => {
          if (recorder === null) {
            if (isRecording) {
              requestRecorder().then(setRecorder, console.error);
            }
            return;
          }
      
          // Manage recorder state.
          if (isRecording) {
            recorder.start();
          } else {
            recorder.stop();
          }
      
          const handleData = e => {
            setAudioURL(URL.createObjectURL(e.data));
          };
      
          recorder.addEventListener("dataavailable", handleData);
          return () => recorder.removeEventListener("dataavailable", handleData);
        }, [recorder, isRecording]);
      
        const startRecording = () => {
          setIsRecording(true);
        };
      
        const stopRecording = () => {
          setIsRecording(false);
        };
      
        return [audioURL, isRecording, startRecording, stopRecording];
      };
      
      async function requestRecorder() {
        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        return new MediaRecorder(stream);
      };
      const [recording, setRecording] = useState();
      const onRecordingChange = () => {
        if (audioURL) {
            messageGetter.messageGetter.push([1, audioURL, (new Date()).toLocaleString(), 'recording']);
            setRecording(audioURL);
            messageGetter.setReRender(messageGetter.messageGetter[messageGetter.messageGetter.length - 1][2]);
            audioURL = null;
            // in order to rerender the chat page and show duplicate messeges
            messageGetter.setMessage(messageGetter.getMessage+1);
        }
    };
      let [audioURL, isRecording, startRecording, stopRecording] = useRecorder();
    return (
        <div className="modal fade" id="recordModal" tabIndex="-1" aria-labelledby="recordModalLabel" aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title" id="exampleModalLabel">Record:</h5>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div>
                        <audio src={audioURL} controls />
                        <button onClick={startRecording} disabled={isRecording}>
                            start
                        </button>
                        <button onClick={stopRecording} disabled={!isRecording}>
                            end
                        </button>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <button type="button" className="btn btn-primary" data-bs-dismiss="modal" onClick={onRecordingChange}>Send</button>
                    </div>
                </div>
            </div>
        </div>
    )
}
export default AttachRecording;