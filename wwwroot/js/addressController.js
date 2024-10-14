function deleteProvince(provinceId) {
    var confirmation = confirm("Are you sure you want to delete this province?");
    if (confirmation) {
        $.ajax({
            url: 'Province/Delete',
            type: 'POST',
            data: {
                provinceId: provinceId
            },
            success: function (response) {
                window.location.reload();
            },
            error: function () {
                alert("There was an error while deleting province.");
            }
        });
    }
}