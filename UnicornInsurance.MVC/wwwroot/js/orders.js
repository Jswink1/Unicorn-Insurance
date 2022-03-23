$(document).ready(function () {

    // Determine what filter to use when loading the table
    var url = window.location.search;
    if (url.includes("pending")) {
        loadDataTable("GetOrderList?status=pending")
    }
    else {
        if (url.includes("approved")) {
            loadDataTable("GetOrderList?status=approved")
        }
        else {
            if (url.includes("rejected")) {
                loadDataTable("GetOrderList?status=rejected")
            }
            else {
                loadDataTable("GetOrderList?status=all")
            }            
        }
    }

});

function loadDataTable(url) {
    dataTable = $('#tblData').DataTable({

        /*ajax Controller URL path defnition*/
        "ajax": {
            "url": "/orders/" + url
        },
        /*column definitions*/
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "userEmail", "width": "25%" },
            {
                "data": "orderDate",
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                },
                "width": "20%"
            },
            { "data": "paymentStatus", "width": "20%" },
            { "data": "orderTotal", "render": $.fn.dataTable.render.number(',', '.', 2, '$'), "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                                <div class="text-center">
                                    <a href="/Orders/Details/${data}" class="btn btn-success text-white" style="cursor:pointer;">
                                        <i class="fas fa-edit"></i>
                                    </a>                                   
                                </div>
                           `
                },
                "width": "5%"
            }
        ]
    })
};