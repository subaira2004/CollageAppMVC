
function onSuccessDepartmentSubmit() {
    clrDept();
    return false;
}

function clrDept() {
    $('[name="Name"]').val('');
    $('[name="DepartmentId"]').val(0);
    return false;
}


function editDepartment(departmentId) {
    $.get("Master/EditDepartments?DepartmentId=" + departmentId, "",
        function (data, textStatus, jqXHR) {
            var editDept = data;
            $('[name="Name"]').val(editDept.Name);
            $('[name="DepartmentId"]').val(editDept.DepartmentId);
        });
    return false;
}

function deleteDepartment(departmentId) {
    if (confirm('Are you sure want to delete this Department?')) {
        $.get("Master/DeleteDepartment?DepartmentId=" + departmentId, "", callBackDepartmentList);
    }
    return false;
}


function callBackDepartmentList(data, textStatus, jqXHR) {
    $('#departmentListOutLet').html(data);
}