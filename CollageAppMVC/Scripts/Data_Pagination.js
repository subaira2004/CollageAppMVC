//
function goToPageNav(url, goToPage, NoOfDataPerPage, callBackfunction) {
    url = url + "?goToPage=" + goToPage + "&noOfDataPerPage=" + NoOfDataPerPage;
    $.get(url, "", callBackfunction);
    return false;
}

function changeDataPerPage(url, NoOfDataPerPage, callBackfunction) {
    var datePerPageIput = $("#dataPerPage").val();
    if (parseInt(datePerPageIput) > 0) {
        goToPageNav(url, 1, datePerPageIput, callBackfunction);
    }
    else {
        $("#dataPerPage").val(NoOfDataPerPage);
    }
    return false
}


function goToPageNumber(url, NoOfDataPerPage, callBackfunction) {
    var goToPageNumberIput = $("#goToPageNumber").val();
    if (parseInt(goToPageNumberIput) > 0) {
        goToPageNav(url, goToPageNumberIput, NoOfDataPerPage, callBackfunction);
    }
    else {
        $("#goToPageNumber").val(1);
    }
    return false
}