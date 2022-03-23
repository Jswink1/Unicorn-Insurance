$(document).ready(function () {

    // Determine what filter to use when loading the table
    var url = window.location.search;
    if (url.includes("good")) {
        loadDataTable("GetDeploymentsList?status=good")
    }
    else {
        if (url.includes("bad")) {
            loadDataTable("GetDeploymentsList?status=bad")
        }
        else {
            loadDataTable("GetDeploymentsList?status=all")
        }
    }

});

function loadDataTable(url) {
    dataTable = $('#tblData').DataTable({

        /*ajax Controller URL path defnition*/
        "ajax": {
            "url": "/deployments/" + url
        },
        /*column definitions*/
        "columns": [
            { "data": "id", "width": "10%" },
            {
                "data": "imageUrl",
                "render": function (data) {
                    return '<img src="' + data +'" style="max-height: 125px">';
                },
                "width": "35%"
            },
            { "data": "resultType", "width": "10%" },
            { "data": "description", "width": "35%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                                <div class="text-center">
                                    <a href="/Deployments/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer;">
                                        <i class="fas fa-edit"></i>
                                    </a>
                                    <a onclick=Delete("/Deployments/Delete/${data}") class="btn btn-danger text-white mt-2" style="cursor:pointer;">
                                        <i class="fas fa-trash"></i>
                                    </a>
                                </div>
                           `
                },
                "width": "5%"
            }
        ]
    })
};

/*SweetAlert to confirm delete*/
function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}