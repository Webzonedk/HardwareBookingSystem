

// Grab elements, create settings, etc.
var video = document.getElementById('video');
var Videostream;

function EnableCam() {

    if (Videostream != null) {
        SnapPicture();
        StopStream();
        //change button text
        document.getElementById("CameraButton").innerText = 'Nyt Billed';

        return;
    }

    // Get access to the camera!
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        // Not adding `{ audio: true }` since we only want video now
        navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
            //video.src = window.URL.createObjectURL(stream);
            Videostream = stream;
            video.srcObject = Videostream;

            video.play();
        });
    }

    //disable placeholder element
    hideElement();

    //change button text
    document.getElementById("CameraButton").innerText = 'Tag Billed';
}

//hide image element
function hideElement() {
    var elem = document.getElementById("pictureFromCamera");
    elem.style.display = "none";
    video.style.display = "block";

}

function SnapPicture() {

    var img = document.getElementById("pictureFromCamera");

    //create canvas
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;

    var context = canvas.getContext('2d');

    //draw image to canvas
    context.drawImage(video, 0, 0, 332, 250);
    const dataurl = canvas.toDataURL();

    //set source of image
    img.src = dataurl;




    //hide video & show image instead
    if (img.style.display == "none") {
        img.style.display = "block";

    }

    //hide video
    if (video.style.display == "block") {

        video.style.display = "none";
    }
}

function StopStream() {

    if (Videostream.active) {
        var track = Videostream.getTracks()[0];
        track.stop();
        Videostream = null;
        
    }
}



