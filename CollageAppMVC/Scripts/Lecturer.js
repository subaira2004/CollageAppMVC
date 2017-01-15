
function onSuccessLecturerSubmit() {
    clrLecturer();
    return false;
}

function clrLecturer() {
    $('[name="Name"]').val('');
    $('[name="DepartmentId"]').val(0);
    $('[name="LecturerId"]').val(0);
    return false;
}


function editLecturer(lecturerId) {
    $.get("Master/EditLecturer?LecturerId=" + lecturerId, "",
        function (data, textStatus, jqXHR) {
            var editLecturer = data;
            $('[name="Name"]').val(editLecturer.Name);
            $('[name="DepartmentId"]').val(editLecturer.DepartmentId);
            $('[name="LecturerId"]').val(editLecturer.LecturerId);
        });
    return false;
}

function deleteLecturer(lecturerId) {
    if (confirm('Are you sure want to delete this Lecturer?')) {
        $.get("Master/DeleteLecturer?LecturerId=" + lecturerId, "", callBackLecturerList);
    }
    return false;
}

function callBackLecturerList(data, textStatus, jqXHR) {
    $('#lecturerListOutLet').html(data);
}