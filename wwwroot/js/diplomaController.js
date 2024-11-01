function deleteDiploma(diplomaId) {
    var confirmation = confirm("Are you sure you want to delete this diploma?");
    console.log("Deleting diploma with ID:" + diplomaId);
    if (confirmation) {
        $.ajax({
            url: '/Diploma/Delete',
            type: 'POST',
            data: {
                Id: diplomaId
            },
            success: function (response) {
                window.location.reload();
            },
            error: function (xhr, status, error) {
                alert("There was an error while deleting diploma.");
            }
        });
    }
}