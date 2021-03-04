
$('textarea').keyup(function () {
    if (this.value.length === 0) {
        $('#limit').show();
        $('#remaining').hide();
    } else {
        $('#limit').hide();
        $('#remaining').show();
    }

    if (this.value.length > 2000) {
        return false;
    }

    $("#remaining").html((2000 - this.value.length) + "/2000");
});