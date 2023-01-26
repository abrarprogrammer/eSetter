/*
 * eLite - Landing Page
 * Version: 1.0
 * Copyright 2013
 * Created by: DamterThemes
 * URL: https://wrapbootstrap.com/user/damterthemes
 * Designed and built based on Twitter Bootstrap 3. 
 */

/* Styles Switcher */
window.console = window.console || (function () {
    var c = {};
    c.log = c.warn = c.debug = c.info = c.error = c.time = c.dir = c.profile = c.clear = c.exception = c.trace = c.assert = function () {};
    return c;
})();

(function ($) {


    jQuery(document).ready(function ($) {

        // Color Changer
        $(".blue").click(function () {
            $("#colors").attr("href", "./assets/css/colors/blue.css");
            return false;
        });

        $(".green").click(function () {
            $("#colors").attr("href", "./assets/css/colors/green.css");
            return false;
        });

        $(".orange").click(function () {
            $("#colors").attr("href", "./assets/css/colors/orange.css");
            return false;
        });

        $(".navy").click(function () {
            $("#colors").attr("href", "./assets/css/colors/navy.css");
            return false;
        });

        $(".yellow").click(function () {
            $("#colors").attr("href", "./assets/css/colors/yellow.css");
            return false;
        });

        $(".peach").click(function () {
            $("#colors").attr("href", "./assets/css/colors/peach.css");
            return false;
        });

        $(".beige").click(function () {
            $("#colors").attr("href", "./assets/css/colors/beige.css");
            return false;
        });

        $(".purple").click(function () {
            $("#colors").attr("href", "./assets/css/colors/purple.css");
            return false;
        });

        $(".red").click(function () {
            $("#colors").attr("href", "./assets/css/colors/red.css");
            return false;
        });

        $(".pink").click(function () {
            $("#colors").attr("href", "./assets/css/colors/pink.css");
            return false;
        });

        $(".celadon").click(function () {
            $("#colors").attr("href", "./assets/css/colors/celadon.css");
            return false;
        });

        $(".brown").click(function () {
            $("#colors").attr("href", "./assets/css/colors/brown.css");
            return false;
        });

        $(".cherry").click(function () {
            $("#colors").attr("href", "./assets/css/colors/cherry.css");
            return false;
        });

        $(".gray").click(function () {
            $("#colors").attr("href", "./assets/css/colors/gray.css");
            return false;
        });

        $(".dark").click(function () {
            $("#colors").attr("href", "./assets/css/colors/dark.css");
            return false;
        });

        $(".cyan").click(function () {
            $("#colors").attr("href", "./assets/css/colors/cyan.css");
            return false;
        });

        $(".olive").click(function () {
            $("#colors").attr("href", "./assets/css/colors/olive.css");
            return false;
        });

        $(".dirty-green").click(function () {
            $("#colors").attr("href", "./assets/css/colors/dirty-green.css");
            return false;
        });


        // Layout Switcher
        $(".boxed").click(function () {
            $("#layout").attr("href", "assets/css/boxed.css");
            $('body').css('backgroundImage', 'url(./assets/js/switcher/bg/bg1.png)')
            return false;
        });

        $("#layout-switcher").on('change', function () {
            $('#layout').attr('href', $(this).val() + '.css');
        });;


        // Style Switcher	
        $('#style-switcher').animate({
            left: '-195px'
        });

        $('#style-switcher h2 a').click(function (e) {
            e.preventDefault();
            var div = $('#style-switcher');
            console.log(div.css('left'));
            if (div.css('left') === '-195px') {
                $('#style-switcher').animate({
                    left: '0px'
                });
            } else {
                $('#style-switcher').animate({
                    left: '-195px'
                });
            }
        })

        $('.colors li a').click(function (e) {
            e.preventDefault();
            $(this).parent().parent().find('a').removeClass('active');
            $(this).addClass('active');
        })

        $('.bg li a').click(function (e) {
            e.preventDefault();
            $(this).parent().parent().find('a').removeClass('active');
            $(this).addClass('active');
            var bg = $(this).css('backgroundImage');
            $('body').css('backgroundImage', bg)
        })

        $('.bgsolid li a').click(function (e) {
            e.preventDefault();
            $(this).parent().parent().find('a').removeClass('active');
            $(this).addClass('active');
            var bg = $(this).css('backgroundColor');
            $('body').css('backgroundColor', bg).css('backgroundImage', 'none')
        })

        $('#reset a').click(function (e) {
            var bg = $(this).css('backgroundImage');
            $('body').css('backgroundImage', 'url(./assets/js/switcher/bg/bg1.png)')
        })
    });
})(jQuery);