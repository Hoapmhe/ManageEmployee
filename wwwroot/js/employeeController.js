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

function getSelectedEmployeeIds() {
    return Array.from(document.querySelectorAll(".employee-checkbox:checked"))
        .map(checkbox => checkbox.value);
}

function exportSelectedEmployees() {
    const selectedIds = getSelectedEmployeeIds();
    if (selectedIds.length === 0) {
        alert("Please select at least one Employee to export.");
        return;
    }

    fetch('/Employee/ExportSelectedEmployees', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(selectedIds)
    })
        .then(response => {
            if (response.ok) {
                return response.blob();
            }
            throw new Error("Failed to export employees.");
        })
        .then(blob => {
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;
            a.download = "SelectedEmployees.xlsx";
            a.click();
            window.URL.revokeObjectURL(url);
        })
        .catch(error => console.error('Error exporting employees:', error));
}
