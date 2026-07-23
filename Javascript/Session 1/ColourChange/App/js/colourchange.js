let div = document.querySelector(".colourBox");

div.addEventListener("mousemove", function (e) {
    x = e.offsetX;
    y = e.offsetY;
    div.style.backgroundColor = `rgb(${x}, ${y}, ${x-y})`;
});
