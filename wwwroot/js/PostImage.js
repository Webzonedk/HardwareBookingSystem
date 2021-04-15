

//set attribute on hidden inputfield & submit
function submit_2() {

    var imagesource = document.getElementById("pictureFromCamera").src;
    document.getElementById("hiddenimage").setAttribute('value', imagesource);
    document.getElementById("form").action = "AddDeviceToDB";

    form.submit();
}

 function UploadImage(event) {
    
     var img = document.getElementById("pictureFromCamera");
     var reader = new FileReader();

     //read blob object
     reader.readAsDataURL(event.target.files[0]);

     //convert url to base64string
     reader.onloadend = function () {
         var base64 = reader.result;
         img.src = base64;
     }

}
