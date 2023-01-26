/*
 * eLite - Landing Page
 * Version: 1.0
 * Copyright 2013
 * Created by: DamterThemes
 * URL: https://wrapbootstrap.com/user/damterthemes
 * Designed and built based on Twitter Bootstrap 3. 
 */
 
//Customers
// On window load. This waits until images have loaded which is essential
$(window).load(function(){
 
// Fade in images so there isn't a color "pop" document load and then on window load
$(".item-customer img").fadeIn(500);
 
// clone image
$('.item-customer img').each(function(){
var el = $(this);
el.css({"position":"absolute"}).wrap("<div class='img_wrapper' style='display: inline-block'>").clone().addClass('img_grayscale').css({"position":"absolute","z-index":"998","opacity":"0"}).insertBefore(el).queue(function(){
var el = $(this);
el.parent().css({"width":this.width,"height":this.height});
el.dequeue();
});
this.src = grayscale(this.src);
});
 
// Fade image
$('.item-customer img').mouseover(function(){
$(this).parent().find('img:first').stop().animate({opacity:1}, 1000);
})
$('.img_grayscale').mouseout(function(){
$(this).stop().animate({opacity:0}, 1000);
});
});
 
// Grayscale w canvas method
function grayscale(src){
var canvas = document.createElement('canvas');
var ctx = canvas.getContext('2d');
var imgObj = new Image();
imgObj.src = src;
canvas.width = imgObj.width;
canvas.height = imgObj.height;
ctx.drawImage(imgObj, 0, 0);
var imgPixels = ctx.getImageData(0, 0, canvas.width, canvas.height);
for(var y = 0; y < imgPixels.height; y++){
for(var x = 0; x < imgPixels.width; x++){
var i = (y * 4) * imgPixels.width + x * 4;
var avg = (imgPixels.data[i] + imgPixels.data[i + 1] + imgPixels.data[i + 2]) / 3;
imgPixels.data[i] = avg;
imgPixels.data[i + 1] = avg;
imgPixels.data[i + 2] = avg;
}
}
ctx.putImageData(imgPixels, 0, 0, 0, 0, imgPixels.width, imgPixels.height);
return canvas.toDataURL();
}
//Faqs
$(document).ready(function () {
    $('.collapse').on('show.bs.collapse', function () {
        var id = $(this).attr('id');
        $('a[href="#' + id + '"]').closest('.panel-heading').addClass('active-faq');
        $('a[href="#' + id + '"] .panel-title span').html('<i class="glyphicon glyphicon-minus"></i>');
    });
    $('.collapse').on('hide.bs.collapse', function () {
        var id = $(this).attr('id');
        $('a[href="#' + id + '"]').closest('.panel-heading').removeClass('active-faq');
        $('a[href="#' + id + '"] .panel-title span').html('<i class="glyphicon glyphicon-plus"></i>');
    });
});


//Slider
$(window).load(function () {
    $('#slider').nivoSlider({
        pauseTime: 8000
    });
});
//Go to top button   
$(document).ready(function (e) {
    $(window).scroll(function () {
        if ($(this).scrollTop() != 0) {
            $('#toTop').fadeIn();
        } else {
            $('#toTop').fadeOut();
        }
    });

    $('#toTop').click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 800);
    });
});