(function($){
	$.fn.fntNext = function() {
		var iCant = $('.UrlLi').length;
		if (iPos == iCant-1)
			iPos = 0;
		else
			iPos++;
		if ($("#ImgLoad").length > 0){
			$("#ImgLoad").remove();
		}
		var img = new Image();
		img.id = "ImgLoad";
		img.src = $(".UrlLi").eq(iPos).attr("title");
		img.onload = function(){
			if (img.width > 690){
			  var iImgHeight = img.height;
			  iImgHeight = ((iImgHeight * Math.round(69000 / img.width))/100);
			  img.width = "690";
			  img.height = iImgHeight; 
			}else{
			  if (img.height > 650){
			    var iImgWidth = img.width;
			    iImgWidth = ((iImgWidth * Math.round(65000 / img.height))/100);
			    img.height = "650";
			    img.width = iImgWidth; 
			  }
			}
			
			$("#loading").append(img);
			if (iPos == iCant)
				iPos = -1;
		}
	};
	
	$.fn.fntPrev = function() {
		var iCant = $('.UrlLi').length;
		if (iPos == 0)
			iPos = iCant - 1;
		else
			iPos--;
		if ($("#ImgLoad").length > 0){
			$("#ImgLoad").remove();
		}
		var img = new Image();
		img.id = "ImgLoad";
		img.src = $(".UrlLi").eq(iPos).attr("title");
		img.onload = function(){
			if (img.width > 690){
			  var iImgHeight = img.height;
			  iImgHeight = ((iImgHeight * Math.round(69000 / img.width))/100);
			  img.width = "690";
			  img.height = iImgHeight; 
			}else{
			  if (img.height > 650){
			    var iImgWidth = img.width;
			    iImgWidth = ((iImgWidth * Math.round(65000 / img.height))/100);
			    img.height = "650";
			    img.width = iImgWidth; 
			  }
			} 
			$("#loading").append(img);
			if (iPos < 0)
				iPos = iCant;
		}
	};
	
})(jQuery);

var iPos = -1;
$(document).ready(function() {
  $("#tumbs").fntNext({
	});
});
		
$(function () {
  $("#startAjaxNext").click(function () {
	  $("#tumbs").fntNext({
	  });
   });
});
			 
$(function () { 
  $("#startAjaxPrev").click(function () {
	  $("#tumbs").fntPrev({
		});
  });
});

$(function () { 
  $("#loading").click(function () {
	  $("#tumbs").fntPrev({
		});
  });
});