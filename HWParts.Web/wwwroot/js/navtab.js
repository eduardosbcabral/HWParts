function changeCss() {
    var navElement = document.querySelector(".hw-navtab-wrapper");
    if (navElement) 
        this.scrollY < 50 ? navElement.style.top = (50 - this.scrollY) + 'px' : navElement.style.top = '0px';
}

window.addEventListener("scroll", changeCss, false);