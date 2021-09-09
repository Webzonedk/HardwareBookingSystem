$(document).ready(function () {
    //get scanner 
    var scanner = new Instascan.Scanner({ video: document.getElementById('preview'), scanPeriod: 1, mirror: false });
    const label = document.getElementById("content");
    const id = document.getElementById("hiddenID");

    //event listening for scan data
    scanner.addListener('scan', function (content) {
       /* alert(content);*/
        label.value = content;
        if (label.value != null) {
            sessionStorage.setItem(id.value, content);
            var checked = document.getElementById("ItemIsScanned");
            checked.click();
        }
    });

    //start scanning QR code if camera exists
    Instascan.Camera.getCameras().then(function (cameras) {
        if (cameras.length > 0) {
            scanner.start(cameras[0]);
        }
        else {
            console.error('No cameras found.');
            alert('No cameras found.');
        }
    }).catch(function (e) {
        console.error(e);
        alert(e);
    });

    function update(e) {
       /* label.textContent = e.target.value;*/
    }

});