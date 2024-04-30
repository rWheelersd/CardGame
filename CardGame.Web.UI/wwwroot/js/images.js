//Creates image element
var img = document.createElement('img');
//sets the element with the image
img.src = '../../img/table_top.png';
//gives the image and id
img.id = 'table';
//gets the game div
var gameContainer = document.getElementById('Game');
//appends the image element to the parent (game div)
gameContainer.appendChild(img);

function resizeImage() {
    //gets the dimensions of the parent element
    var containerWidth = window.innerWidth;
    //gets the aspect ratio so the image mantains it proportions
    var aspectRatio = img.width / img.height;
    //gets a max height and width in relatedion to the parent element
    var maxWidth = containerWidth * 0.85;
    //gets the new image dimensions within its original aspect ratio
    var newHeight = newWidth / aspectRatio;
    var newWidth = maxWidth;
    //sets the images dimentions
    img.style.height = newHeight + 'px';
    img.style.width = newWidth + 'px';
}
//image is sized on load and the and event listener is added for any further window size changes
resizeImage();
window.addEventListener('resize', resizeImage);