
//$('textarea').keyup(function () {
    
//    if (this.value.length === 0) {
//        $('#limit').show();
//        $('#remaining').hide();
//    } else {
//        $('#limit').hide();
//        $('#remaining').show();
//    }

//    if (this.value.length > 2000) {
//        return false;
//    }
//    console.log("typing");
//    $("#remaining").html((2000 - this.value.length) + "/2000");
//});


function textcounter(input) {

    console.log("this works!");
    $('textarea').keyup(function () {

        if (this.value.length === 0) {
            $('#limit').show();
            $('#remaining').hide();
        } else {
            $('#limit').hide();
            $('#remaining').show();
        }

        if (this.value.length > input) {
            return false;
        }
        console.log("typing");
        $("#remaining").html((input - this.value.length) + "/" +input);
    });
}