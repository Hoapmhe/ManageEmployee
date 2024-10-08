function deleteEmployee(employeeId) {
    var confirmation = confirm("Are you sure you want to delete this employee?");
    if (confirmation) {
        $.ajax({
            url: 'Employee/Delete',
            type: 'POST',
            data: {
                Id: employeeId
            },
            success: function (response) {
                window.location.reload();
            },
            error: function () {
                alert("There was an error while deleting employee.");
            }
        });
    }
}