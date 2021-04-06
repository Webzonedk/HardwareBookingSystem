// Elements for taking the snapshot

var video = document.getElementById('video');

// Trigger photo take
document.getElementById("snap").addEventListener("click", function () {

	var canvas = document.createElement("canvas");
	canvas.width = 640;
	canvas.height = 480;

	var context = canvas.getContext('2d');

	context.drawImage(video, 0, 0, 640, 480);
	const dataurl = canvas.toDataURL();
	var img = document.getElementById("pictureFromCamera");
	img.src = dataurl;
});

