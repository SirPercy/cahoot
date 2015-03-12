$(function () {

    function scroll() {
        $('ul#sponsors li:first-child').slideUp(function () {
            $(this).appendTo($('ul#sponsors')).show();
        });
    }
    setInterval(scroll, 4000);

    $.fn.datepicker.defaults.format = "yyyy-mm-dd";
    $('.datepicker').datepicker();


    $('.sendmail').click(function () {
        alert('Funktionen inte införd ännu');
        return false;
    });

    $('a.popup').on('click', function () {
        window.open(this.href);
        return false;
    });

    $('.gbooklink').click(function () {
        return false;
    });

    $('#guestbooklist li').hover(function () {
        $(this).find('span.hidden').removeClass('hidden').addClass('gentrynothidden');
    },
    function () {
        $(this).find('span.gentrynothidden').removeClass('gentrynothidden').addClass('hidden');
    });

    $("#guestbooklist li").mousemove(function (e) {
        $('span.gentrynothidden').css("top", (e.pageY) + "px")
            .css("position", "absolute")
			.css("left", (e.pageX) + "px")
            .css("width", "200px");
    });
    $('.standingslink').append("<span class=\"caret\"></span>");
    $('.standingslink').parent('li').addClass('dropdown');
    $('.teaminfolink').click(function () {
        $('li.open').removeClass('open');
    });
    $('.standingslink').click(function () {
        $(this).parent().children('ul').remove();
        if (!$(this).parent().hasClass('open')) {
            $('li.open').removeClass('open');
            loadstandingsmenu();
            $(this).parent().addClass('open');
        }
        else {
            $(this).parent().removeClass('open');
        }
        return false;
    });

});

function loadstandingsmenu() {
    $.ajaxSetup({
        cache: false
    });

    $.ajax({
        method: "post",
        url: "/Standings/MenuItems",
        success: function (result) {
            $(".standingslink").after(result);
        }
    });

}

