var name = "rt_yamaha";
var domain = ".yamaha.com";
var period = 90;
var expires = getExpireTime();

if (document.cookie) {
  var cookies = document.cookie.split("; ");
  var value = null;
     
  for (var i = 0; i < cookies.length; i++) {
    var str = cookies[i].split("=");

    if (str[0] == name) {
      var value = unescape(str[1]);
      break;
    }
  }

  if (value == null) {
    setCookie(getValue());
  } else {
    setCookie(value);
  }
} else {
  setCookie(getValue());
}
    
function setCookie(value){
  document.cookie = name + "=" + escape(value) + "; expires=" + expires + "; path=/; domain=" + domain;
}


function getExpireTime() {
  var nowtime = new Date().getTime();
  var clear_time = new Date(nowtime + (60 * 60 * 24 * 1000 * period));
  return clear_time.toGMTString();
}

function getValue() {
  var dateobj = new Date;
  var result = dateobj.getTime();
  var len = 35;
  var source = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
    var sourceLen = source.length;
  for (var i = 0; i < len; i++) {
    result += source.charAt(Math.floor(Math.random() * sourceLen));
  }
  return result;
}
