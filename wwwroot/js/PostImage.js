


//set attribute on hidden inputfield & submit
function submitDevice() {

    var imagesource = document.getElementById("pictureFromCamera").src;
    document.getElementById("hiddenimage").setAttribute('value', imagesource);
    document.getElementById("form").action = "AddDeviceToDB";

    if (!$form.checkValidity || $form.checkValidity()) {
        //form.submit();
        doument.forms[0].submit();
    }

}

function UploadImage(event) {

    console.log("this code works");
    var img = document.getElementById("pictureFromCamera");
    var reader = new FileReader();
    var upload = document.getElementById("FileUpload");

    //read blob object
    reader.readAsDataURL(event.target.files[0]);

    //check filesize
    if (validateSize(upload)) {

        //validate image size
        //convert url to base64string
        reader.onloadend = function () {
            var base64 = reader.result;
            let image = new Image();
            image.src = base64;
            if (image.onload = function () {

                let width = this.width;
                let height = this.height;
                console.log(width + "/" + height);
                if (width > 332 && height > 250) {
                    return false
                }
                else {
                    return true;
                }
            }) {
                upload.value = '';
                alert("dimensionerne på billedet er for store, max 332x250 pixels ")
            }
            else {
                img.src = base64;
            }




           
        }
    }
    else {
        alert("billedet fylder mere end 2 mb, upload venligst et nyt")
    }






}

//function to send data to check image 
function PostToCheckImage() {

    var imagesource = document.getElementById("pictureFromCamera").src;
    document.getElementById("hiddenimage").setAttribute('value', imagesource);
    form.submit();
}

//validate image size
function validateSize(input) {
    const fileSize = input.files[0].size / 1024 / 1024; // in MiB
    if (fileSize > 2) {
        alert('File size exceeds 2 MiB');

        input.value = '';
        return false;
    }
    else {
        return true;
    }
}