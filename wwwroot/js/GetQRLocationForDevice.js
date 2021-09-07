
function SubmitQRCode() {

    let checkfield = document.getElementById("ItemIsScanned");
    let id = document.getElementById("hiddenID");
    let outputfield = document.getElementById("content");
    if (checkfield.checked) {

      
        var QRcode = sessionStorage.getItem(id.value);
        outputfield.value = QRcode;
        document.getElementById("ScanLocationForm").submit();

    }
}