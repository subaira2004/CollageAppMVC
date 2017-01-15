
function onSuccessStudentSubmit() {
    clrStudent();
    return false;
}

function clrStudent() {
    $('[name="StudentId"]').val(0);
    $('[name="StudentName"]').val('');
    $('[name="DepartmentId"]').val(0);
    resetSection();
    //('[name="SectionId"]').val(0);
    $('[name="DateOfJoin"]').val('');
    $('[name="DateofGraduaton"]').val('');
    return false;
}


function resetSection() {
    $('[name="SectionId"]').html('<option value="0">--Select Section--</option>');
    $('[name="SectionId"]').val(0);
    return false;
}
function editStudent(studentId) {
    $.get("Master/EditStudent?StudentId=" + studentId, "",
        function (data, textStatus, jqXHR) {
            var editStudent = data;
            $('[name="StudentId"]').val(editStudent.StudentId);
            $('[name="StudentName"]').val(editStudent.StudentName);
            $('[name="DepartmentId"]').val(editStudent.DepartmentId);
            getSectionsByDepartmentId(editStudent.DepartmentId, editStudent.SectionId);
            //$('[name="SectionId"]').val(editStudent.SectionId);
            $('[name="DateOfJoin"]').val(null);
            if (editStudent.DateOfJoin) {
                var DateOfJoin = new Date(parseInt(editStudent.DateOfJoin.replace("/Date(", "").replace(")/", "")));
                $('[name="DateOfJoin"]').val(convertDateToFormat(DateOfJoin));
            }
            $('[name="DateofGraduaton"]').val(null);
            if (editStudent.DateofGraduaton) {
                var DateofGraduaton = new Date(parseInt(editStudent.DateofGraduaton.replace("/Date(", "").replace(")/", "")));
                $('[name="DateofGraduaton"]').val(convertDateToFormat(DateofGraduaton));
            }
        });
    return false;
}

function deleteStudent(studentId) {
    if (confirm('Are you sure want to delete this Student?')) {
        $.get("Master/DeleteStudent?StudentId=" + studentId, "", callBackStudentList);
    }
    return false;
}

function callBackStudentList(data, textStatus, jqXHR) {
    $('#studentListOutLet').html(data);
}


function getSectionsByDepartmentId(departmentId, selectedSectionId) {
    $.get("Master/GetSectionsByDepartmentId?DepartmentId=" + departmentId, "",
        function (data, textStatus, jqXHR) {
            var sections = data;
            //$('[name="SectionId"]').html('<option value="0">--Select Section--</option>');
            resetSection();
            for (var i = 0; i < sections.length; i++) {
                $('[name="SectionId"]').append('<option value="' + sections[i].SectionId + '">' + sections[i].Name + '</option>');
            }
            if (selectedSectionId) {
                $('[name="SectionId"]').val(selectedSectionId);
            }
            //else {
            //    $('[name="SectionId"]').val(0);
            //}
        });
}

//converting given date to this "YYYY-MM-dd" format
function convertDateToFormat(date, format) {
    //var MonthName = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();
    day = day <= 9 ? "0" + day : "" + day;
    month = month <= 9 ? "0" + month : "" + month;

    return year + "-" + month + "-" + day;

}