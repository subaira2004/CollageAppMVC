function onSuccessNavigation(navId)
{
    $('.loaderFrame').addClass("hide").removeClass("show");
    //unbinding or clearing unobtrusiveAjax bound events
    $("a[data-ajax=true]").unbind("click");
    $("form[data-ajax=true] input[type=image]").unbind("click");
    $("form[data-ajax=true] :submit").unbind("click");
    $("form[data-ajax=true]").unbind("submit");
    
    //initiaizing again
    unobtrusiveAjax(jQuery);

    $("#" + navId).closest('ul').children().removeClass("active");
    $("#" + navId).closest('li').addClass("active");
}

function onBeginNavigation()
{
    $('.loaderFrame').addClass("show").removeClass("hide");
}