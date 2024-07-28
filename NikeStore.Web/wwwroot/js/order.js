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
        overlayNoRowsTemplate: '<div class="my-no-rows-overlay">No orders found!</div>',
        rowData: [],
        columnDefs: [
            {
                headerName: "Order ID", field: "orderHeaderId", filter: "agNumberColumnFilter", headerCheckboxSelection: true,
                checkboxSelection: true,
                showDisabledCheckboxes: true,
            },
            {headerName: "Customer Name", field: "name"},
            {
                headerName: "Order Status ", field: "status",
                cellRenderer: (params) => {
                    switch (params.node.data.status.toLowerCase()) {
                        case 'pending':
                            return `<span class="badge text-bg-danger">${params.node.data.status.toUpperCase()}</span>`;
                        case 'approved':
                            return `<span class="badge text-bg-success">${params.node.data.status.toUpperCase()}</span>`;
                        case 'readyforpickup':
                            return `<span class="badge text-bg-dark">READY FOR PICKUP</span>`;
                        case 'cancelled':
                            return `<span class="badge text-bg-danger">${params.node.data.status.toUpperCase()}</span>`;
                        case 'completed':
                            return `<span class="badge text-bg-success">${params.node.data.status.toUpperCase()}</span>`;
                        case 'refunded':
                            return `<span class="badge text-bg-danger">${params.node.data.status.toUpperCase()}</span>`;
                    }
                }
            },
            {headerName: "Email", field: "email"},
            {headerName: "Address", field: "address"},
            {headerName: "Coupon Code", field: "couponCode"},
            {headerName: "Discount", field: "discount", filter: "agNumberColumnFilter"},
            {headerName: "Order Total", field: "orderTotal", filter: "agNumberColumnFilter"},

            {
                headerName: "Actions", field: "couponId", filter: false,
                cellRenderer: (params) => {
                    return `<button onclick="orderDetails(${params.node.data.orderHeaderId})" class="btn btn-sm btn-dark"><i class="fa-solid fa-pen-to-square"></i> Update</button>`;
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
        paginationPageSize: 100,
        paginationPageSizeSelector: [100, 200, 300, 400],
    };

    const gridDiv = document.querySelector("#ordersGrid");
    window.gridApi = agGrid.createGrid(gridDiv, gridOptions);
    RefreshGridData();
}

function RefreshGridData() {
    $.ajax({
        url: '/Order/GetOrders',
        method: 'GET',
        success: function (response) {
            console.log(response);

            if (response.isSuccess) {
                gridApi.setGridOption("rowData", response.result);
            } else {
                Swal.fire("Error!", `${response.message}`, "error");
            }
        }
    });
}

function orderDetails(orderHeaderId) {
    window.location = `/Order/OrderDetail?orderId=${orderHeaderId}`;
}

