
//set attribute on hidden inputfield & submit
function submit_2() {

    var imagesource = document.getElementById("pictureFromCamera").src;
    document.getElementById("hiddenimage").setAttribute('value', imagesource);

    form.submit();
}

