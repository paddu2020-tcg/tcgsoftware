


function setNewWindow(){
	/*
		 1. url  2. window name  3. width  4. height  5. top  6.left
		 7. resizable  8. toolbar 9. scrollbars 10. location 11. menubar 12. status
	*/
	var args = arguments;
	var setWindowValue = 'width=' + ((args[2]) ? args[2] : screen.width-30)
		+ ',height=' + ((args[3]) ? args[3] : screen.height-30)
		+ ',top=' + ((args[4]) ? args[4] : 30)
		+ ',left=' + ((args[5]) ? args[5] : 30)
		+ ',resizable=' + ((args[6]) ? 1 : 0)
		+ ',toolbar=' + ((args[7]) ? 1 : 0)
		+ ',scrollbars=' + ((args[8]) ? 1 : 0)
		+ ',location=' + ((args[9]) ? 1 : 0)
		+ ',menubar=' + ((args[10]) ? 1 : 0)
		+ ',status=' + ((args[11]) ? 1 : 0);
	var newWindow = window.open(args[0],args[1],setWindowValue);
	newWindow.focus();
}
function subWindow(url){
	setNewWindow(url,'subWindow',577,600,30,30,1,1,1,1,1,1);
}