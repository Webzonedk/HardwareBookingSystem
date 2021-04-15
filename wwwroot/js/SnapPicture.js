
// Elements for taking the snapshot
var video = document.getElementById('video');
var img = document.getElementById("pictureFromCamera");

// Trigger photo take
document.getElementById("snap").addEventListener("click", function () {

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
});








