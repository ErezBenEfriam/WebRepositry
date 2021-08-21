

let ImageList = ["firstImage", "SecondImage", "ThirdImage", "ForthImage"];


myFunction()
/*var imageIndex = 1;*/ // oringal code
var imageIndex = 0;


function myFunction() {
    setInterval(function () {
        imageIndex = imageIndex + 1;
        let myimage = document.getElementById("ImageHeaderId");
        if (imageIndex == 4) {
            myimage.classList.add(ImageList[0]);
            myimage.classList.remove(ImageList[3]);
            imageIndex = 0;
        }
        else {
            myimage.classList.remove(ImageList[imageIndex - 1]);
            myimage.classList.add(ImageList[imageIndex]);

        }

    }
        , 7000);
}