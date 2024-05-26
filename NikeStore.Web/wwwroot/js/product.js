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
        overlayNoRowsTemplate: '<div class="my-no-rows-overlay">No product found!</div>',
        rowData: [],
        columnDefs: [
            {
                headerName: "ID", field: "productId", filter: "agNumberColumnFilter", headerCheckboxSelection: true,
                checkboxSelection: true,
                showDisabledCheckboxes: true,
            },
            {headerName: "Product Name", field: "name"},
            {headerName: "Price", field: "price", filter: "agNumberColumnFilter"},
            {headerName: "Category", field: "categoryName"},
            {headerName: "Image path", field: "imageLocalPath"},
            {headerName: "Image URL", field: "imageUrl"},
            {
                headerName: "Actions", field: "productId", filter: false,
                cellRenderer: (params) => {
                    return `<button onclick="editProduct(${params.node.data.productId})" class="btn btn-sm btn-success"><i class="fa-solid fa-pen-to-square"></i> Edit</button>
                            <button onclick="deleteProduct(${params.node.data.productId})" class="btn btn-sm btn-danger"><i class="fa-solid fa-trash-can"></i> Delete</button>`;
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

    const gridDiv = document.querySelector("#productsGrid");
    window.gridApi = agGrid.createGrid(gridDiv, gridOptions);
    RefreshGridData();
}

function RefreshGridData() {
    $.ajax({
        url: '/Product/GetAllProducts',
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


function editProduct(productId) {
    window.location = `/Product/ProductEdit?productId=${productId}`;
}

function deleteProduct(productId) {
    Swal.fire({
        icon: "info",
        title: "Are you sure you want to delete the product?",
        showCancelButton: true,
        confirmButtonText: "Yes",
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                headers: {
                    'Accept': 'application/json',
                },
                method: 'DELETE',
                url: `/Product/ProductDelete?productId=${productId}`,
                success: function (response) {
                    RefreshGridData();
                    if (response.isSuccess) {
                        Swal.fire("Success!", "Product Deleted Successfully!", "success");
                    } else {
                        Swal.fire("Error!", `${response.message}`, "error");
                    }
                }
            });
        }
    });
}
