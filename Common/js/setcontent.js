


function addOnLoad(func) {
    if (typeof window.addEventListener != "undefined") {
        window.addEventListener("load", func, false);
    } else if (typeof window.attachEvent != "undefined") {
        window.attachEvent("onload", func);
    } else {
        if (window.onload != null) {
            var oldOnload = window.onload;
            window.onload = function (e) {
                oldOnload(e);
                window[func]();
            };
        } else {
            window.onload = func;
        }
    }
}



function setLocalNavi(){
	var id = document.getElementById("localNavi3");
	if(!id) return false;
	var elements = id.getElementsByTagName("li");
	for(var i=0; i<elements.length; i++){
		var endTag = elements[i];
	}
	endTag.style.backgroundImage = "none";
}



function setHover() {
	if(!document.getElementById) return;
	var linkImages = new Array();
	var sTempSrc;
	var images = document.getElementsByTagName('img');
	for(var i = 0; i < images.length; i++){		
		if(images[i].className == 'hover'){
			var src = images[i].getAttribute('src');
			var fileType = src.substring(src.lastIndexOf('.'), src.length);
			var hsrc = src.replace(fileType, '_on'+fileType);
			images[i].setAttribute('hsrc', hsrc);
			linkImages[i] = new Image();
			linkImages[i].src = hsrc;
			images[i].onmouseover = function(){
				sTempSrc = this.getAttribute('src');
				this.setAttribute('src', this.getAttribute('hsrc'));
			}
			images[i].onmouseout = function(){
				if (!sTempSrc) sTempSrc = this.getAttribute('src').replace('_on'+fileType, fileType);
				this.setAttribute('src', sTempSrc);
			}
		}
	}
}





addOnLoad(setLocalNavi);
addOnLoad(setHover);
