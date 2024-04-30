//Adds event listenre for window resizing
window.addEventListener('resize', scaleContainer);

function scaleContainer() {
    //Gets element id for the container that needs to be scaled
    var element = document.getElementById('Game');
    //gets heights for header and footer heights including their padding, borders, etc
    var headerHeight = document.querySelector('header').offsetHeight;
    var footerHeight = document.querySelector('footer').offsetHeight;
    //gets Geight for the viewport
    var windowHeight = window.innerHeight;
    //gets the usable height for the container after removing total header and footer height from the viewport (does not include the containers own stylings)
    var availableHeight = windowHeight - headerHeight - footerHeight;
    //applies the new height
    element.style.height = availableHeight + 'px';
}

window.addEventListener('load', scaleContainer);