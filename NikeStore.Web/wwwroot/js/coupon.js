var gridApi;

$(document).ready(function () {
    InitGrid();
});

function InitGrid() {
    let gridApi;

    const gridOptions = {
        overlayNoRowsTemplate: '<div class="my-no-rows-overlay">No data to display</div>',
        rowData: [],
        columnDefs: [
            {headerName: "Coupon Id", field: "couponId", filter: "agNumberColumnFilter"},
            {headerName: "Coupon Code", field: "couponCode"},
            {headerName: "Discount Amount", field: "discountAmount", filter: "agNumberColumnFilter"},
            {headerName: "Minimum Amount", field: "minAmount", filter: "agNumberColumnFilter"},
            {
                headerName: "Actions", field: "couponId", filter: false,
                cellRenderer: (params) => {
                    return `<button onclick="deleteCoupon(${params.node.data.couponId})" class="btn btn-danger">Delete</button>`;
                }
            }
        ],
        defaultColDef: {
            filter: "agTextColumnFilter",
            floatingFilter: true,
        },
        rowSelection: "multiple",
        suppressRowClickSelection: true,
        pagination: true,
        paginationPageSize: 10,
        paginationPageSizeSelector: [10, 25, 50],
    };

    const gridDiv = document.querySelector("#couponsGrid");
    window.gridApi = agGrid.createGrid(gridDiv, gridOptions);
    RefreshGridData();
}

function RefreshGridData() {
    $.ajax({
        url: '/Coupon/GetAllCoupons',
        method: 'GET',
        success: function (response) {
            gridApi.setGridOption("rowData", response.result);
        }
    });
}

function deleteCoupon(couponId) {
    Swal.fire({
        icon: "info",
        title: "Are you sure you want to delete the coupon?",
        showCancelButton: true,
        confirmButtonText: "Yes",
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                url: `/Coupon/CouponDelete`,
                method: 'POST',
                data: JSON.stringify({CouponId: couponId}),
                success: function (response) {
                    RefreshGridData();
                    if (response.isSuccess) {
                        Swal.fire("Success!", "Coupon Deleted Successfully!", "success");
                    } else {
                        Swal.fire("Error!", `${response.message}`, "error");
                    }
                }
            });


        }
    });
}


