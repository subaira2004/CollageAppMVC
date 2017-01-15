
function onSuccessSectionSubmit() {
    clrSect();
    return false;
}

function clrSect() {
    $('[name="Name"]').val('');
    $('[name="DepartmentId"]').val(0);
    $('[name="SectionId"]').val(0);
    return false;
}


function editSection(sectionId) {
    $.get("Master/EditSection?SectionId=" + sectionId, "",
        function (data, textStatus, jqXHR) {
            var editSect = data;
            $('[name="Name"]').val(editSect.Name);
            $('[name="DepartmentId"]').val(editSect.DepartmentId);
            $('[name="SectionId"]').val(editSect.SectionId);
        });
    return false;
}

function deleteSection(sectionId) {
    if (confirm('Are you sure want to delete this Section?')) {
        $.get("Master/DeleteSection?SectionId=" + sectionId, "", callBackSectionList);
    }
    return false;
}

function callBackSectionList(data, textStatus, jqXHR) {
    $('#sectionListOutlet').html(data);
}