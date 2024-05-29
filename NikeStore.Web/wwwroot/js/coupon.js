var gridApi;

$(document).ready(function () {
    InitGrid();
});

function InitGrid() {
    let gridApi;

    const gridOptions = {
        autoSizeStrategy: {
            type: "fitCellContents",
        },
        onGridReady: (event) => {
            autoSizeAll(event.api, false);
        },
        overlayNoRowsTemplate: '<div class="my-no-rows-overlay">No coupon found!</div>',
        rowData: [],
        columnDefs: [
            {
                headerName: "Coupon ID", field: "couponId", filter: "agNumberColumnFilter", headerCheckboxSelection: true,
                checkboxSelection: true,
                showDisabledCheckboxes: true,
            },
            {headerName: "Coupon Code", field: "couponCode"},
            {headerName: "Discount Amount", field: "discountAmount", filter: "agNumberColumnFilter"},
            {headerName: "Minimum Amount", field: "minAmount", filter: "agNumberColumnFilter"},
            {
                headerName: "Actions", field: "couponId", filter: false,
                cellRenderer: (params) => {
                    return `<button onclick="editCoupon(${params.node.data.couponId})" class="btn btn-sm btn-dark"><i class="fa-solid fa-pen-to-square"></i> Edit</button>
                            <button onclick="deleteCoupon(${params.node.data.couponId})" class="btn btn-sm btn-danger"><i class="fa-solid fa-trash-can"></i> Delete</button>`;
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

            if (response.isSuccess) {
                gridApi.setGridOption("rowData", response.result);
            } else {
                Swal.fire("Error!", `${response.message}`, "error");
            }
        }
    });
}


function editCoupon(couponId) {
    window.location = `/Coupon/CouponUpdate?couponId=${couponId}`;
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
                },
                url: `/Coupon/CouponDelete?couponId=${couponId}`,
                method: 'DELETE',
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


