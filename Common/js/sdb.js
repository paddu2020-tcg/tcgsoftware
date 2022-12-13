var tb_unbound = false;

$(function() {

	// global navigation
	$('#gNavi').droppy({speed:0});
	$('#gNavi > li:last-child').css("margin","0");


	// product overview - angle
	$("#mycarousel").jcarousel({
		visible : 8,
		scroll : 2,
		auto: 0,
		animation : 500
	});


	$("#mycarousel01").jcarousel({
		visible : 7,
		scroll : 2,
		auto: 0,
		animation : 500
	});


	$("#mycarousel02").jcarousel({
		visible : 7,
		scroll : 2,
		auto: 0,
		animation : 500
	});

	// product overview - color
	$("#picturesBox ul li").mouseover(function(){ //mouseover on pictures
		setSelected($(this));
	
		if($(this).find("a").length > 0){
			$("#pictArea img").attr("src",$(this).find("a").attr("title"));
			$("#pictArea a").attr("href",$(this).find("a").attr("href"));
			$(".imageFunction .expansion a").attr("href",$(this).find("a").attr("href"));

                        $(".imageFunction .expansion a").css("display","");

                        if (tb_unbound == true) {
                            $("#pictArea a").unbind("click");
                        }

                        tb_unbound = false;
		}else{
			$("#pictArea a").attr("href","#");
			$("#pictArea a").bind("click", function(){return false;});
			$("#pictArea img").attr("src","/common/images/noimage/product_no_image_m.gif");
			$(".imageFunction .expansion a").css("display","none");

                        tb_unbound = true;
		}
	
	});
	
	$("#variationsBox ul li").mouseover(function(){ //mouseover on Colors / Finishes
		setSelected($(this));
		$("#variationName").text($(this).find("img").attr("alt"));
		if($(this).find("a").length > 0){
			$("#pictArea img").attr("src",$(this).find("a").attr("title"));
			$("#pictArea a").attr("href",$(this).find("a").attr("href"));
			$(".imageFunction .expansion a").attr("href",$(this).find("a").attr("href"));

                        $(".imageFunction .expansion a").css("display","");

                        if (tb_unbound == true) {
                            $("#pictArea a").unbind("click");
                        }

                        tb_unbound = false;
		}else{
			$("#pictArea a").attr("href","#");
			$("#pictArea a").bind("click", function(){return false;});
			$("#pictArea img").attr("src","/common/images/noimage/product_no_image_m.gif");
			$(".imageFunction .expansion a").css("display","none");

                        tb_unbound = true;
		}
	
	});
	
	function setSelected(obj){
	
		$("#picturesBox ul li").removeClass("select"); //init
		$("#variationsBox ul li").removeClass("select");//init
		$(obj).addClass("select");
		
	};



	// display text in textfielad focused
	$("#dealerTxt")
		.blur(function(){
			var $$=$(this);
			if($$.val()=='' || $$.val()==$$.attr("title")){
				$$.addClass("hint")
				.val($$.attr("title"));
			}
		})
		.focus(function(){
			var $$=$(this);
			if($$.val()==$$.attr("title")){
				$(this).removeClass("hint")
				   .val('');
			}
		})
		.parents('form:first').submit(function(){
			var $$=$("#dealerTxt");
			if($$.val()==$$.attr("title")){
				$$.triggerHandler("focus");
			}
		})
		.end().blur();

	 $('div#contentArea').
	    append('<div id="download_link_status" style="display: none;"></div>');
});



function rt_flag(url, name, param_name) {
    var flag = new Image(1, 1);
    var href = location.href.replace("#","&");
    url = url.replace("#","&");
    param_name = (arguments[2]) ? param_name : "banner";
    
    if (param_name == 'print') {
        flag.src = "/analysis/index.html" + "?" + param_name + "=" + href + ",&r=" + Math.random();
    } else if (param_name == 'ddm') {
        if ((url == 'http://www.sokureko24.jp/?ddm') || (url == 'http://japan.steinberg.net/?ddm')) {
            flag.src = "/analysis/index.html" + "?" + param_name + "=" + href + "," + url + "&r=" + Math.random();
        }
    } else {
        flag.src = "/analysis/index.html" + "?" + param_name + "=" + href + "," + url + "," + name + "&r=" + Math.random();
    }
}

function getUnixTime() {
	   return parseInt((new Date)/1000);
	}


function getDownloadFile(url) {
    //$.getJSON(url, function(data){
	$.ajax({
		url:url,
		type: 'GET',
		dataType: 'json',
		async: false,
		timeout: 180000,
		success: function(data, status){
        switch (data.code) {
            case 0:
                // file download
                var download_window = window.open();
            	download_window.location.href = url + "&" + getUnixTime() + "&mode=download";
                var download_link_status = document.getElementById('download_link_status');
                download_link_status.innerHTML = "";
                break;

            case 1:
                // sign in
                var download_link_status = document.getElementById('download_link_status');
                download_link_status.innerHTML = "";
                window.open("http://www.yamaha.com/Paragon/myaccount/fullpagesignin.html", "signin");
                break;

            case 2:
                // registration
                var download_link_status = document.getElementById('download_link_status');
                download_link_status.innerHTML = "";
                window.open("http://www.yamaha.com/Paragon/myaccount/fullpagesignin.html", "signin");
                break;

            case 3:
                if (data.path) {
                    location.href = data.path + "&" + getUnixTime();
                } else {
                    alert("Some problem occurred: Please wait for a while and try it again.");
                }
                var download_link_status = document.getElementById('download_link_status');
                download_link_status.innerHTML = "";
                break;

            case 4:
                if (data.path) {
                    window.open(data.path + "&" + getUnixTime());
                } else {
                    alert("Some problem occurred: Please wait for a while and try it again.");
                }
                var download_link_status = document.getElementById('download_link_status');
                download_link_status.innerHTML = "";
                break;

        }
        }
    });
}

function PageLocator(propertyToUse) {
    this.propertyToUse = propertyToUse;
    this.defaultQS = "";
    this.dividingUrlCharacter = '?';
    this.dividingCharacter = '#';
    this.dividingParamCharacter = '&';
    this.queryStringCharacter = '=';
    this.dividingSelectedCharacter = '_';
    this.hash = this.getHash();
    this.urlParams = [];
}

PageLocator.prototype.getLocation = function() {
    return this.propertyToUse;
}

PageLocator.prototype.getHash = function() {
    var url = this.getLocation();
    if(url.indexOf(this.dividingCharacter) > -1) {
        var url_elements = url.split(this.dividingCharacter);
        return url_elements[url_elements.length-1];
    } else {
        return this.defaultQS;
    }
}

PageLocator.prototype.setHash = function(new_hash) {
    if(new_hash.indexOf(this.dividingCharacter) > -1) {
        var hash_elements = new_hash.split(this.dividingCharacter);
        this.hash = hash_elements[hash_elements.length-1];
    } else {
        this.hash = new_hash;
    }
}

PageLocator.prototype.getHref = function() {
    var url = this.getLocation();
    var url_elements = url.split(this.dividingUrlCharacter);
    url = url_elements[0];
    if(url) {
        url_elements = url.split(this.dividingCharacter);
    }
    return url_elements[0];
}

PageLocator.prototype.makeNewLocation = function(new_qs) {
    return this.getHref() + this.dividingCharacter + new_qs;
}

PageLocator.prototype.setQueryToHash = function() {
    var url = this.getLocation();
    var url_elements = url.split(this.dividingUrlCharacter);
    var qs = '';
    if(url_elements.length>1) {
        qs = url_elements[1];
        if(qs) {
            url_elements = qs.split(this.dividingCharacter);
            qs = url_elements[0];
        }
    }
    this.hash = qs;
}

PageLocator.prototype.getNewUrl = function(mode) {
    var new_href = this.getHref();
    if(mode) {
        this.setParam(mode);
    }
    if(this.hash && (this.hash!=this.dividingCharacter)) {
        new_href = new_href + "?" + this.hash;
    }
    return new_href;
}

PageLocator.prototype.getParam = function() {
    var url_hash = this.hash;
    var params = new Array();
    if((url_hash.length > 0) && (url_hash != this.defaultQS)) {
        var param_elements = url_hash.split(this.dividingParamCharacter);
        for(var i = 0; i < param_elements.length; i ++) {
            var element = param_elements[i].split(this.queryStringCharacter);
            if(element.length > 1) {
                params[element[0]] = element[1];
            }
        }
    }
    return params;
}

PageLocator.prototype.setParam = function(param) {
    this.urlParams = this.getParam();
    var url = param.split(this.dividingUrlCharacter);
    if(url.length > 1){
        param = url[1];
    }
    var url_params = param.split(this.dividingParamCharacter);
    
    for(var i=0; i < url_params.length; i++) {
        var element = url_params[i].split(this.queryStringCharacter);
        if(element.length > 1){
            this.urlParams[element[0]] = element[1];
        }
    }
    this.hash = this.makeNewQueryString();
}

PageLocator.prototype.removeParam = function(param) {
    this.urlParams = this.getParam();
    if(this.urlParams[param]){
        delete this.urlParams[param];
    }
    this.hash = this.makeNewQueryString();
}

PageLocator.prototype.addMultiParam = function(key, value) {
    this.urlParams = this.getParam();
    if(this.urlParams[key]) {
        var selected = this.urlParams[key];
        var re = new RegExp(value+this.dividingSelectedCharacter);
        if(!selected.match(re)) {
            this.urlParams[key] = selected + value + this.dividingSelectedCharacter;
        }
    } else {
        this.urlParams[key] = value + this.dividingSelectedCharacter;
    }
    this.hash = this.makeNewQueryString();
}

PageLocator.prototype.delMultiParam = function(key, value) {
    this.urlParams = this.getParam();
    if(this.urlParams[key]) {
        var selected = this.urlParams[key];
        var re = new RegExp(value+this.dividingSelectedCharacter);
        if(selected.match(re)) {
            this.urlParams[key] = this.urlParams[key].replace(re, '');
        }
    }
    this.hash = this.makeNewQueryString();
}

PageLocator.prototype.makeNewQueryString = function() {
    var queryString = '';
    for(var key in this.urlParams) {
        queryString = queryString + key + this.queryStringCharacter + this.urlParams[key] + this.dividingParamCharacter;
    }
    return queryString.substr(0,queryString.length-1);
}

function PageUrlUtil() {
    this.locator = new PageLocator(window.location.href);
}

function loadItems() {
    util.locator.setHash(window.location.hash);
    var params = util.locator.getParam();
    var link_items = params["selected"];
    var tab = params["tab"];

    if(link_items && link_items.length > 0) {
        var items = link_items.split("_");

        for(i = 0; i < items.length; i++) {
            if ((items[i].length > 0) && (typeof $("#"+items[i]).prop("checked") != 'undefined')) {
                $("#"+items[i]).prop("checked",true);
            }
        }
        
        var offset = $("#lineup_top").offset();
        if(offset) {
            scrollTo(0,offset.top);
        }
    }

    if(tab) {
        changeViewArea(tab);
    }
}

function checkCheckedItems() {
    util.locator.setHash(window.location.hash);
    var params = util.locator.getParam();
    var link_items = params["selected"];
    var items = '';
    if(link_items){
        items = link_items.split("_");
    }
    var checked_count = items.length;

    if ((items.length != 0) && (items[items.length - 1].length == 0)) {
        checked_count -= 1;
    }

    if (checked_count < 2) {
        $(".buttonA01 :button").prop("disabled",true);
    } else {
        $(".buttonA01 :button").prop("disabled",false);

        if (checked_count == 4) {
            $(".productOutlineA01 :checkbox[checked=false]").prop("disabled",false);
        } else if (checked_count >= 5) {
            $(".productOutlineA01 :checkbox[checked=false]").prop("disabled",true);
        }
    }
}

function setTab(tab) {
    $("#tab_content > div").css("display", "none");
    $(".tabNaviB01 li").attr("class", "");
    $(".tabNaviB01 li[name='"+tab[0]+"']").attr("class", "select");
    $(".tabNaviB02 li").attr("class", "");
    $(".tabNaviB02 li[name='"+tab[0]+"']").attr("class", "select");

    for (i = 0; i < tab.length; i++) {
        $("#tab_content > #"+tab[i]).css("display", "inline");
    }

    if($(".productDetailA01").size()==0) {
        var p_util = new PageUrlUtil();

        if((tab.length==1) &&
           (tab[0]) &&
           (tab[0]!='overview')) {
            // set tab name to location.hash
            p_util.locator.setParam("tab="+tab[0]);
        } else {
            p_util.locator.removeParam("tab");
        }
        if((window.location.hash.length==0 || window.location.hash=="#") &&
           (p_util.locator.hash.length==0 || p_util.locator.hash.length=="#")) {
           return;
        }
        window.location.hash=p_util.locator.hash;
    }
}

function changeViewArea() {
    setTab(changeViewArea.arguments);

    rt_flag(location.href, changeViewArea.arguments[0], 'tab');
}

function changeViewArea2() {
    setTab(changeViewArea2.arguments);

    var offset = $(".tabNaviB01").offset();
    if(offset) {
        scrollTo(0,offset.top);
    }
    
    rt_flag(location.href, changeViewArea2.arguments[0], 'tab');
}

function moveToPosition(target) {
    var offset = $(target).offset();
    if(offset) {
        scrollTo(0,offset.top);
    }
    return false;
}

function moveToAnchor() {
    var param = location.href.match("(.*)[/\?&/]anchor=(.+)[/&\#/](.*)");
    if(!param) param = location.href.match("(.*)[/¥?&/]anchor=(.+):*(&*)");
    if ((param) && (param.length > 2))  {
        moveToPosition("#"+param[2]);
    }

    return false;
}

$(document).ready(function(){
    var title = document.title;
    
    setInterval(function(){
        if (title != document.title) {
            document.title=title;
        }
    }
    ,200);
});

function checkZipCode(zip_code) {
    //2文字未満の場合
    if(zip_code.length<2){
        $("#dealerLink").prop("disabled", true);
        //2文字以上入力してください。
		
    //2文字以上の場合	
    }else if(zip_code.length>1){
        $("#dealerLink").prop("disabled", false);
    }
}

function SubmitFindDealer() {
    var zipcode = $("#zip").val();

    var dealerLink = $("#dealerLink").attr("href");

    var newDealerLink = dealerLink + "&zip=" + zipcode

    location.href = newDealerLink;
}
