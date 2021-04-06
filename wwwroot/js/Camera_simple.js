﻿const { hide } = require("@popperjs/core");

// Grab elements, create settings, etc.
var video = document.getElementById('video');

function EnableCam(){
// Get access to the camera!
if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
    // Not adding `{ audio: true }` since we only want video now
    navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
        //video.src = window.URL.createObjectURL(stream);
        video.srcObject = stream;
        video.play();
    });
    }

    //disable placeholder element
    hideElement();
}

function hideElement() {
    var elem = document.getElementById("pictureFromCamera");
    elem.style.display = "none";
}



