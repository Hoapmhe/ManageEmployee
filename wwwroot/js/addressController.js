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

function deleteDistrict(districtId) {
    var confirmation = confirm("Are you sure you want to delete this district?");
    if (confirmation) {
        $.ajax({
            url: 'District/Delete',
            type: 'POST',
            data: {
                districtId: districtId
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